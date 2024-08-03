namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_GetTrackBox
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            PrintTrackBox(eyeTracker);
        }
        // <BeginExample>
        private static void PrintTrackBox(IEyeTracker eyeTracker)
        {
            // Get the track box.
            TrackBox trackBox = eyeTracker.GetTrackBox();
            Console.WriteLine("Got track box from tracker with serial number {0} with corners:", eyeTracker.SerialNumber);
            Console.WriteLine("Back Lower Left: ({0}, {1}, {2})",
                trackBox.BackLowerLeft.X, trackBox.BackLowerLeft.Y, trackBox.BackLowerLeft.Z);
            Console.WriteLine("Back Lower Right: ({0}, {1}, {2})",
                trackBox.BackLowerRight.X, trackBox.BackLowerRight.Y, trackBox.BackLowerRight.Z);
            Console.WriteLine("Back Upper Left: ({0}, {1}, {2})",
                trackBox.BackUpperLeft.X, trackBox.BackUpperLeft.Y, trackBox.BackUpperLeft.Z);
            Console.WriteLine("Back Upper Right: ({0}, {1}, {2})",
                trackBox.BackUpperRight.X, trackBox.BackUpperRight.Y, trackBox.BackUpperRight.Z);
            Console.WriteLine("Front Lower Left: ({0}, {1}, {2})",
                trackBox.FrontLowerLeft.X, trackBox.FrontLowerLeft.Y, trackBox.FrontLowerLeft.Z);
            Console.WriteLine("Front Lower Right: ({0}, {1}, {2})",
                trackBox.FrontLowerRight.X, trackBox.FrontLowerRight.Y, trackBox.FrontLowerRight.Z);
            Console.WriteLine("Front Upper Left: ({0}, {1}, {2})",
                trackBox.FrontUpperLeft.X, trackBox.FrontUpperLeft.Y, trackBox.FrontUpperLeft.Z);
            Console.WriteLine("Front Upper Right: ({0}, {1}, {2})",
                trackBox.FrontUpperRight.X, trackBox.FrontUpperRight.Y, trackBox.FrontUpperRight.Z);
        }
        // <EndExample>
    }
}