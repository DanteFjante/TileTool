using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TileMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

            DoubleAnimation slideInAnimation = new DoubleAnimation(0.1, 1, new Duration(TimeSpan.FromMilliseconds(350)));
            Storyboard.SetTargetName(slideInAnimation, SideBar.Name);
            Storyboard.SetTargetProperty(slideInAnimation, new PropertyPath(ColumnDefinition.WidthProperty));


            DoubleAnimation slideOutAnimation = new DoubleAnimation(1, .1, new Duration(TimeSpan.FromMilliseconds(350)));
            Storyboard.SetTargetName(slideOutAnimation, SideBar.Name);
            Storyboard.SetTargetProperty(slideOutAnimation, new PropertyPath(ColumnDefinition.WidthProperty));

            slideAnimator.Children.Add(slideOutAnimation);

            slideAnimator.Completed += (sender, args) =>
            {
                if (slideAnimator.Children.Contains(slideOutAnimation))
                {

                    slideAnimator.Children.Remove(slideOutAnimation);
                    slideAnimator.Children.Add(slideInAnimation);
                }
                else
                {
                    slideAnimator.Children.Remove(slideInAnimation);
                    slideAnimator.Children.Add(slideOutAnimation);
                }

            };

        }



    }
}
