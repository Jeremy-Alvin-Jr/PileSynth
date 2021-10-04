using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Input
{
    public class InstrumentInput : SynthInput
    {
        private double note = 0;
        public double Frequency { get; private set; } = 440;
        public int Note
        {
            get => (int)note;
            set
            {
                note = value;
                Frequency = NoteToFrequency(value);
            }
        }

        public enum MusicalKey
        {
            NOTE_C = -9, NOTE_Db = -8, NOTE_D = -7, NOTE_Eb = -6,
            NOTE_E = -5, NOTE_F = -4, NOTE_Gb = -3, NOTE_G = -2,
            NOTE_Ab = -1, NOTE_A = 0, NOTE_Bb = 1, NOTE_B = 2
        }

        private static readonly double[] frequencyOfNode = {440, 466.1638, 493.8833, 523.2511,
                                                            554.3653, 587.3295, 622.2540, 659.2551,
                                                            698.4565, 739.9888, 783.9909, 830.6094};
        private double NoteToFrequency(int note)
        {
            int noteType = (note % frequencyOfNode.Length + frequencyOfNode.Length) % frequencyOfNode.Length;
            double result = frequencyOfNode[noteType];
            int scale = 0;
            if (note < 0)
                scale = (note - frequencyOfNode.Length + 1) / frequencyOfNode.Length;
            else
                scale = note / frequencyOfNode.Length;

            if (scale >= 0)
                result *= 1 << scale;
            else
                result /= 1 << (-scale);
            return result;
        }

        protected override double? GetInput(string inputName)
        {
            if (inputName == "instrument")
                return 1;
            if (inputName == "instrument.note")
                return Note;
            if (inputName == "instrument.frequency")
                return Frequency;
            return null;
        }
    }
}
