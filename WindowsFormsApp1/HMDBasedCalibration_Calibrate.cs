namespace Tobii.Research.CodeExamples
{
    using System;
    class HMDBasedCalibration_Calibrate
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                Calibrate(eyeTracker);
            }
        }
        // <BeginExample>
        private static void Calibrate(IEyeTracker eyeTracker)
        {
            // Create a calibration object.
            var calibration = new HMDBasedCalibration(eyeTracker);
            // Enter calibration mode.
            calibration.EnterCalibrationMode();
            // Define the points in the HMD space we should calibrate at.
            var pointsToCalibrate = new Point3D[] {
                new Point3D(500f, 500f, 2000f),
                new Point3D(-500f, 500f, 2000f),
                new Point3D(-500f, -500f, 2000f),
                new Point3D(500f, -500f, 2000f),
                new Point3D(0f, 0f, 2000f),
            };
            // Collect data.
            foreach (var point in pointsToCalibrate)
            {
                // Show an image on screen where you want to calibrate.
                Console.WriteLine("Show point in HMD space at ({0}, {1}, {2})", point.X, point.Y, point.Z);
                // Wait a little for user to focus.
                System.Threading.Thread.Sleep(700);
                // Collect data.
                CalibrationStatus status = calibration.CollectData(point);
                if (status != CalibrationStatus.Success)
                {
                    // Try again if it didn't go well the first time.
                    // Not all eye tracker models will fail at this point, but instead fail on ComputeAndApply.
                    calibration.CollectData(point);
                }
            }
            // Compute and apply the calibration.
            HMDCalibrationResult calibrationResult = calibration.ComputeAndApply();
            Console.WriteLine("Compute and apply returned {0}.", calibrationResult.Status);
            // See that you're happy with the result.
            // The calibration is done. Leave calibration mode.
            calibration.LeaveCalibrationMode();
        }
        // <EndExample>
    }
}