using System;
using System.Windows;
using WCmd;
using System.Windows.Controls;

namespace WCmd.Model
{
    public interface IToolControl
    {
        // IAppTools ToolBar { get; }

        FrameworkElement Panel { get; }
        // DataActGrid 
        DataGrid Table { get; }
    }
}
