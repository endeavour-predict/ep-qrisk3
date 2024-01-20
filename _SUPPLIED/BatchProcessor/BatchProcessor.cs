//
// (c) 2008-23 ClinRisk Ltd.
// All rights reserved.
//
// No unauthorised copying, distribution, modification, creating derived works, 
// or even compilation allowed, unless express permission has been granted by 
// ClinRisk Ltd.

using System;
using System.IO;

using QRISK3Engine;

namespace QRISK3BatchProcessor
{
    public class BatchProcessor
    {
        private static string version = "QRISK3 batch processor, .NET version 2018.0 - EH";

        private QRiskCVDAlgorithmCalculator calculator;
        private QRiskCVDResults results;

        public string inputFile = "input.csv";
        public string outputFile = "output.csv";

        private bool intToBool(int i) { if (i == 0) return false; else return true; }

        public string get_version()
        {
            return version;
        }
        public string get_risk_engine_version()
        {
            return QRiskCVDAlgorithmCalculator.version();
        }
        public string get_lookup_version()
        {
            return "No Townsend table";
        }
        public string get_reference_values_version()
        {
            return QRiskReferenceScores.version();
        }

        public BatchProcessor(string userName = null, string applicationKey = null)
        {
            // We instantiate the algorithm
            // The calculator object may raise an exception if, e.g. the risk engine is out of date, or a key has timed out...
            // That mechanism has been removed from this version for Endeavour Health.
            try
            {
                calculator = new QRiskCVDAlgorithmCalculator("", "");
            }
            catch (Exception e)
            {
                Console.WriteLine("e.g. This version of QRISK3 is now obsolete. Please check clinrisk.co.uk for an update.\n" + e);
            }
        }

