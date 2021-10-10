using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace TileMaker.Code
{
    public class GridLengthAnimation : AnimationTimeline
    {
        private static DependencyProperty FromProperty;
        private static DependencyProperty ToProperty;
        private static DependencyProperty ByProperty;
        private static DependencyProperty EasingFunctionProperty;
        private static DependencyProperty DurationProperty;

        public GridLength From
        {
            get => (GridLength)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }
        public GridLength To
        {
            get => (GridLength) GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }
        public GridLength By
        {
            get => (GridLength) GetValue(ByProperty);
            set => SetValue(ByProperty, value);
        }
        public IEasingFunction EasingFunction
        {
            get => (IEasingFunction) GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public Duration Duration
        {
            get => (Duration) GetValue(EasingFunctionProperty);
            set => SetValue(DurationProperty, value);
        }

        static GridLengthAnimation()
        {
            Type typeofProp = typeof(GridLength?);
            Type typeofThis = typeof(GridLengthAnimation);

            FromProperty = DependencyProperty.Register(
                "From",
                typeofProp,
                typeofThis);

            ToProperty = DependencyProperty.Register(
                "To",
                typeofProp,
                typeofThis);

            ByProperty = DependencyProperty.Register(
                "By",
                typeofProp,
                typeofThis);

            EasingFunctionProperty = DependencyProperty.Register(
                "EasingFunction",
                typeof(IEasingFunction),
                typeofThis);
            DurationProperty = DependencyProperty.Register("Duration",
                typeof(Duration),
                typeofThis);
        }
        public GridLengthAnimation() : base()
        {
            
        }

        public GridLengthAnimation(GridLength from, GridLength to, Duration duration)
        {
            From = from;
            To = to;
            Duration = duration;
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {

            if (defaultOriginValue is not GridLength || defaultDestinationValue is not GridLength ||
                !animationClock.CurrentProgress.HasValue)
                return base.GetCurrentValue(defaultOriginValue, defaultDestinationValue, animationClock);

            GridLength fromValue = (GridLength) defaultOriginValue;
            GridLength toValue = (GridLength) defaultDestinationValue;
            double progress = animationClock.CurrentProgress.Value;
            
            if(fromValue.GridUnitType != toValue.GridUnitType)
            {
                return progress < 0.5 ? fromValue : toValue;
            }

            //lerp
            double delta = toValue.Value - fromValue.Value;
            return new GridLength(fromValue.Value + delta * progress, fromValue.GridUnitType);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        public override Type TargetPropertyType { get => typeof(GridLength); }
    }
}