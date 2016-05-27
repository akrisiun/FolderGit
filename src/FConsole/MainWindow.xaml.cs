using WCmd.Controller;
using WCmd.Model;

using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using MahApps.Metro.Controls;
//using Wpf.DataAct;
//using Bind;
//using Wpf.Platform;
//using Wpf.Controls;
using WCmd.Model.DirScan;
using System.Configuration;
using System.IO;

namespace WCmd
{
    //<local:MainWindowBase
    //    x:Class="WCmd.MainWindow"

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MainWindowBase, IPageWindow
    {
        //TabItem

        //public System.Windows.ResourceDictionary Resources { set; get; }


        // debugger entry point
        static MainWindow() { }
        static MainWindow _active;
        public static MainWindow Active
        {
            get { return _active; }
            protected set { _active = value; MainWindowBase.ActiveWindow = _active as IPageWindow; }
        }

        #region Frame properties

        
        public override // Data
               TabControl Frame { [DebuggerStepThrough] get { return this.frame; } }
        /*
        // Page Translate
        TabItem IPageWindow.pageConsole { get { return this.pageConsole; } }
        */

        //public DirScanModel contentTranslate { get { return panelTranslate; } }
        public override ConsoleModel modelConsole { get { return this._modelConsole; } }

        #endregion

        #region ctor, load, login

        public MainWindow()
        {
            MainWindow.Active = this;
           
            InitializeComponent();

            MainWindowBase.ActiveWindow = this as IPageWindow;

            //this.TitlebarHeight = 35;
            //WindowIconHelper.SetIcon(this, "WCmd.App.ico");
            //ShowSystemMenuOnRightClick = true;

            string dir = ConfigurationManager.AppSettings["dir"];
            if (!string.IsNullOrWhiteSpace(dir) && Directory.Exists(dir))
                Directory.SetCurrentDirectory(dir);

            this.Pages = new List<IPageModel>();
            _modelConsole = new ConsoleModel { Page = this.pageConsole, PanelContent = this.contentConsole };
            Pages.Add(_modelConsole);

            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //frame.SelectedIndex = 1;    // page #2
            if (!isConnected)
                Connect();
        }

        public // override 
            void AfterRendered() { }

        #endregion

        #region Data connect, bind

        bool isConnected = false;
        public override void Connect()
        {
            isConnected = true;

            ConsoleModel model3 = modelConsole;
            TabItem panelConsole = this.pageConsole;

            PagesBind();
            var page = PageActive;
            //page.Reload();

            model3.Bind(this as IPageWindow, this.pageConsole);

            // TODOD
            var Items = contentConsole.cmd.Items;
            Items.Add("cmd /c git fetch --all");
            contentConsole.cmd.SelectedIndex = 0;
        }

        #endregion
    }
}