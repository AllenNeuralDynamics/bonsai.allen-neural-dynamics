SniffDetector
==========

The repository of this device can be found [here](https://github.com/AllenNeuralDynamics/harp.device.sniff-detector).

# Examples

It is advisable to first get familiar with the general Bonsai interface for Harp devices. This documentation can be found at [harp-tech.org](https://harp-tech.org/articles/intro.html).

## Parsing sensor data

The thermistor data ca be parsed using the following pattern
:::workflow
![SniffDetectorParsing](~/workflows/SniffDetectorParsing.bonsai)
:::

## Setting the dispatch rate

The dispatch rate of the sensor data event can be set using the following pattern:

:::workflow
![SniffDetectorChangeDispatchRate](~/workflows/SniffDetectorChangeDispatchRate.bonsai)
:::
