using PileSynth.Nodes.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Operation
{
    public class LinearConversionNode : CustomSynthNode
    {
        public CustomSynthNode PreviousValue
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
        protected override double CalculateNode()
        {
            double previousRange = PreviousRangeMax - PreviousRangeMin;
            double newRange = NewRangeMax - NewRangeMin;
            double previousValue = PreviousValue.GetValue();
            return (previousValue - PreviousRangeMin) * newRange / previousRange + NewRangeMin;
        }

        public LinearConversionNode() {
            ChildNodes.Add(new ConstantNode());
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
