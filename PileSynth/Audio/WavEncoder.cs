using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Audio
{
    public class WavEncoder
    {
        public ushort ChannelsNumber = 2;
        public uint SampleRate = 44100;
        public AudioResolution SampleResolution = AudioResolution.UNSIGNED_8_BIT;

        private int[] samples;

        public WavEncoder(int[] audioSamples)
        {
            samples = audioSamples;
        }

        public void EncodeToStream(Stream streamToEncode)
        {
            EncodeWav(streamToEncode);
        }

        private void EncodeWav(Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                WriteHeader(writer);
                WriteDataChunk(writer);
                WriteMissingGaps(writer);
            }
            stream.Seek(0, SeekOrigin.Begin);
        }

        private void WriteHeader(BinaryWriter writer)
        {
            writer.Write("RIFF".ToArray());
            writer.Write(0u);
            writer.Write("WAVE".ToArray());
            writer.Write("fmt ".ToArray());
            writer.Write(16u);
            WriteFormatChunk(writer);
            writer.Write("data".ToArray());
            writer.Write(0u);
        }

        private void WriteFormatChunk(BinaryWriter writer)
        {
            writer.Write((ushort)(1u));
            writer.Write(ChannelsNumber);
            writer.Write(SampleRate);
            writer.Write(SampleRate * (ushort)SampleResolution * ChannelsNumber / 8);
            writer.Write((ushort)((ushort)SampleResolution * ChannelsNumber / 8));
            writer.Write((ushort)SampleResolution);
        }

        private void WriteDataChunk(BinaryWriter writer)
        {
            if(SampleResolution == AudioResolution.UNSIGNED_8_BIT)
            {
                writer.Write(BitUtility.ConvertSampleToByteArray(samples));
            } else if(SampleResolution == AudioResolution.SIGNED_16_BIT)
            {
                short[] shortSamples = BitUtility.ConvertSampleToShortArray(samples);
                byte[] buffer = new byte[shortSamples.Length * 2];
                Buffer.BlockCopy(shortSamples, 0, buffer, 0, shortSamples.Length * 2);
                writer.Write(buffer);
            }
        }

        private void WriteMissingGaps(BinaryWriter writer)
        {
            writer.Seek(4, SeekOrigin.Begin);
            writer.Write((uint)(writer.BaseStream.Length - 8));
            writer.Seek(40, SeekOrigin.Begin);
            writer.Write((uint)(writer.BaseStream.Length - 44));
        }

        public void CreateCoolSamples(int frequency, double durationInSeconds, int amplitude)
        {
            samples = new int[(int)(SampleRate * durationInSeconds)];
            int changeSoundEvery = (int)(SampleRate / frequency);
            for(int i = 0; i < samples.Length; i++)
            {
                samples[i] = test(i, changeSoundEvery) * amplitude;
            }
        }

        private int test(int sampleIndex, int changeSoundEvery)
        {
            int result = 1;
            if (sampleIndex % changeSoundEvery > changeSoundEvery / 2)
                result = -1;
            return result;
        }
    }
}
