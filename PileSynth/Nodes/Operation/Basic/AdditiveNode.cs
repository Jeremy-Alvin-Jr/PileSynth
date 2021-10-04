using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Operation.Basic
{
    public class AdditiveNode : AssociativeOperationNode
    {
        protected override double BinaryOperation(double total, double a)
        {
            return total + a;
        }
    }
}
