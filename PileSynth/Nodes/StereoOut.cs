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
    public class StereoOut
    {
        public MonoOut left = new();
        public MonoOut right = new();

        public WavEncoder.Settings Settings = new WavEncoder.Settings();
        public SynthInput Input = new SynthInput();
        public int[] GetAllSamples(double durationInSeconds)
        {
            left.Input = Input;
            right.Input = Input;
            int lengthOfAllSamples = (int)(durationInSeconds * Settings.SampleRate) * 2;
            int[] samples = new int[lengthOfAllSamples];
            for (int t = 0; t < samples.Length; t += 2)
            {
                double timeInSeconds = (double)t / Settings.SampleRate;
                samples[t] = left.GetSampleAt(timeInSeconds);
                samples[t + 1] = right.GetSampleAt(timeInSeconds);
            }
            return samples;
        }

        public void GenerateWavToStream(Stream streamToGenerateInto, double durationInSeconds)
        {
            int[] samples = GetAllSamples(durationInSeconds);
            WavEncoder encoder = new WavEncoder(samples);
            encoder.QualitySettings = Settings;
            encoder.ChannelsNumber = 2;
            encoder.EncodeToStream(streamToGenerateInto);
        }
    }
}
