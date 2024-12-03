namespace PoeAndmebaas
{
    partial class KorviVorm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tootePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cartListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tootePanel
            // 
            this.tootePanel.AutoScroll = true;
            this.tootePanel.Location = new System.Drawing.Point(5, 3);
            this.tootePanel.Name = "tootePanel";
            this.tootePanel.Size = new System.Drawing.Size(600, 450);
            this.tootePanel.TabIndex = 0;
            // 
            // cartListBox
            // 
            this.cartListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.cartListBox.FormattingEnabled = true;
            this.cartListBox.ItemHeight = 24;
            this.cartListBox.Location = new System.Drawing.Point(611, 38);
            this.cartListBox.Name = "cartListBox";
            this.cartListBox.Size = new System.Drawing.Size(168, 196);
            this.cartListBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(639, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ostukorv";
            // 
            // KorviVorm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 456);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cartListBox);
            this.Controls.Add(this.tootePanel);
            this.Name = "KorviVorm";
            this.Text = "KorviVorm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel tootePanel;
        private System.Windows.Forms.ListBox cartListBox;
        private System.Windows.Forms.Label label1;
    }
}