using Demo_Project.Audio.Tools;
using PileSynth;
using PileSynth.Audio;
using PileSynth.Nodes;
using PileSynth.Nodes.Input;
using PileSynth.Nodes.Input.Generator.Oscillator;
using PileSynth.Nodes.Operation;
using PileSynth.Nodes.Operation.Basic;
using System;
using System.IO;
using System.Linq;

namespace Demo_Project
{
    static class Program
    {
        static void Main(string[] args)
        {
            var output = new MonoOut();

            var amplify = new MultiplicativeNode();
            amplify.AddChildNode(new ConstantNode(int.MaxValue));

            var outOscillator = new SquareOscillator();
            var inOscillator = new SquareOscillator();
            inOscillator.Frequency = new ConstantNode(105);
            var modulationConversion = new LinearConversionNode(-1, 1, 350, 440);
            modulationConversion.PreviousValue = inOscillator;
            outOscillator.Frequency = modulationConversion;

            amplify.AddChildNode(outOscillator);

            output.OutNode = amplify;
            var outStream = new MemoryStream();
            output.GenerateWavToStream(outStream, 1);
            new WindowsSoundPlayer(outStream).PlaySync().Dispose();
        }
        
    }
}
