# RaspberryPi-Control-SG90-Example
This project is how to control an SG90 server from a respberry pi using Windows 10

An SG 90 is a servor used to control different pieces of equipment. It is a favorite for hobbyist who fly drones and helicopters because of it light weight.

An SG90 server  is controled by a pulse being sent to it, different lengths of pulse deteremine how the motor will turn. But before we get into how to control the motor lest first look at how to connect the Sg90 to the rasberry pi.

![Alt text](https://raw.githubusercontent.com/StuartSmith/RaspberryPi-Control-Sg90-Example/master/Images/ServoDiagramImage.PNG "")

from the diagram we can see that the brown wire to the server should go to ground the red is the positive current and the orange is the pusle.

A pulse is where we send a pulse of high polarity from a GPIO pin for a certain amount of time which which will make the motor turn one way or the other.


