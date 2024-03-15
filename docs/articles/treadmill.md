Treadmill
==========

The repository of this device can be found [here](https://github.com/AllenNeuralDynamics/harp.device.treadmill).

# Examples

It is advisable to first get familiar with the general Bonsai interface for Harp devices. This documentation can be found at [harp-tech.org](https://harp-tech.org/articles/intro.html).

## Parsing sensor data

Device sensor data can be parsed from the individual sensor registers ([`Encoder`, `Torque` and `TorqueLoadCurrent`]). For simplicity, a single register with a packed data structure is also provided. The following pattern can be used to parse the treadmill sensor data:

:::workflow
![TreadmillParsing](~/workflows/TreadmillParsing.bonsai)
:::

## Using the magnetic particle break

The treadmill is fitted with a [magnetic particle break](https://placidindustries.com/d/?h=a03be4b) that can be used to control the torque of the wheel experiences up to a set point. The following pattern can be used to control the break. In this example we will use a sinusoidal pattern to set the value of the break, but any other numeric input can be used.

:::workflow
![TreadmillBreak](~/workflows/TreadmillBreak.bonsai)
:::

## Configuring additional settings

Additional settings are available for the treadmill. These include the ability to:

- Set the dispatch rate of the sensor data event;
- Tare the value of all or individual sensors;
- Reset the Tare function of all or individual sensors;

The following example shows how to achieve all the above, respectively:

:::workflow
![TreadmillSettings](~/workflows/TreadmillSettings.bonsai)
:::
