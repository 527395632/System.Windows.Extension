using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Extension.Data;
using System.Windows.Input;

namespace System.Windows.Extension.Controls
{
    public static class TreeViewItemElement
    {
        #region CheckChanged
        public static ICommand GetCheckChanged(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CheckChangedProperty);
        }

        public static void SetCheckChanged(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CheckChangedProperty, value);
        }

        public static readonly DependencyProperty CheckChangedProperty =
            DependencyProperty.RegisterAttached("CheckChanged", typeof(ICommand), typeof(TreeViewItemElement), new PropertyMetadata(null));
        #endregion




        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(TreeViewItemElement), new PropertyMetadata(null));



        #region Status
        public static Nullable<CheckBoxState> GetStatus(DependencyObject obj)
        {
            return (Nullable<CheckBoxState>)obj.GetValue(StatusProperty);
        }

        public static void SetStatus(DependencyObject obj, Nullable<CheckBoxState> value)
        {
            obj.SetValue(StatusProperty, value);
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.RegisterAttached("Status", typeof(Nullable<CheckBoxState>), typeof(TreeViewItemElement), new PropertyMetadata(null, (s, e) =>
            {
                if (s is TreeViewItem item && e.NewValue is CheckBoxState state)
                {
                    if (item.Parent is TreeViewItem parent)
                    {
                        var pNowState = parent.GetValue(StatusProperty);
                        var pNewState = CheckBoxState.Unchecked;

                        var temp = GetChildrenState(parent);
                        if (!temp.Any(q => q.Equals(CheckBoxState.Unchecked) || q.Equals(CheckBoxState.Partial)))
                        {
                            pNewState = CheckBoxState.Checked;
                        }
                        else if (!temp.Any(q => q.Equals(CheckBoxState.Checked) || q.Equals(CheckBoxState.Partial)))
                        {
                            pNewState = CheckBoxState.Unchecked;
                        }
                        else
                        {
                            pNewState = CheckBoxState.Partial;
                        }

                        if (!(pNowState is CheckBoxState oldState) || oldState != pNewState)
                        {
                            parent.SetValue(StatusProperty, pNewState);
                        }
                    }
                    if (state != CheckBoxState.Partial)
                    {
                        foreach (var i in item.Items)
                        {
                            if (i is TreeViewItem children &&
                            children.GetValue(StatusProperty) is CheckBoxState oldStatus &&
                            state != oldStatus)
                            {
                                children.SetValue(StatusProperty, state);
                            }
                        }
                    }

                    var command = item.GetValue(CheckChangedProperty);
                    if (command != null && command is ICommand cmd)
                    {
                        cmd.Execute(item.GetValue(CommandParameterProperty)?? item);
                    }
                }
            }));

        private static List<CheckBoxState> GetChildrenState(TreeViewItem source)
        {
            var list = new List<CheckBoxState>();
            foreach (var item in source.Items)
            {
                if (item is TreeViewItem children)
                {
                    if (children.GetValue(StatusProperty) is CheckBoxState state)
                        list.Add(state);
                    if (children.Items.Count > 0)
                        list.AddRange(GetChildrenState(children));
                }
            }
            return list;
        }
        #endregion
    }
}