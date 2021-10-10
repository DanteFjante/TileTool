using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TileMaker.Code;

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

            DoubleAnimation slideInAnimation = new DoubleAnimation(20, 150, new Duration(TimeSpan.FromSeconds(0.35)));
            Storyboard.SetTargetName(slideInAnimation, SideBar.Name);
            Storyboard.SetTargetProperty(slideInAnimation, new PropertyPath(StackPanel.WidthProperty));


            DoubleAnimation slideOutAnimation = new DoubleAnimation(150, 20, new Duration(TimeSpan.FromSeconds(0.35)));
            Storyboard.SetTargetName(slideOutAnimation, SideBar.Name);
            Storyboard.SetTargetProperty(slideOutAnimation, new PropertyPath(StackPanel.WidthProperty));

            slideAnimator.Children.Add(slideInAnimation);

            slideAnimator.Completed += (sender, args) =>
            {
                if (slideAnimator.Children.Contains(slideOutAnimation))
                {
                    SideBtn.Content = ">";
                    slideAnimator.Children.Remove(slideOutAnimation);
                    slideAnimator.Children.Add(slideInAnimation);
                }
                else
                {
                    SideBtn.Content = "<";
                    slideAnimator.Children.Remove(slideInAnimation);
                    slideAnimator.Children.Add(slideOutAnimation);
                }

            };

        }



    }
}
