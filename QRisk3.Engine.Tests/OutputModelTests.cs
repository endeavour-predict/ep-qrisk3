using CRStandardDefinitions;
using QRisk3.Engine.Tests.Models;
using QRISK3Engine;
using System.ComponentModel.DataAnnotations;

namespace QRisk3.Engine.Tests
{
    [TestFixture]
    public class OutputModelTests : BaseTest
    {                
        [Test]
        public void Output_resultStatus()
        {            
            var inputModel = new QRiskInputModel();
            inputModel.age = 30;

            // CVD = true = expect this to not be able to produce a score and return: NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA
            inputModel.CVD = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            var engineScore = calcResult.score;            
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(engineScore == 0);


            // full set of input data, we want to see: CALCULATED_USING_PATIENTS_OWN_DATA
            inputModel.CVD = false;
            inputModel.familyHistoryCHD = false;
            inputModel.systemicLupusErythematosus = false;
            inputModel.diabetesStatus = DiabetesCat.None;
            inputModel.atrialFibrillation = false;
            inputModel.atypicalAntipsychoticMedication = false;
            inputModel.bloodPressureTreatment = false;
            inputModel.BMI = 30;
            inputModel.cholesterolRatio = 10;
            inputModel.chronicRenalDisease = false;
            inputModel.ethnicity = Ethnicity.Bangladeshi;
            inputModel.impotence = false;
            inputModel.sex = Gender.Male;
            inputModel.rheumatoidArthritis = false;
            inputModel.migraines = false;
            inputModel.smokingStatus = SmokeCat.HeavySmoker;
            inputModel.systemicCorticosteroids = false;
            inputModel.systolicBloodPressureMean = 190;
            inputModel.systolicBloodPressureStDev = 10;
            inputModel.townsendScore = 5;

            calcResult = RunCalc(inputModel);
            engineScore = calcResult.score;
            double expectedScore = 14.750226;
            engineScore = calcResult.score;  
            
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_PATIENTS_OWN_DATA);
            Assert.IsTrue(engineScore == expectedScore);

        }

        [Test]
        public void Output_reason()
        {
            var inputModel = new QRiskInputModel();
            inputModel.age = 30;

            // CVD = true = expect this to not be able to produce a score and return: NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA
            inputModel.CVD = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            var engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(calcResult.reason == ReasonInvalid.ALREADY_HAD_A_CVD_EVENT);
            Assert.IsTrue(engineScore == 0);


            // we can provide an age < min since it's a number
            inputModel.CVD = false;
            inputModel.age = 10;
            calcResult = RunCalc(inputModel);
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(calcResult.reason == ReasonInvalid.AGE_OUT_OF_RANGE);
            Assert.IsTrue(engineScore == 0);

            // also test > max, this should also not be able to run a calculation
            inputModel.CVD = false;
            inputModel.age = 85;
            calcResult = RunCalc(inputModel);
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(calcResult.reason == ReasonInvalid.AGE_OUT_OF_RANGE);
            Assert.IsTrue(engineScore == 0);

        }


        [Test]
        public void Output_score()
        {
            // although this is tested in many of the other unit tests, let's have a single test here that looks for a known score
            var inputModel = new QRiskInputModel();
            inputModel.age = 60;
            double expectedScore = 5.047293;            
            inputModel.CVD = false;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            var engineScore = calcResult.score;
            Assert.IsTrue(engineScore == expectedScore);

        }


        [Test]
        public void Output_typicalScore()
        {
            var inputModel = new QRiskInputModel();
            inputModel.age = 60;
            double expectedTypicalScore = 5.1;
            inputModel.CVD = false;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            var engineTypicalScore = calcResult.typical_score;
            Assert.IsTrue(engineTypicalScore == expectedTypicalScore);
        }


        [Test]
        public void Output_dataQuality()
        {
            var inputModel = new QRiskInputModel();
            inputModel.age = 60;
            inputModel.CVD = false;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            
            Assert.IsTrue(calcResult.DataQuality.townsend.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.ratio.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.sbp.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.bmi.data == Data.MISSING);            
            Assert.IsTrue(calcResult.DataQuality.sbps5.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.smokingStatus.data == Data.OK);
            Assert.IsTrue(calcResult.DataQuality.ethnicity.data == Data.MISSING);

            Assert.IsTrue(calcResult.DataQuality.townsend.substitute_value== 0);
            Assert.IsTrue(calcResult.DataQuality.ratio.substitute_value == 3.5);
            Assert.IsTrue(calcResult.DataQuality.sbp.substitute_value == 130.8);
            Assert.IsTrue(calcResult.DataQuality.bmi.substitute_value == 26.9);            
            Assert.IsTrue(calcResult.DataQuality.sbps5.substitute_value == 0);
            Assert.IsTrue(calcResult.DataQuality.smokingStatus.substitute_value == "");
            Assert.IsTrue(calcResult.DataQuality.ethnicity.substitute_value == "British");

        }

    }
}