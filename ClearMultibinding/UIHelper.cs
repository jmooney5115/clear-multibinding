using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Collections.Generic;

namespace ClearMultibinding
{
    internal static class UIHelper
    {

        /// <summary>
        /// https://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type/978352#978352
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// Reset the data binding after a save to clear the blue that could be there when
        /// a change is detected.
        /// </summary>
        /// <typeparam name="T">Type to search for</typeparam>
        /// <param name="parentDepObj">Parent object we want to reset the binding for their children.</param>
        public static void UpdateDataBindings<T>(DependencyObject parentDepObj) where T : DependencyObject
        {
            if (parentDepObj != null)
            {
                MultiBindingExpression multiBindingExpression;

                foreach (var control in UIHelper.FindVisualChildren<T>(parentDepObj))
                {
                    /*
                     * i was hoping this would get the style for the data grid cells. it is slow and does not work
                     */
                     /* foreach(var child in FindVisualChildren<Control>(control))
                    {
                        ResetDataBinding<Control>(child);
                    }*/

                    multiBindingExpression = BindingOperations.GetMultiBindingExpression(control, Control.BackgroundProperty);
                    if (multiBindingExpression != null)
                        multiBindingExpression.UpdateTarget();

                    multiBindingExpression = BindingOperations.GetMultiBindingExpression(control, DataGridCell.StyleProperty);
                    if (multiBindingExpression != null)
                        multiBindingExpression.UpdateTarget();
                }
            }
        }
    }
}
