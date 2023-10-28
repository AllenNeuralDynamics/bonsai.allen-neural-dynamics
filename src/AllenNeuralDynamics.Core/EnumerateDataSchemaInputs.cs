using Bonsai;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace AllenNeuralDynamics.Core
{
    public class EnumerateDataSchemas : Source<string>
    {
        public FileTypes FileFilter = FileTypes.YAML;

        [Editor("Bonsai.Design.FolderNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        [Description("The relative or absolute path of the selected folder.")]
        public string Directory = ".";

        [TypeConverter(typeof(EnumeratedDataSchemasConverter))]
        public string DataSchema;

        private static string BuildFullPath(string dir, string file)
        {
            return Path.Combine(dir, file);
        }

        public override IObservable<string> Generate()
        {
            return Observable.Return(BuildFullPath(Directory, DataSchema));
        }

        public IObservable<string> Generate<TSource>(IObservable<TSource> source)
        {
            return source.Select(x =>
            {
                return Path.Combine(BuildFullPath(Directory, DataSchema));
            }
                );
        }

        class EnumeratedDataSchemasConverter : StringConverter
        {
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                EnumerateDataSchemas outer = (EnumerateDataSchemas)context.Instance;
                var filterString = getFilterString(outer.FileFilter);
                var schemaFiles = System.IO.Directory.GetFiles(outer.Directory, filterString).ToList();
                var filenames = schemaFiles.Select(x => Path.GetFileName(x)).ToList();

                return new StandardValuesCollection(filenames);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }

        private static string getFilterString(FileTypes filter)
        {
            switch (filter)
            {
                case FileTypes.YAML:
                    return "*.yaml";
                case FileTypes.YML:
                    return "*.yml";
                case FileTypes.JSON:
                    return "*.json";
                default:
                    throw new Exception("Unknown file type.");
            }
        }
        public enum FileTypes
        {
            YAML,
            YML,
            JSON,
        }
    }
}
