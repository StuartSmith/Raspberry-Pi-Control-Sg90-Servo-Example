using ControlSg90Example.ViewModels.BaseViewModel;
using Windows.UI.Xaml;

namespace ControlSg90Example.ViewModels
{
    public class VM_ServoControllerMain: ViewModelBase
    {
        private const int NumberOfTimesToSendMotorPulse = 25;
        private SG90MotorController sgMotorController;  

        public VM_ServoControllerMain()
        {
            sgMotorController = new SG90MotorController();
        }

        /// <summary>
        /// Rotate the Servo Motor to 180 degrees or all the way to the Right.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void RotateToOneEighty(object sender, RoutedEventArgs args)
        {
            for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++)
                 sgMotorController.PulseMotor(RotateServer.RotateToRight);

        }

        /// <summary>
        /// Rotate the Servo Motor to 90 degrees. servo motor controller figures out
        /// if it should rotate from the left or right. 
        /// </summary>
        public void RotateTo90FromTheLeft(object sender, RoutedEventArgs args)
        {
            for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++) 
                sgMotorController.PulseMotor(RotateServer.RotateToMiddle);
        }

        /// <summary>
        /// Rotate the Servo Motor to 90 degrees. servo motor controller figures out
        /// if it should rotate from the left or right. 
        /// </summary>
        public void RotateTo90FromTheRight(object sender, RoutedEventArgs args)
        {
            for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++) 
            sgMotorController.PulseMotor(RotateServer.RotateToMiddle);
        }

        /// <summary>
        /// Rotate the Servo Motor to 90 degrees. servo motor controller figures out
        /// if it should rotate from the left or right. 
        /// </summary>
        public void RotateTozero(object sender, RoutedEventArgs args)
        {
            for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++) 
                sgMotorController.PulseMotor(RotateServer.RotateToLeft);
        }

        /// <summary>
        ///When the keyboard arrow keys are pressed rotat the server to the
        ///Right, Left Or Middle
        /// </summary>
        /// 
        public void KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var args = new RoutedEventArgs();

            switch (e.Key)
            {
                case (Windows.System.VirtualKey.Up):
                    RotateTo90FromTheRight(this, args);
                    break;
                case (Windows.System.VirtualKey.Right):
                    RotateTozero(this, args);
                    break;
                case (Windows.System.VirtualKey.Left):
                    RotateToOneEighty(this, args);
                    
                    break;
            }

        }



    }
}
