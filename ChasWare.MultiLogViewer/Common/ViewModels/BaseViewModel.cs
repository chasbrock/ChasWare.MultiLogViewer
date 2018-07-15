using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ChasWare.MultiLogViewer.Properties;

namespace ChasWare.MultiLogViewer.Common.ViewModels
{
    namespace ChasWare.Utils.ViewModels
    {
        public class BaseViewModel
        {
            #region Constants and fields 

            private Cursor _cursor;

            #endregion

            #region public events, delegates and enums

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            #region public properties

            public Cursor Cursor
            {
                get => _cursor;
                set
                {
                    if (_cursor != value)
                    {
                        _cursor = value;
                        FirePropertyChanged();
                    }
                }
            }

            #endregion

            #region public methods

            public bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
            {
                if (propertyName == null)
                {
                    return false;
                }

                if (!EqualityComparer<T>.Default.Equals(field, value))
                {
                    field = value;

                    NotifyChanges(propertyName);
                    return true;
                }

                return false;
            }

            #endregion

            #region other methods

            /// <summary>
            ///     use with commands when you want to control editibity
            /// </summary>
            /// <returns>
            ///     true if is editable <see cref="bool" />.
            /// </returns>
            protected bool CanExecuteEnabled()
            {
                return true;
            }

            /// <summary>
            ///     method to call to trigger notification event
            /// </summary>
            /// <param name="propertyName">
            ///     either the name of a property, or null which will default to
            ///     calling member name (ie setting property)
            /// </param>
            [NotifyPropertyChangedInvocator]
            protected void FirePropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            protected void NotifyChanges(string propertyName)
            {
                FirePropertyChanged(propertyName);
            }

            /// <summary>
            ///     Sets value of field, and if it has changed a notify event will be fired
            /// </summary>
            /// <typeparam name="T">type of data being changed</typeparam>
            /// <param name="dataObject">the member being set</param>
            /// <param name="value">the value to be set</param>
            /// <param name="propertyName">name of property being set</param>
            /// <returns></returns>
            protected bool SetField<T>(object dataObject, T value, [CallerMemberName] string propertyName = null)
            {
                if (propertyName == null)
                {
                    return false;
                }

                PropertyInfo property = dataObject.GetType().GetProperty(propertyName);
                if (property == null)
                {
                    return false;
                }

                var current = (T) property.GetValue(dataObject);
                if (!EqualityComparer<T>.Default.Equals(current, value))
                {
                    property.SetValue(dataObject, value);

                    NotifyChanges(propertyName);
                    return true;
                }

                return false;
            }

            #endregion

            #region nested classes

            /// <summary>
            ///     class that can be used to push and pop cursor during long operations
            /// </summary>
            protected sealed class WaitCursor : IDisposable
            {
                #region Constants and fields 

                private readonly BaseViewModel _model;
                private readonly Cursor _oldCursor;

                #endregion

                #region Constructors

                /// <summary>
                ///     Initializes a new instance of the <see cref="WaitCursor" /> class.
                /// </summary>
                /// <param name="model">
                ///     The model.
                /// </param>
                public WaitCursor(BaseViewModel model)
                {
                    _model = model;
                    _oldCursor = _model.Cursor;
                }

                #endregion

                #region public methods

                /// <inheritdoc />
                public void Dispose()
                {
                    _model.Cursor = _oldCursor;
                }

                #endregion
            }

            #endregion
        }
    }
}