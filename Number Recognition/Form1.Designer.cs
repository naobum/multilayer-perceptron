namespace Number_Recognition
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
            panel1 = new Panel();
            label1 = new Label();
            button2 = new Button();
            button1 = new Button();
            splitContainer1 = new SplitContainer();
            drawingField = new PictureBox();
            predictNum = new Label();
            menuStrip1 = new MenuStrip();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)drawingField).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 348);
            panel1.Name = "panel1";
            panel1.Size = new Size(765, 196);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(488, 6);
            label1.Name = "label1";
            label1.Size = new Size(180, 25);
            label1.TabIndex = 2;
            label1.Text = "Распознанное число";
            // 
            // button2
            // 
            button2.Location = new Point(0, 46);
            button2.Name = "button2";
            button2.Size = new Size(379, 34);
            button2.TabIndex = 1;
            button2.Text = "Очистить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(0, 6);
            button1.Name = "button1";
            button1.Size = new Size(379, 34);
            button1.TabIndex = 0;
            button1.Text = "Распознать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(drawingField);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(predictNum);
            splitContainer1.Panel2.Controls.Add(menuStrip1);
            splitContainer1.Size = new Size(765, 348);
            splitContainer1.SplitterDistance = 379;
            splitContainer1.TabIndex = 1;
            // 
            // drawingField
            // 
            drawingField.BackColor = Color.White;
            drawingField.Dock = DockStyle.Fill;
            drawingField.Location = new Point(0, 0);
            drawingField.Name = "drawingField";
            drawingField.Size = new Size(379, 348);
            drawingField.TabIndex = 0;
            drawingField.TabStop = false;
            drawingField.MouseDown += drawingField_MouseDown;
            drawingField.MouseMove += drawingField_MouseMove;
            drawingField.MouseUp += drawingField_MouseUp;
            // 
            // predictNum
            // 
            predictNum.AutoSize = true;
            predictNum.Font = new Font("Palatino Linotype", 72F, FontStyle.Regular, GraphicsUnit.Point, 204);
            predictNum.Location = new Point(131, 85);
            predictNum.Name = "predictNum";
            predictNum.Size = new Size(154, 193);
            predictNum.TabIndex = 0;
            predictNum.Text = "0";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(382, 33);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(97, 29);
            справкаToolStripMenuItem.Text = "Справка";
            справкаToolStripMenuItem.Click += справкаToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(765, 544);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Распознавание цифр";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)drawingField).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private SplitContainer splitContainer1;
        private Label predictNum;
        private PictureBox drawingField;
        private Button button2;
        private Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private Label label1;
    }
}
