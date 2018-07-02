using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClearMultibinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        /// The PropertyChanged event is used by consuming code
        /// (like WPF's binding infrastructure) to detect when
        /// a value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the PropertyChanged event for the 
        /// specified property.
        /// </summary>
        /// <param name="propertyName">
        /// A string representing the name of 
        /// the property that changed.</param>
        /// <remarks>
        /// Only raise the event if the value of the property 
        /// has changed from its previous value</remarks>
        protected void OnPropertyChanged(string propertyName)
        {
            // Validate the property name in debug builds
            VerifyProperty(propertyName);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Verifies whether the current class provides a property with a given
        /// name. This method is only invoked in debug builds, and results in
        /// a runtime exception if the <see cref="OnPropertyChanged"/> method
        /// is being invoked with an invalid property name. This may happen if
        /// a property's name was changed but not the parameter of the property's
        /// invocation of <see cref="OnPropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        [System.Diagnostics.Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            Type type = this.GetType();

            // Look for a *public* property with the specified name
            System.Reflection.PropertyInfo pi = type.GetProperty(propertyName);
            if (pi == null)
            {
                // There is no matching property - notify the developer
                string msg = "OnPropertyChanged was invoked with invalid " +
                                "property name {0}. {0} is not a public " +
                                "property of {1}.";
                msg = String.Format(msg, propertyName, type.FullName);
                System.Diagnostics.Debug.Fail(msg);
            }
        }

        #endregion


        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                if (value == items)
                    return;

                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }


        private TextBoxItem textItem;
        public TextBoxItem TextItem
        {
            get { return textItem; }
            set
            {
                if (value == textItem)
                    return;

                textItem = value;
                OnPropertyChanged(nameof(TextItem));
            }
        }





        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            /*
             * initialize objects
             */
            TextItem = new TextBoxItem()
            {
                Text1 = "Text Box 1",
                Text2 = "Text Box 2",
                Text3 = "Text Box 3"
            };
            TextItem.UpdateState();

            Items = new ObservableCollection<Item>();

            for(var i = 0; i < 100; i++)
            {
                Items.Add(new Item()
                {
                    Alias = string.Format("Item {0}", i.ToString()),
                    Description = string.Format("Description {0}", i.ToString()),
                    Name = string.Format("Name {0}", i.ToString()),
                    Value = string.Format("Value {0}", i.ToString())
                });

                Items[i].UpdateState();
            }
        }

        /// <summary>
        /// Update the bindings to clear the background color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Items)
                item.UpdateState();

            TextItem.UpdateState();
        }
    }
}
