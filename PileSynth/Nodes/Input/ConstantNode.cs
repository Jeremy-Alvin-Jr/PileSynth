using PileSynth.Nodes.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Input
{
    public class ConstantNode : CustomSynthNode
    {
        public double Value = 0;
        protected override void CalculateOutputs()
        {
            SetOutput("output", Value);
        }
        public ConstantNode(double value)
        {
            Value = value;
        }

        public ConstantNode() { }

        public SynthOutput CreateOutput()
        {
            return new SynthOutput(this, "output");
        }
    }
}
