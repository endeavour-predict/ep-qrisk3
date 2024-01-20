using QRisk_API.Models;
using QRISK3Engine;
using RestSharp;
using System.Diagnostics;

namespace QRisk_API.Services
{

    /// <summary>
    /// Method and supporting functions to call the QRisk3 calculator.
    /// Escapsulated in this PUBLIC class so it can be called from nUnit AND the API Controller
    /// </summary>
    public class CalculationService
    {
        public void PerformQRiskCalculation(QRiskInputModel inputModel, string CalculatorVersion, string ApiVersion,                                            
                                            out bool performedSBPCalc, out DataQuality SBPListQuality, out Prediction outputModel)
        {            
            var meta = new Meta();
            meta.ApiVersion = ApiVersion;
            meta.ApiTimeStampUTC = DateTime.UtcNow;
            meta.CalculatorVersion = CalculatorVersion;

            // Model Pre-Validation passed, we can call the calculator to do further validation and value substitution
            var calc = new QRiskCVDAlgorithmCalculator("", "");

            // SBP: If provided we use the mean and StDev, If not we calculate it from the list of SBPs
            Double? meanSBP = inputModel.systolicBloodPressureMean;
            Double? stDev = inputModel.systolicBloodPressureStDev;
            performedSBPCalc = false;
            SBPListQuality = new DataQuality();
            if (!meanSBP.HasValue && inputModel.systolicBloodPressures != null)
            {
                meanSBP = inputModel.systolicBloodPressures.Average();
                stDev = inputModel.systolicBloodPressures.StandardDeviation();
                // we have performed a SBP calculation, we need to tell the user about this in the response.
                // use the DataQuality object for this                
                SBPListQuality.Parameter = "systolicBloodPressures";
                SBPListQuality.Quality = ParameterQuality.OK;
                SBPListQuality.SubstituteValue = "Using list of SBP readings to create values: systolicBloodPressureMean=" + meanSBP + ", and systolicBloodPressureStDev=" + stDev;
                performedSBPCalc = true;
                meta.PerformedSystolicBloodPressureCalc = true;
            }

            // if we have a townsend score we will try and use it (the calculator will substitute values if it is out of range, we don't range check here)                        
            double? townsendScore = inputModel.townsendScore;
            // Call QRisk 3 calc
            var calcResult = calc.calculate(
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
                                            sbp: meanSBP,
                                            sbps5: stDev,
                                            smoke_cat: inputModel.smokingStatus,
                                            town:townsendScore
                                            );


            outputModel = new Prediction(calcResult, inputModel, meta);
        }




    }


}
