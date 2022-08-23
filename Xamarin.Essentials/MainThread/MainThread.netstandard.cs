using System;

namespace Xamarin.Essentials
{
    public static partial class MainThread
    {
#pragma warning disable SA1130
        static void PlatformBeginInvokeOnMainThread(Action action){
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                Gtk.Application.Invoke(delegate { action(); });
            else
                throw ExceptionUtils.NotSupportedOrImplementedException;
        }
        static int? uiThreadId = null;

        static bool PlatformIsMainThread
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (uiThreadId == null)
                    {
                        Gtk.Application.Invoke(delegate { uiThreadId = Thread.CurrentThread.ManagedThreadId; });
                        return false;
                    }
                    else
                    {
                        return System.Threading.Thread.CurrentThread.ManagedThreadId == uiThreadId;
                    }
                }
                else
                {
                    throw ExceptionUtils.NotSupportedOrImplementedException;
                }
            }
        }
#pragma warning restore SA1130
    }
}
