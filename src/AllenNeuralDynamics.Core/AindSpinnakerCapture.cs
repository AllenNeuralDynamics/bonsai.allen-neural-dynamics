using System.ComponentModel;
using Bonsai.Spinnaker;
using SpinnakerNET;
using OpenCV.Net;
using System;

namespace AllenNeuralDynamics.Core
{
    [Description("Configures and initializes a Spinnaker camera for triggered acquisition.")]
    public class AindSpinnakerCapture : SpinnakerCapture
    {
        public AindSpinnakerCapture()
        {
            ExposureTime = 1e6 / 50 - 1000;
            Binning = 1;
            Gain = 0;
            Binning = 1;
            PixelFormat = PixelFormatEnums.Mono8;
            Gamma = null;
            AdcBitDepth = null;
        }

        [Description("The duration of each individual exposure, in microseconds. In general, this should be 1 / frameRate - 1 millisecond to prepare for next trigger.")]
        public double ExposureTime { get; set; }

        [Description("The gain of the sensor.")]
        public double Gain { get; set; }

        [Description("The size of the binning area of the sensor, e.g. a binning size of 2 specifies a 2x2 binning region.")]
        public int Binning { get; set; }

        [Description("Parameter used for gamma correction. If null, gamma correction is disabled.")]
        public double? Gamma { get; set; }

        [Description("Sensor pixel format.")]
        public PixelFormatEnums PixelFormat { get; set; }

        [Description("Region of interest to crop the sensor with.")]
        public Rect RegionOfInterest { get; set; } = new Rect(0,0,0,0);

        [Description("Sensor ADC bit depth used to acquired data.")]
        public AdcBitDepthEnums? AdcBitDepth { get; set; }

        protected override void Configure(IManagedCamera camera)
        {
            try { camera.AcquisitionStop.Execute(); }
            catch { }
            camera.PixelFormat.Value = PixelFormat.ToString();
            if (AdcBitDepth.HasValue)
            {
                camera.AdcBitDepth.Value = AdcBitDepth.Value.ToString();
            }
            camera.BinningSelector.Value = BinningSelectorEnums.All.ToString();
            camera.BinningHorizontalMode.Value = BinningHorizontalModeEnums.Sum.ToString();
            camera.BinningVerticalMode.Value = BinningVerticalModeEnums.Sum.ToString();
            camera.BinningHorizontal.Value = Binning;
            camera.BinningVertical.Value = Binning;
            camera.AcquisitionFrameRateEnable.Value = false;
            camera.IspEnable.Value = false;
            camera.TriggerMode.Value = TriggerModeEnums.On.ToString();
            camera.TriggerDelay.Value = camera.TriggerDelay.Min;
            camera.TriggerSelector.Value = TriggerSelectorEnums.FrameStart.ToString();
            camera.TriggerSource.Value = TriggerSourceEnums.Line0.ToString();
            camera.TriggerOverlap.Value = TriggerOverlapEnums.ReadOut.ToString();
            camera.TriggerActivation.Value = TriggerActivationEnums.RisingEdge.ToString();
            camera.ExposureAuto.Value = ExposureAutoEnums.Off.ToString();
            camera.ExposureMode.Value = ExposureModeEnums.Timed.ToString();
            camera.ExposureTime.Value = ExposureTime;
            camera.DeviceLinkThroughputLimit.Value = camera.DeviceLinkThroughputLimit.Max;
            camera.GainAuto.Value = GainAutoEnums.Off.ToString();
            camera.Gain.Value = Gain;

            if (Gamma.HasValue){
                camera.GammaEnable.Value = true;
                camera.Gamma.Value = Gamma.Value;
            }
            else{
                camera.GammaEnable.Value = false;
            }
            SetRegionOfInterest(camera);

            base.Configure(camera);
        }

