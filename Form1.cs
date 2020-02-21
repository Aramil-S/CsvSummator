using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CsvSummator
{
    public partial class Main : Form
    {
        #pragma warning disable
        string appPath;
        #pragma warning enable
        #region initialization
        public Main()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
         //   backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
         //   backgroundWorker1.WorkerReportsProgress = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
#pragma warning disable
            string[] argument = new string[2];
#pragma warning enable
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            bool droppedEnvPath = false;
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (!droppedEnvPath) { droppedEnvPath = true; appPath = arg; continue; }
                if (!arg.StartsWith("--")) {LogWrongArg (arg); continue;}
                try { argument = arg.Split('='); } catch { LogWrongArg(arg); continue; }
                arguments.Add(argument[0], argument[1]);
            }
            bool autostart = false;
            foreach (KeyValuePair<string,string> arg in arguments)
            {
                switch (arg.Key)
                {
                    case "--Path":
                        {
                            FolderBox.Text = arg.Value;
                            break;
                        }
                    case "--NoticeTime":
                        {
                            NoticeBox.Text = arg.Value;
                            break;
                        }
                    case "--ThreadCount":
                        {
                            ThreadBox.Text = arg.Value;
                            break;
                        }
                    case "--Autostart":
                        {
                            if (arg.Value.ToLower() == "true") autostart = true;
                            break;
                        }
                    default:
                        {
                            LogWrongArg(arg.Key+"="+arg.Value);
                            break;
                        }
                }
            }
            UpdateStatus("PROGRAM GOTOWY DO PRACY", Color.Empty);
            if (autostart) WorkStart();
            }

        private void WorkStart()
        {
            this.Text = "Csv Summator - Working";
            timersManuallyActivated = false;
            isDirectoryFinished = false;
            SpawnTimer.Start(); 
            UpdateStatus("ROZPOCZYNAM PRACĘ PROGRAMU", Color.Yellow); 
        }
        #endregion

        #region WorkerSpawn
        Dictionary<int,Thread> threadList = new Dictionary<int,Thread>();
        Queue<FileInfo> fileList;
        bool isDirectoryFinished = false;
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private void SpawnWorkers()
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            ActivateTimers();
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (isDirectoryFinished) { SpawnTimer.Stop(); return; }
            if (fileList == null) fileList = EnqeueFileList();
            if (!Int32.TryParse(ThreadBox.Text, out int desiredThreadNumber)) { MessageBox.Show("Podano błędną wartość ilości wątków"); return; }
            if (fileList.Count <= desiredThreadNumber)
            {
                AddToLog("Zlecono przetwarzanie ostatniej partii danych, proszę czekać na ostateczne wyniki");
                desiredThreadNumber = fileList.Count;
                isDirectoryFinished = true;
            }

            for (int i = 0; i < desiredThreadNumber; i++)
            {
                if (threadList.ContainsKey(i) && threadList[i].ThreadState != ThreadState.Stopped) continue;


                Thread t = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true; //niepotrzebny jeśli aplikacja się zamyka
                    try
                    {
                        Reader worker = new Reader(this);
                        worker.ProcessFile(fileList.Dequeue().FullName);
                    }
                    catch (Exception exception)
                    {
                        AddToLog("Wystąpił nieznany błąd przy tworzeniu nowego wątku, jeśli problem się powtarza: skontaktuj się z dostawcą programu\r\n" + exception.Message);
                    }
                });
                threadList[i] = t;
                threadList[i].Start();
            }
        }



        private Queue<FileInfo> EnqeueFileList(bool append = false)
        {
            Queue<FileInfo> result = new Queue<FileInfo>();
            try
            {
                DirectoryInfo path = new DirectoryInfo(FolderBox.Text);
                if (append & fileList != null)
                {
                    int count = 0;
                    foreach (FileInfo file in path.GetFiles("*.csv"))
                    {
                        fileList.Enqueue(file);
                        count++;
                    }

                    AddToLog("Dopisano do kolejki  " + count.ToString() + " poprawnych pozycji. Długość listy do przetworzenia: " + fileList.Count.ToString());
                }
                else
                {
                    foreach (FileInfo file in path.GetFiles("*.csv"))
                    {
                        result.Enqueue(file);
                    }
                    AddToLog("Wczytano listę plików zawierającą " + result.Count.ToString() + " poprawnych pozycji");
                }
            }
            catch (Exception e){ AddToLog("UWAGA!\r\nBłąd pobrania ścieżki do plików. Prawdopodobnie zbyt dużo plików.\r\nDane debugowania: " + e.Message); }
            return result;
        }

        #endregion

        #region calculations

        private void SaveYearlySums()
        {
            KeyValuePair<DateTime, int> pair;
            int year;
            while (Memory.YearlyQueue.Count > 0)
            {
                try
                {
                    Memory.YearlyQueue.TryDequeue(out pair);
                    year = pair.Key.Year;
                    checked
                    {
                        if (Memory.ValuesPerYear.ContainsKey(year))
                            Memory.ValuesPerYear[year] += pair.Value;
                        else Memory.ValuesPerYear.Add(year, pair.Value);
                    }
                }
                catch (OverflowException) { AddToLog("Dane źródłowe (minimum jedno z lat) przekroczyły pojemność magazynów, zmniejsz ilość danych"); isBufferOverflowed = true; }
                catch (Exception e) {
                    AddToLog("Problem z przetwarzaniem danych wg. daty: " + e.Message);
                }
                }
        }

        private void SaveCategorizedSums()
        {
            KeyValuePair<string, int> pair;
            while (Memory.CategoryQueue.Count > 0)
            {
                Memory.CategoryQueue.TryDequeue(out pair);
                try
                {
                    checked
                    {
                        if (Memory.ValuesPerCategory.ContainsKey(pair.Key))
                            Memory.ValuesPerCategory[pair.Key] += pair.Value;
                        else Memory.ValuesPerCategory.Add(pair.Key, pair.Value);
                    }
                }
                catch (OverflowException) { AddToLog("Dane źródłowe (minimum jedna z kategorii) przekroczyły pojemność magazynów, zmniejsz ilość danych"); isBufferOverflowed = true; }
                catch (Exception e){
                    AddToLog("Problem z przetwarzaniem danych wg. kategorii: " + e.Message); 
                }
            }
        }

        private void SaveSum()
        {
            long value;
            try
            {
                while (Memory.SumQueue.Count > 0)
                {
                    Memory.SumQueue.TryDequeue(out value);
                    checked { Memory.ValuesSummary += value; }
                }
            }
            catch (OverflowException) { AddToLog("Dane źródłowe (suma) przekroczyły pojemność magazynów, zmniejsz ilość danych"); isBufferOverflowed = true; }

        }

    private string GetOverallSum()
        {
            return Memory.ValuesSummary.ToString();
        }
        private string GetYearSums()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int,int> pair in new SortedDictionary<int, int>(Memory.ValuesPerYear))
            {
                sb.Append(String.Format("{0}: {1}{2}", pair.Key.ToString(), pair.Value.ToString(), Environment.NewLine));
            }
            return sb.ToString();
        }
        private string GetCatSums()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> pair in new SortedDictionary<string, int>(Memory.ValuesPerCategory))
            {
                sb.Append(String.Format("{0}: {1}{2}",pair.Key, pair.Value.ToString(), Environment.NewLine));
            }
            return sb.ToString();
        }
        bool isBufferOverflowed;
        private string WriteDataInfo(bool finished = false)
        {
            StringBuilder sb = new StringBuilder();
            if (finished) sb.Append("\r\n=====ZAKOŃCZONO POBIERANIE DANYCH=====\r\n=====TRWA ZAPISYWANIE DO PLIKU========");
            sb.Append("\r\nŁączna suma z plików: ");
            sb.Append(GetOverallSum());
            sb.Append("\r\nRoczne sumy z plików:\r\n");
            sb.Append(GetYearSums());
            sb.Append("\r\nSumy według kategorii:\r\n");
            sb.Append(GetCatSums());
            if (isBufferOverflowed) sb.Append("\r\n====NASTĄPIŁO PRZEKROCZENIE POJEMNOŚCI BUFORÓW, ZMNIEJSZ ILOŚĆ DANYCH====");
            return sb.ToString();
        }
        System.Timers.Timer exitTimer;
        private void CheckIfCompleted()
        { 
            if (exitTimer == null) exitTimer = new System.Timers.Timer();
            exitTimer.Elapsed += new System.Timers.ElapsedEventHandler(ExitTimeout);
            exitTimer.Interval = 60000;
            exitTimer.Enabled = true;
            bool isWorking = false;
            if (fileList.Count != 0) isWorking = true;
            try
            {
                foreach (KeyValuePair<int,Thread> worker in threadList)
                {
#if DEBUG
                    AddToLog(String.Format("Sprawdzam status ostatniego wątku nr{0} : {1}",worker.Key,worker.Value.ThreadState.ToString()));
#endif
                    if (worker.Value.IsAlive) { isWorking = true; break; }
                }
            }catch{ return; }

            if (isWorking) { AddToLog("Czekam na ukończenie pracy wątków"); return; }

            exitTimer.Enabled = false;
            UpdateStatus("Zakończono odczyt danych, dane gotowe do zapisania w pliku", Color.GreenYellow);
            StopTimers();
            SaveSum();
            SaveCategorizedSums();
            SaveYearlySums();
            AddToLog(WriteDataInfo(true));
            SaveToCsvFile();
            this.Text = "Csv Summator - Job finished";
            Memory.ClearData();
            fileList = null;
        }
        private void ExitTimeout(object sender, System.Timers.ElapsedEventArgs args)
        {
            if (MessageBox.Show(
                "Nieznany błąd uniemożliwia dokończenie obliczeń. Jeśli klikniesz \"Anuluj\" aplikacja zamknie się, w przeciwnym wypadku ponowi próbę. " +
                "Jeśli problem będzie się powtarzał, skontaktuj się z dostawcą programu", 
                "Błąd przetwarzania danych - Timeout", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
              Environment.Exit(2);
        }
        bool isFileWaiting = false;
        private void SaveToCsvFile()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Typ sumy;Identyfikator;Wartość");
            sb.AppendLine("Ogólna;nie dotyczy;" + GetOverallSum());
            foreach (KeyValuePair<int, int> pair in new SortedDictionary<int, int>(Memory.ValuesPerYear))
            {
                sb.AppendLine(String.Format("Roczna;{0};{1}", pair.Key.ToString(), pair.Value.ToString()));
            }
            foreach (KeyValuePair<string, int> pair in new SortedDictionary<string, int>(Memory.ValuesPerCategory))
            {
                sb.AppendLine(String.Format("Kategoria;{0};{1}", pair.Key, pair.Value.ToString()));
            }
            SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveDialog.CheckFileExists = false;
            saveDialog.CheckPathExists = true;
            saveDialog.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            saveDialog.FileName = "CsvSummator.csv";
            try
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);
                    AddToLog("Dane zapisane do pliku: " + saveDialog.FileName);
                    Memory.ClearData();
                    AddToLog("Bufor danych usunięty");
                    UpdateStatus("Plik zapisany. Program gotowy do nowej pracy", Color.Green);
                    isFileWaiting = false;
                }
                else { isFileWaiting = true; }
            }
            catch (IOException e) { System.Media.SystemSounds.Hand.Play();  UpdateStatus("Nastąpił błąd zapisu pliku, spróbuj ponownie", Color.Orange);AddToLog("Błąd zapisu pliku: " + e.Message); }
            catch { MessageBox.Show("Nastąpił nieznany problem zapisu do pliku. Jeśli problem będzie się powtarzał, skontaktuj się z dostawcą programu"); }
        }
