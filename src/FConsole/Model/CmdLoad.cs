//using Wpf.Dialogs;
using System;
using System.IO;
using System.Windows.Controls;

namespace WCmd.Model
{
    class CmdLoad
    {

        //public DataActGrid Table {[DebuggerStepThrough] get { return this.grd1; } }

        public void Bind(IPageWindow window, Button cmdSelect, TextBox FileLoad) // , DataTabItem page, TranslateModel model)
        {
            // this.FileLoad.IsReadOnly = false;

            cmdSelect.Click += (s, e) =>
            {
                var file = FileLoad.Text;
                if (Directory.Exists(file))
                    System.IO.Directory.SetCurrentDirectory(file);

                //SelectFile.Instance.Dialog(SelectFile.Xml, (f)
                //    => Load(f));
            };

        }

        static Action<string> Load(string f)
        {
            return null;
        }

    }
}
