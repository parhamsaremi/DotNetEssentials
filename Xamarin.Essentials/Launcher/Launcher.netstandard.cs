using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Xamarin.Essentials
{
    public static partial class Launcher
    {
        static Task<bool> PlatformCanOpenAsync(Uri uri)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Task.FromResult(true);
            else
                throw ExceptionUtils.NotSupportedOrImplementedException;
        }

        static async Task PlatformOpenAsync(Uri uri)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                await GTKTryOpenAsync(uri);
            else
                throw ExceptionUtils.NotSupportedOrImplementedException;
        }

        static Task PlatformOpenAsync(OpenFileRequest request) =>
            throw ExceptionUtils.NotSupportedOrImplementedException;

        static async Task<bool> PlatformTryOpenAsync(Uri uri)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return await GTKTryOpenAsync(uri);
            else
                throw ExceptionUtils.NotSupportedOrImplementedException;
        }

        static async Task<bool> GTKTryOpenAsync(Uri uri)
        {
            string stdout, stderr;
            int exitCode;
            var task = Task.Run(
                () => GLib.Process.SpawnCommandLineSync("xdg-open " + uri.ToString(), out stdout, out stderr, out exitCode));
            var result = await task;
            return result;
        }
    }
}
