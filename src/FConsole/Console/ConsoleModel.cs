using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WCmd.Console;

namespace WCmd.Model.DirScan
{
    public class Settings
    {
        public string Directory { get; set; }
    }

    public class ConsoleModel : IPageModel<object>, IPageModel
    {
        public TabItem Page { get; set; }
        public ConsoleUI PanelContent { get; set; }

        public void Activate() { }
        public void Deactivate() { }
        public void Reload()
        {
            PanelContent.directory.Text = Environment.CurrentDirectory;
        }

        public void Dispose() { }

        public void Bind(IPageWindow wnd, TabItem pageConsole)
        {
            Reload();
            var content = PanelContent;

            ConsoleCmd.Instance.Action = () => Scan();

            ConsoleCmd.Instance.Button = content.go;
            content.go.Command = ConsoleCmd.Instance; 
        }

        async void Scan()
        {
            var content = PanelContent; 

            Settings sets = new Settings { Directory = content.directory.Text };
            try
            {
                var newDir = content.directory.Text;
                if (!string.IsNullOrWhiteSpace(newDir) && Directory.Exists(newDir))
                    Directory.SetCurrentDirectory(newDir);
            }
            catch { }

            var dir = Environment.CurrentDirectory;
            // "git pull";
            string arg = content.cmd.Text;

            await Task.Factory.StartNew(() => ScanAsync(arg, sets));
            // ScanAsync(arg, sets);

            if (content.CheckAccess())
                content.go.IsEnabled = true;
        }

        void ScanAsync(string arg, Settings sets)
        {
            var content = PanelContent;
            var outRedirect = new ConsoleSetOut(content.content) { Directory = sets.Directory };
            outRedirect.WriteLine(sets.Directory + ">" + (arg ?? ""));

            if (!string.IsNullOrWhiteSpace(arg))
            { 
                var scannner = new ConsoleCmd();
                try
                {
                    scannner.Go(new[] { sets.Directory, arg });
                }
                catch (Exception ex)
                {
                    outRedirect.WriteLine("Error " + ex.Message);
                    // System.Windows.MessageBox.Show("Error " + ex.Message);
                }

            }
            outRedirect.Dispose();
        }
    }

    class ConsoleCmd : ICommand
    {
        public static ConsoleCmd Instance = new ConsoleCmd();
        public Button Button { get; set; }
        public Action Action { get; set; }

        public event EventHandler CanExecuteChanged;
        public void Update(bool enable)
        {
            Button.IsEnabled = enable;
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter) { return Action != null && Button.IsEnabled; }
        void ICommand.Execute(object parameter) { Action(); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Go(object[] args)
        {
            //Guard.Check(args.Length >= 1);

            var directory = args[0] as string;
            var arguments = args[1] as string;
            if (string.IsNullOrWhiteSpace(arguments))
                return;

            string file = "cmd.exe"; // args[1] as string;
                                  // file = @"c:\Program Files\Git\bin\git.exe";
            int index1 = arguments.IndexOf(" ");
            if (index1 <= 0)
            {
                file = arguments;
                arguments = "";
            }
            else
            {
                file = arguments.Substring(0, index1).TrimEnd();
                arguments = arguments.Substring(index1 + 1);
            }

            // Run process
            var info = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = file,
                Arguments = arguments,
                WorkingDirectory = directory,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };
            bool isShell = file.IndexOf("cmd.exe", StringComparison.InvariantCultureIgnoreCase) >= 0;
            if (isShell)
            {
                info.UseShellExecute = false;
                info.Arguments = "/c " + arguments;
            }

            var proc = new Process() { StartInfo = info, EnableRaisingEvents = true };

            Exception lastError = null;
            try
            {
                proc.OutputDataReceived +=
                    (o, e) =>
                    {
                        if (e.Data != null)
                            System.Console.WriteLine(e.Data);
                    };

                proc.ErrorDataReceived +=
                    (o, e) =>
                    {
                        if (e.Data != null)
                            System.Console.WriteLine(e.Data);
                    };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                if (!proc.HasExited && isShell)
                {
                    var input = proc.StandardInput;
                    input.WriteLine(arguments);
                    input.WriteLine("exit");
                    // proc.WaitForInputIdle();
                }
                proc.WaitForExit();

                // proc.Close();
                var ExitCode = proc.ExitCode;
            }
            catch (Exception ex)
            {
                lastError = ex;
                System.Console.WriteLine(ex.Message);
            }

            proc.Dispose();

        }
    }

}
