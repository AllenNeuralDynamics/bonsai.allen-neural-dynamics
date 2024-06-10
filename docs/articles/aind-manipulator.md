AIND Manipulator
=================

# Assembly instructions

The manipulator is composed of two main components:

- The physical manipulator composed of multiple axis that makes up a configuration (e.g. a 4 axis manipulator)
- A controller board that interfaces with the manipulator and the computer

## Physical manipulator

The physical manipulator is expected to be assembled as per the following figure. The color of the axis is indicative of the color of the cable that should be connected to the corresponding port on the controller board.

![Manipulator](~/images/aind_manipulator_3d.png)

| Motor | Axis                                                  | Cable Color        |
|-------|-------------------------------------------------------|--------------|
| 0     | X (lateral)                                           | Blue         |
| 1     | Y1 (forward, "port", motor's left mouse's right)      | Red          |
| 2     | Y2 (forward, "starboard", motor's right mouse's left) | Green        |
| 3     | Z (vertical)                                          | White/Silver |


Each motor should also be wired to its respective cable following the color code below:

- Green (B+)
- Green-White (B-)
- Red (A+)
- Red-White (A-)

Finally, each end-of-travel switch follows the color code below:

- Blue (GND)
- White (Signal)
- Brown (+V)


## Controller board

### Bonsai Interface

To interface with the motors, the user can choose to use the Harp package for the [Harp.StepperDriver](https://github.com/harp-tech/device.stepperdriver). For simplicity, we maintain our own wrapper with core functionality and user-interface. The user can install it from the [Aind Nuget feed](https://www.nuget.org/packages/AllenNeuralDynamics.AindManipulator/).

(To be continued...)