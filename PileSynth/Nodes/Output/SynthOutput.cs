using PileSynth.Nodes.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Output
{
    public class SynthOutput
    {
        private string outputName;
        private CustomSynthNode outputNode;

        public SynthOutput(CustomSynthNode node, string name)
        {
            outputNode = node;
            outputName = name;
        }

        public double CalculateValue()
        {
            return outputNode.GetOutput(outputName);
        }

        public void Recalculate(SynthInput i)
        {
            outputNode.Recalculate(i);
        }
    }
}
