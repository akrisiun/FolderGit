using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading; 

namespace WCmd.Console
{
    // Set Output

    class ConsoleSetOut : TextWriter, IDisposable
    {
        TextBox textBox = null;
        public string Directory { get; set; }

        public ConsoleSetOut(TextBox output)
        {
            textBox = output;
            System.Console.SetOut(this);
        }

        public new void Dispose()
        {
            base.Dispose();
            try {
                System.Console.SetOut(null);
            } catch { }
        }

        public override void Write(string value)
        {
            base.Write(value);
        }

        public override void WriteLine(string value)
        {
            base.Write(value);

            //if (Directory != null && value.StartsWith("Merge:"))
            //    value = "Merge " + Directory + value.Substring(7) + ".xml";

            textBox.Dispatcher.BeginInvoke(DispatcherPriority.Render,
                new Action(() =>
                    textBox.AppendText(value + Environment.NewLine)
            ));
        }

        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
