using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Operation
{
    public abstract class AssociativeOperationNode : CustomSynthNode
    {
        protected sealed override double CalculateNode()
        {
            if (ChildNodes.Count == 0)
                return 0;
            double total = ChildNodes[0].GetValue();
            for(int i = 1; i < ChildNodes.Count; i++)
            {
                total = BinaryOperation(total, ChildNodes[i].GetValue());
            }
            return total;
        }

        public void AddChildNode(CustomSynthNode node)
        {
            ChildNodes.Add(node);
        }

        protected abstract double BinaryOperation(double total, double a);
    }
}
