using System;
namespace Tobii.Research.CodeExamples
{
    internal static class EyeTrackingOperations_GetEyeTracker_Uri
    {
        internal static void Execute(Uri address)
        {
            EyeTrackingOperations_GetEyeTracker_Uri.GetEyeTracker(address);
        }
        // <BeginExample>
        internal static IEyeTracker GetEyeTracker(Uri address)
        {
            Console.WriteLine("\nGet eye tracker from Uri: {0}.", address);
            var eyeTracker = EyeTrackingOperations.GetEyeTracker(address);
            Console.WriteLine("Got eye tracker");
            Console.WriteLine("Address: {0}", eyeTracker.Address);
            Console.WriteLine("Device name: {0}", eyeTracker.DeviceName);
            Console.WriteLine("Model: {0}", eyeTracker.Model);
            Console.WriteLine("Serial number: {0}", eyeTracker.SerialNumber);
            Console.WriteLine("Firmware version: {0}", eyeTracker.FirmwareVersion);
            Console.WriteLine("Runtime version: {0}", eyeTracker.RuntimeVersion);
            return eyeTracker;
        }
        // <EndExample>
    }
}