using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WCmd.Model;

namespace WCmd.Controller
{
    public static class Commands
    {
        public static void Init(this IPageWindow window)
        {
            // var usersPagesModel = window.modelUsers; // Pages.First() as MainWindow.UsersModel;
            //DataGrid grid = usersPagesModel.TableData.Table;
            //GridDataSource.PrepareControl(grid);
        }

        static void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = sender as IPageWindow;
            //window.Loaded -= Window_Loaded;
            //window.PagesBind();
        }

        //public static void FilterKey<T>(this MainWindowBase window, DataGrid grid, object model, TextBox s, KeyEventArgs e)
        //{
        //    if (e.Key == System.Windows.Input.Key.Enter)
        //    {
        //        var textList = (s as TextBox).Text;
        //        e.Handled = true;
        //    }
        //}
    }
}