namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_ConnectionLost
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            ConnectionLost(eyeTracker);
        }
        // <BeginExample>
        private static void ConnectionLost(IEyeTracker eyeTracker)
        {
            // Start listening to connection lost event.
            eyeTracker.ConnectionLost += EyeTracker_ConnectionLost;
            // You can't trigger this event programatically.
            Console.WriteLine("Event will be triggered if connection is lost here.");
            // Stop listening to connection lost event.
            eyeTracker.ConnectionLost -= EyeTracker_ConnectionLost;
        }
        private static void EyeTracker_ConnectionLost(object sender, ConnectionLostEventArgs e)
        {
            Console.WriteLine("Connection to the eye tracker was lost at time stamp {0}.", e.SystemTimeStamp);
        }
        // <EndExample>
    }
}