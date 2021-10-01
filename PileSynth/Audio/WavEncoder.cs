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

        private Stream stream;
        private int[] samples;

        public WavEncoder(byte[] dataBuffer)
        {

        }
    }
}
