using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;

namespace capcha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Vectorinitialize();
            Loadcapcha();
            tickrate();
        }
        List<Vector2> imgcoord = new List<Vector2>();
        List<Image> imges = new List<Image>();
        Vector2 Mousepos = new Vector2();
        bool isclicked = false;
        void Vectorinitialize()
        {
            Vector2 vimg1 = new Vector2((float)Canvas.GetLeft(img1), (float)Canvas.GetTop(img1));
            Vector2 vimg2 = new Vector2((float)Canvas.GetLeft(img2), (float)Canvas.GetTop(img2));
            Vector2 vimg3 = new Vector2((float)Canvas.GetLeft(img3), (float)Canvas.GetTop(img3));
            Vector2 vimg4 = new Vector2((float)Canvas.GetLeft(img4), (float)Canvas.GetTop(img4));
            imgcoord.Add(vimg1);
            imgcoord.Add(vimg2);
            imgcoord.Add(vimg3);
            imgcoord.Add(vimg4);
            imges.Add(img1);
            imges.Add(img2);
            imges.Add(img3);
            imges.Add(img4);
        }
        async void tickrate()
        {
            while (true)
            {
                UpdateVectors();
                await Task.Delay(16);
            }
        }
        void UpdateVectors()
        {
            for (int i = 0; i < imgcoord.Count; i++)
            {
                Vector2 actualvector = imgcoord[i];
                Image actualimage = imges[i];
                actualvector.X = (float)Canvas.GetLeft(actualimage);
                actualvector.Y = (float)Canvas.GetTop(actualimage);
                imgcoord[i] = actualvector;
            }
        }
        void Checkposition()
        {
            int passdist = (int)(img1.Width*0.15);
            Vector2 Center1 = imgcoord[0];
            Center1.X = Center1.X + (float)(img1.Width / 2);
            Center1.Y = Center1.Y + (float)(img1.Height / 2);
            Vector2 Center2 = imgcoord[1];
            Center2.X = Center2.X + (float)(img1.Width / 2);
            Center2.Y = Center2.Y + (float)(img1.Height / 2);
            Vector2 Center3 = imgcoord[2];
            Center3.X = Center3.X + (float)(img1.Width / 2);
            Center3.Y = Center3.Y + (float)(img1.Height / 2);
            Vector2 Center4 = imgcoord[3];
            Center4.X = Center4.X + (float)(img1.Width / 2);
            Center4.Y = Center4.Y + (float)(img1.Height / 2);
            if (Center1.X < Center2.X && Center3.X < Center4.X)
            {
                MessageBox.Show("первый этап проверки капчи пройден, по горизонтали расположены верно");
                    if (Center1.Y < Center3.Y && Center2.Y < Center4.Y)
                {
                    MessageBox.Show("Второй этап проверки капчи пройден, по вертикали расположены верно");
                    if (Vector2.Distance(Center1, Center2) >= img1.Width - passdist && Vector2.Distance(Center1, Center2) <= img1.Width + passdist)
                    {
                        MessageBox.Show("Дистанция между первой и второй картинкой приемлемо");
                        if (Vector2.Distance(Center3, Center4) >= img1.Width - passdist && Vector2.Distance(Center3, Center4) <= img1.Width + passdist)
                        {
                            MessageBox.Show("Дистанция между третьей и четвёртой картинкой приемлемо");
                            if (Vector2.Distance(Center1, Center3) >= img1.Width - passdist && Vector2.Distance(Center1, Center3) <= img1.Width + passdist)
                            {
                                MessageBox.Show("Дистанция между первой и третьей картинкой приемлемо");
                                if (Vector2.Distance(Center2, Center4) >= img1.Width - passdist && Vector2.Distance(Center2, Center4) <= img1.Width + passdist)
                                {
                                    MessageBox.Show("Дистанция между Второй и четвёртой картинкой приемлемо");
                                }
                            }
                        }
                    }
                }
                    else
                {
                    MessageBox.Show("Второй этап капчи завален");
                }
            }
            else
            {
                MessageBox.Show("Первый этап завален");
            }
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            if (isclicked)
            {
                Canvas.SetLeft(image, e.GetPosition(maincanvas).X - Mousepos.X);
                Canvas.SetTop(image, e.GetPosition(maincanvas).Y - Mousepos.Y);
            }
        }
        public void MouseDown(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            Mousepos.X = (float)Mouse.GetPosition(image).X;
            Mousepos.Y = (float)Mouse.GetPosition(image).Y;
            isclicked = true;
            Panel.SetZIndex(image, 1);
        }
        public void MouseUp(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            Canvas.SetLeft(image, Mouse.GetPosition(maincanvas).X - Mousepos.X);
            Canvas.SetTop(image, Mouse.GetPosition(maincanvas).Y - Mousepos.Y);
            isclicked = false;
        }
        public void Loadcapcha()
        {
            Random random = new Random();
            for (int i = 0; i < imges.Count; i++)
            {
                Image img = imges[i];
                Canvas.SetLeft(img, random.Next(0, 800 - (int)img.Width));
                Canvas.SetTop(img, random.Next(0, 450 - (int)img.Height));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Checkposition();
        }
    }
}