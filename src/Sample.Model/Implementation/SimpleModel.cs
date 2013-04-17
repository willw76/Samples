using System.ComponentModel;
using Sample.UI.Model.Interfaces;

namespace Sample.UI.Model.Implementation
{
    public class SimpleModel : ISimpleModel
    {
        private string _labelText;
        private string _editText;
        public event PropertyChangedEventHandler PropertyChanged;

        public string LabelText
        {
            get { return _labelText; }
            set
            {
                if (value == _labelText) return;
                _labelText = value;
                OnPropertyChanged("LabelText");
            }
        }

        public string EditText
        {
            get { return _editText; }
            set
            {
                if (value == _editText) return;
                _editText = value;
                OnPropertyChanged("EditText");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
