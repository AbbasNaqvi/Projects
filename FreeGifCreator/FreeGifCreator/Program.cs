using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vlc;
using Vlc.DotNet.Core;

namespace FreeGifCreator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            ////Set libvlc.dll and libvlccore.dll directory path
            //VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_AMD64;
            ////Set the vlc plugins directory path
            //VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_AMD64;

            ////Set the startup options
            //VlcContext.StartupOptions.IgnoreConfig = true;
            //VlcContext.StartupOptions.LogOptions.LogInFile = true;
            //VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
            //VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;

            ////Initialize the VlcContext
            //VlcContext.Initialize();

            //Application.Run(new Form1());

            ////Close the VlcContext
            //VlcContext.CloseAll();










            LibVLCLibrary library = LibVLCLibrary.Load(null);
            IntPtr inst, mp, m;

            inst = library.libvlc_new();                                      // Load the VLC engine 
            m = library.libvlc_media_new_location(inst, "path/to/your/file"); // Create a new item 
            mp = library.libvlc_media_player_new_from_media(m);               // Create a media player playing environement 
            library.libvlc_media_release(m);                                  // No need to keep the media now 
            library.libvlc_media_player_play(mp);                             // play the media_player 
            Thread.Sleep(10000);                                              // Let it play a bit 
            library.libvlc_media_player_stop(mp);                             // Stop playing 
            library.libvlc_media_player_release(mp);                          // Free the media_player 
            library.libvlc_release(inst);

            LibVLCLibrary.Free(library);
















        }
    }
}
