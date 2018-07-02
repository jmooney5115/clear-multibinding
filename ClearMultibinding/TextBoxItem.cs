using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearMultibinding
{
    public class TextBoxItem : INotifyPropertyChanged
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
            if (pi == null && propertyName.ToUpper() != "ITEM[]")
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

        private string text1;
        public string Text1
        {
            get { return text1; }
            set
            {
                if (value == text1)
                    return;

                text1 = value;
                OnPropertyChanged(nameof(Text1));
            }
        }


        private string text2;
        public string Text2
        {
            get { return text2; }
            set
            {
                if (value == text2)
                    return;

                text2 = value;
                OnPropertyChanged(nameof(Text2));
            }
        }


        private string text3;
        public string Text3
        {
            get { return text3; }
            set
            {
                if (value == text3)
                    return;

                text3 = value;
                OnPropertyChanged(nameof(Text3));
            }
        }
        
        /*
         * https://stackoverflow.com/questions/51107133/update-multibinding-on-datagridcell
         */
        #region Update MultiBinding Code

        [field: NonSerialized()]
        private Dictionary<string, object> _memo = new Dictionary<string, object>();

        [field: NonSerialized()]
        public object this[string key]
        {
            get
            {
                object o;
                _memo.TryGetValue(key, out o);
                return o;
            }
        }

        public void UpdateState()
        {
            foreach (var prop in GetType().GetProperties())
            {
                //properties we can ignore. exception is thrown if not ignore item property
                if (prop.Name.ToUpper().Equals("ITEM")) continue;

                _memo[prop.Name] = prop.GetValue(this);
            }

            OnPropertyChanged("Item[]");
        }

        #endregion
    }
}
