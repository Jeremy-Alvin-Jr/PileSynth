using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileSynth.Nodes.Input
{
    public class SynthInput
    {
        public double TimeInSeconds { get; protected internal set; }

        public bool TryGetInput(string inputName, out double input)
        {
            input = 0;
            double? result = GetInput(inputName.ToLower().Replace(" ", null));
            if (result == null)
                return false;
            input = (double)result;
            return true;
        }

        protected virtual double? GetInput(string inputName)
        {
            return null;
        }
    }
}
