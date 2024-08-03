using System;
namespace Tobii.Research.CodeExamples
{
    internal static class EyeTrackingOperations_FindAllEyeTrackers
    {
        internal static EyeTrackerCollection Execute()
        {
            // <BeginExample>
            Console.WriteLine("\nSearching for all eye trackers");
            EyeTrackerCollection eyeTrackers = EyeTrackingOperations.FindAllEyeTrackers();
            foreach (IEyeTracker eyeTracker in eyeTrackers)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", eyeTracker.Address, eyeTracker.DeviceName, eyeTracker.Model, eyeTracker.SerialNumber, eyeTracker.FirmwareVersion, eyeTracker.RuntimeVersion);
            }
            // <EndExample>
            return eyeTrackers;
        }
    }
}