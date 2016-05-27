using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WCmd;
using WCmd.Controller;
using WCmd.Model;
using WCmd.Model.DirScan;

namespace WCmd
{
    public interface IPageModel
    { 
    }

    public interface IPageWindowBase : IEnumerable // : IControl, IPagesContainer
    {
        List<IPageModel> Pages { get; }
        IPageModel PageActive { get; }
    }

    public interface IPageWindow : IPageWindowBase
    {
        TabControl Frame { get; }

        // XmlTranslate 
        ConsoleModel modelConsole { get; }
        //TabItem pageConsole { get; }
    }


    //Wpf.DataAct.Data
    public abstract class MainWindowBase : Window, IPageWindowBase, IDisposable
    {
        // public abstract Segment Db { get; protected set; }
        public static IPageWindow ActiveWindow { get; set; } 
        public abstract void Connect(); 

        public virtual void Dispose() { PagesReset(); }

        #region Pages

        public virtual void PagesBind()
        {
            oldPageActive = null;
            Frame.SelectionChanged += Frame_SelectionChanged;
        }

        public virtual void PagesReset()
        {
            Frame.SelectionChanged -= Frame_SelectionChanged;
            //foreach (IPageModel page in Pages)
            //    page.Dispose();
        }

        void Frame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabFrame = Frame;
            IPageModel page = PageActive;
            if (tabFrame.SelectedIndex < 0 || page == null)
                return;

            if (oldPageActive != null && page != oldPageActive)
            {
                //oldPageActive.Deactivate();
                //oldPageActive = null;
            }

            //page.Activate();
            oldPageActive = page;
        }

        // UI:

        public static readonly int idxTrans = 0;

        public virtual List<IPageModel> Pages { get; protected set; }

        public IEnumerator<IPageModel> GetEnumerator() { return Pages.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        protected ConsoleModel _modelConsole;
        public abstract TabControl Frame { get; }

        private IPageModel oldPageActive;
        public virtual IPageModel PageActive
        {
            get
            {
                var frame = Frame;
                return frame.SelectedIndex <= 0 ? null
                    : Pages[frame.SelectedIndex - 1];
            }
        }

        #endregion

        public abstract ConsoleModel modelConsole { get; }
        // + DataTabItem pageTranslate

    }
}
