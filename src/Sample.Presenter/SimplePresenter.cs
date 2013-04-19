using Sample.UI.Model.Implementation;
using Sample.UI.Model.Interfaces;

namespace Sample.Presenter
{
    public class SimplePresenter : ISimplePresenter
    {
        private int _labelCount;
        private int _textCount;

        public ISimpleView View { get; set; }
        private ISimpleModel Model { get; set; }
        
        public void Init()
        {

            View.Output("SimplePresenter.Init");
            View.Presenter = this;
            View.InitView();
            Model = new SimpleModel
                        {
                            LabelText =
                                string.Format("Label text defaulted from presenter, current value: {0}", _labelCount),
                            EditText =
                                string.Format("Edit text defaulted from presenter, current value: {0}", _textCount),
                        };
            View.BindModel(Model);
        }
        
        public void UpdateLabelCount()
        {
            Model.LabelText = string.Format("Label button clicked {0} times", ++_labelCount);
        }

        public void UpdateTextCount()
        {
            Model.EditText = string.Format("TextEditor button clicked {0} times", ++_textCount);
        }
    }
}
