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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "Status";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnNewLayer
            // 
            this.btnNewLayer.Location = new System.Drawing.Point(700, 298);
            this.btnNewLayer.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewLayer.Name = "btnNewLayer";
            this.btnNewLayer.Size = new System.Drawing.Size(56, 20);
            this.btnNewLayer.TabIndex = 1;
            this.btnNewLayer.Text = "NewLayer";
            this.btnNewLayer.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(693, 96);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(693, 143);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Visible";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(610, 341);
            this.btnLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(56, 20);
            this.btnLine.TabIndex = 4;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(610, 384);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(56, 20);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // buttonFittingLine
            // 
            this.buttonFittingLine.Location = new System.Drawing.Point(610, 209);
            this.buttonFittingLine.Name = "buttonFittingLine";
            this.buttonFittingLine.Size = new System.Drawing.Size(75, 25);
            this.buttonFittingLine.TabIndex = 6;
            this.buttonFittingLine.Text = "Fitting a line";
            this.buttonFittingLine.UseVisualStyleBackColor = true;
            this.buttonFittingLine.Click += new System.EventHandler(this.fittingFeatureClick);
            // 
            // textBoxSelectionCounter
            // 
            this.textBoxSelectionCounter.Location = new System.Drawing.Point(663, 181);
            this.textBoxSelectionCounter.Name = "textBoxSelectionCounter";
            this.textBoxSelectionCounter.ReadOnly = true;
            this.textBoxSelectionCounter.Size = new System.Drawing.Size(56, 20);
            this.textBoxSelectionCounter.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(572, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Selection counter";
            // 
            // buttonSelectionClear
            // 
            this.buttonSelectionClear.Location = new System.Drawing.Point(725, 181);
            this.buttonSelectionClear.Name = "buttonSelectionClear";
            this.buttonSelectionClear.Size = new System.Drawing.Size(75, 25);
            this.buttonSelectionClear.TabIndex = 9;
            this.buttonSelectionClear.Text = "Clear";
            this.buttonSelectionClear.UseVisualStyleBackColor = true;
            this.buttonSelectionClear.Click += new System.EventHandler(this.fittingFeatureClick);
            // 
            // checkBoxMulti
            // 
            this.checkBoxMulti.AutoSize = true;
            this.checkBoxMulti.Location = new System.Drawing.Point(700, 217);
            this.checkBoxMulti.Name = "checkBoxMulti";
            this.checkBoxMulti.Size = new System.Drawing.Size(48, 17);
            this.checkBoxMulti.TabIndex = 10;
            this.checkBoxMulti.Text = "Multi";
            this.checkBoxMulti.UseVisualStyleBackColor = true;
            this.checkBoxMulti.CheckedChanged += new System.EventHandler(this.multiSelectHandler);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Location = new System.Drawing.Point(610, 241);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(95, 25);
            this.buttonCircle.TabIndex = 11;
            this.buttonCircle.Text = "Fitting a circle";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.fittingFeatureClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.menuActionHandler);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "*.png|";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 488);
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
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

