namespace Tobii.Research.CodeExamples
{
    class IEyeTracker_CalibrationData
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            CalibrationData(eyeTracker);
            // Cleanup.
            if (System.IO.File.Exists("calibrationData.xml"))
            {
                System.IO.File.Delete("calibrationData.xml");
            }
        }
        // BeginExample
        private static void CalibrationData(IEyeTracker eyeTracker)
        {
            // Create a file in the current directory and store the calibration data in it.
            string path = "calibrationData.xml";
            using (System.IO.FileStream fileStream = System.IO.File.Create(path))
            {
                var dataContractSerializer = new System.Runtime.Serialization.DataContractSerializer(typeof(CalibrationData));
                // Retrieve the calibration data from the eye tracker.
                CalibrationData calibrationData = eyeTracker.RetrieveCalibrationData();
                // Save the calibration data to the file;
                dataContractSerializer.WriteObject(fileStream, calibrationData);
            }
            // Reopen the file and read the calibration data back.
            using (System.IO.FileStream fileStream = System.IO.File.OpenRead(path))
            {
                var dataContractSerializer = new System.Runtime.Serialization.DataContractSerializer(typeof(CalibrationData));
                // Read the calibration data from file.
                CalibrationData calibrationData = dataContractSerializer.ReadObject(fileStream) as CalibrationData;
                // Don't apply empty calibration.
                if (calibrationData.Data.Length != 0)
                {
                    // Apply the calibration data to the eye tracker.
                    eyeTracker.ApplyCalibrationData(calibrationData);
                }
            }
        }
        // EndExample
    }
}