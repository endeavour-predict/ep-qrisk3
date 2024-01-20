using CRStandardDefinitions;
using QRisk3.Engine.Tests.Models;
using QRISK3Engine;
using System.ComponentModel.DataAnnotations;

namespace QRisk3.Engine.Tests
{
    [TestFixture]
    public class InputModelTests : BaseTest
    {                
        [Test]
        public void Param_CVD()
        {            
            var inputModel = new QRiskInputModel();
            inputModel.age = 30;

            // CVD = true = expect this to not be able to produce a score
            inputModel.CVD = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            var engineScore = calcResult.score;            
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(calcResult.reason == ReasonInvalid.ALREADY_HAD_A_CVD_EVENT);
            Assert.IsTrue(engineScore == 0);

            // CVD = false
            inputModel.CVD = false;
            calcResult = RunCalc(inputModel);
            engineScore = calcResult.score;            
            double expectedScore = 0.194826;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);            
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_sex()
        {
            var inputModel = new QRiskInputModel();
            inputModel.age = 30;
            inputModel.CVD = false;

            // Male test
            inputModel.sex = Gender.Male;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.333064;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Female test
            inputModel.sex = Gender.Female;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_age()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;

            // Age OK            
            inputModel.age = 30;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Age too low
            inputModel.age = 10;            
            calcResult = RunCalc(inputModel);
            expectedScore = 0;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(calcResult.reason == ReasonInvalid.AGE_OUT_OF_RANGE);
            Assert.IsTrue(engineScore == expectedScore);

            // Age too high
            inputModel.age = 85;
            expectedScore = 0;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(calcResult.reason == ReasonInvalid.AGE_OUT_OF_RANGE);
            Assert.IsTrue(engineScore == expectedScore);

            // Age negative
            inputModel.age = -40;
            expectedScore = 0;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA);
            Assert.IsTrue(calcResult.reason == ReasonInvalid.AGE_OUT_OF_RANGE);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_atrialFibrillation()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.atrialFibrillation = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 3.305394;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.atrialFibrillation = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_atypicalAntipsychoticMedication()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.atypicalAntipsychoticMedication = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.250686;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.atypicalAntipsychoticMedication = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_systemicCorticosteroids()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.systemicCorticosteroids = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.391264;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.systemicCorticosteroids = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_impotence()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;
            inputModel.sex = Gender.Male;

            inputModel.impotence = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.37908;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.impotence = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.333064;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_migraines()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;            

