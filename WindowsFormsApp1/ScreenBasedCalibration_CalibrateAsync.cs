namespace Tobii.Research.CodeExamples
{
    using System;
    class ScreenBasedCalibration_CalibrateAsync
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
            var calibration = new ScreenBasedCalibration(eyeTracker);
            // Enter calibration mode.
            await calibration.EnterCalibrationModeAsync();
            // Define the points on screen we should calibrate at.
            // The coordinates are normalized, i.e. (0.0f, 0.0f) is the upper left corner and (1.0f, 1.0f) is the lower right corner.
            var pointsToCalibrate = new NormalizedPoint2D[] {
                new NormalizedPoint2D(0.5f, 0.5f),
                new NormalizedPoint2D(0.1f, 0.1f),
                new NormalizedPoint2D(0.1f, 0.9f),
                new NormalizedPoint2D(0.9f, 0.1f),
                new NormalizedPoint2D(0.9f, 0.9f),
            };
            // Collect data.
            foreach (var point in pointsToCalibrate)
            {
                // Show an image on screen where you want to calibrate.
                Console.WriteLine("Show point on screen from UI thread at ({0}, {1})", point.X, point.Y);
                // Wait a little for user to focus.
                System.Threading.Thread.Sleep(700);
                // Collect data.
                CalibrationStatus status = await calibration.CollectDataAsync(point);
                if (status != CalibrationStatus.Success)
                {
                    // Try again if it didn't go well the first time.
                    // Not all eye tracker models will fail at this point, but instead fail on ComputeAndApplyAsync.
                    await calibration.CollectDataAsync(point);
                }
            }
            // Compute and apply the calibration.
            CalibrationResult calibrationResult = await calibration.ComputeAndApplyAsync();
            Console.WriteLine("Compute and apply returned {0} and collected at {1} points.",
                calibrationResult.Status, calibrationResult.CalibrationPoints.Count);
            // Analyze the data and maybe remove points that weren't good.
            calibration.DiscardData(new NormalizedPoint2D(0.1f, 0.1f));
            // Redo collection at the discarded point.
            Console.WriteLine("Show point on screen from UI thread at ({0}, {1})", 0.1f, 0.1f);
            await calibration.CollectDataAsync(new NormalizedPoint2D(0.1f, 0.1f));
            // Compute and apply again.
            calibrationResult = await calibration.ComputeAndApplyAsync();
            Console.WriteLine("Second compute and apply returned {0} and collected at {1} points.",
                calibrationResult.Status, calibrationResult.CalibrationPoints.Count);
            // See that you're happy with the result.
            // The calibration is done. Leave calibration mode.
            await calibration.LeaveCalibrationModeAsync();
        }
        // <EndExample>
    }
}