using System;
using System.Linq;


namespace AllenNeuralDynamics.AlicatFlowmeter 
{
 
    public class FlowmeterDataframe
    {
        private string rawString { get; set; }
        public string DeviceId { get; set; }
        public float AbsolutePressure { get; set; }
        public float Temperature { get; set; }
        public float VolumetricFlowRate { get; set; }
        public float MassFlowRate { get; set; }
        public float MassFlowTotal { get; set; }
        public string Gas { get; set; }

        public FlowmeterDataframe()
        {
        }

        public FlowmeterDataframe(string serialString)
        {
            var parsed = Parse(serialString);
            rawString = serialString;
            DeviceId = parsed.DeviceId;
            AbsolutePressure = parsed.AbsolutePressure;
            Temperature = parsed.Temperature;
            VolumetricFlowRate = parsed.VolumetricFlowRate;
            MassFlowRate = parsed.MassFlowRate;
            MassFlowTotal = parsed.MassFlowRate;
            Gas = parsed.Gas;
        }

        public static FlowmeterDataframe Parse(string value)
        {
            try {
                var stringFragment = value.Trim().Split(' ');
                stringFragment = stringFragment.Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                if (stringFragment.Length > 6)
                {
                    var dataframe = new FlowmeterDataframe
                    {
                        rawString = value,
                        DeviceId = stringFragment[0],
                        AbsolutePressure = float.Parse(stringFragment[1]),
                        Temperature = float.Parse(stringFragment[2]),
                        VolumetricFlowRate = float.Parse(stringFragment[3]),
                        MassFlowRate = float.Parse(stringFragment[4]),
                        MassFlowTotal = float.Parse(stringFragment[5]),
                        Gas = stringFragment[6]
                    };
                    return dataframe;
                }
                else { return Default(); }
            }
            catch (Exception e)
            {
                return Default();
            }
        }
    
        public static FlowmeterDataframe Default()
        {
            return new FlowmeterDataframe
            {
                rawString = string.Empty,
                DeviceId = "",
                AbsolutePressure = float.NaN,
                Temperature = float.NaN,
                VolumetricFlowRate = float.NaN,
                MassFlowRate = float.NaN,
                MassFlowTotal = float.NaN,
                Gas = ""
            };
        }

        public override string ToString()
        {
            return rawString;
        }
    }
}
