using Bonsai;
using System;
using System.ComponentModel;
using Bonsai.Harp;
using System.Xml.Serialization;

namespace AllenNeuralDynamics.HarpUtils 
{ 
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Dynamically creates Harp devices with the specified WhoAmI and Firmware metadata.")]
    public class ValidatedDevice
    {
        [Description("The WhoAmI value of the device to be connected to.")]
        public int WhoAmI { get; set; } = 0;

        [XmlIgnore]
        [Description("The Firmware metadata specifications expected to be found in the target device.")]
        public HarpDeviceMetadata HarpMetadata { get; set; }

        [Description("The name of the serial port used to communicate with the Harp device.")]
        [TypeConverter(typeof(PortNameConverter))]
        public string PortName { get; set; }
        //
        // Summary:
        //     Gets or sets a value specifying the state of the LED reporting device operation.
        [Description("Specifies the state of the LED reporting device operation.")]
        public LedState OperationLed { get; set; } = LedState.On;
        //
        // Summary:
        //     Gets or sets a value indicating whether the device should send the content of
        //     all registers during initialization.
        [Description("Specifies whether the device should send the content of all registers during initialization.")]
        public bool DumpRegisters { get; set; } = true;
        //
        // Summary:
        //     Gets or sets a value specifying the state of all the visual indicators in the
        //     device.
        [Description("Specifies the state of all the visual indicators in the device.")]
        public LedState VisualIndicators { get; set; } = LedState.On;
        //
        // Summary:
        //     Gets or sets a value indicating whether the Device sends the Timestamp event
        //     each second.
        [Description("Specifies if the Device sends the Timestamp event each second.")]
        private EnableFlag Heartbeat { get; set; } = EnableFlag.Enabled;
        //
        // Summary:
        //     Gets or sets a value indicating whether error messages parsed during acquisition
        //     should be ignored or raise an exception.
        [Description("Specifies whether error messages parsed during acquisition should be ignored or raise an error.")]
        public bool IgnoreErrors { get; set; } = false;
        //
        // Summary:
        //     Gets or sets a value specifying the operation mode of the device at initialization.
        [Description("Specifies the operation mode of the device at initialization.")]
        public OperationMode OperationMode { get; set; } = OperationMode.Active;

        Device CreateDevice()
        {
            FirmwareMetadata firmwareMetadata = HarpMetadata.ToFirmwareMetadata();
            if (firmwareMetadata != null)
            {
                return new Device(WhoAmI, firmwareMetadata)
                {
                    PortName = PortName,
                    DumpRegisters = DumpRegisters,
                    IgnoreErrors = IgnoreErrors,
                    Heartbeat = Heartbeat,
                    OperationLed = OperationLed,
                    OperationMode = OperationMode,
                    VisualIndicators = VisualIndicators
                };
            }
            else
            {
                return new Device(WhoAmI)
                {
                    PortName = PortName,
                    DumpRegisters = DumpRegisters,
                    IgnoreErrors = IgnoreErrors,
                    Heartbeat = Heartbeat,
                    OperationLed = OperationLed,
                    OperationMode = OperationMode,
                    VisualIndicators = VisualIndicators
                };
            }
        }

        public IObservable<HarpMessage> Generate()
        {
            return CreateDevice().Generate();
        }

        public IObservable<HarpMessage> Generate(IObservable<HarpMessage> source)
        {
            return CreateDevice().Generate(source);
        }
    }
}