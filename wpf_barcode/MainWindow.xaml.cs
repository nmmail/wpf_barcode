using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;
using System.Windows.Interop;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace wpf_barcode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    

        public MainWindow()
        {
            InitializeComponent();         
            ghost_k();
        }
       
        void ghost_k()
        {
            //Process.GetCurrentProcess().Kill();
            dir_info(@"D:\code\barcode\");
            dir_info(@"D:\code\qr\");
            WindowsIdentity wi = WindowsIdentity.GetCurrent();       
        }

        void dir_info(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo d in info.GetFiles())
            {
                d.Delete();
            }
        }

        private void tClose_MouseMove(object sender, MouseEventArgs e)
        {
            tClose.Background = Brushes.HotPink;
        }

        private void tClose_MouseLeave(object sender, MouseEventArgs e)
        {
            tClose.Background = Brushes.Gray;
        }

        private void tClose_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            Application.Current.Shutdown();
        }

        void mlb_t()
        {
            try
            {
                this.DragMove();
            }
            catch (Exception)
            {

            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mlb_t();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!flag)
            {
                if (!Char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }
            }
            else
            {

            }
        }

        BarcodeWriter writen = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
        int i = 0;
        void f_barcode_draw()
        {            
            writen.Write(textBox_code.Text).Save(@"D:\code\barcode\barcode_"+ i+".png");
            Uri imgUri = new Uri(@"D:\code\barcode\barcode_" + i + ".png");
            ImageSource img = new BitmapImage(imgUri);
            barcodePicture.Source = img;
            i++;
        }

        private void Button_MouseDown(object sender, RoutedEventArgs e)
        {
          
            if (textBox_code.Text.Length == 0)
            {               
                return;
            }
            f_barcode_draw();


        }

        private void Button_MouseDown_d(object sender, RoutedEventArgs e)
        {
            
            BarcodeReader reader = new BarcodeReader();
            Uri imgUri = new Uri(@"D:\code\barcode\barcode_" + (i - 1) + ".png");
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = imgUri;
            img.EndInit();
            var result = reader.Decode(img);
            textBox_decode.Text = result.ToString();
        }
       
    
        int iq = 0;
        void f_qr_draw()
        {
            Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            qrBarcode.Draw(textBox_code.Text , 200).Save(@"D:\code\qr\qr_" + iq + ".png");
            Uri imgUri = new Uri(@"D:\code\qr\qr_" + iq + ".png");
            ImageSource img = new BitmapImage(imgUri);
            barcodePicture.Source = img;
            iq++;
        }

        bool flag = false;
        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
        
            if (textBox_code.Text.Length == 0)
            {
                return;
            }
            f_qr_draw();
           
          
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flag = !flag;
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
