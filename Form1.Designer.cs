namespace PoeAndmebaas
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectDB_btn = new System.Windows.Forms.Button();
            this.otsipilt = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uuenda_btn = new System.Windows.Forms.Button();
            this.kustuta_btn = new System.Windows.Forms.Button();
            this.lisa_btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Hind_txt = new System.Windows.Forms.TextBox();
            this.Kogus_txt = new System.Windows.Forms.TextBox();
            this.Nimetus_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Kogus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Ladu_cb = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // connectDB_btn
            // 
            this.connectDB_btn.Location = new System.Drawing.Point(675, 198);
            this.connectDB_btn.Name = "connectDB_btn";
            this.connectDB_btn.Size = new System.Drawing.Size(93, 33);
            this.connectDB_btn.TabIndex = 25;
            this.connectDB_btn.Text = "Lisa andmebaas";
            this.connectDB_btn.UseVisualStyleBackColor = true;
            //this.connectDB_btn.Click += new System.EventHandler(this.ConnectDB_btn_Click);
            // 
            // otsipilt
            // 
            this.otsipilt.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold);
            this.otsipilt.Location = new System.Drawing.Point(355, 201);
            this.otsipilt.Name = "otsipilt";
            this.otsipilt.Size = new System.Drawing.Size(92, 24);
            this.otsipilt.TabIndex = 24;
            this.otsipilt.Text = "Otsi pilt";
            this.otsipilt.UseVisualStyleBackColor = true;
            this.otsipilt.Click += new System.EventHandler(this.Otsipilt_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(469, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(299, 174);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // uuenda_btn
            // 
            this.uuenda_btn.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uuenda_btn.Location = new System.Drawing.Point(255, 201);
            this.uuenda_btn.Name = "uuenda_btn";
            this.uuenda_btn.Size = new System.Drawing.Size(94, 24);
            this.uuenda_btn.TabIndex = 22;
            this.uuenda_btn.Text = "Uuenda";
            this.uuenda_btn.UseVisualStyleBackColor = true;
            this.uuenda_btn.Click += new System.EventHandler(this.Uuenda_btn_Click);
            // 
            // kustuta_btn
            // 
            this.kustuta_btn.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kustuta_btn.Location = new System.Drawing.Point(148, 201);
            this.kustuta_btn.Name = "kustuta_btn";
            this.kustuta_btn.Size = new System.Drawing.Size(101, 24);
            this.kustuta_btn.TabIndex = 21;
            this.kustuta_btn.Text = "Kustuta";
            this.kustuta_btn.UseVisualStyleBackColor = true;
            this.kustuta_btn.Click += new System.EventHandler(this.Kustuta_btn_Click);
            // 
            // lisa_btn
            // 
            this.lisa_btn.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lisa_btn.Location = new System.Drawing.Point(38, 201);
            this.lisa_btn.Name = "lisa_btn";
            this.lisa_btn.Size = new System.Drawing.Size(104, 24);
            this.lisa_btn.TabIndex = 20;
            this.lisa_btn.Text = "Lisa andmed";
            this.lisa_btn.UseVisualStyleBackColor = true;
            this.lisa_btn.Click += new System.EventHandler(this.Lisa_btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(38, 248);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(730, 172);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // Hind_txt
            // 
            this.Hind_txt.Location = new System.Drawing.Point(114, 105);
            this.Hind_txt.Name = "Hind_txt";
            this.Hind_txt.Size = new System.Drawing.Size(119, 20);
            this.Hind_txt.TabIndex = 18;
            // 
            // Kogus_txt
            // 
            this.Kogus_txt.Location = new System.Drawing.Point(114, 68);
            this.Kogus_txt.Name = "Kogus_txt";
            this.Kogus_txt.Size = new System.Drawing.Size(119, 20);
            this.Kogus_txt.TabIndex = 17;
            // 
            // Nimetus_txt
            // 
            this.Nimetus_txt.Location = new System.Drawing.Point(115, 32);
            this.Nimetus_txt.Name = "Nimetus_txt";
            this.Nimetus_txt.Size = new System.Drawing.Size(119, 20);
            this.Nimetus_txt.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(34, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 21);
            this.label2.TabIndex = 15;
            this.label2.Text = "Hind";
            // 
            // Kogus
            // 
            this.Kogus.AutoSize = true;
            this.Kogus.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.Kogus.Location = new System.Drawing.Point(34, 66);
            this.Kogus.Name = "Kogus";
            this.Kogus.Size = new System.Drawing.Size(58, 21);
            this.Kogus.TabIndex = 14;
            this.Kogus.Text = "Kogus";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "Nimetus";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(34, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 21);
            this.label3.TabIndex = 26;
            this.label3.Text = "Ladu";
            // 
            // Ladu_cb
            // 
            this.Ladu_cb.FormattingEnabled = true;
            this.Ladu_cb.Location = new System.Drawing.Point(112, 142);
            this.Ladu_cb.Name = "Ladu_cb";
            this.Ladu_cb.Size = new System.Drawing.Size(121, 21);
            this.Ladu_cb.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Ladu_cb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.connectDB_btn);
            this.Controls.Add(this.otsipilt);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.uuenda_btn);
            this.Controls.Add(this.kustuta_btn);
            this.Controls.Add(this.lisa_btn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Hind_txt);
            this.Controls.Add(this.Kogus_txt);
            this.Controls.Add(this.Nimetus_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Kogus);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectDB_btn;
        private System.Windows.Forms.Button otsipilt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button uuenda_btn;
        private System.Windows.Forms.Button kustuta_btn;
        private System.Windows.Forms.Button lisa_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox Hind_txt;
        private System.Windows.Forms.TextBox Kogus_txt;
        private System.Windows.Forms.TextBox Nimetus_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Kogus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Ladu_cb;
    }
}