        public void batchProcess()
        {
            StreamReader sr = null;
            StreamWriter sw = null;

            // "processed" is reported in one of the catch blocks
            int processed = 0;  

            try
            {
                if (!File.Exists(inputFile))
                {
                    throw new Exception("Input file '" + inputFile + "' does not exist.");
                }
                Console.WriteLine("Attempting to process '{0}' to '{1}'." + Environment.NewLine, inputFile, outputFile);
                // Open files
                sr = new StreamReader(inputFile);
                sw = new StreamWriter(outputFile);
                Console.WriteLine("Files opened for reading and writing." + Environment.NewLine);
                sw.WriteLine("# " + get_version());
                sw.WriteLine("# This implements the QRISK3 algorithm using");
                sw.WriteLine("#   " + QRiskCVDAlgorithmCalculator.version() + ", Townsend lookup table version " + get_lookup_version() + ", and " + get_reference_values_version());
                sw.WriteLine("#");
                sw.WriteLine("# Copyright (c) 2008-" + "23" + " ClinRisk Ltd.");
                sw.WriteLine("# All rights reserved.");
                sw.WriteLine("#");
                sw.WriteLine("# \"QRISK\" is a registered trademark of the University of Nottingham and EMIS.");
                sw.WriteLine("#");
                int count = 0;
                int blanks = 0;
                int comments = 0;
                int failed_to_process = 0;
                string line;
                bool first_line = true;
                string first_line_format = "row_id,b_cvd,sex,age,b_AF,b_atypicalantipsy,b_corticosteroids,b_impotence2,b_migraine,b_ra,b_renal,b_semi,b_sle,b_treatedhyp,diabetes_cat,bmi(nullable),ethnicity,fh_cvd,rati(nullable),sbp(nullable),sbps5(nullable),smoke_cat,town(nullable),postcode(nullable)";
                while ((line = sr.ReadLine()) != null)
                {
                    // check line is in the right format
                    // ignore blank lines
                    if (line.Trim() == "")
                    {
                        sw.WriteLine(line);
                        count++;
                        blanks++;
                        continue;
                    }
                    // ignore comments
                    if (line[0] == '#')
                    {
                        sw.WriteLine(line);
                        count++;
                        comments++;
                        continue;
                    }
                    // if it is the first "proper" line, then check that the headers are in the standard format
                    if (first_line)
                    {
                        if (line == first_line_format)
                        {
                            sw.WriteLine(line + ",inputValidity," + QRiskCVDResults.DummyString());
                            first_line = false;
                        }
                        else
                        {
                            sw.WriteLine("# Required header is either not present or incorrect.");
                            sw.WriteLine("#");
                            sw.WriteLine("# The following was found:");
                            sw.WriteLine(line);
                            sw.WriteLine("#");
                            sw.WriteLine("# The following is required: -- please check that your columns match!");
                            sw.WriteLine(first_line_format);
                            throw new HeaderException("There is a problem with the header row in the source file.  Please see the output file for details." + Environment.NewLine);
                        }
                        count++;
                        continue;
                    }
                    // this constructor performs validation on the csv line
                    // it checks that the appropriate types are on each line
                    inputParameters inputs = new inputParameters(line);

                    // if the types are as expected, then see if we can calculate a score
                    if (inputs.validity == inputValidity.valid)
                    {
                        /* 
                         * The inputs are valid, but there is still work to be done on them:
                         *  i.e process the postcode + townsend score combination
                         *    The logic implemented is
                         *     if postcode is present, look it up:
                         *        case 1:  postcode not in table => use town=null
                         *        case 2:  postcode in table, value unknown => use town=null
                         *        case 3:  postcode in table, value known => use value
                         *     if postcode is not present, and townsend is present use this.

                         *     do we want to record this state in the output? probably!
                         */
                        try
                        {
                            if (inputs.postcode != "")
                            {
                                //// We use these with the Townsend class if we have a Townsend table in our implementation
                                // string hash = Townsend.GetHash(inputs.postcode);
                                // inputs.town = Townsend.GetEncryptedTownsend(hash);

                                // For this version we use the default Townsend score of zero
                                inputs.town = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Couldn't read the Townsend lookup table: " + ex.Message);
                            throw new Exception("Townsend table problem");
                        }
                        // get score

                        results = calculator.calculate(
                            inputs.b_cvd,
                            inputs.sex,
                            inputs.age,
                            inputs.b_AF,
                            inputs.b_atypicalantipsy,
                            inputs.b_corticosteroids,
                            inputs.b_impotence2,
                            inputs.b_migraine,
                            inputs.b_ra,
                            inputs.b_renal,
                            inputs.b_semi,
                            inputs.b_sle,
                            inputs.b_treatedhyp,
                            inputs.diabetes_cat,
                            inputs.bmi,
                            inputs.ethnicity,
                            inputs.fh_cvd,
                            inputs.rati,
                            inputs.sbp,
                            inputs.sbps5,
                            inputs.smoke_cat,
                            inputs.town
                            );
                        
                        line += ",inputs:" + inputs.validity + "," + "results:" + results.ToString();
                        processed++;
                    }
                    else
                    {
                        // mark invalid
                        line += ",inputs:" + inputs.validity + "," + QRiskCVDResults.DummyString();
                        failed_to_process++;
                    }
                    sw.WriteLine(line);
                    // tick for large files
                    if ((count % 20000) == 0)
                    {
                        Console.WriteLine("Lines processed: " + count);
                    }
                    count++;
                }
                Console.WriteLine("Finished processing: {0} lines read:", count);
                Console.WriteLine("  ({0} successfully processed, {1} failed, {2} blanks or comments, 1 header).", processed, failed_to_process, blanks + comments);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine("Input file could not be found: " + ex.Message);
            }
            catch (HeaderException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General exception: " + ex.Message + "Input line: " + processed);
            }
            finally
            {
                // Close the files, if open
                if (sr != null)
                {
                    sr.Close();
                }
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        class HeaderException : System.Exception
        {
            public HeaderException(string message) : base(message)
            {
            }
        }
    }
}