#endregion



#region Logging
        public void AddToLog(string text)
        {
            string line = String.Format(@"{0} {1}{2}", System.DateTime.Now.ToString(), text, Environment.NewLine);
            LogBox.BeginInvoke(
                ((Action)(() => LogBox.AppendText(line))));
        }
        private void LogWrongArg(string text)
        {
            AddToLog("Otrzymano niewłaściwy argument: " + text);
        }

        private void UpdateStatus(string text, Color color)
        {
            if (text != null) StatusLabel.BeginInvoke(
    ((Action)(() => StatusLabel.Text = text)));
            if (color != Color.Empty) StatusLabel.BeginInvoke(
    ((Action)(() => StatusLabel.BackColor = color)));
        }
        #endregion

        #region UiActions
        private void ActivateControls(bool state)
        {
            this.Enabled = state;
            /*  foreach (Control item in this.Controls)
              {
                  this.Enabled = state;
              }*/
        }
        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            LogBox.Text = "";
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (isFileWaiting) { SaveToCsvFile(); return; }
            ActivateControls(false);
            WorkStart();
            ActivateControls(true);
        }
        private void ReadDirectoryButton_Click(object sender, EventArgs e)
        {
            EnqeueFileList(true);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funkcjonalnośc niewdrożona");
        }
