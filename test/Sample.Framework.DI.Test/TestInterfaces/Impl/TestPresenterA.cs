namespace Sample.Framework.DI.Test.TestInterfaces.Impl
{
    public class TestPresenterA : ITestPresenterA
    {
        public ITestViewA View { get; set; }
        public bool InitCalled { get; set; }
        public void Init()
        {
            InitCalled = true;
        }
    }
}
