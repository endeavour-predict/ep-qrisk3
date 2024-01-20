//
// (c) 2008-23 ClinRisk Ltd.
// All rights reserved.
//
// No unauthorised copying, distribution, modification, creating derived works, 
// or even compilation allowed, unless express permission has been granted by 
// ClinRisk Ltd.

using System;
using System.IO;

namespace QRISK3BatchProcessor
{
    class Program
    {
         static void Main(string[] args)
        {
            var program = new Program();

            if (args.Length != 2)
            {
                Console.WriteLine("Usage: QRISK3BatchProcessor <input.csv> <output.csv>");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }

            Console.WriteLine("Instantiating BP");
            var bp = new BatchProcessor("not", "needed");

            Console.WriteLine("Batch processing...");
            bp.inputFile = args[0];
            bp.outputFile = args[1];
            bp.batchProcess();

        }
    }
}
