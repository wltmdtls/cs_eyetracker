using System;
using System.Threading.Tasks;
namespace Tobii.Research.CodeExamples
{
    internal static class EyeTrackingOperations_FindAllEyeTrackersAsync
    {
        internal static async Task<EyeTrackerCollection> Execute()
        {
            return await SearchAsync();
        }
        // BeginExample
        internal static async Task<EyeTrackerCollection> SearchAsync()
        {
            Console.WriteLine("nSearching for all eye trackers asynchronously.");
            // Search for eye trackers asynchronously.
            EyeTrackerCollection eyeTrackers = await EyeTrackingOperations.FindAllEyeTrackersAsync();
            // Execution resumes here when SearchAsync is complete.
            foreach (IEyeTracker eyeTracker in eyeTrackers)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}", eyeTracker.Address, eyeTracker.DeviceName, eyeTracker.Model, eyeTracker.SerialNumber, eyeTracker.FirmwareVersion, eyeTracker.RuntimeVersion);
            }
            return eyeTrackers;
        }
        // EndExample
    }
}