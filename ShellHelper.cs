using System.Diagnostics;
using System.Runtime.InteropServices;

namespace vldaptest
{
    public static class ShellHelper
    {
        public static string Execute(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var fileName = "/bin/bash";
            var arguments = $"-c \"{escapedArgs}\"";

            // Uncomment this block to run in Windows environment
            //
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    fileName = "cmd.exe";
            //    arguments = $"/c \"{escapedArgs}\"";
            //}

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            string error = process.StandardError.ReadToEnd();
            string result = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                return error + result;
            }

            return result;
        }
    }
}

