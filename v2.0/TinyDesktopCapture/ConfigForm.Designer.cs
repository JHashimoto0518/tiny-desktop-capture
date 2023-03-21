namespace TinyDesktopCapture {
    partial class ConfigForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
		private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.imageComboBox = new System.Windows.Forms.ComboBox();
            this.倍率NumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.倍率NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "倍率(&M):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "画像(&I):";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(58, 86);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK(&O)";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(139, 86);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(79, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "キャンセル(&C)";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // imageComboBox
            // 
            this.imageComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TinyDesktopCapture.Properties.Settings.Default, "ImageType", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.imageComboBox.DisplayMember = "1";
            this.imageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageComboBox.FormattingEnabled = true;
            this.imageComboBox.Location = new System.Drawing.Point(66, 32);
            this.imageComboBox.Name = "imageComboBox";
            this.imageComboBox.Size = new System.Drawing.Size(66, 20);
            this.imageComboBox.TabIndex = 4;
            this.imageComboBox.Text = global::TinyDesktopCapture.Properties.Settings.Default.ImageType;
            this.imageComboBox.ValueMember = "1";
            // 
            // 倍率NumericUpDown
            // 
            this.倍率NumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::TinyDesktopCapture.Properties.Settings.Default, "ImageMagnification", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.倍率NumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.倍率NumericUpDown.Location = new System.Drawing.Point(66, 7);
            this.倍率NumericUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.倍率NumericUpDown.Name = "倍率NumericUpDown";
            this.倍率NumericUpDown.Size = new System.Drawing.Size(66, 19);
            this.倍率NumericUpDown.TabIndex = 2;
            this.倍率NumericUpDown.Value = global::TinyDesktopCapture.Properties.Settings.Default.ImageMagnification;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 121);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.imageComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.倍率NumericUpDown);
            this.Controls.Add(this.label1);
            this.Name = "ConfigForm";
            this.Text = "設定";
            ((System.ComponentModel.ISupportInitialize)(this.倍率NumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown 倍率NumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox imageComboBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;

    }
}