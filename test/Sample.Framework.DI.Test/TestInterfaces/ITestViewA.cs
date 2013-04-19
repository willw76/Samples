namespace Sample.Framework.DI.Test.TestInterfaces
{
    public interface ITestViewA
    {
        ITestPresenterA Presenter { get; set; }
        bool InitCalled { get; set; }

        void Init();
    }
}
