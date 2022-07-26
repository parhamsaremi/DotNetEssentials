using System.Runtime.InteropServices;

namespace Xamarin.Essentials.Background
{
    public static partial class Background
    {
        internal static void PlatformStart()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                Background.StartJobs();
            else
                throw ExceptionUtils.NotSupportedOrImplementedException;
        }
    }
}
