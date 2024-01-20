using QRisk_API.Models;
using QRISK3Engine;
using System.Text.Json.Serialization;

namespace QRisk_API.Models
{
    public class Prediction
    {

        public Prediction()
        {
            
        }

        public Prediction(QRiskCVDResults calcResult, QRiskInputModel inputModel, Meta meta)
        {            
            this.Meta= meta;            
            this.Meta.CalculatorResultStatus= calcResult.resultStatus;
            this.Meta.CalculatorResultStatusReason = calcResult.reason;
            this.QRiskInputModel = inputModel;            

            var QRiskPredictionResult = new PredictionResult();
            QRiskPredictionResult.id = new Uri("http://endhealth.info/im#Qrisk3");
            QRiskPredictionResult.score = calcResult.score;
            QRiskPredictionResult.typicalScore = calcResult.typical_score;
            Results.Add(QRiskPredictionResult);

            if (calcResult.qHeartAge.HasValue)
            {
                var QHeartScoreResult = new PredictionResult();
                QHeartScoreResult.id = new Uri("http://endhealth.info/im#QHeartAge");
                QHeartScoreResult.score = calcResult.qHeartAge.Value;
                Results.Add(QHeartScoreResult);
            }

            AddCalculatorDataQualityReadings(calcResult, inputModel);

        }

        private void AddCalculatorDataQualityReadings(QRiskCVDResults calcResult, QRiskInputModel inputModel)
        {            
            var smokingQuality = new DataQuality();
            smokingQuality.Parameter = "smokingStatus";
            smokingQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.smokingStatus.data.ToString());
            smokingQuality.SubstituteValue = calcResult.DataQuality.smokingStatus.substitute_value;
            Quality.Add(smokingQuality);
            
            var sbpQuality = new DataQuality();
            sbpQuality.Parameter = "systolicBloodPressureMean";
            sbpQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.sbp.data.ToString());
            sbpQuality.SubstituteValue = calcResult.DataQuality.sbp.substitute_value.ToString();
            Quality.Add(sbpQuality);
            
            var sbp5sQuality = new DataQuality();
            sbp5sQuality.Parameter = "systolicBloodPressureStDev";
            sbp5sQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.sbps5.data.ToString());
            sbp5sQuality.SubstituteValue = calcResult.DataQuality.sbps5.substitute_value.ToString();
            Quality.Add(sbp5sQuality);
            
            var ratioQuality = new DataQuality();
            ratioQuality.Parameter = "ratio";
            ratioQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.ratio.data.ToString());
            ratioQuality.SubstituteValue = calcResult.DataQuality.ratio.substitute_value.ToString();
            Quality.Add(ratioQuality);
            
            var ethnicityQuality = new DataQuality();
            ethnicityQuality.Parameter = "ethnicity";
            ethnicityQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.ethnicity.data.ToString());
            ethnicityQuality.SubstituteValue = calcResult.DataQuality.ethnicity.substitute_value.ToString();
            Quality.Add(ethnicityQuality);
            

            var bmiQuality = new DataQuality();
            bmiQuality.Parameter = "BMI";
            bmiQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.bmi.data.ToString());
            bmiQuality.SubstituteValue = calcResult.DataQuality.bmi.substitute_value.ToString();
            Quality.Add(bmiQuality);

            var townsendQuality = new DataQuality();
            townsendQuality.Parameter = "townsendScore";
            townsendQuality.Quality = (ParameterQuality)Enum.Parse(typeof(ParameterQuality), calcResult.DataQuality.townsend.data.ToString());
            townsendQuality.SubstituteValue = calcResult.DataQuality.townsend.substitute_value.ToString();
            Quality.Add(townsendQuality);
        }

        
        /// <summary>
        /// List of Prediction Scores
        /// </summary>
        public List<PredictionResult> Results { get; set; } = new List<PredictionResult>();

        /// <summary>
        /// List of DataQuality checks, showing any substituted values
        /// </summary>
        public List<DataQuality> Quality { get; set; } = new List<DataQuality>();

        /// <summary>
        /// Metadata about this call to the API
        /// </summary>
        public Meta Meta { get; set; } = new Meta();

        /// <summary>
        /// The data provided by the user for this Prediction. Identifiable fields are stripped of data and marked as **PI** 
        /// </summary>
        public QRiskInputModel QRiskInputModel { get; set; }

    }

    public enum ParameterQuality
    {
           OK, MISSING, OUT_OF_RANGE
    }


}


public class DataQuality
{

    /// <summary>
    /// Name of the Parameter used by the Calculator
    /// </summary>
    public string Parameter { get; set; } = "";

    /// <summary>
    /// Was the parameter provided OK, out of range etc?
    /// </summary>
    public ParameterQuality Quality { get; set; }
    
    /// <summary>
    /// The value used by the calculator if the value provided was substituted
    /// </summary>
    public string SubstituteValue { get; set; } = "";

    /// <summary>
    /// Quality report from the calculator, showing any substituted values for missing or out of range parameters
    /// </summary>
    public DataQuality()
    { 
    }

}

/// <summary>
/// Contains the ID, Score and Typical score (if available)
/// </summary>
public class PredictionResult
{

    public PredictionResult()
    {        
    }

    [JsonPropertyName("@id")]
    public Uri id { get; set; } 
    public double score { get; set; }
    public double? typicalScore { get; set; }
    
}



/// <summary>
/// Contains MetaData about the prediction: timings, versions, etc
/// </summary>
public class Meta
{
    public Meta()
    {
    }
    /// <summary>
    /// Build version of the API.
    /// </summary>
    public string ApiVersion { get; set; }
    /// <summary>
    /// ISO DateTime (UTC) that the API was invoked
    /// </summary>
    public DateTime ApiTimeStampUTC { get; set; }
    /// <summary>
    /// Build version of the Calculator.
    /// </summary>
    public string CalculatorVersion { get; set; }
    /// <summary>
    /// Status result from the Calculator.
    /// </summary>
    public ResultStatus CalculatorResultStatus { get; set; }
    /// <summary>
    /// Used to hold extra information if the Calculator was unable to produce a Score (reason for failure).
    /// </summary>
    public ReasonInvalid CalculatorResultStatusReason { get; set; }            
    /// <summary>
    /// True if we have converted a list of SystolicBloodPressures to the Mean and Standard Deviation. Details of the conversion will be found in a "systolicBloodPressures" DataQuality section
    /// </summary>
    public bool PerformedSystolicBloodPressureCalc { get; set; }
}

