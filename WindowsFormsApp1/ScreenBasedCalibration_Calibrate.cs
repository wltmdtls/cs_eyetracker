namespace Tobii.Research.CodeExamples
{
    using System;
    class ScreenBasedCalibration_Calibrate
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
            var calibration = new ScreenBasedCalibration(eyeTracker);
            // Enter calibration mode.
            calibration.EnterCalibrationMode();
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
                Console.WriteLine("Show point on screen at ({0}, {1})", point.X, point.Y);
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
            CalibrationResult calibrationResult = calibration.ComputeAndApply();
            Console.WriteLine("Compute and apply returned {0} and collected at {1} points.",
            calibrationResult.Status, calibrationResult.CalibrationPoints.Count);
            // Analyze the data and maybe remove points that weren't good.
            calibration.DiscardData(new NormalizedPoint2D(0.1f, 0.1f));
            // Redo collection at the discarded point.
            Console.WriteLine("Show point on screen at ({0}, {1})", 0.1f, 0.1f);
            calibration.CollectData(new NormalizedPoint2D(0.1f, 0.1f));
            // Compute and apply again.
            calibrationResult = calibration.ComputeAndApply();
            Console.WriteLine("Second compute and apply returned {0} and collected at {1} points.",
            calibrationResult.Status, calibrationResult.CalibrationPoints.Count);
            // See that you're happy with the result.
            // The calibration is done. Leave calibration mode.
            calibration.LeaveCalibrationMode();
        }
        // <EndExample>
    }
}