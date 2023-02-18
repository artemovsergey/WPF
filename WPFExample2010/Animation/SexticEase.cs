using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows;

namespace Animation
{
    class SexticEase : EasingFunctionBase
    {

        protected override double EaseInCore(double normalizedTime)
        {
            return normalizedTime * normalizedTime * normalizedTime
                   * normalizedTime * normalizedTime * normalizedTime;
        }


        protected override Freezable CreateInstanceCore()
        {
            return new SexticEase();
        }
    }
}
