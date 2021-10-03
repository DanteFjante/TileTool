using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using TileMaker.Code;

namespace TileMaker
{
    /// <summary>
    /// Interaction logic for MainPane.xaml
    /// </summary>
    public partial class MainPane : Page
    {

        public BitImage bg;
        public BitImage fg;

        public MainPane()
        {
            InitializeComponent();
            
            AddBG.Click += new RoutedEventHandler((sender, args) => AddImg(BGImg, bg, false));
            AddFG.Click += new RoutedEventHandler((sender, args) => AddImg(FGImg, fg, true));
            Pop.IsOpen = false;
        }

        

        public void AddImg(Image img, BitImage bitimg, bool change)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.ShowDialog();
            Stream stream = fileDialog.OpenFile();
            string path = fileDialog.FileName;

            bitimg = new BitImage(path);

            if (change)
            {
                bitimg.Change();
            }


            img.Source = bitimg.LoadBitmapImage();
            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);


        }
    }
}
