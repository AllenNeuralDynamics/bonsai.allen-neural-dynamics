Environment Sensor
==========

The repository of this device can be found [here](https://github.com/AllenNeuralDynamics/harp.device.environment-sensor).

# Examples

It is advisable to first get familiar with the general Bonsai interface for Harp devices. This documentation can be found at [harp-tech.org](https://harp-tech.org/articles/intro.html).

## Parsing sensor data

Device sensor data can be parsed from the individual sensor registers ([`Pressure`, `Temperature`, `Humidity`]). For simplicity, a single register with a packed data structure is also provided. By default, this register will emit a period message with the data from all sensors. The following pattern can be used to parse the register:

:::workflow
![EnvironmentSensorDataParsing](~/workflows/EnvironmentSensorExample.bonsai)
:::

