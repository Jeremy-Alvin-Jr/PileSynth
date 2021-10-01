using Demo_Project.Audio.Tools;
using PileSynth;
using PileSynth.Audio;
using System;
using System.IO;
using System.Linq;
using System.Media;

namespace Demo_Project
{
    static class Program
    {
        static void Main(string[] args)
        {
            Stream s = new MemoryStream();
            WavEncoder e = new WavEncoder(null);
            e.CreateCoolSamples(440, 1, int.MaxValue / 2);
            e.EncodeToStream(s);
            new WindowsSoundPlayer(s).PlaySync();
        }
        
    }
}
