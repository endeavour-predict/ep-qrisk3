using CRStandardDefinitions;
using QRisk3.Engine.Tests.Models;
using QRISK3Engine;

namespace QRisk3.Engine.Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        // Create an instace of the supplied engine
        protected QRiskCVDAlgorithmCalculator engine = new QRiskCVDAlgorithmCalculator("", "");

        protected QRiskCVDResults RunCalc(QRiskInputModel inputModel)
        {
            return engine.calculate(
                                            b_cvd: inputModel.CVD,
                                            sex: inputModel.sex,
                                            age: inputModel.age,
                                            b_AF: inputModel.atrialFibrillation,
                                            b_atypicalantipsy: inputModel.atypicalAntipsychoticMedication,
                                            b_corticosteroids: inputModel.systemicCorticosteroids,
                                            b_impotence2: inputModel.impotence,
                                            b_migraine: inputModel.migraines,
                                            b_ra: inputModel.rheumatoidArthritis,
                                            b_renal: inputModel.chronicRenalDisease,
                                            b_semi: inputModel.severeMentalIllness,
                                            b_sle: inputModel.systemicLupusErythematosus,
                                            b_treatedhyp: inputModel.bloodPressureTreatment,
                                            diabetes_cat: inputModel.diabetesStatus,
                                            bmi: inputModel.BMI,
                                            ethnicity: inputModel.ethnicity,
                                            fh_cvd: inputModel.familyHistoryCHD,
                                            rati: inputModel.cholesterolRatio,
                                            sbp: inputModel.systolicBloodPressureMean,
                                            sbps5: inputModel.systolicBloodPressureStDev,
                                            smoke_cat: inputModel.smokingStatus,
                                            town: inputModel.townsendScore
                                          );
        }
    }
}