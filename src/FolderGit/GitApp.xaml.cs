using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Forms.Integration;
using System.Threading;
using System.Windows.Media.Imaging;

namespace FolderGit
{
    /// <summary>
    /// Interaction logic for CsApp.xaml
    /// </summary>
    public partial class GitApp : Application
    {
        static GitApp() { } // debugger entry

        static GitApp startRef;
        public static GitApp Instance
        {
            [DebuggerStepThrough]
            get { return startRef ?? Application.Current as GitApp; }
            set { startRef = value; }
        }

        public static new FolderWindowGit MainWindow { get { return GitApp.Current.MainWindow as FolderWindowGit; } }
        public FolderWindowGit Window { get; set; }

        public static GitApp Ref()
        {
            return Instance ?? new GitApp();
        }

        public static bool StartupMode = false;
        public GitApp()
        {
            startRef = this;
            if (!StartupMode)
                Startup += App_StartupLoad;
        }

        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs args)
        {
            var ex = args.Exception;

            Trace.Write(ex.Message);
        }

        void App_StartupLoad(object sender, StartupEventArgs e)
        {
            (this as Application).MainWindow = new FolderWindowGit();

            Folder.CsApp.FolderWindow = MainWindow;
            MainWindow.Show();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            JumpList jumpList1 = JumpList.GetJumpList(GitApp.Current);

            var window = new FolderWindowGit();   // NoBorder 

            Folder.CsApp.FolderWindow = window;

            window.AllowsTransparency = false;
            window.Show();
        }

    }
}
