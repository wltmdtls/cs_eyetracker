namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_EyeImageEvent
    {
        internal static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                EyeImages(eyeTracker);
            }
        }
        // <BeginExample>
        internal static void EyeImages(IEyeTracker eyeTracker)
        {
            // Start receiving eye images.
            eyeTracker.EyeImageReceived += EyeTracker_EyeImageReceived;
            // Wait for some eye images to come.
            System.Threading.Thread.Sleep(5000);
            // Stop receiving eye images.
            eyeTracker.EyeImageReceived -= EyeTracker_EyeImageReceived;
        }
        private static void EyeTracker_EyeImageReceived(object sender, EyeImageEventArgs e)
        {
            Console.WriteLine("Received a {0} image at system time {1}.", e.ImageType, e.SystemTimeStamp);
            // You can convert the image stream to a System.Drawing.Bitmap if you're making a Windows Form application.
            var drawingBitmap = new System.Drawing.Bitmap(e.ImageStream);
            // Or you can convert it to a System.Windows.Media.Imaging.BitmapImage if you're making a WPF application.
            var imagingBitmap = new System.Windows.Media.Imaging.BitmapImage();
            imagingBitmap.BeginInit();
            imagingBitmap.StreamSource = e.ImageStream;
            imagingBitmap.EndInit();
        }
        // <EndExample>
    }
}