namespace Sample.UI.Model.Interfaces
{
    public interface ISimpleView
    {
        ISimplePresenter Presenter { get; set; }

        void InitView();
        void BindModel(ISimpleModel model);

        void Output(string output);
    }
}