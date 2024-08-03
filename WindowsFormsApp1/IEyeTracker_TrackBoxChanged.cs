namespace Tobii.Research.CodeExamples
{
    using System;
    using System.Linq;
    class IEyeTracker_TrackBoxChanged
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            var originalFrequency = eyeTracker.GetGazeOutputFrequency();
            try
            {
                TrackBoxChanged(eyeTracker);
            }
            finally
            {
                eyeTracker.SetGazeOutputFrequency(originalFrequency);
            }
        }
        // <BeginExample>
        private static void TrackBoxChanged(IEyeTracker eyeTracker)
        {
            // Start listening to track box changed event.
            eyeTracker.TrackBoxChanged += EyeTracker_TrackBoxChanged;
            var originalGazeOutputFrequency = eyeTracker.GetGazeOutputFrequency();
            // Find a frequency (using System.Linq).
            var newGazeOutputFrequency = eyeTracker.GetAllGazeOutputFrequencies().Where(f => f != originalGazeOutputFrequency).FirstOrDefault();
            if (newGazeOutputFrequency == default(float))
            {
                // This eye tracker only had one frequency.
                return;
            }
            // Trigger the event. Some trackers have different track boxes at different
            // gaze output frequencies.
            eyeTracker.SetGazeOutputFrequency(newGazeOutputFrequency);
            // Set back original frequency.
            eyeTracker.SetGazeOutputFrequency(originalGazeOutputFrequency);
            // Stop listening to track box changed event.
            eyeTracker.TrackBoxChanged -= EyeTracker_TrackBoxChanged;
        }
        private static void EyeTracker_TrackBoxChanged(object sender, TrackBoxEventArgs e)
        {
            Console.WriteLine("Track box changed at time stamp {0}.", e.SystemTimeStamp);
        }
        // <EndExample>
    }
}