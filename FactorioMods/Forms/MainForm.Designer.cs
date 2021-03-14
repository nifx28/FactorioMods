namespace FactorioMods.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        ///  此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        ///  這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.lstMods = new System.Windows.Forms.ListBox();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.radEnabled = new System.Windows.Forms.RadioButton();
            this.radDisabled = new System.Windows.Forms.RadioButton();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnPack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.worker_ProgressChanged);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // lstMods
            // 
            this.lstMods.DisplayMember = "Key";
            this.lstMods.Font = new System.Drawing.Font("細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstMods.FormattingEnabled = true;
            this.lstMods.ItemHeight = 12;
            this.lstMods.Location = new System.Drawing.Point(12, 12);
            this.lstMods.Name = "lstMods";
            this.lstMods.Size = new System.Drawing.Size(461, 244);
            this.lstMods.TabIndex = 0;
            this.lstMods.ValueMember = "Value";
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(12, 266);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(136, 23);
            this.progBar.TabIndex = 1;
            // 
            // radEnabled
            // 
            this.radEnabled.AutoSize = true;
            this.radEnabled.Checked = true;
            this.radEnabled.Location = new System.Drawing.Point(160, 267);
            this.radEnabled.Name = "radEnabled";
            this.radEnabled.Size = new System.Drawing.Size(61, 19);
            this.radEnabled.TabIndex = 4;
            this.radEnabled.TabStop = true;
            this.radEnabled.Text = "已啟用";
            this.radEnabled.UseVisualStyleBackColor = true;
            this.radEnabled.CheckedChanged += new System.EventHandler(this.btnReload_CheckedChanged);
            // 
            // radDisabled
            // 
            this.radDisabled.AutoSize = true;
            this.radDisabled.Location = new System.Drawing.Point(227, 267);
            this.radDisabled.Name = "radDisabled";
            this.radDisabled.Size = new System.Drawing.Size(61, 19);
            this.radDisabled.TabIndex = 5;
            this.radDisabled.TabStop = true;
            this.radDisabled.Text = "已停用";
            this.radDisabled.UseVisualStyleBackColor = true;
            this.radDisabled.CheckedChanged += new System.EventHandler(this.btnReload_CheckedChanged);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(294, 265);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(81, 25);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "重新載入(&R)";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_CheckedChanged);
            // 
            // btnPack
            // 
            this.btnPack.Location = new System.Drawing.Point(381, 265);
            this.btnPack.Name = "btnPack";
            this.btnPack.Size = new System.Drawing.Size(92, 25);
            this.btnPack.TabIndex = 3;
            this.btnPack.Text = "匯出壓縮包(&P)";
            this.btnPack.UseVisualStyleBackColor = true;
            this.btnPack.Click += new System.EventHandler(this.btnPack_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 297);
            this.Controls.Add(this.radDisabled);
            this.Controls.Add(this.radEnabled);
            this.Controls.Add(this.btnPack);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.lstMods);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factorio Mods";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.ListBox lstMods;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.RadioButton radEnabled;
        private System.Windows.Forms.RadioButton radDisabled;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnPack;
    }
}
