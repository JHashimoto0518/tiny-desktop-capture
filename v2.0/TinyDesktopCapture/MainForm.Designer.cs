namespace TinyDesktopCapture {
    partial class MainForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            CopyToClipboardToolStripMenuItem = new ToolStripMenuItem();
            設定ToolStripMenuItem = new ToolStripMenuItem();
            FixOnDesktopToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { FixOnDesktopToolStripMenuItem, CopyToClipboardToolStripMenuItem, 設定ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(241, 133);
            // 
            // CopyToClipboardToolStripMenuItem
            // 
            CopyToClipboardToolStripMenuItem.Name = "CopyToClipboardToolStripMenuItem";
            CopyToClipboardToolStripMenuItem.Size = new Size(240, 32);
            CopyToClipboardToolStripMenuItem.Text = "クリップボードにコピー";
            // 
            // 設定ToolStripMenuItem
            // 
            設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            設定ToolStripMenuItem.Size = new Size(240, 32);
            設定ToolStripMenuItem.Text = "設定";
            // 
            // FixOnDesktopToolStripMenuItem
            // 
            FixOnDesktopToolStripMenuItem.Name = "FixOnDesktopToolStripMenuItem";
            FixOnDesktopToolStripMenuItem.Size = new Size(240, 32);
            FixOnDesktopToolStripMenuItem.Text = "デスクトップに固定";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 515);
            Margin = new Padding(5, 6, 5, 6);
            Name = "MainForm";
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }


        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToClipboardToolStripMenuItem;
        private ToolStripMenuItem FixOnDesktopToolStripMenuItem;
    }
}