            inputModel.migraines = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.318029;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.migraines = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_rheumatoidArthritis()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.rheumatoidArthritis = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.241174;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.rheumatoidArthritis = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_chronicRenalDisease()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.chronicRenalDisease = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.410681;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.chronicRenalDisease = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_severeMentalIllness()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.severeMentalIllness = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.22086;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.severeMentalIllness = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_systemicLupusErythematosus()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.systemicLupusErythematosus = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 1.166514;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.systemicLupusErythematosus = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_bloodPressureTreatment()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.bloodPressureTreatment = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.766941;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.bloodPressureTreatment = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_diabetesStatus()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.diabetesStatus = DiabetesCat.None;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.diabetesStatus = DiabetesCat.Type1;
            calcResult = RunCalc(inputModel);
            expectedScore = 1.578742;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.diabetesStatus = DiabetesCat.Type2;
            calcResult = RunCalc(inputModel);
            expectedScore = 1.051386;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_BMI()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            // Missing - we expect a predicted BMI of 25.7 for this input
            inputModel.BMI = null;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.bmi.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.bmi.substitute_value == 25.7);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Negative - we expect the calc to work, but for the min BMI value to be substituted in 
            inputModel.BMI = -10;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.166498;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.bmi.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.bmi.substitute_value == Constants.minBmi);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Too low - we expect the calc to work, but for the min BMI value to be substituted in 
            inputModel.BMI = 17;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.166498;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.bmi.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.bmi.substitute_value == Constants.minBmi);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // OK (in range)
            inputModel.BMI = 20;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.166498;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.bmi.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // OK (in range)
            inputModel.BMI = 40;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.285423;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.bmi.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Too high - we expect the calc to work, but for the max BMI value to be substituted in 
            inputModel.BMI = 49;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.285423;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.bmi.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.bmi.substitute_value == Constants.maxBmi);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

        }

        [Test]
        public void Param_ethnicity()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            // null (missing) - we expect a score with a substitution
            inputModel.ethnicity = new Ethnicity(); // null
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.ethnicity.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.ethnicity.substitute_value == "British");
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.ethnicity = Ethnicity.Indian;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.242052;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.ethnicity = Ethnicity.OtherBlack;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.158099;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_familyHistoryCHD()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            inputModel.familyHistoryCHD = true;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.359858;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.familyHistoryCHD = false;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.194826;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_cholesterolRatio()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            // Missing - we expect a substitution of 3.4 from the predictor for this input
            inputModel.cholesterolRatio = null;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.ratio.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.ratio.substitute_value == 3.4);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Negative - we expect the calc to work, but for the min value to be substituted in 
            inputModel.cholesterolRatio = -10;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.134867;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.ratio.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.ratio.substitute_value == Constants.minRati);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Too low - we expect the calc to work, but for the min value to be substitutedin
            inputModel.cholesterolRatio = 0.5;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.134867;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.ratio.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.ratio.substitute_value == Constants.minRati);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // OK (in range)
            inputModel.cholesterolRatio = 1;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.134867;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.ratio.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // OK (in range)
            inputModel.cholesterolRatio = 12;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.726694;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.ratio.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Too high - we expect the calc to work, but for the max value to be substituted in 
            inputModel.cholesterolRatio = 13;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.726694;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.ratio.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.ratio.substitute_value == Constants.maxRati);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }

        [Test]
        public void Param_systolicBloodPressureMean()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            // Missing - we expect a substitution of 116.3 from the predictor for this input
            inputModel.systolicBloodPressureMean = null;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbp.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.sbp.substitute_value == 116.3);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Negative - we expect the calc to work, but for the min value to be substituted in 
            inputModel.systolicBloodPressureMean = -10;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.088252;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbp.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.sbp.substitute_value == Constants.minSbp);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Too low - we expect the calc to work, but for the min value to be substitutedin
            inputModel.systolicBloodPressureMean = 69;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.088252;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbp.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.sbp.substitute_value == Constants.minSbp);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // OK (in range)
            inputModel.systolicBloodPressureMean = 71;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.089775;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbp.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // OK (in range)
            inputModel.systolicBloodPressureMean = 120;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.207549;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbp.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Too high - we expect the calc to work, but for the max value to be substituted in 
            inputModel.systolicBloodPressureMean = 221;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.964818;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbp.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.sbp.substitute_value == Constants.maxSbp);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_systolicBloodPressureStDev()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            // Missing - 
            inputModel.systolicBloodPressureStDev = null;
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;            
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Unlike the other numeric input, systolicBloodPressureStDev does't have a min/max allowable value
            // so test some extremes and check against known results

            // Negative - this is OK
            inputModel.systolicBloodPressureStDev = -100;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.088561;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbps5.data == Data.OK);
            Assert.IsTrue(calcResult.DataQuality.sbps5.substitute_value == 0);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // Massive numbers are OK for StDev too..
            inputModel.systolicBloodPressureMean = 2000;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.439499;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbps5.data == Data.OK);
            Assert.IsTrue(calcResult.DataQuality.sbps5.substitute_value == 0);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // test a sensible values too
            inputModel.systolicBloodPressureMean = 10;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.040105;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.sbps5.data == Data.OK);
            Assert.IsTrue(calcResult.DataQuality.sbps5.substitute_value == 0);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }



        [Test]
        public void Param_smokingStatus()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            // null (missing) - we expect a score with no  substitution
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.smokingStatus.data == Data.OK);
            Assert.IsTrue(calcResult.DataQuality.smokingStatus.substitute_value == "");
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.smokingStatus = SmokeCat.HeavySmoker;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.639019;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.smokingStatus.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            inputModel.smokingStatus = SmokeCat.LightSmoker;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.343795;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.smokingStatus.data == Data.OK);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


        [Test]
        public void Param_townsend()
        {
            var inputModel = new QRiskInputModel();
            inputModel.CVD = false;
            inputModel.age = 30;

            // null (missing) - we expect a score with no  substitution
            QRiskCVDResults calcResult = RunCalc(inputModel);
            double expectedScore = 0.194826;
            double engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.townsend.data == Data.MISSING);
            Assert.IsTrue(calcResult.DataQuality.townsend.substitute_value == 0);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // too low, look for min substitution
            inputModel.townsendScore = -10;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.120847;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.townsend.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.townsend.substitute_value == Constants.minTown);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // too high, look for max substitution
            inputModel.townsendScore = 14;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.398621;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.townsend.data == Data.OUT_OF_RANGE);
            Assert.IsTrue(calcResult.DataQuality.townsend.substitute_value== Constants.maxTown);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);

            // just right...
            inputModel.townsendScore = 4;
            calcResult = RunCalc(inputModel);
            expectedScore = 0.247353;
            engineScore = calcResult.score;
            Assert.IsTrue(calcResult.DataQuality.townsend.data == Data.OK);
            Assert.IsTrue(calcResult.DataQuality.townsend.substitute_value == 0);
            Assert.IsTrue(calcResult.resultStatus == ResultStatus.CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA);
            Assert.IsTrue(engineScore == expectedScore);
        }


    }
}