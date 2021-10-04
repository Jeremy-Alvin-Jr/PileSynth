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
            amplify.AddChildNode(new ConstantNode(int.MaxValue).CreateOutput());

            var outOscillator = new SquareOscillator();
            var inOscillator = new SquareOscillator();
            inOscillator.Frequency = new ConstantNode(2).CreateOutput();
            var modulationConversion = new LinearConversionNode(-1, 1, 100, 440);
            modulationConversion.PreviousValue = inOscillator.CreateOutput();
            outOscillator.Frequency = modulationConversion.CreateOutput();

            amplify.AddChildNode(outOscillator.CreateOutput());

            output.OutNode = amplify.CreateOutput();
            var outStream = new MemoryStream();
            output.GenerateWavToStream(outStream, 3);
            new WindowsSoundPlayer(outStream).PlaySync().Dispose();
        }
        
    }
}
