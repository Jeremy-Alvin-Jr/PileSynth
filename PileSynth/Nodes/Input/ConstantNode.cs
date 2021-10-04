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
        protected override double CalculateNode()
        {
            return Value;
        }

        public ConstantNode()
        {

        }
        public ConstantNode(double value)
        {
            Value = value;
        }
    }
}
