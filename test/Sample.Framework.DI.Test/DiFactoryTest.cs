using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sample.Framework.DI.Test.TestInterfaces;
using Sample.Framework.DI.Test.TestInterfaces.Impl;

namespace Sample.Framework.DI.Test
{
    [TestFixture]
    public class DiFactoryTest
    {

        [SetUp]
        public void SetUp()
        {
            DiFactory.RegisterContext(DiContext.Register<TestViewA>().RegisterInit(x=>x.Init()));
            DiFactory.RegisterContext(DiContext.Register<TestPresenterA>().RegisterInit(x => x.Init()).InjectProperty(x=>x.View).WithContextValue<ITestViewA>());
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void TestMethod1()
        {
            var presenter = DiFactory.GetContext<ITestPresenterA>();
            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.View, Is.Not.Null);
            Assert.That(presenter.InitCalled, Is.True);
            Assert.That(presenter.View.InitCalled, Is.True);
            
        }
    }
}
