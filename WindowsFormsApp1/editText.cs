using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class editText : Form
    {
        private string strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;Sslmode=none";
        MySqlConnection conn;
        MySqlCommand cmd;
        private string pre_str;
        Management tx;

        public editText()
        {
            InitializeComponent();
            pre_str = maskedTextBox1.Text;
        }

        public editText(Management Text)
        {
            InitializeComponent();
            pre_str = maskedTextBox1.Text;
            this.tx = Text;
        }

        private void editText_Load(object sender, EventArgs e)
        {
            this.Text = "Edit Text";
        }

        private  void update_Text()
        {
            string text = maskedTextBox1.Text;
            string num = label1.Text;
            try
            {
                conn = new MySqlConnection(strConn);
                conn.Open();
                String sql = "UPDATE letters SET contents ='" + text + "'WHERE num =" + num; 
                cmd = new MySqlCommand(sql,conn);
                Console.WriteLine(sql);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            update_Text();
            this.Close();
        }        

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
