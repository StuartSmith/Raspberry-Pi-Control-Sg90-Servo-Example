using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ControlSg90Example
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static SG90MotorController sg90;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (sg90 == null)
            {
                sg90 = new SG90MotorController();
                int a = 1;
                for (int x = 0;  x != 20; x++)
                {
                    sg90.PulseMotor(RotateServer.RotateToLeft);
                }

                int b = 1;
                for (int x = 0; x != 20; x++)
                {
                    sg90.PulseMotor(RotateServer.RotateToRight);
                }

                int c = 1;
                for (int x = 0; x != 20; x++)
                {

                    sg90.PulseMotor(RotateServer.RotateToMiddle);
                }
            }

        }
    }
}
