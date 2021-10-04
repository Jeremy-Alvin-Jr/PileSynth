using PileSynth.Nodes.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Input.Generator.Oscillator
{
    public abstract class OscillatorNode : CustomSynthNode
    {
        public SynthOutput Frequency
        {
            private get => ChildNodes[0];
            set
            {
                ChildNodes[0] = value;
            }
        }

        private double progress = 0;
        private double lastTime = -1;

        protected override void CalculateOutputs()
        {
            if (lastTime == -1)
                lastTime = Input.TimeInSeconds;
            progress += (Input.TimeInSeconds - lastTime) * Frequency.CalculateValue();
            lastTime = Input.TimeInSeconds;
            progress -= (int)(progress);
            SetOutput("output", GenerateValue(progress));
        }

        protected abstract double GenerateValue(double time);

        public SynthOutput CreateOutput()
        {
            return new SynthOutput(this, "output");
        }

        public OscillatorNode()
        {
            ChildNodes.Add(new ConstantNode(440).CreateOutput());
        }
    }
}
