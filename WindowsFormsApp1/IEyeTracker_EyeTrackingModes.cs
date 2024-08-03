using System;
namespace Tobii.Research.CodeExamples
{
    internal static class IEyeTracker_EyeTrackingModes
    {
        internal static void Execute(IEyeTracker eyeTracker)
        {
            EyeTrackingModes(eyeTracker);
        }
        // <BeginExample>
        internal static void EyeTrackingModes(IEyeTracker eyeTracker)
        {
            Console.WriteLine("\nEye tracking modes.");
            // Get and store current eye tracking mode so it can be restored.
            var initialEyeTrackingMode = eyeTracker.GetEyeTrackingMode();
            Console.WriteLine("New eye tracking mode is: {0}.", initialEyeTrackingMode);
            try
            {
                // Get all eye tracking modes.
                var allEyeTrackingModes = eyeTracker.GetAllEyeTrackingModes();
                foreach (var eyeTrackingMode in allEyeTrackingModes)
                {
                    eyeTracker.SetEyeTrackingMode(eyeTrackingMode);
                    Console.WriteLine("New eye tracking mode is: {0}.", eyeTrackingMode.ToString());
                }
            }
            finally
            {
                eyeTracker.SetEyeTrackingMode(initialEyeTrackingMode);
                Console.WriteLine("Eye tracking mode reset to: {0}.", eyeTracker.GetEyeTrackingMode());
            }
        }
        // <EndExample>
    }
}