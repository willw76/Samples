namespace Sample.Framework.DI.Test.TestInterfaces.Impl
{
    public class TestViewA : ITestViewA
    {
        public ITestPresenterA Presenter { get; set; }
        public bool InitCalled { get; set; }
        public void Init()
        {
            InitCalled = true;
        }
    }
}