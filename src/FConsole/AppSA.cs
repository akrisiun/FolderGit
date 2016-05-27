using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Resources;

//using Wpf;
using WCmd.Model;

[assembly: AssemblyAssociatedContentFile("App.xaml")]

namespace WCmd
{
    public class AppSA : App
    {
        static AppSA() { }
        public AppSA() { }

        public static string AsmName { get { return "/FConsole"; } }

        [STAThread]
        public static void Main()
        {
            var app = new AppSA();

            // InitializeComponent
            var names = System.Reflection.Assembly.GetAssembly(typeof(AppSA)).GetManifestResourceNames();
            var info = Assembly.GetExecutingAssembly().GetManifestResourceStream("WCmd.App.xaml");
            ResourceDictionary appResourceDictionary = (ResourceDictionary)new XamlReader().LoadAsync(info);
            Application.Current.Resources.MergedDictionaries.Add(appResourceDictionary);

            var w = new MainWindow();
            app.MainWindow = w;

            w.Show();
            
            app.Run();
        }
    }
}
