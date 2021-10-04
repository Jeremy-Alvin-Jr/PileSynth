using PileSynth.Nodes.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Operation
{
    public abstract class AssociativeOperationNode : CustomSynthNode
    {
        protected sealed override void CalculateOutputs()
        {
            if (ChildNodes.Count == 0)
                return;
            double total = ChildNodes[0].CalculateValue();
            for(int i = 1; i < ChildNodes.Count; i++)
            {
                total = BinaryOperation(total, ChildNodes[i].CalculateValue());
            }
            SetOutput("output", total);
        }

        public void AddChildNode(SynthOutput node)
        {
            ChildNodes.Add(node);
        }

        public SynthOutput CreateOutput()
        {
            return new SynthOutput(this, "output");
        }

        protected abstract double BinaryOperation(double total, double a);
    }
}
