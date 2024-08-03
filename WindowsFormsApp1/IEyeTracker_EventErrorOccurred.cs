using System;
namespace Tobii.Research.CodeExamples
{
    class IEyeTracker_EventErrorOccurred
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            EventErrorOccurred(eyeTracker);
        }
        // <BeginExample>
        private static bool errorThrownInEventHandler = false;
        private static void EventErrorOccurred(IEyeTracker eyeTracker)
        {
            // Start listening to event errors.
            eyeTracker.EventErrorOccurred += EyeTracker_EventErrorOccurred;
            // Trigger an event by throwing an exception in an event handler.
            errorThrownInEventHandler = false;
            eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
            System.Threading.Thread.Sleep(1000);
            eyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
            // Stop listening to event errors
            eyeTracker.EventErrorOccurred -= EyeTracker_EventErrorOccurred;
        }
        private static void EyeTracker_EventErrorOccurred(object sender, EventErrorEventArgs e)
        {
            Console.WriteLine("An error occured at time stamp {0}.", e.SystemTimeStamp);
            Console.WriteLine(e.Message);
        }
        private static void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            if (!errorThrownInEventHandler)
            {
                // Throw some generic error.
                errorThrownInEventHandler = true;
                throw new ArgumentNullException();
            }
        }
        // <EndExample>
    }
}