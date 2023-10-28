//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.9.0.0 (Newtonsoft.Json v9.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------


namespace AllenNeuralDynamics.Core.Logging
{
    #pragma warning disable // Disable all warnings

    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class RenderSynchState
    {
    
        private double? _synchQuadValue;
    
        private int? _frameIndex;
    
        private double? _frameTimestamp;
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("synchQuadValue")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="synchQuadValue")]
        public double? SynchQuadValue
        {
            get
            {
                return _synchQuadValue;
            }
            set
            {
                _synchQuadValue = value;
            }
        }
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("frameIndex")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="frameIndex")]
        public int? FrameIndex
        {
            get
            {
                return _frameIndex;
            }
            set
            {
                _frameIndex = value;
            }
        }
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("frameTimestamp")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="frameTimestamp")]
        public double? FrameTimestamp
        {
            get
            {
                return _frameTimestamp;
            }
            set
            {
                _frameTimestamp = value;
            }
        }
    
        public System.IObservable<RenderSynchState> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new RenderSynchState
                {
                    SynchQuadValue = _synchQuadValue,
                    FrameIndex = _frameIndex,
                    FrameTimestamp = _frameTimestamp
                }));
        }
    }


    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class SoftwareEvent
    {
    
        private double? _timestamp;
    
        private SoftwareEventTimestampSource _timestampSource = AllenNeuralDynamics.Core.Logging.SoftwareEventTimestampSource.None;
    
        private int? _frameIndex;
    
        private double? _frameTimestamp;
    
        private string _name;
    
        private object _data;
    
        private SoftwareEventDataType _dataType;
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("timestamp")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="timestamp")]
        public double? Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
            }
        }
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("timestampSource")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="timestampSource")]
        public SoftwareEventTimestampSource TimestampSource
        {
            get
            {
                return _timestampSource;
            }
            set
            {
                _timestampSource = value;
            }
        }
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("frameIndex")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="frameIndex")]
        public int? FrameIndex
        {
            get
            {
                return _frameIndex;
            }
            set
            {
                _frameIndex = value;
            }
        }
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("frameTimestamp")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="frameTimestamp")]
        public double? FrameTimestamp
        {
            get
            {
                return _frameTimestamp;
            }
            set
            {
                _frameTimestamp = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("data")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="data")]
        public object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("dataType")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="dataType")]
        public SoftwareEventDataType DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                _dataType = value;
            }
        }
    
        public System.IObservable<SoftwareEvent> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new SoftwareEvent
                {
                    Timestamp = _timestamp,
                    TimestampSource = _timestampSource,
                    FrameIndex = _frameIndex,
                    FrameTimestamp = _frameTimestamp,
                    Name = _name,
                    Data = _data,
                    DataType = _dataType
                }));
        }
    }


    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class HarpLogger
    {
    
        private string _logName = "harpDevice";
    
        private string _extension = "bin";
    
        [Newtonsoft.Json.JsonPropertyAttribute("logName")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="logName")]
        public string LogName
        {
            get
            {
                return _logName;
            }
            set
            {
                _logName = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("extension")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="extension")]
        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
            }
        }
    
        public System.IObservable<HarpLogger> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new HarpLogger
                {
                    LogName = _logName,
                    Extension = _extension
                }));
        }
    }


    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class SpinnakerLogger
    {
    
        private string _logName = "camera";
    
        private double _encodingFrameRate = 60D;
    
        private string _videoExtension = "avi";
    
        private string _metadataExtension = "csv";
    
        private string _codec = "FMP4";
    
        [Newtonsoft.Json.JsonPropertyAttribute("logName")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="logName")]
        public string LogName
        {
            get
            {
                return _logName;
            }
            set
            {
                _logName = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("encodingFrameRate")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="encodingFrameRate")]
        public double EncodingFrameRate
        {
            get
            {
                return _encodingFrameRate;
            }
            set
            {
                _encodingFrameRate = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("videoExtension")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="videoExtension")]
        public string VideoExtension
        {
            get
            {
                return _videoExtension;
            }
            set
            {
                _videoExtension = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("metadataExtension")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="metadataExtension")]
        public string MetadataExtension
        {
            get
            {
                return _metadataExtension;
            }
            set
            {
                _metadataExtension = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("codec")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="codec")]
        public string Codec
        {
            get
            {
                return _codec;
            }
            set
            {
                _codec = value;
            }
        }
    
        public System.IObservable<SpinnakerLogger> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new SpinnakerLogger
                {
                    LogName = _logName,
                    EncodingFrameRate = _encodingFrameRate,
                    VideoExtension = _videoExtension,
                    MetadataExtension = _metadataExtension,
                    Codec = _codec
                }));
        }
    }


    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class GenericCsvLogger
    {
    
        private string _logName;
    
        private string _deviceName = "generic";
    
        private string _extension = "csv";
    
        private bool _omitHeader = false;
    
        private string _delimiter;
    
        [Newtonsoft.Json.JsonPropertyAttribute("logName", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="logName")]
        public string LogName
        {
            get
            {
                return _logName;
            }
            set
            {
                _logName = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("deviceName")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="deviceName")]
        public string DeviceName
        {
            get
            {
                return _deviceName;
            }
            set
            {
                _deviceName = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("extension")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="extension")]
        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("omitHeader")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="omitHeader")]
        public bool OmitHeader
        {
            get
            {
                return _omitHeader;
            }
            set
            {
                _omitHeader = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("Delimiter")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="Delimiter")]
        public string Delimiter
        {
            get
            {
                return _delimiter;
            }
            set
            {
                _delimiter = value;
            }
        }
    
        public System.IObservable<GenericCsvLogger> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new GenericCsvLogger
                {
                    LogName = _logName,
                    DeviceName = _deviceName,
                    Extension = _extension,
                    OmitHeader = _omitHeader,
                    Delimiter = _delimiter
                }));
        }
    }


    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class DataSchemaLogger
    {
    
        private string _logName;
    
        private string _deviceName = "config";
    
        private string _extension = "json";
    
        [Newtonsoft.Json.JsonPropertyAttribute("logName", Required=Newtonsoft.Json.Required.Always)]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="logName")]
        public string LogName
        {
            get
            {
                return _logName;
            }
            set
            {
                _logName = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("deviceName")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="deviceName")]
        public string DeviceName
        {
            get
            {
                return _deviceName;
            }
            set
            {
                _deviceName = value;
            }
        }
    
        [Newtonsoft.Json.JsonPropertyAttribute("extension")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="extension")]
        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
            }
        }
    
        public System.IObservable<DataSchemaLogger> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new DataSchemaLogger
                {
                    LogName = _logName,
                    DeviceName = _deviceName,
                    Extension = _extension
                }));
        }
    }


    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Source)]
    public partial class AindLogging
    {
    
        private string _version = "0.1.0";
    
        [Newtonsoft.Json.JsonPropertyAttribute("Version")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="Version")]
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }
    
        public System.IObservable<AindLogging> Process()
        {
            return System.Reactive.Linq.Observable.Defer(() => System.Reactive.Linq.Observable.Return(
                new AindLogging
                {
                    Version = _version
                }));
        }
    }


    public enum SoftwareEventTimestampSource
    {
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="none")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="none")]
        None = 0,
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="harp")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="harp")]
        Harp = 1,
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="render")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="render")]
        Render = 2,
    }


    public enum SoftwareEventDataType
    {
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="string")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="string")]
        String = 0,
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="number")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="number")]
        Number = 1,
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="boolean")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="boolean")]
        Boolean = 2,
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="object")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="object")]
        Object = 3,
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="array")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="array")]
        Array = 4,
    
        [System.Runtime.Serialization.EnumMemberAttribute(Value="null")]
        [YamlDotNet.Serialization.YamlMemberAttribute(Alias="null")]
        Null = 5,
    }


    /// <summary>
    /// Serializes a sequence of data model objects into JSON strings.
    /// </summary>
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.ComponentModel.DescriptionAttribute("Serializes a sequence of data model objects into JSON strings.")]
    public partial class SerializeToJson
    {
    
        private System.IObservable<string> Process<T>(System.IObservable<T> source)
        {
            return System.Reactive.Linq.Observable.Select(source, value => Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        public System.IObservable<string> Process(System.IObservable<RenderSynchState> source)
        {
            return Process<RenderSynchState>(source);
        }

        public System.IObservable<string> Process(System.IObservable<SoftwareEvent> source)
        {
            return Process<SoftwareEvent>(source);
        }

        public System.IObservable<string> Process(System.IObservable<HarpLogger> source)
        {
            return Process<HarpLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<SpinnakerLogger> source)
        {
            return Process<SpinnakerLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<GenericCsvLogger> source)
        {
            return Process<GenericCsvLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<DataSchemaLogger> source)
        {
            return Process<DataSchemaLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<AindLogging> source)
        {
            return Process<AindLogging>(source);
        }
    }


    /// <summary>
    /// Deserializes a sequence of JSON strings into data model objects.
    /// </summary>
    [System.ComponentModel.DefaultPropertyAttribute("Type")]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<RenderSynchState>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<SoftwareEvent>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<HarpLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<SpinnakerLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<GenericCsvLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<DataSchemaLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<AindLogging>))]
    [System.ComponentModel.DescriptionAttribute("Deserializes a sequence of JSON strings into data model objects.")]
    public partial class DeserializeFromJson : Bonsai.Expressions.SingleArgumentExpressionBuilder
    {
    
        public DeserializeFromJson()
        {
            Type = new Bonsai.Expressions.TypeMapping<AindLogging>();
        }

        public Bonsai.Expressions.TypeMapping Type { get; set; }

        public override System.Linq.Expressions.Expression Build(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
        {
            var typeMapping = (Bonsai.Expressions.TypeMapping)Type;
            var returnType = typeMapping.GetType().GetGenericArguments()[0];
            return System.Linq.Expressions.Expression.Call(
                typeof(DeserializeFromJson),
                "Process",
                new System.Type[] { returnType },
                System.Linq.Enumerable.Single(arguments));
        }

        private static System.IObservable<T> Process<T>(System.IObservable<string> source)
        {
            return System.Reactive.Linq.Observable.Select(source, value => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value));
        }
    }


    /// <summary>
    /// Serializes a sequence of data model objects into YAML strings.
    /// </summary>
    [Bonsai.CombinatorAttribute()]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.ComponentModel.DescriptionAttribute("Serializes a sequence of data model objects into YAML strings.")]
    public partial class SerializeToYaml
    {
    
        private System.IObservable<string> Process<T>(System.IObservable<T> source)
        {
            return System.Reactive.Linq.Observable.Defer(() =>
            {
                var serializer = new YamlDotNet.Serialization.SerializerBuilder().Build();
                return System.Reactive.Linq.Observable.Select(source, value => serializer.Serialize(value)); 
            });
        }

        public System.IObservable<string> Process(System.IObservable<RenderSynchState> source)
        {
            return Process<RenderSynchState>(source);
        }

        public System.IObservable<string> Process(System.IObservable<SoftwareEvent> source)
        {
            return Process<SoftwareEvent>(source);
        }

        public System.IObservable<string> Process(System.IObservable<HarpLogger> source)
        {
            return Process<HarpLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<SpinnakerLogger> source)
        {
            return Process<SpinnakerLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<GenericCsvLogger> source)
        {
            return Process<GenericCsvLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<DataSchemaLogger> source)
        {
            return Process<DataSchemaLogger>(source);
        }

        public System.IObservable<string> Process(System.IObservable<AindLogging> source)
        {
            return Process<AindLogging>(source);
        }
    }


    /// <summary>
    /// Deserializes a sequence of YAML strings into data model objects.
    /// </summary>
    [System.ComponentModel.DefaultPropertyAttribute("Type")]
    [Bonsai.WorkflowElementCategoryAttribute(Bonsai.ElementCategory.Transform)]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<RenderSynchState>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<SoftwareEvent>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<HarpLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<SpinnakerLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<GenericCsvLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<DataSchemaLogger>))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Bonsai.Expressions.TypeMapping<AindLogging>))]
    [System.ComponentModel.DescriptionAttribute("Deserializes a sequence of YAML strings into data model objects.")]
    public partial class DeserializeFromYaml : Bonsai.Expressions.SingleArgumentExpressionBuilder
    {
    
        public DeserializeFromYaml()
        {
            Type = new Bonsai.Expressions.TypeMapping<AindLogging>();
        }

        public Bonsai.Expressions.TypeMapping Type { get; set; }

        public override System.Linq.Expressions.Expression Build(System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> arguments)
        {
            var typeMapping = (Bonsai.Expressions.TypeMapping)Type;
            var returnType = typeMapping.GetType().GetGenericArguments()[0];
            return System.Linq.Expressions.Expression.Call(
                typeof(DeserializeFromYaml),
                "Process",
                new System.Type[] { returnType },
                System.Linq.Enumerable.Single(arguments));
        }

        private static System.IObservable<T> Process<T>(System.IObservable<string> source)
        {
            return System.Reactive.Linq.Observable.Defer(() =>
            {
                var serializer = new YamlDotNet.Serialization.DeserializerBuilder().Build();
                return System.Reactive.Linq.Observable.Select(source, value =>
                {
                    var reader = new System.IO.StringReader(value);
                    var parser = new YamlDotNet.Core.MergingParser(new YamlDotNet.Core.Parser(reader));
                    return serializer.Deserialize<T>(parser);
                });
            });
        }
    }
}