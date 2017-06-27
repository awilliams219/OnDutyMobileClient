// Copyright (C)2017 RescueCoder - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Adam Williams <adam.williams@rescuecoder.com>, 6/2017
//
using System;
using UIKit;

namespace OnDuty.iOS.UIFactory
{
    public class UiElementFactoryHelper
    {
        public UiElementFactoryHelper()
        {
        }

        public static UIActivityIndicatorView CreateLoader() {
            UIActivityIndicatorView loader = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            loader.Frame = new CoreGraphics.CGRect(0, 0, 40, 40);
            loader.HidesWhenStopped = true;
            return loader;
        }

    }
}
