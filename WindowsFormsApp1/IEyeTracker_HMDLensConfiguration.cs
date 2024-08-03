namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_HMDLensConfiguration
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                ReadAndWriteHMDLensConfiguration(eyeTracker);
            }
        }
        // <BeginExample>
        private static void ReadAndWriteHMDLensConfiguration(IEyeTracker eyeTracker)
        {
            // Check that the tracker supports HMD lens configuration
            if(!eyeTracker.DeviceCapabilities.HasFlag(Capabilities.HasHMDLensConfig))
            {
                Console.WriteLine("Tracker does not support HMD lens configuration");
                return;
            }
            // Read the current HMDLensConfiguration
            HMDLensConfiguration originalHMDLensConfiguration = eyeTracker.GetHMDLensConfiguration();
            Console.WriteLine("Original HMD lens config:\nLeft=({0,4}, {1,4}, {2,4}) Right=({3,4}, {4,4}, {5,4})",
                originalHMDLensConfiguration.Left.X, originalHMDLensConfiguration.Left.Y, originalHMDLensConfiguration.Left.Z,
                originalHMDLensConfiguration.Right.X, originalHMDLensConfiguration.Right.Y, originalHMDLensConfiguration.Right.Z);
            // Write new configuration
            HMDLensConfiguration newHMDLensConfiguration = new HMDLensConfiguration(new Point3D(30, 0, 0), new Point3D(-30, 0, 0));
            eyeTracker.SetHMDLensConfiguration(newHMDLensConfiguration);
            HMDLensConfiguration readHMDLensConfiguration = eyeTracker.GetHMDLensConfiguration();
            Console.WriteLine("New HMD lens config:\nLeft=({0,4}, {1,4}, {2,4}) Right=({3,4}, {4,4}, {5,4})",
                readHMDLensConfiguration.Left.X, readHMDLensConfiguration.Left.Y, readHMDLensConfiguration.Left.Z,
                readHMDLensConfiguration.Right.X, readHMDLensConfiguration.Right.Y, readHMDLensConfiguration.Right.Z);
            // Reset configuration to original settings
            eyeTracker.SetHMDLensConfiguration(originalHMDLensConfiguration);
            readHMDLensConfiguration = eyeTracker.GetHMDLensConfiguration();
            Console.WriteLine("Final config:\nLeft=({0,4}, {1,4}, {2,4}) Right=({3,4}, {4,4}, {5,4})",
                readHMDLensConfiguration.Left.X, readHMDLensConfiguration.Left.Y, readHMDLensConfiguration.Left.Z,
                readHMDLensConfiguration.Right.X, readHMDLensConfiguration.Right.Y, readHMDLensConfiguration.Right.Z);
        }
        // <EndExample>
    }
}