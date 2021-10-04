using PileSynth.Nodes.Input;
using PileSynth.Nodes.Output;
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

        protected List<SynthOutput> ChildNodes = new List<SynthOutput>();

        private bool calculated = false;
        private Dictionary<string, double> outputs = new Dictionary<string, double>();

        public void Recalculate(SynthInput i)
        {
            calculated = false;
            Input = i;
            foreach(var child in ChildNodes)
            {
                child.Recalculate(i);
            }
        }

        public double GetOutput(string outputName)
        {
            RefreshValues();
            return outputs[outputName];
        }

        protected void SetOutput(string outputName, double value)
        {
            outputs[outputName] = value;
        }

        private void RefreshValues()
        {
            if (calculated)
                return;
            CalculateOutputs();
            calculated = true;
        }
        protected abstract void CalculateOutputs();
    }
}
