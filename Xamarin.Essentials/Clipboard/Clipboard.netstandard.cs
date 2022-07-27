using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Xamarin.Essentials
{
    public static partial class Clipboard
    {
        static Task PlatformSetTextAsync(string text)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return GTKSetTextAsync(text);
            else
                throw ExceptionUtils.NotSupportedOrImplementedException;
        }

        static Task GTKSetTextAsync(string text)
        {
            var clipboardAtom = Gdk.Atom.Intern("CLIPBOARD", false);
            var clipboard = Gtk.Clipboard.Get(clipboardAtom);
            clipboard.Text = text;
            return Task.FromResult(0);
        }

        static bool PlatformHasText
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return GTKHasText;
                else
                    throw ExceptionUtils.NotSupportedOrImplementedException;
            }
        }

        static bool GTKHasText
        {
            get
            {
                var clipboardAtom = Gdk.Atom.Intern("CLIPBOARD", false);
                var clipboard = Gtk.Clipboard.Get(clipboardAtom);
                return clipboard.WaitIsTextAvailable();
            }
        }

        static Task<string> PlatformGetTextAsync()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return GTKetTextAsync();
            else
                throw ExceptionUtils.NotSupportedOrImplementedException;
        }

        static Task<string> GTKetTextAsync()
        {
            var clipboardAtom = Gdk.Atom.Intern("CLIPBOARD", false);
            var clipboard = Gtk.Clipboard.Get(clipboardAtom);
            return Task.FromResult(clipboard.WaitForText());
        }

        static void StartClipboardListeners()
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static void StopClipboardListeners()
            => throw ExceptionUtils.NotSupportedOrImplementedException;
    }
}
