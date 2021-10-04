using PileSynth.Nodes.Input;
using PileSynth.Nodes.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Operation
{
    public class LinearConversionNode : CustomSynthNode
    {
        public SynthOutput PreviousValue
        {
            get => ChildNodes[0];
            set
            {
                ChildNodes[0] = value;
            }
        }
        public double PreviousRangeMin = -1;
        public double PreviousRangeMax = 1;
        public double NewRangeMin = 0;
        public double NewRangeMax = 1;

        public SynthOutput NewValue;
        protected override void CalculateOutputs()
        {
            double previousRange = PreviousRangeMax - PreviousRangeMin;
            double newRange = NewRangeMax - NewRangeMin;
            double previousValue = PreviousValue.CalculateValue();
            SetOutput("output", (previousValue - PreviousRangeMin) * newRange / previousRange + NewRangeMin);
        }

        public SynthOutput CreateOutput()
        {
            return new SynthOutput(this, "output");
        }

        public LinearConversionNode() {
            ChildNodes.Add(new ConstantNode().CreateOutput());
        }
        public LinearConversionNode(double previousRangeMin, double previousRangeMax) : this()
        {
            PreviousRangeMin = previousRangeMin;
            PreviousRangeMax = previousRangeMax;
        }

        public LinearConversionNode(double previousRangeMin, double previousRangeMax, double newRangeMin, double newRangeMax) : this(previousRangeMin, previousRangeMax)
        {
            NewRangeMin = newRangeMin;
            NewRangeMax = newRangeMax;
        }
    }
}
