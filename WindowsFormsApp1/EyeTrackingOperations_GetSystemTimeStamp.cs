using System;
namespace Tobii.Research.CodeExamples
{
    internal static class EyeTrackingOperations_GetSystemTimeStamp
    {
        internal static void Execute()
        {
            // <BeginExample>
            Console.WriteLine("\nGet system time stamp.");
            var systemTimeStamp = EyeTrackingOperations.GetSystemTimeStamp();
            Console.WriteLine("System time stamp is: {0}.", systemTimeStamp);
            // <EndExample>
        }
    }
}