namespace PoeAndmebaas
{
    partial class AvaVorm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ab_btn = new System.Windows.Forms.Button();
            this.kaup_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Poe";
            // 
            // ab_btn
            // 
            this.ab_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ab_btn.Location = new System.Drawing.Point(137, 219);
            this.ab_btn.Name = "ab_btn";
            this.ab_btn.Size = new System.Drawing.Size(175, 43);
            this.ab_btn.TabIndex = 1;
            this.ab_btn.Text = "Andmebaas";
            this.ab_btn.UseVisualStyleBackColor = true;
            this.ab_btn.Click += new System.EventHandler(this.ab_btn_Click);
            // 
            // kaup_btn
            // 
            this.kaup_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kaup_btn.Location = new System.Drawing.Point(160, 151);
            this.kaup_btn.Name = "kaup_btn";
            this.kaup_btn.Size = new System.Drawing.Size(124, 43);
            this.kaup_btn.TabIndex = 2;
            this.kaup_btn.Text = "Kauplus";
            this.kaup_btn.UseVisualStyleBackColor = true;
            this.kaup_btn.Click += new System.EventHandler(this.kaup_btn_Click);
            // 
            // AvaVorm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 450);
            this.Controls.Add(this.kaup_btn);
            this.Controls.Add(this.ab_btn);
            this.Controls.Add(this.label1);
            this.Name = "AvaVorm";
            this.Text = "AvaVorm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ab_btn;
        private System.Windows.Forms.Button kaup_btn;
    }
}