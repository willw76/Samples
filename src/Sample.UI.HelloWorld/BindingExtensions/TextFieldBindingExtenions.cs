using System;
using System.ComponentModel;
using MonoTouch.UIKit;

namespace Sample.UI.HelloWorld.BindingExtensions
{
    public static class TextFieldBindingExtenions
    {
        public static void BindData<T>(this UITextField textField, T dataObject, string property)
            where T : INotifyPropertyChanged
        {
            try
            {
                var propertyInfo = typeof(T).GetProperty(property);
                dataObject.PropertyChanged += (sender, args) =>
                    {
                        string oldTextValue = textField.Text;
                        string newTextValue = Convert.ToString(propertyInfo.GetValue(dataObject, null));

                        Console.Out.WriteLine("Old Text Field: {0}, New Text Field: {1}", oldTextValue, newTextValue);

                        textField.Text = newTextValue;
                        Console.Out.WriteLine("TextField text updated. New Text: {0}", textField.Text);

                    };
                
                
                textField.EditingDidEnd += (sender, args) =>
                    {
                        string oldDataValue = Convert.ToString(propertyInfo.GetValue(dataObject, null));
                        string newDataValue = textField.Text;

                        Console.Out.WriteLine("Old Data Value For Text Field: {0}, New Data Value For Text Field: {1}", oldDataValue, newDataValue);

                        propertyInfo.SetValue(dataObject, newDataValue, null);
                    
                        Console.Out.WriteLine("New value updated on data object: {0}", Convert.ToString(propertyInfo.GetValue(dataObject, null)));

                    };
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
        }
    }
}