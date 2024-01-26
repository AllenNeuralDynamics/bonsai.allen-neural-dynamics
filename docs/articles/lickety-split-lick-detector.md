LicketySplitLickDetector
==========

The repository of this device can be found [here](https://github.com/AllenNeuralDynamics/harp.device.lickety-split).

# Examples

It is advisable to first get familiar with the general Bonsai interface for Harp devices. This documentation can be found at [harp-tech.org](https://harp-tech.org/articles/intro.html).

## Detecting licks

Licks detected by the board can be `Parse`d from register `LickState` using the following pattern:

:::workflow
![LickDetectionExample](~/workflows/LiketySplitLickDetection.bonsai)
:::

`True` and `False` values will correspond to the onset and offset of lick events, respectively.