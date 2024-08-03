namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_ExternalSignalReceived
    {
        public static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                ExternalSignal(eyeTracker);
            }
        }
        // <BeginExample>
        private static void ExternalSignal(IEyeTracker eyeTracker)
        {
            // Start listening to external signals.
            eyeTracker.ExternalSignalReceived += EyeTracker_ExternalSignalReceived;
            // Wait for signals to arrive.
            System.Threading.Thread.Sleep(1000);
            // Stop listening to external signals.
            eyeTracker.ExternalSignalReceived -= EyeTracker_ExternalSignalReceived;
        }
        private static void EyeTracker_ExternalSignalReceived(object sender, ExternalSignalValueEventArgs e)
        {
            Console.WriteLine(
                "Got external signal with change type {0}, value {1} and device time stamp {2}.",
                e.ChangeType, e.Value, e.DeviceTimeStamp);
        }
        // <EndExample>
    }
}