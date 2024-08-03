namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_GetAndSetDisplayArea
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                PrintDisplayArea(eyeTracker);
            }
        }
        // <BeginExample>
        private static void PrintDisplayArea(IEyeTracker eyeTracker)
        {
            // Get the display area.
            DisplayArea displayArea = eyeTracker.GetDisplayArea();
            Console.WriteLine(
                "Got display area from tracker with serial number {0} with height {1}, width {2} and coordinates:",
                eyeTracker.SerialNumber,
                displayArea.Height,
                displayArea.Width);
            Console.WriteLine("Bottom Left: ({0}, {1}, {2})",
                displayArea.BottomLeft.X, displayArea.BottomLeft.Y, displayArea.BottomLeft.Z);
            Console.WriteLine("Bottom Right: ({0}, {1}, {2})",
                displayArea.BottomRight.X, displayArea.BottomRight.Y, displayArea.BottomRight.Z);
            Console.WriteLine("Top Left: ({0}, {1}, {2})",
                displayArea.TopLeft.X, displayArea.TopLeft.Y, displayArea.TopLeft.Z);
            Console.WriteLine("Top Right: ({0}, {1}, {2})",
                displayArea.TopRight.X, displayArea.TopRight.Y, displayArea.TopRight.Z);
            // Set the display area. A new object is used to show usage.
            DisplayArea displayAreaToSet = new DisplayArea(displayArea.TopLeft, displayArea.BottomLeft, displayArea.TopRight);
            eyeTracker.SetDisplayArea(displayAreaToSet);
        }
        // <EndExample>
    }
}