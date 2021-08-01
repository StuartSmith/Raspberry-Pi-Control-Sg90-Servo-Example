# RaspberryPi-Control-SG90-Example
<p>
How to control an SG90 Servo motor from a Raspberry Pi using Windows 10 IOT core.

</p>
<p>
<img style="float:left;" src="https://raw.githubusercontent.com/StuartSmith/RaspberryPi-Control-Sg90-Example/master/Images/Sketchsg90.jpg">
An SG90 is a servo used to control different pieces of equipment. It is a favorite among hobbyist who fly drones and helicopters, because of its light weight.
</p>


An SG90 servo is controlled by a pulse sent to it over a 20 millisecond time span, different durations of pulse determine how the motor will turn. 

### Wiring Diagram:
<p>
Wiring the servo to the Raspberry Pi is rather straight forward: the ground of the servo is connected to pin 2 (Positive 5V), the positive wire from the servo is connected to pin 6 (Ground) and the pulse control can go to many of the GPIO pins but for simplicity, connect this to pin 29 (GPIO 5).
</p>

![Alt text](https://raw.githubusercontent.com/StuartSmith/RaspberryPi-Control-Sg90-Example/master/Images/GPIOWiringDiagram.png "")
<br>
<br>

![Alt text](https://raw.githubusercontent.com/StuartSmith/RaspberryPi-Control-Sg90-Example/master/Images/ServoDiagramImage.PNG "")

<br><br>

### Pulse Control Diagram:
A pulse is where a signal of high polarity is sent from a GPIO pin of the Raspberry Pi to SG90 Servo for a given amount of time. The pulse will make the motor turn in one direction or the other. The duration of time between pulses must add up to 20 milliseconds. If a pulse is one millisecond, the next pulse sent to the servo must take place no sooner than 19 milliseconds later. The amount of time the pulse is sent, determines how the servo will rotate. For example, if a pulse is sent to the servo for 1 millisecond the motor will move towards 0 degrees or to the left.

![Alt text](https://raw.githubusercontent.com/StuartSmith/RaspberryPi-Control-Sg90-Example/master/Images/PulseControl.jpg "")

### Specification for pulse control 
* A pulse of 1 millisecond will move the servo towards 0 degrees
* A pulse of 1.5 millisecond will move the servo towards 90 degrees
* A pulse of 2  millisecond will move the servo towards 180 degrees

### What was found through trial and error with the Raspberry Pi
* A pulse of .4 millisecond will move the servo towards 0 degrees
* A pulse of 1.2 millisecond will move the servo towards 90 degrees
* A pulse of 2  millisecond will move the servo towards 180 degrees
<br><br>


#### Video of SG90
Click on the image to watch the video<br>
[![SG90 in action](http://img.youtube.com/vi/v_Ni77Dx_HM/0.jpg)](http://www.youtube.com/watch?v=v_Ni77Dx_HM)

#### Source code

The SG 90 servo is controlled through one object SG90MotorController, below is the source code for this class.
<br>

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
        /// GPIO Pin 2
        /// </summary>
        public SG90MotorController()
        {
            RaspberryGPIOpin = RaspberryPiGPI0Pin.GPIO05;
            GpioInit();
        }

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
        //Function to wait so many milliseconds, this is required because a task.delay
        // time to execute is too long. This is a blocking thread but since the time
        // to wait are so small for the SG90 it may not matter
        /// </summary>
        /// <param name="millisecondsToWait">Number of milliseconds before the function returns</param>
        private void MillisecondToWait(double millisecondsToWait)
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
        /// Sends enough pulses to the server motor that will 
        /// turn it all the way in one direction or another or towards the center.
        /// </summary>
        /// <param name="motorPulse">number of milliseconds to wait to pulse the servo</param>
        public void PulseMotor(double motorPulse)
        {
          
                //Total amount of time for a pulse
                double TotalPulseTime;
                double timeToWait;

                TotalPulseTime = 25;
                timeToWait = TotalPulseTime - motorPulse;

                //Send the pulse to move the servo over a given time span
                _motorPin.Write(GpioPinValue.High);
                MillisecondToWait(motorPulse);
                _motorPin.Write(GpioPinValue.Low);
                MillisecondToWait(timeToWait);
                _motorPin.Write(GpioPinValue.Low);
        }

        /// <summary>
        /// 
        /// Retrieves the number of milliconds to send as a pulse to turn the motor
        /// to the left right or middle
        /// 
        /// Values from Specification 
        ///     Position "0"   (1.5 ms pulse) is middle,
        ///     Position "90"  (~2 ms pulse) is all the way to the right
        ///     Position"-90" (~1 ms pulse) is all the way to the left
        ///     
        /// Values that were found to actually work
        ///     Position "0"   (1.2 ms pulse) is middle,
        ///     Position "90"  (~2 ms pulse) is all the way to the right
        ///     Position"-90" (~.4 ms pulse) is all the way to the left

        /// 
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
