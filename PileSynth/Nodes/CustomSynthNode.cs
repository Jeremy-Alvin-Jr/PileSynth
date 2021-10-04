using PileSynth.Nodes.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes
{
    public abstract class CustomSynthNode
    {
        public SynthInput Input { get; private set; }

        protected List<CustomSynthNode> ChildNodes = new List<CustomSynthNode>();

        private double value;
        private bool calculated = false;

        public void Recalculate(SynthInput i)
        {
            calculated = false;
            Input = i;
            foreach(var child in ChildNodes)
            {
                child.Recalculate(i);
            }
        }
        public double GetValue()
        {
            if (calculated)
                return value;
            value = CalculateNode();
            calculated = true;
            return value;
        }
        protected abstract double CalculateNode();
    }
}
