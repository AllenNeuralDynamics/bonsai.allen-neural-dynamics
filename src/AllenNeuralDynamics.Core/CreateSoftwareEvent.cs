using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Harp;
using AllenNeuralDynamics.AindBehaviorServices.DataTypes;

namespace AllenNeuralDynamics.Core { 
    [Combinator]
    [Description("Creates a fully populated SoftwareEvent a <T> generic object. If a Harp.Timestamped<T> is provided, temporal information will be added.")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class CreateSoftwareEvent
    {
        public string EventName { get; set; } = "SoftwareEvent";

        public IObservable<SoftwareEvent> Process<TSource>(IObservable<Timestamped<TSource>> source)
        {
            var thisName = EventName;
            return source.Select(value => {
                return new SoftwareEvent
                {
                    Data = value.Value,
                    Timestamp = value.Seconds,
                    TimestampSource = TimestampSource.Harp,
                    FrameIndex = null,
                    FrameTimestamp = null,
                    Name = thisName,
                    DataType = getDataType(value.Value)
                };
            });
        }

        public IObservable<SoftwareEvent> Process<TSource>(IObservable<TSource> source)
        {
            var thisName = EventName;
            return source.Select(value => {
                return new SoftwareEvent
                {
                    Data = value,
                    Timestamp = null,
                    TimestampSource = TimestampSource.Null,
                    FrameIndex = null,
                    FrameTimestamp = null,
                    Name = thisName,
                    DataType = getDataType(value)
                };
            });
        }

        private static DataType getDataType<T>(T value)
        {
            double parsed;
            if (value == null)
            {
                return DataType.Null;
            }
            if (double.TryParse(value.ToString(), out parsed))
            {
                return DataType.Number;
            }
            var type = value.GetType();
            if (type == typeof(string))
            {
                return DataType.String;
            }
            if (type == typeof(bool))
            {
                return DataType.Boolean;
            }
            if (type.IsArray)
            {
                return DataType.Array;
            }
            return DataType.Object;
        }
    }
}
