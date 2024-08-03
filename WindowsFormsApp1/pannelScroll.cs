using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PanelScroll : UserControl
    {
        private List<Button> buttons = new List<Button>();

        public PanelScroll()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        public void SetLabel_item(string text)
        {
            Button button = new Button();
            int cnt = buttons.Count;
            int x = (cnt == 0) ? 0 : (buttons[cnt - 1].Location.X + buttons[cnt - 1].Size.Width + 20);
            button.AutoSize = true;
            button.Font = new System.Drawing.Font("나눔스퀘어라운드 ExtraBold", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            button.Location = new System.Drawing.Point(x, 0);
            button.Name = "label" + (cnt);
            button.Text = text;
            panel1.Controls.Add(button);
            buttons.Add(button);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Bounds);
        }

        public List<Button> GetButtons()
        {
            return buttons;
        }

        public void ClearLabels()
        {
            buttons.Clear();
        }
    }
}
