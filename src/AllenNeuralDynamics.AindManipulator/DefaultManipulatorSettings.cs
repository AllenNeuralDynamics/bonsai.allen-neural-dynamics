using Bonsai;

using Bonsai.IO;
using Harp.StepperDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;


namespace AllenNeuralDynamics.AindManipulator
{
    public class DefaultManipulatorSettings : Source<AindManipulatorCalibrationInput>
    {

        public int InitialPositionX { get; set; } = 0;
        public int InitialPositionY1 { get; set; } = 0;
        public int InitialPositionY2 { get; set; } = 0;
        public int InitialPositionZ { get; set; } = 0;

        [TypeConverter(typeof(UnidimensionalArrayConverter))]
        public Axis[] HomingOrder { get; set; } = new Axis[] { Axis._1, Axis._2, Axis._3, Axis._4 };

        [TypeConverter(typeof(UnidimensionalArrayConverter))]
        public Axis[] EnabledAxis { get; set; } = new Axis[] { Axis._1, Axis._2, Axis._3, Axis._4 };

        public int StepAccelerationInterval { get; set; } = 100;

        public int StepInterval { get; set; } = 100;

        public MicrostepResolution MicrostepResolution { get; set; } = 0;

        public int MaximumStepInterval { get; set; } = 2000;

        public MotorOperationMode MotorOperationMode { get; set; } = 0;

        public int MaxLimit { get; set; } = 24000;

        public int MinLimit { get; set; } = 100;


        public override IObservable<AindManipulatorCalibrationInput> Generate()
        {
            return Observable.Return(new AindManipulatorCalibrationInput()
            {
                InitialPosition = new ManipulatorPosition() { X = InitialPositionX, Y1 = InitialPositionY1, Y2 = InitialPositionY2, Z = InitialPositionZ},
                FullStepToMm = new ManipulatorPosition(),
                HomingOrder = HomingOrder.ToList(),
                AxisConfiguration = EnabledAxis.Select(x => DefaultAxisConfiguration(x)).ToList()
            });
        }

        private AxisConfiguration DefaultAxisConfiguration(Axis axis)
        {
            return new AxisConfiguration()
            {
                Axis = axis,
                StepAccelerationInterval = StepAccelerationInterval,
                StepInterval = StepInterval,
                MicrostepResolution = MicrostepResolution,
                MaximumStepInterval = MaximumStepInterval,
                MotorOperationMode = MotorOperationMode,
                MaxLimit = MaxLimit,
                MinLimit = MinLimit
            };
        }
    }
}
