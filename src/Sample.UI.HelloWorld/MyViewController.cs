using System;
using MonoTouch.UIKit;
using System.Drawing;
using Sample.UI.HelloWorld.BindingExtensions;
using Sample.UI.Model.Implementation;
using Sample.UI.Model.Interfaces;

namespace Sample.UI.HelloWorld
{
    public class MyViewController : UIViewController
    {
        private UIButton _buttonSetLabel;
        private UIButton _buttonSetText;
        private UILabel _label;
        private UITextField _textField;
        int numClicks = 0;
        float buttonWidth = 200;
        float buttonHeight = 50;
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.Frame = UIScreen.MainScreen.Bounds;
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            _buttonSetLabel = UIButton.FromType(UIButtonType.RoundedRect);
            _label = new UILabel();

            _buttonSetText = UIButton.FromType(UIButtonType.RoundedRect);
            _textField = new UITextField();

            _buttonSetLabel.Frame = new RectangleF(
                View.Frame.Width / 2 - buttonWidth / 2,
                10,
                buttonWidth,
                buttonHeight);
            _label.Frame = new RectangleF(
                View.Frame.Width / 2 - buttonWidth / 2,
                _buttonSetLabel.Frame.Y + _buttonSetLabel.Frame.Height,
                buttonWidth,
                buttonHeight);

            _buttonSetText.Frame = new RectangleF(
                View.Frame.Width / 2 - buttonWidth / 2,
                _label.Frame.Y + _label.Frame.Height,
                buttonWidth,
                buttonHeight);
            _textField.Frame = new RectangleF(
                View.Frame.Width / 2 - buttonWidth / 2,
                _buttonSetText.Frame.Y + _buttonSetText.Frame.Height,
                buttonWidth,
                buttonHeight);
            _textField.BorderStyle = UITextBorderStyle.RoundedRect;

            _buttonSetLabel.SetTitle("Set Label", UIControlState.Normal);
            _buttonSetText.SetTitle("Set Text", UIControlState.Normal);

            ISimpleModel model = new SimpleModel();
            _label.BindData(model, "LabelText");
            _textField.BindData(model, "EditText");

            _buttonSetLabel.TouchUpInside += (object sender, EventArgs e) =>
            {
                Console.Out.WriteLine("SetLabelClick 1: Current Data Object Value: {0}", model.LabelText);
                string text = string.Format("Label: Clicked {0} times.", numClicks++);
                model.LabelText = text;
                Console.Out.WriteLine("SetLabelClick : Current Data Object Value: {0}", model.LabelText);
            };
            _buttonSetText.TouchUpInside += (object sender, EventArgs e) =>
            {
                Console.Out.WriteLine("SetTextClick 1: Current Data Object Value: {0}", model.EditText);
                string text = string.Format("Label: Clicked {0} times.", numClicks++);
                model.EditText = text;
                Console.Out.WriteLine("SetTextClick 1: Current Data Object Value: {0}", model.EditText);
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

    }
}

