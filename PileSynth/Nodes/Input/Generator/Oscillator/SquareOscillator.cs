using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Input.Generator.Oscillator
{
    public class SquareOscillator : OscillatorNode
    {
        protected override double GenerateValue(double time)
        {
            if (time > 0.5)
                return 1;
            return -1;
        }
    }
}
