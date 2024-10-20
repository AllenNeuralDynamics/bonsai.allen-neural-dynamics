using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;

namespace AllenNeuralDynamics.Core.Design
{
    [Combinator]
    [Description("Combines two IplImages into one by performing a weighted sum.")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class AddImageWeighted
    {
        [Range(0, 1)]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        [Description("Parameter that controls the blending between the two images. dst= α*src1+(1-α)*src2")]
        public float Alpha { get; set; } = 0.5f;

        public IObservable<IplImage> Process(IObservable<Tuple<IplImage, IplImage>> source)
        {
            IplImage dst = null;
            return source.Select(value =>
            {
                if (dst == null || dst.Size != value.Item1.Size)
                {
                    dst = new IplImage(value.Item1.Size, value.Item1.Depth, value.Item1.Channels);
                }
                CV.AddWeighted(value.Item1, Alpha, value.Item2, 1.0 - Alpha, 0, dst);
                return dst;
            });
        }
    }
}
