using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class startMenu : Form
    {
        public startMenu(Main main)
        {
            InitializeComponent();
        }

        private void start_Load(object sender, EventArgs e)
        {
            this.Text = "Collection System of Eye Movements";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //start showstart = new start(this, 1);
            //showstart.ShowDialog();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //start showstart = new start(this, 2);
            //showstart.ShowDialog();
            //this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //start showstart = new start(this, 3);
            //showstart.ShowDialog();
            //this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //start showstart = new start(this, 4);
            //showstart.ShowDialog();
            //this.Close();
        }   
    }
}
