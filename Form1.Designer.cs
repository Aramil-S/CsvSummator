namespace CsvSummator
{
    partial class Main
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.GuiPanel = new System.Windows.Forms.Panel();
            this.ReadDirectoryButton = new System.Windows.Forms.Button();
            this.FolderBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.NoticeBox = new System.Windows.Forms.TextBox();
            this.FolderLabel = new System.Windows.Forms.Label();
            this.NoticeLabel = new System.Windows.Forms.Label();
            this.ThreadBox = new System.Windows.Forms.TextBox();
            this.ThreadLabel = new System.Windows.Forms.Label();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.YearlyWriter = new System.Windows.Forms.Timer(this.components);
            this.CategoryWriter = new System.Windows.Forms.Timer(this.components);
            this.SumWriter = new System.Windows.Forms.Timer(this.components);
            this.NotifyTimer = new System.Windows.Forms.Timer(this.components);
            this.SpawnTimer = new System.Windows.Forms.Timer(this.components);
            this.ManualButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).BeginInit();
            this.MainContainer.Panel1.SuspendLayout();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            this.GuiPanel.SuspendLayout();
            this.FooterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainContainer.IsSplitterFixed = true;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            // 
            // MainContainer.Panel1
            // 
            this.MainContainer.Panel1.Controls.Add(this.GuiPanel);
            this.MainContainer.Panel1.Controls.Add(this.FooterPanel);
            this.MainContainer.Panel1MinSize = 220;
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.LogBox);
            this.MainContainer.Size = new System.Drawing.Size(800, 450);
            this.MainContainer.SplitterDistance = 220;
            this.MainContainer.TabIndex = 0;
            // 
            // GuiPanel
            // 
            this.GuiPanel.Controls.Add(this.ManualButton);
            this.GuiPanel.Controls.Add(this.ReadDirectoryButton);
            this.GuiPanel.Controls.Add(this.FolderBox);
            this.GuiPanel.Controls.Add(this.StartButton);
            this.GuiPanel.Controls.Add(this.ClearLogButton);
            this.GuiPanel.Controls.Add(this.SaveButton);
            this.GuiPanel.Controls.Add(this.NoticeBox);
            this.GuiPanel.Controls.Add(this.FolderLabel);
            this.GuiPanel.Controls.Add(this.NoticeLabel);
            this.GuiPanel.Controls.Add(this.ThreadBox);
            this.GuiPanel.Controls.Add(this.ThreadLabel);
            this.GuiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GuiPanel.Location = new System.Drawing.Point(0, 0);
            this.GuiPanel.Name = "GuiPanel";
            this.GuiPanel.Size = new System.Drawing.Size(220, 400);
            this.GuiPanel.TabIndex = 11;
            // 
            // ReadDirectoryButton
            // 
            this.ReadDirectoryButton.Location = new System.Drawing.Point(3, 81);
            this.ReadDirectoryButton.Name = "ReadDirectoryButton";
            this.ReadDirectoryButton.Size = new System.Drawing.Size(202, 23);
            this.ReadDirectoryButton.TabIndex = 11;
            this.ReadDirectoryButton.Text = "Ręcznie wczytaj nową listę plików";
            this.ReadDirectoryButton.UseVisualStyleBackColor = true;
            this.ReadDirectoryButton.Click += new System.EventHandler(this.ReadDirectoryButton_Click);
            // 
            // FolderBox
            // 
            this.FolderBox.Location = new System.Drawing.Point(3, 3);
            this.FolderBox.Name = "FolderBox";
            this.FolderBox.Size = new System.Drawing.Size(100, 20);
            this.FolderBox.TabIndex = 0;
            this.FolderBox.Text = "\\";
            // 
            // StartButton
            // 
            this.StartButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.StartButton.Location = new System.Drawing.Point(0, 377);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(220, 23);
            this.StartButton.TabIndex = 7;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.Location = new System.Drawing.Point(117, 348);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(100, 23);
            this.ClearLogButton.TabIndex = 9;
            this.ClearLogButton.Text = "Wyczyść log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(3, 110);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(202, 23);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "Wprowadź zmiany";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Visible = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // NoticeBox
            // 
            this.NoticeBox.Location = new System.Drawing.Point(3, 29);
            this.NoticeBox.Name = "NoticeBox";
            this.NoticeBox.Size = new System.Drawing.Size(100, 20);
            this.NoticeBox.TabIndex = 1;
            this.NoticeBox.Text = "60";
            // 
            // FolderLabel
            // 
            this.FolderLabel.AutoSize = true;
            this.FolderLabel.Location = new System.Drawing.Point(109, 6);
            this.FolderLabel.Name = "FolderLabel";
            this.FolderLabel.Size = new System.Drawing.Size(78, 13);
            this.FolderLabel.TabIndex = 4;
            this.FolderLabel.Text = "Folder startowy";
            // 
            // NoticeLabel
            // 
            this.NoticeLabel.AutoSize = true;
            this.NoticeLabel.Location = new System.Drawing.Point(109, 32);
            this.NoticeLabel.Name = "NoticeLabel";
            this.NoticeLabel.Size = new System.Drawing.Size(95, 13);
            this.NoticeLabel.TabIndex = 5;
            this.NoticeLabel.Text = "Powiadamiaj co (s)";
            // 
            // ThreadBox
            // 
            this.ThreadBox.Location = new System.Drawing.Point(3, 55);
            this.ThreadBox.Name = "ThreadBox";
            this.ThreadBox.Size = new System.Drawing.Size(100, 20);
            this.ThreadBox.TabIndex = 2;
            this.ThreadBox.Text = "2";
            // 
            // ThreadLabel
            // 
            this.ThreadLabel.AutoSize = true;
            this.ThreadLabel.Location = new System.Drawing.Point(109, 58);
            this.ThreadLabel.Name = "ThreadLabel";
            this.ThreadLabel.Size = new System.Drawing.Size(69, 13);
            this.ThreadLabel.TabIndex = 6;
            this.ThreadLabel.Text = "Ilość wątków";
            // 
            // FooterPanel
            // 
            this.FooterPanel.Controls.Add(this.StatusLabel);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FooterPanel.Location = new System.Drawing.Point(0, 400);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(220, 50);
            this.FooterPanel.TabIndex = 8;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLabel.Location = new System.Drawing.Point(0, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(220, 50);
            this.StatusLabel.TabIndex = 12;
            this.StatusLabel.Text = "STATUS";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogBox
            // 
            this.LogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogBox.Location = new System.Drawing.Point(0, 0);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(576, 450);
            this.LogBox.TabIndex = 0;
            // 
            // YearlyWriter
            // 
            this.YearlyWriter.Interval = 1000;
            this.YearlyWriter.Tick += new System.EventHandler(this.YearlyWriter_Tick);
            // 
            // CategoryWriter
            // 
            this.CategoryWriter.Interval = 1000;
            this.CategoryWriter.Tick += new System.EventHandler(this.CategoryWriter_Tick);
            // 
            // SumWriter
            // 
            this.SumWriter.Interval = 1000;
            this.SumWriter.Tick += new System.EventHandler(this.SumWriter_Tick);
            // 
            // NotifyTimer
            // 
            this.NotifyTimer.Tick += new System.EventHandler(this.NotifyTimer_Tick);
            // 
            // SpawnTimer
            // 
            this.SpawnTimer.Interval = 50;
            this.SpawnTimer.Tick += new System.EventHandler(this.SpawnTimer_Tick);
            // 
            // ManualButton
            // 
            this.ManualButton.Location = new System.Drawing.Point(4, 347);
            this.ManualButton.Name = "ManualButton";
            this.ManualButton.Size = new System.Drawing.Size(99, 23);
            this.ManualButton.TabIndex = 12;
            this.ManualButton.Text = "Instrukcja (ang.)";
            this.ManualButton.UseVisualStyleBackColor = true;
            this.ManualButton.Click += new System.EventHandler(this.ManualButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainContainer);
            this.Name = "Main";
            this.Text = "Csv Summator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainContainer.Panel1.ResumeLayout(false);
            this.MainContainer.Panel2.ResumeLayout(false);
            this.MainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainContainer)).EndInit();
            this.MainContainer.ResumeLayout(false);
            this.GuiPanel.ResumeLayout(false);
            this.GuiPanel.PerformLayout();
            this.FooterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainContainer;
        private System.Windows.Forms.Label ThreadLabel;
        private System.Windows.Forms.Label NoticeLabel;
        private System.Windows.Forms.Label FolderLabel;
        private System.Windows.Forms.TextBox ThreadBox;
        private System.Windows.Forms.TextBox NoticeBox;
        private System.Windows.Forms.TextBox FolderBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.Button ClearLogButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Timer YearlyWriter;
        private System.Windows.Forms.Timer CategoryWriter;
        private System.Windows.Forms.Timer SumWriter;
        private System.Windows.Forms.Panel GuiPanel;
        private System.Windows.Forms.Timer NotifyTimer;
        private System.Windows.Forms.Timer SpawnTimer;
        private System.Windows.Forms.Button ReadDirectoryButton;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button ManualButton;
    }
}

