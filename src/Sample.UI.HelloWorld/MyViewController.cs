using System;
using MonoTouch.UIKit;
using System.Drawing;
using Sample.UI.HelloWorld.BindingExtensions;
using Sample.UI.Model.Interfaces;

namespace Sample.UI.HelloWorld
{
    public class MyViewController : UIViewController, ISimpleView
    {
        private UIButton _buttonSetLabel;
        private UIButton _buttonSetText;
        private UILabel _label;
        private UITextField _textField;
        //private int _numberOfClicks;
        private const float _buttonWidth = 200;
        private const float _buttonHeight = 50;
        private ISimpleModel Model { get; set; }
        public ISimplePresenter Presenter { get; set; }

        public void InitView ()
        {
            Console.Out.WriteLine("InitView()");
            _buttonSetLabel = UIButton.FromType(UIButtonType.RoundedRect);
            _label = new UILabel();

            _buttonSetText = UIButton.FromType(UIButtonType.RoundedRect);
            _textField = new UITextField();
        }
        
        public override void ViewDidLoad()
        {
            Console.Out.WriteLine("ViewDidLoad()");
            base.ViewDidLoad();

            View.Frame = UIScreen.MainScreen.Bounds;
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            //_buttonSetLabel = UIButton.FromType(UIButtonType.RoundedRect);
            //_label = new UILabel();

            //_buttonSetText = UIButton.FromType(UIButtonType.RoundedRect);
            //_textField = new UITextField();
            _buttonSetLabel.Frame = new RectangleF(
                View.Frame.Width / 2 - _buttonWidth / 2,
                10,
                _buttonWidth,
                _buttonHeight);
            _label.Frame = new RectangleF(
                View.Frame.Width / 2 - _buttonWidth / 2,
                _buttonSetLabel.Frame.Y + _buttonSetLabel.Frame.Height,
                _buttonWidth,
                _buttonHeight);

            _buttonSetText.Frame = new RectangleF(
                View.Frame.Width / 2 - _buttonWidth / 2,
                _label.Frame.Y + _label.Frame.Height,
                _buttonWidth,
                _buttonHeight);
            _textField.Frame = new RectangleF(
                View.Frame.Width / 2 - _buttonWidth / 2,
                _buttonSetText.Frame.Y + _buttonSetText.Frame.Height,
                _buttonWidth,
                _buttonHeight);
            _textField.BorderStyle = UITextBorderStyle.RoundedRect;

            _buttonSetLabel.SetTitle("Set Label", UIControlState.Normal);
            _buttonSetText.SetTitle("Set Text", UIControlState.Normal);

            //Model = new SimpleModel();
            //_label.BindData(model, "LabelText");
            //_textField.BindData(model, "EditText");

            _buttonSetLabel.TouchUpInside += (sender, e) =>
            {
                Console.Out.WriteLine("==> SetLabelClick 1: Current Data Object Value: {0}", Model.LabelText);
                //string text = string.Format("Label: Clicked {0} times.", _numberOfClicks++);
                //Model.LabelText = text;
                Presenter.UpdateLabelCount();
                Console.Out.WriteLine("====> SetLabelClick : Current Data Object Value: {0}", Model.LabelText);
            };
            _buttonSetText.TouchUpInside += (sender, e) =>
            {
                Console.Out.WriteLine("==> SetTextClick 1: Current Data Object Value: {0}", Model.EditText);
                //string text = string.Format("Label: Clicked {0} times.", _numberOfClicks++);
                //Model.EditText = text;
                Presenter.UpdateTextCount();
                Console.Out.WriteLine("====> SetTextClick 1: Current Data Object Value: {0}", Model.EditText);
            }; 

            _buttonSetLabel.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin |
                UIViewAutoresizing.FlexibleBottomMargin;
            _buttonSetText.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin |
                UIViewAutoresizing.FlexibleBottomMargin;

            View.AddSubview(_buttonSetLabel);
            View.AddSubview(_label);
            View.AddSubview(_buttonSetText);
            View.AddSubview(_textField);
        }



        public void BindModel(ISimpleModel model)
        {
            Console.Out.WriteLine("BindModel(ISimpleModel)");
            Model = model;
            _label.BindData(Model, "LabelText");
            _textField.BindData(Model, "EditText");
        }
    }
}

