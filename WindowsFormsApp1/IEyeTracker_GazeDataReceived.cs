namespace Tobii.Research.CodeExamples
{
    using System;
    using System.Data;

    class IEyeTracker_GazeDataReceived
    {
        public static void Execute(IEyeTracker eyeTracker, DataSet ds)
        {
            if (eyeTracker != null)
            {
                GazeData(eyeTracker);
            }
        }
        // <BeginExample>
        private static void GazeData(IEyeTracker eyeTracker)
        {
            // Start listening to gaze data.
            eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
            // Wait for some data to be received.
            System.Threading.Thread.Sleep(2000);
            // Stop listening to gaze data. 
            eyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
        }
        private static void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            Console.WriteLine(
                "Got gaze data with {0} left eye origin at point ({1}, {2}, {3}) in the user coordinate system.",
                e.LeftEye.GazeOrigin.Validity,
                e.LeftEye.GazeOrigin.PositionInUserCoordinates.X,
                e.LeftEye.GazeOrigin.PositionInUserCoordinates.Y,
                e.LeftEye.GazeOrigin.PositionInUserCoordinates.Z);
        }
        // <EndExample>
    }
}