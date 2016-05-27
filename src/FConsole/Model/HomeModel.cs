using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Wpf.DataAct;
using System.Windows.Controls;

namespace WCmd.Model
{
    public interface IPageModel<T>
    {
       // IEnumerable<T> GetEnumerator();
    }

    public abstract class HomeModel : IPageModel<HomeModel.Dimensions>
    {
        public abstract TabItem Page { get; }
        //public abstract IAppTools Toolbar { get; }

        public abstract void Reload();

        public class Dimensions
        {
            public string TargetTable { get { return null; } }
        }

        // protected string LastFirmID { get; set; }
        public virtual void Dispose() { }
        public virtual void Activate() { }
        public void Deactivate() { }
    }
}
