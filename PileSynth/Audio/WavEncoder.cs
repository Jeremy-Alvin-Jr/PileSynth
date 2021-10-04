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
        public class Settings
        {
            public uint SampleRate = 44100;
            public AudioResolution SampleResolution = AudioResolution.UNSIGNED_8_BIT;
        }
        public Settings QualitySettings = new Settings();
        public ushort ChannelsNumber = 2;

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
            writer.Write(QualitySettings.SampleRate);
            writer.Write(QualitySettings.SampleRate * (ushort)QualitySettings.SampleResolution * ChannelsNumber / 8);
            writer.Write((ushort)((ushort)QualitySettings.SampleResolution * ChannelsNumber / 8));
            writer.Write((ushort)QualitySettings.SampleResolution);
        }

        private void WriteDataChunk(BinaryWriter writer)
        {
            if(QualitySettings.SampleResolution == AudioResolution.UNSIGNED_8_BIT)
            {
                writer.Write(BitUtility.ConvertSampleToByteArray(samples));
            } else if(QualitySettings.SampleResolution == AudioResolution.SIGNED_16_BIT)
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
    }
}
