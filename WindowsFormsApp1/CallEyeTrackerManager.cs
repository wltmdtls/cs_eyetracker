using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
namespace Tobii.Research.CodeExamples
{
    class CallEyeTrackerManager
    {
        internal static void Execute(IEyeTracker eyeTracker)
        {
            if (eyeTracker != null)
            {
                CallEyeTrackerManagerExample(eyeTracker);
            }
        }
        // <BeginExample>
        private static void CallEyeTrackerManagerExample(IEyeTracker eyeTracker)
        {
            string etmStartupMode = "displayarea";
            string etmBasePath = Path.GetFullPath(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"),
                                                                "TobiiProEyeTrackerManager"));
            string appFolder = Directory.EnumerateDirectories(etmBasePath, "app*").FirstOrDefault();
            string executablePath = Path.GetFullPath(Path.Combine(etmBasePath,
                                                                    appFolder,
                                                                    "TobiiProEyeTrackerManager.exe"));
            string arguments = "--device-address=" + eyeTracker.Address + " --mode=" + etmStartupMode;
            try
            {
                Process etmProcess = new Process();
                // Redirect the output stream of the child process.
                etmProcess.StartInfo.UseShellExecute = false;
                etmProcess.StartInfo.RedirectStandardError = true;
                etmProcess.StartInfo.RedirectStandardOutput = true;
                etmProcess.StartInfo.FileName = executablePath;
                etmProcess.StartInfo.Arguments = arguments;
                etmProcess.Start();
                string stdOutput = etmProcess.StandardOutput.ReadToEnd();
                
                etmProcess.WaitForExit();
                int exitCode = etmProcess.ExitCode;
                if (exitCode == 0)
                {
                    Console.WriteLine("Eye Tracker Manager was called successfully!");
                }
                else
                {
                    Console.WriteLine("Eye Tracker Manager call returned the error code: {0}", exitCode);
                    foreach (string line in stdOutput.Split(Environment.NewLine.ToCharArray()))
                    {
                        if (line.StartsWith("ETM Error:"))
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        // <EndExample>
    }
}