#endregion

#region Timers
        bool timersManuallyActivated = false;
        public void ActivateTimers()
        {
            if (timersManuallyActivated) return;
            YearlyWriter.Start();
            CategoryWriter.Start();
            SumWriter.Start();
            try { NotifyTimer.Interval = Int32.Parse(NoticeBox.Text) * 1000; } catch { MessageBox.Show("Podano błędną wartość czasu pomiędzy podaniem statusu"); }
            NotifyTimer.Start();
        }
        private void YearlyWriter_Tick(object sender, EventArgs e)
        {
            YearlyWriter.Stop();
            SaveYearlySums();
            YearlyWriter.Start();
        }

        private void CategoryWriter_Tick(object sender, EventArgs e)
        {
            CategoryWriter.Stop();
            SaveCategorizedSums();
            CategoryWriter.Start();
        }

        private void SumWriter_Tick(object sender, EventArgs e)
        {
            SumWriter.Stop();
            SaveSum();
            SumWriter.Start();
        }

        private void StopTimers()
        {
            SumWriter.Stop();
            CategoryWriter.Stop();
            YearlyWriter.Stop();
            NotifyTimer.Stop();
            SpawnTimer.Stop();
        }

        private void NotifyTimer_Tick(object sender, EventArgs e)
        {
            NotifyTimer.Stop();
            AddToLog(WriteDataInfo());
            NotifyTimer.Start();
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            if (isDirectoryFinished) CheckIfCompleted();
            else SpawnWorkers();
            #endregion
        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start("_README.txt"); } catch { AddToLog("Nie udało się otworzyć instrukcji (brak pliku?)"); }
        }
    }
}
