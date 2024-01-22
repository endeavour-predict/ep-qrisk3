using ep_models;

namespace QRisk3.Engine.Tests
{
    [TestFixture]
    public class ParamTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void QRisk3_FileBasedParamTests()
        {            
            var tests = test_packs.QRisk3_Resources.FileTests;            
            var service = new ep_service.PredictionService();

            foreach (var test in tests)
            {
                var actual_serviceResult = service.GetScore(test.EPInputModel);
                var actual_engineScores = actual_serviceResult.EngineResults.Where(p => p.EngineName == Core.EPStandardDefinitions.Engines.QRisk3).Single();
                var actual_QRisk3Score = actual_engineScores.Results.Where(p => p.id.ToString() == Globals.QRiskScoreUri).Single();
                var actual_QRisk3HeartAgeScore = actual_engineScores.Results.Where(p => p.id.ToString() == Globals.QRiskScoreUri + "HeartAge").SingleOrDefault();

                var expected_serviceResult = test.PredictionModel;
                var expected_engineScores = expected_serviceResult.EngineResults.Where(p => p.EngineName == Core.EPStandardDefinitions.Engines.QRisk3).Single();
                var expected_QRisk3Score = expected_engineScores.Results.Where(p => p.id.ToString() == Globals.QRiskScoreUri).Single();
                var expected_QRisk3HeartAgeScore = expected_engineScores.Results.Where(p => p.id.ToString() == Globals.QRiskScoreUri + "HeartAge").SingleOrDefault();

                Assert.IsTrue(expected_QRisk3Score.score == actual_QRisk3Score.score, test.TestName);
                Assert.IsTrue(expected_QRisk3HeartAgeScore.score == actual_QRisk3HeartAgeScore.score, test.TestName);
            }
                        
        }
    }
}