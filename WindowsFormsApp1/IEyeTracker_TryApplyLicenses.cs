namespace Tobii.Research.CodeExamples
{
    using System;
    class IEyeTracker_TryApplyLicenses
    {
        public static void Execute(IEyeTracker eyeTracker, string licensePath)
        {
            ApplyLicense(eyeTracker, licensePath);
            ClearLicense(eyeTracker);
        }
        // <BeginExample>
        private static void ApplyLicense(IEyeTracker eyeTracker, string licensePath)
        {
            // Create a collection with the license.
            var licenseCollection = new LicenseCollection(
                new System.Collections.Generic.List<LicenseKey>
                {
                   new LicenseKey(System.IO.File.ReadAllBytes(licensePath))
                });
            // See if we can apply the license.
            FailedLicenseCollection failedLicenses;
            if (eyeTracker.TryApplyLicenses(licenseCollection, out failedLicenses))
            {
                Console.WriteLine(
                    "Successfully applied license from {0} on eye tracker with serial number {1}.",
                    licensePath, eyeTracker.SerialNumber);
            }
            else
            {
                Console.WriteLine(
                    "Failed to apply license from {0} on eye tracker with serial number {1}.\n" +
                    "The validation result is {2}.",
                    licensePath, eyeTracker.SerialNumber, failedLicenses[0].ValidationResult);
            }
        }
        // <EndExample>
        private static void ClearLicense(IEyeTracker eyeTracker)
        {
            // Clear any applied license.
            eyeTracker.ClearAppliedLicenses();
        }
    }
}