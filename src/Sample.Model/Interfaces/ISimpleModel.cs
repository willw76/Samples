using System.ComponentModel;

namespace Sample.UI.Model.Interfaces
{
    public interface ISimpleModel : INotifyPropertyChanged
    {
        string LabelText { get; set; }
        string EditText { get; set; }
    }
}