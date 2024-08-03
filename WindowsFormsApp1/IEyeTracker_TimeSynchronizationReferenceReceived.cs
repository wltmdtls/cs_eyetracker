namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_TimeSynchronizationReferenceReceived
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                TimeSynchronizationData(eyeTracker);
            }
        }
        // <BeginExample>
        private static void TimeSynchronizationData(IEyeTracker eyeTracker)
        {
            // Start listening to time synchronization events.
            eyeTracker.TimeSynchronizationReferenceReceived += EyeTracker_TimeSynchronizationReferenceReceived;
            // Wait for some events to arrive.
            System.Threading.Thread.Sleep(2000);
            // Stop listening to time synchronization events.
            eyeTracker.TimeSynchronizationReferenceReceived -= EyeTracker_TimeSynchronizationReferenceReceived;
        }
        private static void EyeTracker_TimeSynchronizationReferenceReceived(object sender, TimeSynchronizationReferenceEventArgs e)
        {
            Console.WriteLine("Received a time synchronization package with latency {0} microseconds. The eye tracker time stamp was {1}.",
                e.SystemResponseTimeStamp - e.SystemRequestTimeStamp, e.DeviceTimeStamp);
        }
        // <EndExample>
    }
}