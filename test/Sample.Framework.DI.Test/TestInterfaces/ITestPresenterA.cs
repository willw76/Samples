namespace Sample.Framework.DI.Test.TestInterfaces
{
    public interface ITestPresenterA
    {
        ITestViewA View { get; set; }
        bool InitCalled { get; set; }
        void Init();
    }
}