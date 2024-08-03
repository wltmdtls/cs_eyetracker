using MySql.Data.MySqlClient;
using System;
namespace WindowsFormsApp1
{
    partial class start
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
        private void initailizeDB()
        {
            String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";

            try
            {
                conn = new MySqlConnection(strConn);
                conn.Open();
                string truncateTransformresult = "TRUNCATE TABLE transformresult";
                //string truncateResultSet = "DELETE TABLE resultset";
                cmd = new MySqlCommand(truncateTransformresult, conn);
                //cmd = new MySqlCommand(truncateResultSet, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
            //TRUNCATE TABLE transformresult;
            //TRUNCATE TABLE resultset;
        }
        private void initailizeSQLDB()
        {
            String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";

            try
            {
                conn = new MySqlConnection(strConn);
                conn.Open();
                //string truncateTransformresult = "TRUNCATE TABLE transformresult";
                string truncateResultSet = "TRUNCATE TABLE resultset";
                //cmd = new MySqlCommand(truncateTransformresult, conn);
                cmd = new MySqlCommand(truncateResultSet, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
            //TRUNCATE TABLE transformresult;
            //TRUNCATE TABLE resultset;
        }
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(35, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Consolas", 80F);
            this.label1.Location = new System.Drawing.Point(186, 130);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(900, 261);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(35, 130);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(77, 82);
            this.textBox1.TabIndex = 3;
            // 
            // start
            // 
            this.ClientSize = new System.Drawing.Size(1196, 566);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "start";
            this.Load += new System.EventHandler(this.start_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.start_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}