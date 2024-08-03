using System;
namespace Tobii.Research.CodeExamples
{
    internal static class EyeTrackingOperations_LogReceived
    {
        internal static void Execute()
        {
            CodeExamples.EyeTrackingOperations_LogReceived.ReceiveLogs();
        }
        // <BeginExample>
        internal static void ReceiveLogs()
        {
            Console.WriteLine("\nReceive logs");
            // Subscribe to debug logs.
            EyeTrackingOperations.LogReceived += LogReceived;
            // Provoke some logs.
            EyeTrackingOperations.FindAllEyeTrackers();
            // Unsubscribe from debug logs.
            EyeTrackingOperations.LogReceived -= LogReceived;
        }
        private static void LogReceived(object sender, LogEventArgs e)
        {
            Console.WriteLine("\nSource: {0}\nLevel: {1}\nMessage: \"{2}\" Time Stamp: \"{3}\"", e.Source, e.Level, e.Message, e.SystemTimeStamp);
        }
        // <EndExample>
    }
}