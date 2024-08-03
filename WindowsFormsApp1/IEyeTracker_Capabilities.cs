using System;
namespace Tobii.Research.CodeExamples
{
    internal static class IEyeTracker_Capabilities
    {
        internal static void Execute(IEyeTracker eyeTracker)
        {
            ReadCapabilities(eyeTracker);
        }
        // <BeginExample>
        internal static void ReadCapabilities(IEyeTracker eyeTracker)
        {
            Console.WriteLine("Capabilities:");
            // Get capabilities.
            Capabilities trackerCapabilities = eyeTracker.DeviceCapabilities;
            Console.WriteLine(" CanDoHMDBasedCalibration: {0}", trackerCapabilities.HasFlag(Capabilities.CanDoHMDBasedCalibration));
            Console.WriteLine(" CanDoScreenBasedCalibration: {0}", trackerCapabilities.HasFlag(Capabilities.CanDoScreenBasedCalibration));
            Console.WriteLine(" CanSetDisplayArea: {0}", trackerCapabilities.HasFlag(Capabilities.CanSetDisplayArea));
            Console.WriteLine(" HasExternalSignal: {0}", trackerCapabilities.HasFlag(Capabilities.HasExternalSignal));
            Console.WriteLine(" HasEyeImages: {0}", trackerCapabilities.HasFlag(Capabilities.HasEyeImages));
            Console.WriteLine(" HasGazeData: {0}", trackerCapabilities.HasFlag(Capabilities.HasGazeData));
            Console.WriteLine(" HasHMDGazeData: {0}", trackerCapabilities.HasFlag(Capabilities.HasHMDGazeData));
            Console.WriteLine(" HasHMDLensConfig: {0}", trackerCapabilities.HasFlag(Capabilities.HasHMDLensConfig));
            Console.WriteLine(" CanDoMonocularCalibration: {0}", trackerCapabilities.HasFlag(Capabilities.CanDoMonocularCalibration));
        }
        // <EndExample>
    }
}