using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sample.UI.Model.Implementation;
using Sample.UI.Model.Interfaces;

namespace Sample.UI.HelloWorld
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        private UIWindow _window;
        private MyViewController _viewController;
        private ISimplePresenter Presenter { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            //_viewController = new MyViewController();
            //ISimplePresenter presenter = new SimplePresenter();
            InitPresenter();

            _window.RootViewController = _viewController;

            _window.MakeKeyAndVisible();

            return true;
        }

        private void InitPresenter()
        {
            Presenter = new SimplePresenter
                            {
                                View = (_viewController = new MyViewController()),
                            };
            Presenter.Init();
        }
    }
}

