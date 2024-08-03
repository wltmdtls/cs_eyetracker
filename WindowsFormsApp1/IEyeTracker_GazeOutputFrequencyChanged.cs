namespace Tobii.Research.CodeExamples
{
    using System;
    using System.Linq;
    class IEyeTracker_GazeOutputFrequencyChanged
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            var originalFrequency = eyeTracker.GetGazeOutputFrequency();
            try
            {
                GazeOutputFrequencyChanged(eyeTracker);
            }
            finally
            {
                eyeTracker.SetGazeOutputFrequency(originalFrequency);
            }
        }
        // <BeginExample>
        private static void GazeOutputFrequencyChanged(IEyeTracker eyeTracker)
        {
            // Start listening to gaze output frequency changed event.
            eyeTracker.GazeOutputFrequencyChanged += EyeTracker_GazeOutputFrequencyChanged;
            var originalGazeOutputFrequency = eyeTracker.GetGazeOutputFrequency();
            // Find a frequency (using System.Linq).
            var newGazeOutputFrequency = eyeTracker.GetAllGazeOutputFrequencies().Where(f => f != originalGazeOutputFrequency).FirstOrDefault();
            if (newGazeOutputFrequency == default(float))
            {
                // This eye tracker only had one frequency.
                return;
            }
            // Trigger the event.
            eyeTracker.SetGazeOutputFrequency(newGazeOutputFrequency);
            // Set back original frequency.
            eyeTracker.SetGazeOutputFrequency(originalGazeOutputFrequency);
            // Stop listening to gaze output frequency changed event.
            eyeTracker.GazeOutputFrequencyChanged -= EyeTracker_GazeOutputFrequencyChanged;
        }
        private static void EyeTracker_GazeOutputFrequencyChanged(object sender, GazeOutputFrequencyEventArgs e)
        {
            Console.WriteLine("Gaze output frequency changed to {0} at time stamp {1}.", e.GazeOutputFrequency, e.SystemTimeStamp);
        }
        // <EndExample>
    }
}