using System;
using System.ComponentModel;
using MonoTouch.UIKit;

namespace Sample.UI.HelloWorld.BindingExtensions
{
    public static class LabelBindingExtenions
    {
        public static void BindData<T>(this UILabel label, T dataObject, string property)
            where T : INotifyPropertyChanged
        {
            try
            {
                var propertyInfo = typeof (T).GetProperty(property);
                dataObject.PropertyChanged += (sender, args) =>
                    {
                        string oldLabelValue = label.Text;
                        string newLabelValue = Convert.ToString(propertyInfo.GetValue(dataObject, null));
                        
                        Console.Out.WriteLine("Old Label: {0}, New Label: {1}", oldLabelValue, newLabelValue);

                        label.Text = newLabelValue;
                        Console.Out.WriteLine("Label text updated. New Text: {0}", label.Text);

                    };
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
        }
    }
}
