namespace GaussFilterASM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.beforeBmpBox = new System.Windows.Forms.PictureBox();
            this.afterBmpBox = new System.Windows.Forms.PictureBox();
            this.beforeLabel = new System.Windows.Forms.Label();
            this.afterLabel = new System.Windows.Forms.Label();
            this.uploadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cppButton = new System.Windows.Forms.RadioButton();
            this.asmButton = new System.Windows.Forms.RadioButton();
            this.selectDllLabel = new System.Windows.Forms.Label();
            this.threadsTrackbar = new System.Windows.Forms.TrackBar();
            this.threadsLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.testCheckbox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.timeValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.beforeBmpBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterBmpBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // beforeBmpBox
            // 
            this.beforeBmpBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.beforeBmpBox.Location = new System.Drawing.Point(12, 46);
            this.beforeBmpBox.Name = "beforeBmpBox";
            this.beforeBmpBox.Size = new System.Drawing.Size(362, 239);
            this.beforeBmpBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.beforeBmpBox.TabIndex = 0;
            this.beforeBmpBox.TabStop = false;
            // 
            // afterBmpBox
            // 
            this.afterBmpBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.afterBmpBox.Location = new System.Drawing.Point(426, 46);
            this.afterBmpBox.Name = "afterBmpBox";
            this.afterBmpBox.Size = new System.Drawing.Size(362, 239);
            this.afterBmpBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.afterBmpBox.TabIndex = 1;
            this.afterBmpBox.TabStop = false;
            // 
            // beforeLabel
            // 
            this.beforeLabel.AutoSize = true;
            this.beforeLabel.Location = new System.Drawing.Point(12, 9);
            this.beforeLabel.Name = "beforeLabel";
            this.beforeLabel.Size = new System.Drawing.Size(56, 20);
            this.beforeLabel.TabIndex = 2;
            this.beforeLabel.Text = "Before:";
            this.beforeLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // afterLabel
            // 
            this.afterLabel.AutoSize = true;
            this.afterLabel.Location = new System.Drawing.Point(426, 9);
            this.afterLabel.Name = "afterLabel";
            this.afterLabel.Size = new System.Drawing.Size(45, 20);
            this.afterLabel.TabIndex = 3;
            this.afterLabel.Text = "After:";
            this.afterLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(12, 291);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(131, 48);
            this.uploadButton.TabIndex = 4;
            this.uploadButton.Text = "Upload image";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(657, 291);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(131, 48);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cppButton
            // 
            this.cppButton.AutoSize = true;
            this.cppButton.Checked = true;
            this.cppButton.Location = new System.Drawing.Point(12, 425);
            this.cppButton.Name = "cppButton";
            this.cppButton.Size = new System.Drawing.Size(59, 24);
            this.cppButton.TabIndex = 6;
            this.cppButton.TabStop = true;
            this.cppButton.Text = "C++";
            this.cppButton.UseVisualStyleBackColor = true;
            // 
            // asmButton
            // 
            this.asmButton.AutoSize = true;
            this.asmButton.Location = new System.Drawing.Point(12, 455);
            this.asmButton.Name = "asmButton";
            this.asmButton.Size = new System.Drawing.Size(61, 24);
            this.asmButton.TabIndex = 7;
            this.asmButton.Text = "ASM";
            this.asmButton.UseVisualStyleBackColor = true;
            // 
            // selectDllLabel
            // 
            this.selectDllLabel.AutoSize = true;
            this.selectDllLabel.Location = new System.Drawing.Point(12, 402);
            this.selectDllLabel.Name = "selectDllLabel";
            this.selectDllLabel.Size = new System.Drawing.Size(81, 20);
            this.selectDllLabel.TabIndex = 8;
            this.selectDllLabel.Text = "Select DLL:";
            // 
            // threadsTrackbar
            // 
            this.threadsTrackbar.Location = new System.Drawing.Point(135, 425);
            this.threadsTrackbar.Maximum = 64;
            this.threadsTrackbar.Minimum = 1;
            this.threadsTrackbar.Name = "threadsTrackbar";
            this.threadsTrackbar.Size = new System.Drawing.Size(499, 56);
            this.threadsTrackbar.TabIndex = 9;
            this.threadsTrackbar.Value = 8;
            this.threadsTrackbar.Scroll += new System.EventHandler(this.trackBar1Scroll);
            // 
            // threadsLabel
            // 
            this.threadsLabel.AutoSize = true;
            this.threadsLabel.Location = new System.Drawing.Point(135, 402);
            this.threadsLabel.Name = "threadsLabel";
            this.threadsLabel.Size = new System.Drawing.Size(178, 20);
            this.threadsLabel.TabIndex = 10;
            this.threadsLabel.Text = "Select number of threads:";
            // 
            // runButton
            // 
            this.runButton.Enabled = false;
            this.runButton.Location = new System.Drawing.Point(657, 388);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(131, 48);
            this.runButton.TabIndex = 11;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // testCheckbox
            // 
            this.testCheckbox.AutoSize = true;
            this.testCheckbox.Location = new System.Drawing.Point(657, 455);
            this.testCheckbox.Name = "testCheckbox";
            this.testCheckbox.Size = new System.Drawing.Size(140, 24);
            this.testCheckbox.TabIndex = 12;
            this.testCheckbox.Text = "Speedtest mode";
            this.testCheckbox.UseVisualStyleBackColor = true;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(657, 342);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(110, 20);
            this.timeLabel.TabIndex = 13;
            this.timeLabel.Text = "Execution time:";
            // 
            // timeValueLabel
            // 
            this.timeValueLabel.AutoSize = true;
            this.timeValueLabel.Location = new System.Drawing.Point(657, 365);
            this.timeValueLabel.Name = "timeValueLabel";
            this.timeValueLabel.Size = new System.Drawing.Size(21, 20);
            this.timeValueLabel.TabIndex = 14;
            this.timeValueLabel.Text = "--";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 491);
            this.Controls.Add(this.timeValueLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.testCheckbox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.threadsLabel);
            this.Controls.Add(this.threadsTrackbar);
            this.Controls.Add(this.selectDllLabel);
            this.Controls.Add(this.asmButton);
            this.Controls.Add(this.cppButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.afterLabel);
            this.Controls.Add(this.beforeLabel);
            this.Controls.Add(this.afterBmpBox);
            this.Controls.Add(this.beforeBmpBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "GaussFilter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.beforeBmpBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterBmpBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadsTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void trackBar1Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(threadsTrackbar, threadsTrackbar.Value.ToString());
        }

        private PictureBox beforeBmpBox;
        private PictureBox afterBmpBox;
        private Label beforeLabel;
        private Label afterLabel;
        private Button uploadButton;
        private Button saveButton;
        private RadioButton cppButton;
        private RadioButton asmButton;
        private Label selectDllLabel;
        private TrackBar threadsTrackbar;
        private Label threadsLabel;
        private Button runButton;
        private CheckBox testCheckbox;
        private ToolTip toolTip1;
        private Label timeLabel;
        private Label timeValueLabel;
    }
}