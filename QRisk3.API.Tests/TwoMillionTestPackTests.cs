using System.Runtime.InteropServices;
using CRStandardDefinitions;
using QRisk3.Engine.Tests.Models;
using QRISK3Engine;

namespace QRisk3.Engine.Tests
{
    [TestFixture]
    public class TwoMillionTestPackTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void QRisk3_TestPack()
        {
            /***************
            1. Arrange
            ***************/

            // Create an instace of the supplied engine
            var engine = new QRiskCVDAlgorithmCalculator("", "");

            // Get the 2M row test pack from the test_packs dependency            
            var csv = test_packs.Resources.QRisk_2e6;
            /***************
              2. Act
             ***************/
            
            // Read the test file..            
            // Call the Engine for each row and compare the Engine result to the testfile result
            int rowsProcessed = 0;
            int rowsMatched = 0;
            int rowsFailed = 0;
            
            int headerRows = 1;
            foreach (var row in csv.Skip(headerRows)
                .TakeWhile(r => r.Length > 1 && r.Last().Trim().Length > 0))
            {                
                string rowId = row[0];
                var inputModel = new QRiskInputModel(row);                
                var expectedscore= Double.Parse(row[42]);
                var calcResult = RunCalc(inputModel);
                var engineScore = calcResult.score;
                rowsProcessed++;
                if (engineScore == expectedscore)
                {
                    rowsMatched++;
                }
                else
                {
                    rowsFailed++;                                        
                }
            }


            /***************
              3. Assert
             ***************/
            Assert.IsTrue(rowsProcessed == 2000000);
            Assert.IsTrue(rowsProcessed == (rowsMatched + rowsFailed));
            Assert.IsTrue(rowsMatched == rowsProcessed);
            Assert.IsTrue(rowsFailed == 0);
        }
    }
}