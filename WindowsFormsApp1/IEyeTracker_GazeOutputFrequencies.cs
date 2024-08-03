using System;
namespace Tobii.Research.CodeExamples
{
    internal static class IEyeTracker_GazeOutputFrequencies
    {
        internal static void Execute(IEyeTracker eyeTracker)
        {
            GazeOutputFrequencies(eyeTracker);
        }
        // <BeginExample>
        internal static void GazeOutputFrequencies(IEyeTracker eyeTracker)
        {
            Console.WriteLine("\nGaze output frequencies.");
            // Get and store current frequency so it can be restored.
            var initialGazeOutputFrequency = eyeTracker.GetGazeOutputFrequency();
            Console.WriteLine("Gaze output frequency is: {0} hertz.", initialGazeOutputFrequency);
            try
            {
                // Get all gaze output frequencies.
                var allGazeOutputFrequencies = eyeTracker.GetAllGazeOutputFrequencies();
                foreach (var gazeOutputFrequency in allGazeOutputFrequencies)
                {
                    eyeTracker.SetGazeOutputFrequency(gazeOutputFrequency);
                    Console.WriteLine("New gaze output frequency is: {0} hertz.", gazeOutputFrequency.ToString());
                }
            }
            finally
            {
                eyeTracker.SetGazeOutputFrequency(initialGazeOutputFrequency);
                Console.WriteLine("Gaze output frequency reset to: {0} hertz.", eyeTracker.GetGazeOutputFrequency());
            }
        }
        // <EndExample>
    }
}