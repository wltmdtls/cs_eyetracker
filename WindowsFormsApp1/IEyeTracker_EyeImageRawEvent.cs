namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_EyeImageRawEvent
    {
        internal static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                EyeImageRaws(eyeTracker);
            }
        }
        // <BeginExample>
        internal static void EyeImageRaws(IEyeTracker eyeTracker)
        {
            // Start receiving eye image raws.
            eyeTracker.EyeImageRawReceived += EyeTracker_EyeImageRawReceived;
            // Wait for some eye image raws to come.
            System.Threading.Thread.Sleep(5000);
            // Stop receiving eye image raws.
            eyeTracker.EyeImageRawReceived -= EyeTracker_EyeImageRawReceived;
        }
        private static void EyeTracker_EyeImageRawReceived(object sender, EyeImageRawEventArgs e)
        {
            Console.WriteLine("Received a {0} image at system time {1}.", e.ImageType, e.SystemTimeStamp);
            // You can use the eye image raw data to create a System.Windows.Media.Imaging.BitmapSource that can be used in a WPF application.
            const double dpi = 96;
            int bytesPerPixel = (e.BitsPerPixel + e.PaddingBitsPerPixel == 8) ? 1 : 2;
            int stride = e.Width * bytesPerPixel;
            System.Windows.Media.PixelFormat pixelFormat = bytesPerPixel == 1 ?
                                                    System.Windows.Media.PixelFormats.Gray8 :
                                                    System.Windows.Media.PixelFormats.Gray16;
            System.Windows.Media.Imaging.BitmapSource bitmapSource;
            bitmapSource = System.Windows.Media.Imaging.BitmapSource.Create(e.Width, e.Height, dpi, dpi, pixelFormat, null, e.ImageData, stride);
        }
        // <EndExample>
    }
}