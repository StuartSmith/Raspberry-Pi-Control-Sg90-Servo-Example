using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

using static System.Math;
using static System.Convert;

namespace ControlSg90Example
{

    enum RotateServer {
        RotateToLeft = 0,
        RotateToMiddle = 1,
        RotateToRight = 2,
    }

    /// <summary>
    /// controls an SG90 Motor 
    ///
    /// When Creating this class always Create as a static object...
    /// There should only be one instance of this class for 
    /// each GPIO pin it represents
    /// 
    /// Position "0"   (1.5 ms pulse) is middle,
    ///          "90"  (~2 ms pulse) is all the way to the right
    ///          "-90" (~1 ms pulse) is all the way to the left
    // </summary>
    class SG90MotorController
    {
        private  GpioController _gpioController;
        private  GpioPin _motorPin = null;
        private ulong _ticksPerMilliSecond = (ulong)(Stopwatch.Frequency) / 1000; //Number of ticks per millisecond this is different for different processor
            
          
        
        public RaspberryPiGPI0Pin RaspberryGPIOpin { get; }

        public  bool GpioInitialized
        {
            get;
            private set;
        }          

        #region Constructors


        /// <summary>
        /// Create a Motor contoller that is connected to 
        /// a sepcified GPIO Pin
        /// </summary>
        /// <param name="gpioPin"></param>
        public SG90MotorController(RaspberryPiGPI0Pin gpioPin)
        {
            RaspberryGPIOpin = gpioPin;
            GpioInit();
        }


        /// <summary>
        /// Create a Motor contoller that is connected to 
        /// GPIO Pin 2
        /// </summary>
        public SG90MotorController()
        {
            RaspberryGPIOpin = RaspberryPiGPI0Pin.GPIO05;
            GpioInit();
        }
        #endregion

        /// <summary>
        /// Initialize the GPIO pin
        /// </summary>
        private void GpioInit()
        {
            try
            {
                GpioInitialized = false;

                _gpioController = GpioController.GetDefault();

                _motorPin =  _gpioController.OpenPin(Convert.ToInt32(RaspberryGPIOpin));
                _motorPin.SetDriveMode(GpioPinDriveMode.Output);

                GpioInitialized = true;

              
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: GpioInit failed - " + ex.ToString());
            }
        }

        /// <summary>
        /// Sends a pulse to the server motor that will 
        /// turn it in one direction or another
        /// </summary>
        /// <param name="rotateServer">Enumeration for rotating the server</param>
        public void PulseMotor(RotateServer rotateServer)
        {
            PulseMotor(ServoPulseTime(rotateServer));      
        }

        /// <summary>
        /// if you need to wait less than a single millisecond 
        /// then use this function. It is not the best since
        /// it does not delay the execution but causes a while
        /// loop that stops the current executing script
        /// </summary>
        /// <param name="millisecondsToWait"></param>
        private void LessThanamillisecondToWait(double millisecondsToWait)
        {
            var sw = new Stopwatch();
            double durationTicks = _ticksPerMilliSecond * millisecondsToWait;
            sw.Start(); 
            while (sw.ElapsedTicks < durationTicks)
            {
                int x = 3;
            }
        }

        /// <summary>
        /// Sends a pulse to the server motor that will 
        /// turn it in one direction or another or towards the center
        /// </summary>
        /// <param name="motorPulse">number of milliseconds to wait to pulse the servo</param>
        public void PulseMotor(double motorPulse)
        {
                //Total amount of time for a pulse
                double TotalPulseTime;
                double timeToWait;

                TotalPulseTime = 25;
                timeToWait = TotalPulseTime - motorPulse;

                //Send the pulse to move the servo
                _motorPin.Write(GpioPinValue.High);
                LessThanamillisecondToWait(motorPulse);
                _motorPin.Write(GpioPinValue.Low);
                LessThanamillisecondToWait(timeToWait);
                //Task.Run(async delegate { await Task.Delay(Convert.ToInt32(timeToWait)); }).Wait();

                _motorPin.Write(GpioPinValue.Low);            
            
        }

        /// <summary>
        /// 
        /// Retrieves the number of milliconds to send as a pulse to turn the motor
        /// to the left right or middle
        /// 
        /// Position "0"   (1.5 ms pulse) is middle,
        ///  Position "90"  (~2 ms pulse) is all the way to the right
        ///  Position"-90" (~1 ms pulse) is all the way to the left
        /// </summary>
        /// <returns>
        /// The number of milliseconds to send as a pulse to the servo to move the motor
        /// </returns>
        private double ServoPulseTime(RotateServer rotateServer)
        {
            switch (rotateServer)
            {
                case RotateServer.RotateToLeft:
                    return 2;
                case RotateServer.RotateToMiddle:
                    return 1.2;                  
                case RotateServer.RotateToRight:
                    return .4;
                                        
            }
            return -1;
        }
    }
}
