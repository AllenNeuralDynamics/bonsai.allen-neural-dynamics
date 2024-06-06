using Bonsai;
using Bonsai.Harp;
using Harp.StepperDriver;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;


namespace AllenNeuralDynamics.AindManipulator
{
    
    public class DefaultManipulatorSettings : Source<AindManipulatorCalibrationInput>
    {

        [TypeConverter(typeof(NumericRecordConverter))]
        public AindManipulatorPosition InitialPosition { get; set; } = new AindManipulatorPosition();

        [TypeConverter(typeof(NumericRecordConverter))]
        public AindManipulatorPosition StepToMm { get; set; } = new AindManipulatorPosition();

        [TypeConverter(typeof(UnidimensionalArrayConverter))]
        public Axis[] HomingOrder { get; set; } = new Axis[] {Axis.Y1, Axis.Y2, Axis.X, Axis.Z};

        [TypeConverter(typeof(UnidimensionalArrayConverter))]
        public Axis[] EnabledAxis { get; set; } = new Axis[] { Axis.Y1, Axis.Y2, Axis.X, Axis.Z };

        public int StepAccelerationInterval { get; set; } = 100;

        public int StepInterval { get; set; } = 100;

        public Harp.StepperDriver.MicrostepResolution MicrostepResolution { get; set; } = 0;

        public int MaximumStepInterval { get; set; } = 2000;

        public Harp.StepperDriver.MotorOperationMode MotorOperationMode { get; set; } = 0;

        public int MaxLimit { get; set; } = 24000;

        public int MinLimit { get; set; } = 100;


        public override IObservable<AindManipulatorCalibrationInput> Generate()
        {
            return Observable.Return(new AindManipulatorCalibrationInput()
            {
                InitialPosition = InitialPosition.ToManipulatorPosition(),
                FullStepToMm = StepToMm.ToManipulatorPosition(),
                HomingOrder = HomingOrder.Select(x => (Axis) x).ToList(),
                AxisConfiguration = EnabledAxis.Select(x => DefaultAxisConfiguration(x)).ToList()
            });
        }

        private AxisConfiguration DefaultAxisConfiguration(Axis axis)
        {
            return new AxisConfiguration()
            {
                Axis = (Axis) axis,
                StepAccelerationInterval = StepAccelerationInterval,
                StepInterval = StepInterval,
                MicrostepResolution = (MicrostepResolution) MicrostepResolution,
                MaximumStepInterval = MaximumStepInterval,
                MotorOperationMode = (MotorOperationMode) MotorOperationMode,
                MaxLimit = MaxLimit,
                MinLimit = MinLimit
            };
        }
    }
}