        private void SetRegionOfInterest(IManagedCamera camera)
        {
            if ((RegionOfInterest.Height == 0) || (RegionOfInterest.Width == 0))
            {
                if (RegionOfInterest.X != 0 || RegionOfInterest.Y != 0 || RegionOfInterest.Height != 0 || RegionOfInterest.Width != 0)
                {
                    throw new InvalidOperationException("If Heigh or Width is 0, all size arguments must be 0.");
                }

                // If the region of interest is not set, set the width and height to the maximum values
                // allowed by the sensor
                camera.OffsetX.Value = 0;
                camera.OffsetY.Value = 0;
                camera.Width.Value = camera.WidthMax.Value;
                camera.Height.Value = camera.HeightMax.Value;
            }
            else
            {
                camera.Width.Value = RegionOfInterest.Width;
                camera.Height.Value = RegionOfInterest.Height;

                // Set the offset to the top left corner of the region of interest
                // Passing a valid value is the responsibility of the user
                camera.OffsetX.Value = RegionOfInterest.X;
                camera.OffsetY.Value = RegionOfInterest.Y;
            }
        }

        static Func<IManagedImage, IplImage> GetConverter(PixelFormatEnums pixelFormat, Bonsai.Spinnaker.ColorProcessingAlgorithm colorProcessing)
        {
            int outputChannels;
            IplDepth outputDepth;
            if (pixelFormat < PixelFormatEnums.BayerGR8 || pixelFormat == PixelFormatEnums.BGR8 ||
                pixelFormat <= PixelFormatEnums.BayerBG16 && colorProcessing == Bonsai.Spinnaker.ColorProcessingAlgorithm.NoColorProcessing)
            {
                if (pixelFormat == PixelFormatEnums.BGR8)
                {
                    outputChannels = 3;
                    outputDepth = IplDepth.U8;
                }
                else
                {
                    outputChannels = 1;
                    var depthFactor = (int)pixelFormat;
                    if (pixelFormat > PixelFormatEnums.Mono16) depthFactor = (depthFactor - 3) / 4;
                    outputDepth = (IplDepth)(8 * (depthFactor + 1));
                }

                return image =>
                {
                    var width = (int)image.Width;
                    var height = (int)image.Height;
                    using (var bitmapHeader = new IplImage(new Size(width, height), outputDepth, outputChannels, image.DataPtr))
                    {
                        var output = new IplImage(bitmapHeader.Size, outputDepth, outputChannels);
                        CV.Copy(bitmapHeader, output);
                        return output;
                    }
                };
            }

            PixelFormatEnums outputFormat;
            if (pixelFormat == PixelFormatEnums.Mono10p ||
                pixelFormat == PixelFormatEnums.Mono10Packed ||
                pixelFormat == PixelFormatEnums.Mono12p ||
                pixelFormat == PixelFormatEnums.Mono12Packed)
            {
                outputFormat = PixelFormatEnums.Mono16;
                outputDepth = IplDepth.U16;
                outputChannels = 1;
            }
            else if (pixelFormat >= PixelFormatEnums.BayerGR8 && pixelFormat <= PixelFormatEnums.BayerBG16)
            {
                outputFormat = PixelFormatEnums.BGR8;
                outputDepth = IplDepth.U8;
                outputChannels = 3;
            }
            else throw new InvalidOperationException(string.Format("Unable to convert pixel format {0}.", pixelFormat));

            return image =>
            {
                var width = (int)image.Width;
                var height = (int)image.Height;
                var output = new IplImage(new Size(width, height), outputDepth, outputChannels);
                unsafe
                {
                    using (var destination = new ManagedImage((uint)width, (uint)height, 0, 0, outputFormat, output.ImageData.ToPointer()))
                    {
                        image.Convert(destination, outputFormat, (SpinnakerNET.ColorProcessingAlgorithm)colorProcessing);
                        return output;
                    }
                }
            };
        }
    }
}


/*
This file was adapted from https://github.com/SainsburyWellcomeCentre/aeon_acquisition under the following license:
BSD 3-Clause License

Copyright (c) 2023 University College London
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1.Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
