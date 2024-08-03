namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_HMDGazeDataReceived
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                HMDGazeData(eyeTracker);
            }
        }
        // <BeginExample>
        private static void HMDGazeData(IEyeTracker eyeTracker)
        {
            // Start listening to HMD gaze data.
            eyeTracker.HMDGazeDataReceived += EyeTracker_HMDGazeDataReceived;
            // Wait for some data to be received.
            System.Threading.Thread.Sleep(2000);
            // Stop listening to HMD gaze data.
            eyeTracker.HMDGazeDataReceived -= EyeTracker_HMDGazeDataReceived;
        }
        private static void EyeTracker_HMDGazeDataReceived(object sender, HMDGazeDataEventArgs e)
        {
            Console.WriteLine(
                "Got HMD gaze data with {0} left eye direction described by unit vector ({1}, {2}, {3}) in the HMD coordinate system.",
                e.LeftEye.GazeDirection.Validity,
                e.LeftEye.GazeDirection.UnitVector.X,
                e.LeftEye.GazeDirection.UnitVector.Y,
                e.LeftEye.GazeDirection.UnitVector.Z);
        }
        // <EndExample>
    }
}