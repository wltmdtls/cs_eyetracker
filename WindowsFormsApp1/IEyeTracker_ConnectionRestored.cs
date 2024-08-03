namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_ConnectionRestored
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            ConnectionRestored(eyeTracker);
        }
        // <BeginExample>
        private static void ConnectionRestored(IEyeTracker eyeTracker)
        {
            // Start listening to connection restored event.
            eyeTracker.ConnectionRestored += EyeTracker_ConnectionRestored;
            // You can't trigger this event programatically.
            Console.WriteLine("Event will be triggered if connection is restored here.");
            // Stop listening to connection restored event.
            eyeTracker.ConnectionRestored -= EyeTracker_ConnectionRestored;
        }
        private static void EyeTracker_ConnectionRestored(object sender, ConnectionRestoredEventArgs e)
        {
            Console.WriteLine("Connection to the eye tracker was restored at time stamp {0}.", e.SystemTimeStamp);
        }
        // <EndExample>
    }
}