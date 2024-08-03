using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class insertText : Form
    {
        public String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
        MySqlConnection conn;
        MySqlCommand cmd;
        private string pre_str;
        Management MG;
        public insertText(Management Form1)
        {
            InitializeComponent();
            pre_str = maskedTextBox1.Text;
            this.MG = Form1;
        }

        private void insert_Text()
        {
            string msg = maskedTextBox1.Text;
            int code = (comboBox1.SelectedIndex + 1);
            Console.WriteLine(msg);
            try
            {
               conn = new MySqlConnection(strConn);             
               conn.Open();
               cmd = new MySqlCommand("INSERT INTO letters (contents, code)" +
                        "VALUES ('" + msg + "','" + code + "')",conn);
                Console.WriteLine(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
               
            }                
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void insertText_Load(object sender, EventArgs e)
        {
            this.Text = "Text Settings";

            //comboBox1.Items.AddRange(new string[] {"글자","단어","문장","문단"});
            comboBox1.Items.AddRange(new string[] { "Letter", "Word", "Sentence", "Paragraph" });
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insert_Text();
          //  MG.refresh_listbox();
            this.Close();
        }
    }
}