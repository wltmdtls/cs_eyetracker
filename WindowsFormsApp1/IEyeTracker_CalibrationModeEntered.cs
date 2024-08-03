namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_CalibrationModeEntered
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                CalibrationModeEntered(eyeTracker);
            }
        }
        // <BeginExample>
        private static void CalibrationModeEntered(IEyeTracker eyeTracker)
        {
            // Start listening to calibration mode entered event.
            eyeTracker.CalibrationModeEntered += EyeTracker_CalibrationModeEntered;
            var calibration = new ScreenBasedCalibration(eyeTracker);
            // Trigger the event.
            calibration.EnterCalibrationMode();
            // Stop listening to calibration mode entered event.
            eyeTracker.CalibrationModeEntered -= EyeTracker_CalibrationModeEntered;
            // Cleanup.
            calibration.LeaveCalibrationMode();
        }
        private static void EyeTracker_CalibrationModeEntered(object sender, CalibrationModeEnteredEventArgs e)
        {
            Console.WriteLine("Calibration mode was entered at time stamp {0}.", e.SystemTimeStamp);
        }
        // <EndExample>
    }
}