using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvSummator
{
    class Reader
    {
        readonly Main mainHandle;
        public Reader(Main mainHandle)
        {
            this.mainHandle = mainHandle;
        }

        public void ProcessFile(string path)
        {
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                string[] parts;
                string category;
                int count = 0;
                int errors=0;
                if (sr.Peek() == 84)
                {
                    sr.ReadLine();
                    count++;
                    errors++;
#if DEBUG
                    mainHandle.AddToLog("Znaleziono i usunięto linię z nagłówkami z pliku: " + path);
#endif
                } //linia zaczyna się od "T"
                else
                {
#if DEBUG
                    mainHandle.AddToLog("Przetwarzam plik: " + path);
#endif
                }
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        parts = line.Split(';');
                        if (!DateTime.TryParse(parts[0], CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out DateTime date))
                        {
                            mainHandle.AddToLog(String.Format("Problem z przetwarzaniem daty {2} w linii: {0} w pliku: {1}", line, path, parts[0]));
                            errors++; continue;
                    }

                        if (!Int32.TryParse(parts[3], out int value))
                        {
#if DEBUG
                        mainHandle.AddToLog(String.Format("Problem z przetwarzaniem wartości {2} w linii: {0} w pliku: {1}", line, path, parts[3]));
#endif
                            errors++; continue;
                        }


                    category = parts[1];
                        Memory.CategoryQueue.Enqueue(new KeyValuePair<string, int>(category, value));
                        Memory.YearlyQueue.Enqueue(new KeyValuePair<DateTime, int>(date, value));
                        Memory.SumQueue.Enqueue(value);
                        count++;
                    }
                    catch (Exception error) { mainHandle.AddToLog("Niezidentyfikowany problem przy przetwarzaniu danych. Wynik może być nieprawidłowy\r\n" + error.Message); }

                }
#if DEBUG
                mainHandle.AddToLog(String.Format("Przetworzono plik zawierający {0} linii, porzucono {1}", count.ToString(), errors.ToString()));
#endif
            }
        }
    }
}
