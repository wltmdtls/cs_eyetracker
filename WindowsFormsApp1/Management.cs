using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace WindowsFormsApp1
{

    public partial class Management : Form
    {

        // connection string
        MySqlConnection conn;
        MySqlCommand cmd;

        public String strConn = "Server=localhost;Database=dyslexia;Uid=root;Pwd=7350;";
        public string selectedlist;
        Main f;

        public Management(Main form1)
        {
            InitializeComponent();
            this.f = form1;
        }

        private void Management_Load_1(object sender, EventArgs e)
        {
            this.Text = "Text Settings";

            //string connStr = string.Format(@"Server=localhost;Database=mydb;Uid=root;Pwd=autoset;");
            MySqlConnection conn = new MySqlConnection(strConn);

            try
            {
                conn.Open();
                MessageBox.Show("MySQL 연결 성공");
            }
            catch
            {
                conn.Close();
                MessageBox.Show("MySQL 연결 실패");
                //Application.OpenForms["Management"].Close();      // 실패시 폼을 닫아준다.

                Main MainForm = new Main();
                MainForm.Show();
                this.Close();
            }
        }

        void ConnectCheck()
        {
            try
            {
                conn.Open();
                MessageBox.Show("MySQL 연결 성공");
            }
            catch
            {
                conn.Close();
                MessageBox.Show("MySQL 연결 실패");
                //Application.OpenForms["Management"].Close();      // 실패시 폼을 닫아준다.
            }
        }

        internal void Management_Load_1()
        {
            throw new NotImplementedException();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 글자조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new MySqlConnection(strConn);                    // MySQL 연결
                ConnectCheck();                                         // MySQL 연결 체크

                listView1.View = View.Details;
                MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM letters WHERE code = 1", conn);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                listView1.Items.Clear();                                // View가 중복조회 되는것을 방지한다.

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["contents"].ToString());
                    listitem.SubItems.Add(dr["num"].ToString());
                    //listitem.SubItems.Add(dr["contents"].ToString()); 
                    listView1.Items.Add(listitem);
                }

                conn.Close();
            }                               // MySQL 연결 해제
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 단어조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new MySqlConnection(strConn);                    // MySQL 연결
                ConnectCheck();                                         // MySQL 연결 체크

                listView1.View = View.Details;
                MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM letters WHERE code = 2", conn);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                listView1.Items.Clear();                                // View가 중복조회 되는것을 방지한다.

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["contents"].ToString());
                    listitem.SubItems.Add(dr["num"].ToString());
                    //listitem.SubItems.Add(dr["contents"].ToString()); 
                    listView1.Items.Add(listitem);
                }

                conn.Close();
            }                               // MySQL 연결 해제
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 문장조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new MySqlConnection(strConn);                    // MySQL 연결
                ConnectCheck();                                         // MySQL 연결 체크

                listView1.View = View.Details;
                MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM letters WHERE code = 3", conn);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                listView1.Items.Clear();                                // View가 중복조회 되는것을 방지한다.

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["contents"].ToString());
                    listitem.SubItems.Add(dr["num"].ToString());
                    //listitem.SubItems.Add(dr["contents"].ToString()); 
                    listView1.Items.Add(listitem);
                }

                conn.Close();
            }                               // MySQL 연결 해제
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 문단조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new MySqlConnection(strConn);                    // MySQL 연결
                ConnectCheck();                                         // MySQL 연결 체크

                listView1.View = View.Details;
                MySqlDataAdapter adapt = new MySqlDataAdapter("SELECT * FROM letters WHERE code = 4", conn);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                listView1.Items.Clear();                                // View가 중복조회 되는것을 방지한다.

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["contents"].ToString());
                    listitem.SubItems.Add(dr["num"].ToString());
                    //listitem.SubItems.Add(dr["contents"].ToString()); 
                    listView1.Items.Add(listitem);
                }

                conn.Close();
            }                               // MySQL 연결 해제
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void 등록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertText showInsertText = new insertText(this);
            showInsertText.ShowDialog();
        }

       void delete_Text()
        {
            string contents = listView1.SelectedItems[0].Text;
            try
            {
                conn = new MySqlConnection(strConn);
                conn.Open();                
                String sql = "DELETE FROM letters WHERE contents = '" + contents + "'";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();                
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void edit_listItem()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
                ListViewItem lvItem = items[0];
                string msg = lvItem.SubItems[0].Text;
                string cnt = lvItem.SubItems[1].Text;
                Console.WriteLine(cnt);
                editText edit = new editText(this);
                edit.maskedTextBox1.Text = msg;
                edit.label1.Text = cnt;
                edit.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e) //수정
        {
            edit_listItem();
            
        }

        private void button2_Click(object sender, EventArgs e) //삭제
        {
            string contents = listView1.SelectedItems[0].Text;
            if (MessageBox.Show(contents + "를 삭제하시겠습니까?", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                delete_Text();
            }
            else
            {
                
            }
        }

        private void button3_Click(object sender, EventArgs e) //돌아가기
        {
            try
            {
                if (f != null)
                {
                    f.Show();
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
                ListViewItem listitem = items[0];
                string msg = listitem.SubItems[0].Text;

                Console.WriteLine(msg);
                start showStart = new start(this, msg)
                {
                    Owner = this
                };
                showStart.Show();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       
    }
}
