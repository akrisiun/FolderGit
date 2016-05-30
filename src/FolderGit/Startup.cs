using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Threading;
using System.ComponentModel;
using Folder; 
using Application = System.Windows.Application;
using System.Runtime.InteropServices;

namespace FolderGit
{
    public class Startup : IComponent, IDisposable
    {
        static Startup()
        {
            if (GitApp.Instance == null)
                GitApp.StartupMode = true;
            Instance = new Startup();
        }

        public void Dispose()
        {
            appCur = null;
            //App = null;
        }

        ISite IComponent.Site { get; set; }

#pragma warning disable 0067
        public event EventHandler Disposed;

        public static string Dll { get; set; }
        public static Startup Instance { get; protected set; }
        public static Startup Load() { return Instance; }
        //public FoxApplication App { get; set; }

        protected GitApp appCur;

        [STAThread]
        public void Main() // FoxApplication app = null, bool lRun = false)
        {
            Dll = "/FolderGit";

            appCur = Application.Current as GitApp ?? GitApp.Instance;
            try
            {
                //if (appCur == null)
                //    appCur = Folder.GitApp.Ref();
                appCur.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
            catch (Exception) { }

            var mainWnd = appCur.Window;
            if (mainWnd == null)
                mainWnd = new FolderWindowGit();

            CsApp.FolderWindow = mainWnd;
        }
    }
}