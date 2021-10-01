using PileSynth.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Project.Audio.Tools
{
    class WindowsSoundPlayer : IDisposable
    {
        private SoundPlayer player;
        private Stream stream;
        public void Dispose()
        {
            if(player != null)
                player.Dispose();
            if (stream != null)
                stream.Dispose();
        }

        public WindowsSoundPlayer(Stream buffer)
        {
            player = new SoundPlayer();
            player.Stream = buffer;
        }

        public WindowsSoundPlayer(int[] buffer)
        {
            var encoder = new WavEncoder(buffer);
            stream = new MemoryStream();
            encoder.EncodeToStream(stream);

            player = new SoundPlayer();
            player.Stream = stream;
            stream.Dispose();
        }

        public WindowsSoundPlayer PlayAsync(bool looping = false)
        {
            if (looping)
                player.PlayLooping();
            else
                player.Play();
            return this;
        }

        public WindowsSoundPlayer PlaySync()
        {
            player.PlaySync();
            return this;
        }

        public WindowsSoundPlayer Stop()
        {
            player.Stop();
            return this;
        }
    }
}
