# RaspberryPi-Control-SG90-Example
<p>
This project is how to control an SG90 server from a respberry pi using Windows 10.

</p>
<p>
<img style="float:left;" src="https://raw.githubusercontent.com/StuartSmith/RaspberryPi-Control-Sg90-Example/master/Images/Sketchsg90.jpg">
An SG 90 is a servro used to control different pieces of equipment. It is a favorite among hobbyist who fly drones and helicopters because of it's light weight.
</p>


An SG90 server  is controled by a pulse being sent to it over a 20 millisecond time span, different lengths of pulse deteremine how the motor will turn. But before we get into how to control the motor lets first look at how to connect the Sg90 to the rasberry pi.

![Alt text](https://raw.githubusercontent.com/StuartSmith/RaspberryPi-Control-Sg90-Example/master/Images/ServoDiagramImage.PNG "")

From the diagram; we can see that the brown wire goes to ground, the red is the positive current and the orange is the pulse.

A pulse is where we send a pulse of high polarity from a GPIO pin for a certain amount of time which which will make the motor turn one way or the other.

To wire the servo to the rasberry pi is farily straight foreward the ground of the servo with go to pin 2 (Positive 5V), the positive wire from the servo will go to pin 6 (Ground) and the pulse control can go to any of the GPIO pins but for simplicity connect this to pin 29 (GPIO 5)


