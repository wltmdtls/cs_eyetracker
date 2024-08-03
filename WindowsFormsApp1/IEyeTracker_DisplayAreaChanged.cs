namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_DisplayAreaChanged
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                DisplayAreaChanged(eyeTracker);
            }
        }
        // <BeginExample>
        private static void DisplayAreaChanged(IEyeTracker eyeTracker)
        {
            // Start listening to display area changed event.
            eyeTracker.DisplayAreaChanged += EyeTracker_DisplayAreaChanged;
            // You can't trigger this event programatically.
            Console.WriteLine("Event will be triggered if display area is changed here.");
            // Stop listening to display area changed event.
            eyeTracker.DisplayAreaChanged -= EyeTracker_DisplayAreaChanged;
        }
        private static void EyeTracker_DisplayAreaChanged(object sender, DisplayAreaEventArgs e)
        {
            Console.WriteLine("Display area changed at time stamp {0}.", e.SystemTimeStamp);
            Console.WriteLine("New height {0} and width {1}.", e.DisplayArea.Height, e.DisplayArea.Width);
            Console.WriteLine("Coordinates:");
            Console.WriteLine("Bottom Left: ({0}, {1}, {2})",
                e.DisplayArea.BottomLeft.X, e.DisplayArea.BottomLeft.Y, e.DisplayArea.BottomLeft.Z);
            Console.WriteLine("Bottom Right: ({0}, {1}, {2})",
                e.DisplayArea.BottomRight.X, e.DisplayArea.BottomRight.Y, e.DisplayArea.BottomRight.Z);
            Console.WriteLine("Top Left: ({0}, {1}, {2})",
                e.DisplayArea.TopLeft.X, e.DisplayArea.TopLeft.Y, e.DisplayArea.TopLeft.Z);
            Console.WriteLine("Top Right: ({0}, {1}, {2})",
                e.DisplayArea.TopRight.X, e.DisplayArea.TopRight.Y, e.DisplayArea.TopRight.Z);
        }
        // <EndExample>
    }
}