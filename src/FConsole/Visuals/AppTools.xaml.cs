using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WCmd.Model;

namespace WCmd
{
    //public enum FormAction
    //{
    //    List = 1,
    //    New = 2,
    //    Edit = 3,
    //    Delete = 4
    //}

    /// <summary>
    /// Interaction logic for AppTools.xaml
    /// </summary>
    public partial class AppTools : UserControl // , IAppTools, IPanel
    {
        #region Dependency

        //public static readonly DependencyProperty ActionProperty =
        //    Dependancy.RegisterDependency<FormAction?>("Action", typeof(AppTools), defaultValue: (object)FormAction.List);

        //public FormAction Action
        //{
        //    get { return Dependancy.GetProperty<FormAction?>(this, ActionProperty) ?? FormAction.List; }
        //    set
        //    {
        //        this.SetValue(ActionProperty, value);
        //        if (IsLoaded)
        //            UpdateStatus();
        //    }
        //}

        //public static readonly DependencyProperty ButtonsVisibleProperty =
        //            Dependancy.RegisterDependency<string>("ButtonsVisible", typeof(AppTools), defaultValue: (object)string.Empty);

        //public string ButtonsVisible
        //{
        //    get { return this.GetValue(ButtonsVisibleProperty) as string; }
        //    set
        //    {
        //        this.SetValue(ButtonsVisibleProperty, value);
        //        // ButtonsVisibleSet(value);
        //    }
        //}

        //public static readonly DependencyProperty ButtonsEnabledProperty =
        //            Dependancy.RegisterDependency<string>("ButtonsEnabled", typeof(AppTools), defaultValue: (object)string.Empty);
        //public string ButtonsEnabled { get; set; }

        //FormAction IAppTools.Action { get { return this.Action; } set { this.Action = value; } }
        //ComboBox IAppTools.comboCmd { get { return this.comboCmd; } }
        //Button IAppTools.cmdEdit { get { return this.cmdEdit; } }
        //Button IAppTools.cmdSave { get { return this.cmdSave; } }
        //Button IAppTools.cmdUndo { get { return this.cmdUndo; } }

        //Button IAppTools.cmdRefresh { get { return this.cmdRefresh; } }
        //Button IAppTools.cmdPaste { get { return this.cmdPaste; } }

        #endregion

        public bool Changed { get; set; }
        //IEnumerable<object> IPanel.LogicalChildren { get { return PanelChildren.NumerateLogicalChildren(this, LogicalChildren); } }

        //public AppTools()
        //{
        //    Action = FormAction.List;
        //    InitializeComponent();
        //    this.Loaded += AppTools_Loaded;
        //}

        //private void AppTools_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.Loaded -= AppTools_Loaded;
        //    UpdateStatus();
        //}

        protected virtual void ButtonsVisibleSet(string value)
        {
            if (value.Length == 0) return;
            this.cmdEdit.Visibility = value.Substring(0, 1) == "1" ? Visibility.Visible : Visibility.Collapsed;
            this.cmdSave.Visibility = value.Substring(1, 1) == "1" ? Visibility.Visible : Visibility.Collapsed;
            this.cmdUndo.Visibility = value.Substring(2, 1) == "1" ? Visibility.Visible : Visibility.Collapsed;
            this.cmdRefresh.Visibility = value.Substring(3, 1) == "1" ? Visibility.Visible : Visibility.Collapsed;
            this.cmdPaste.Visibility = value.Substring(4, 1) == "1" ? Visibility.Visible : Visibility.Collapsed;

            this.cmdIntoExcel.Visibility = value.Substring(5, 1) == "1" ? Visibility.Visible : Visibility.Collapsed;
            this.filterPanel.Visibility = value.Substring(6, 1) == "1" ? Visibility.Visible : Visibility.Collapsed;
        }

        public virtual void UpdateStatus()
        {
            var toolbar = this;
            //var buttonsVisible = ButtonsVisible;
            //if (buttonsVisible.Length > 0)
            //    ButtonsVisibleSet(buttonsVisible);

            //toolbar.cmdEdit.IsEnabled = this.Action == FormAction.List;
            //toolbar.cmdSave.IsEnabled = this.Action == FormAction.List && this.Changed;
            //toolbar.cmdRefresh.IsEnabled = this.Action == FormAction.List && !toolbar.cmdUndo.IsVisible;
            toolbar.cmdUndo.IsEnabled = false;
            toolbar.cmdIntoExcel.IsEnabled = false;
        }

    }
}
