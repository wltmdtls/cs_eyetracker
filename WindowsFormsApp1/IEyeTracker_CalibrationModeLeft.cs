namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_CalibrationModeLeft
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                CalibrationModeLeft(eyeTracker);
            }
        }
        // <BeginExample>
        private static void CalibrationModeLeft(IEyeTracker eyeTracker)
        {
            // Start listening to calibration mode left event.
            eyeTracker.CalibrationModeLeft += EyeTracker_CalibrationModeLeft;
            var calibration = new ScreenBasedCalibration(eyeTracker);
            calibration.EnterCalibrationMode();
            // Trigger the event.
            calibration.LeaveCalibrationMode();
            // Stop listening to calibration mode left event.
            eyeTracker.CalibrationModeLeft -= EyeTracker_CalibrationModeLeft;
        }
        private static void EyeTracker_CalibrationModeLeft(object sender, CalibrationModeLeftEventArgs e)
        {
            Console.WriteLine("Calibration mode was left at time stamp {0}.", e.SystemTimeStamp);
        }
        // <EndExample>
    }
}