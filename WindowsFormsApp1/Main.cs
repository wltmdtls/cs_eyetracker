using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Research;
using Tobii.Research.Addons;
using Tobii.Research.CodeExamples;

namespace WindowsFormsApp1
{
 

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Collection System of Eye Movements";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            //startMenu showStart = new startMenu(this);
            //showStart.ShowDialog();
            //this.Close();
            this.Visible = false;
            Management showManagement = new Management(this);
            showManagement.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Management showManagement = new Management(this);
            showManagement.ShowDialog();

        }
        /*[DllImport("tobii_pro.dll")]
        public static extern EyeTrackerCollection FindAllEyeTrackers();
       
        private void button3_Click(object sender, EventArgs e)
        {
            var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();
            Console.WriteLine("Found eye tracker {0}", eyeTracker.Address);

            var calibrationValidation = new ScreenBasedCalibrationValidation(eyeTracker);

            var points = new NormalizedPoint2D[] {
                new NormalizedPoint2D(0.1f, 0.1f),
                new NormalizedPoint2D(0.1f, 0.9f),
                new NormalizedPoint2D(0.5f, 0.5f),
                new NormalizedPoint2D(0.9f, 0.1f),
                new NormalizedPoint2D(0.9f, 0.9f)
            };

            calibrationValidation.EnterValidationMode();

            foreach (var point in points)
            {
                Console.WriteLine("Collecting for point {0}, {1}", point.X, point.Y);

                calibrationValidation.StartCollectingData(point);
                while (calibrationValidation.State == ScreenBasedCalibrationValidation.ValidationState.CollectingData)
                {
                    System.Threading.Thread.Sleep(25);
                }
            }

            var result = calibrationValidation.Compute();
            Console.WriteLine(calibrationValidation);
            calibrationValidation.LeaveValidationMode();
        }*/
        
        private void button3_Click(object sender, EventArgs e)
        {
            var eyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();

            //ScreenBasedCalibration_Calibrate.Execute(eyeTracker);

            CallEyeTrackerManager.Execute(eyeTracker);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
