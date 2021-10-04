using PileSynth.Audio;
using PileSynth.Nodes.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes
{
    public class MonoOut
    {
        public WavEncoder.Settings Settings = new WavEncoder.Settings();
        public SynthInput Input = new SynthInput();
        public CustomSynthNode OutNode = new ConstantNode();
        public int GetSampleAt(double timeInSeconds)
        {
            Input.TimeInSeconds = timeInSeconds;
            OutNode.Recalculate(Input);
            return (int)OutNode.GetValue();
        }

        public int[] GetAllSamples(double durationInSeconds)
        {
            int lengthOfAllSamples = (int)(durationInSeconds * Settings.SampleRate);
            int[] samples = new int[lengthOfAllSamples];
            for(int t = 0; t < samples.Length; t++)
            {
                double timeInSeconds = (double)t / Settings.SampleRate;
                samples[t] = GetSampleAt(timeInSeconds);
            }
            return samples;
        }

        public void GenerateWavToStream(Stream streamToGenerateInto, double durationInSeconds)
        {
            int[] samples = GetAllSamples(durationInSeconds);
            WavEncoder encoder = new WavEncoder(samples);
            encoder.QualitySettings = Settings;
            encoder.ChannelsNumber = 1;
            encoder.EncodeToStream(streamToGenerateInto);
        }
    }
}
