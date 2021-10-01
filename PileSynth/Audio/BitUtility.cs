using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Audio
{
    public static class BitUtility
    {
        public static byte ConvertSampleToByte(int sample)
        {
            int result = sample >> 24;
            result += 128;
            return (byte)result;
        }

        public static byte[] ConvertSampleToByteArray(int[] samples)
        {
            byte[] results = new byte[samples.Length];
            for(int i = 0; i < samples.Length; i++)
            {
                results[i] = ConvertSampleToByte(samples[i]);
            }
            return results;
        }

        public static short ConvertSampleToShort(int sample)
        {
            int result = sample >> 16;
            return (short)result;
        }

        public static short[] ConvertSampleToShortArray(int[] samples)
        {
            short[] results = new short[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                results[i] = ConvertSampleToShort(samples[i]);
            }
            return results;
        }
    }
}
