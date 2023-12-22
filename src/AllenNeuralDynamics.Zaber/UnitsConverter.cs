using System.ComponentModel;
using Zaber.Motion;

namespace AllenNeuralDynamics.Zaber
{

    public class PositionUnitsConverter: EnumConverter
    {
        public PositionUnitsConverter() : base(typeof(Units))
        {
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new[]
            {
                Units.Native,
                Units.Length_Nanometres,
                Units.Length_Micrometres,
                Units.Length_Millimetres,
                Units.Length_Centimetres,
                Units.Length_Metres,
                Units.Length_Inches
            });
        }
    }

    public class VelocityUnitsConverter : EnumConverter
    {
        public VelocityUnitsConverter() : base(typeof(Units))
        {
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new[]
            {
                Units.Native,
                Units.Velocity_NanometresPerSecond,
                Units.Velocity_MicrometresPerSecond,
                Units.Velocity_MillimetresPerSecond,
                Units.Velocity_CentimetresPerSecond,
                Units.Velocity_MetresPerSecond,
                Units.Velocity_InchesPerSecond,
            });
        }
    }

    public class AccelerationUnitsConverter : EnumConverter
    {
        public AccelerationUnitsConverter() : base(typeof(Units))
        {
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new[]
            {
                Units.Native,
                Units.Acceleration_NanometresPerSecondSquared,
                Units.Acceleration_MicrometresPerSecondSquared,
                Units.Acceleration_MillimetresPerSecondSquared,
                Units.Acceleration_CentimetresPerSecondSquared,
                Units.Acceleration_MetresPerSecondSquared,
                Units.Acceleration_InchesPerSecondSquared,
            });
        }
    }
}
