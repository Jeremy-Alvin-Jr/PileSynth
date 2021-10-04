using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Output
{
    public class SynthOutput
    {
        private CustomSynthNode owner;
        public double Value { get; internal set; }

        public SynthOutput(CustomSynthNode ownerNode)
        {
            owner = ownerNode;
        }
    }
}
