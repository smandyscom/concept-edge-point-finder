namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnNewLayer = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btnLine = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.buttonFittingLine = new System.Windows.Forms.Button();
			this.textBoxSelectionCounter = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSelectionClear = new System.Windows.Forms.Button();
			this.checkBoxMulti = new System.Windows.Forms.CheckBox();
			this.buttonCircle = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
			this.userControlCanvs2 = new WindowsFormsApp2.WPF.UserControlCanvs();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 732);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(1067, 24);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "Status";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(158, 19);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// btnNewLayer
			// 
			this.btnNewLayer.Location = new System.Drawing.Point(933, 344);
			this.btnNewLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnNewLayer.Name = "btnNewLayer";
			this.btnNewLayer.Size = new System.Drawing.Size(75, 22);
			this.btnNewLayer.TabIndex = 1;
			this.btnNewLayer.Text = "NewLayer";
			this.btnNewLayer.UseVisualStyleBackColor = true;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(924, 111);
			this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 25);
			this.numericUpDown1.TabIndex = 2;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(924, 165);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(69, 19);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "Visible";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// btnLine
			// 
			this.btnLine.Location = new System.Drawing.Point(813, 393);
			this.btnLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnLine.Name = "btnLine";
			this.btnLine.Size = new System.Drawing.Size(75, 22);
			this.btnLine.TabIndex = 4;
			this.btnLine.Text = "Line";
			this.btnLine.UseVisualStyleBackColor = true;
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(813, 442);
			this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 22);
			this.btnSelect.TabIndex = 5;
			this.btnSelect.Text = "Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			// 
			// buttonFittingLine
			// 
			this.buttonFittingLine.Location = new System.Drawing.Point(813, 241);
			this.buttonFittingLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonFittingLine.Name = "buttonFittingLine";
			this.buttonFittingLine.Size = new System.Drawing.Size(100, 29);
			this.buttonFittingLine.TabIndex = 6;
			this.buttonFittingLine.Text = "Fitting a line";
			this.buttonFittingLine.UseVisualStyleBackColor = true;
			this.buttonFittingLine.Click += new System.EventHandler(this.fittingFeatureClick);
			// 
			// textBoxSelectionCounter
			// 
			this.textBoxSelectionCounter.Location = new System.Drawing.Point(884, 209);
			this.textBoxSelectionCounter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.textBoxSelectionCounter.Name = "textBoxSelectionCounter";
			this.textBoxSelectionCounter.ReadOnly = true;
			this.textBoxSelectionCounter.Size = new System.Drawing.Size(73, 25);
			this.textBoxSelectionCounter.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(763, 222);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 15);
			this.label1.TabIndex = 8;
			this.label1.Text = "Selection counter";
			// 
			// buttonSelectionClear
			// 
			this.buttonSelectionClear.Location = new System.Drawing.Point(967, 209);
			this.buttonSelectionClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSelectionClear.Name = "buttonSelectionClear";
			this.buttonSelectionClear.Size = new System.Drawing.Size(100, 29);
			this.buttonSelectionClear.TabIndex = 9;
			this.buttonSelectionClear.Text = "Clear";
			this.buttonSelectionClear.UseVisualStyleBackColor = true;
			this.buttonSelectionClear.Click += new System.EventHandler(this.fittingFeatureClick);
			// 
			// checkBoxMulti
			// 
			this.checkBoxMulti.AutoSize = true;
			this.checkBoxMulti.Location = new System.Drawing.Point(933, 250);
			this.checkBoxMulti.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.checkBoxMulti.Name = "checkBoxMulti";
			this.checkBoxMulti.Size = new System.Drawing.Size(61, 19);
			this.checkBoxMulti.TabIndex = 10;
			this.checkBoxMulti.Text = "Multi";
			this.checkBoxMulti.UseVisualStyleBackColor = true;
			this.checkBoxMulti.CheckedChanged += new System.EventHandler(this.multiSelectHandler);
			// 
			// buttonCircle
			// 
			this.buttonCircle.Location = new System.Drawing.Point(813, 278);
			this.buttonCircle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonCircle.Name = "buttonCircle";
			this.buttonCircle.Size = new System.Drawing.Size(127, 29);
			this.buttonCircle.TabIndex = 11;
			this.buttonCircle.Text = "Fitting a circle";
			this.buttonCircle.UseVisualStyleBackColor = true;
			this.buttonCircle.Click += new System.EventHandler(this.fittingFeatureClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(0, 31);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(756, 698);
			this.tabControl1.TabIndex = 12;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(748, 669);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.elementHost1);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(748, 616);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// elementHost1
			// 
			this.elementHost1.AutoSize = true;
			this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.elementHost1.Location = new System.Drawing.Point(3, 3);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new System.Drawing.Size(742, 610);
			this.elementHost1.TabIndex = 0;
			this.elementHost1.Text = "elementHost1";
			this.elementHost1.Child = this.userControlCanvs2;
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1067, 27);
			this.menuStrip1.TabIndex = 13;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(59, 23);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.menuActionHandler);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 756);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.buttonCircle);
			this.Controls.Add(this.checkBoxMulti);
			this.Controls.Add(this.buttonSelectionClear);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxSelectionCounter);
			this.Controls.Add(this.buttonFittingLine);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.btnLine);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.btnNewLayer);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnNewLayer;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button buttonFittingLine;
        private System.Windows.Forms.TextBox textBoxSelectionCounter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSelectionClear;
        private System.Windows.Forms.CheckBox checkBoxMulti;
        private System.Windows.Forms.Button buttonCircle;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private WPF.UserControlCanvs userControlCanvs1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Integration.ElementHost elementHost1;
		private WPF.UserControlCanvs userControlCanvs2;
	}
}

