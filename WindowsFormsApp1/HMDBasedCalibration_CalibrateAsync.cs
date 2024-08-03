namespace Tobii.Research.CodeExamples
{
    using System;
    class HMDBasedCalibration_CalibrateAsync
    {
        public static async System.Threading.Tasks.Task Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                await Calibrate(eyeTracker);
            }
        }
        // <BeginExample>
        private static async System.Threading.Tasks.Task Calibrate(IEyeTracker eyeTracker)
        {
            // Create a calibration object.
            var calibration = new HMDBasedCalibration(eyeTracker);
            // Enter calibration mode.
            await calibration.EnterCalibrationModeAsync();
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
                CalibrationStatus status = await calibration.CollectDataAsync(point);
                if (status != CalibrationStatus.Success)
                {
                    // Try again if it didn't go well the first time.
                    // Not all eye tracker models will fail at this point, but instead fail on ComputeAndApply.
                    await calibration.CollectDataAsync(point);
                }
            }
            // Compute and apply the calibration.
            HMDCalibrationResult calibrationResult = await calibration.ComputeAndApplyAsync();
            Console.WriteLine("Compute and apply returned {0}.", calibrationResult.Status);
            // See that you're happy with the result.
            // The calibration is done. Leave calibration mode.
            await calibration.LeaveCalibrationModeAsync();
        }
        // <EndExample>
    }
}