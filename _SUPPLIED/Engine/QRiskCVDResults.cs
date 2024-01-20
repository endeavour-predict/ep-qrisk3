using System;
using System.Collections.Generic;
using System.Text;

namespace QRISK3Engine
{
    public enum ResultStatus { NO_CALCULATION_POSSIBLE_AS_PATIENT_FAILED_CRITERIA, CALCULATED_USING_PATIENTS_OWN_DATA, CALCULATED_USING_ESTIMATED_OR_CORRECTED_DATA, NO_CALCULATION_POSSIBLE_AS_ENGINE_LOCKED };
    public enum ReasonInvalid { VALID, AGE_OUT_OF_RANGE, ALREADY_HAD_A_CVD_EVENT, ETHNICITY_OUT_OF_RANGE, VARIABLE_NON_BOOLEAN, QRISK_ENGINE_LOCKED, SMOKING_STATUS_OUT_OF_RANGE };
    public enum Data { OK, MISSING, OUT_OF_RANGE };

    public class MissingOrOutOfRangeData
    {
        public Data data;
        public double substitute_value;

        public MissingOrOutOfRangeData()
        {
            data = Data.OK;
            substitute_value = 0.0;
        }

        public override string ToString()
        {
            return data.ToString() + "," + substitute_value.ToString("0.0#####");
        }

        public static string DummyString(string s)
        {
            return s + "Status," + s + "SubstituteValue";
        }
    }

    public class MissingOrOutOfRangeDataText
    {
        public Data data;
        public string substitute_value;

        public MissingOrOutOfRangeDataText()
        {
            data = Data.OK;
            substitute_value = "";
        }

        public override string ToString()
        {
            return data.ToString() + "," +
                substitute_value;
        }

        public static string DummyString(string s)
        {
            return s + "Status," + s + "SubstituteValue";
        }
    }


    public class QRiskCVDDataQuality
    {
        public MissingOrOutOfRangeData ratio;
        public MissingOrOutOfRangeData sbp;
        public MissingOrOutOfRangeData bmi;
        public MissingOrOutOfRangeData townsend;
        public MissingOrOutOfRangeDataText smokingStatus;
        public MissingOrOutOfRangeDataText ethnicity;
        public MissingOrOutOfRangeData sbps5;

        // constructor
        public QRiskCVDDataQuality()
        {
            ratio = new MissingOrOutOfRangeData();
            sbp = new MissingOrOutOfRangeData();
            bmi = new MissingOrOutOfRangeData();
            townsend = new MissingOrOutOfRangeData();
            smokingStatus = new MissingOrOutOfRangeDataText();
            ethnicity = new MissingOrOutOfRangeDataText();
            sbps5 = new MissingOrOutOfRangeData();
        }

        public override string ToString()
        {
            return "RATIO_" + ratio.ToString() + "," +
                "SBP_" + sbp.ToString() + "," +
                "BMI_" + bmi.ToString() + "," +
                "TOWN_" + townsend.ToString() + "," +
                "SMOKE_CAT_" + smokingStatus.ToString() + "," +
                "ETHNICITY_" + ethnicity.ToString() + "," +
                "SBPS5_" + sbps5.ToString();
        }

        public static string DummyString()
        {
            return MissingOrOutOfRangeData.DummyString("ratio") + "," +
                MissingOrOutOfRangeData.DummyString("sbp") + "," +
                MissingOrOutOfRangeData.DummyString("bmi") + "," +
                MissingOrOutOfRangeData.DummyString("town") + "," +
                MissingOrOutOfRangeData.DummyString("smoke_cat") + "," +
                MissingOrOutOfRangeData.DummyString("ethnicity") + "," +
                MissingOrOutOfRangeData.DummyString("sbps5");
        }
    }

    public class QRiskCVDResults
    {
        public ResultStatus resultStatus;
        public ReasonInvalid reason;
        public double score;
        public double typical_score;
        public QRiskCVDDataQuality DataQuality;
        public int? qHeartAge = null;

        // constructor
        public QRiskCVDResults()
        {
            resultStatus = ResultStatus.CALCULATED_USING_PATIENTS_OWN_DATA;
            reason = ReasonInvalid.VALID;
            score = 0.0;
            typical_score = 0.0;

            DataQuality = new QRiskCVDDataQuality();
        }

        public override string ToString()
        {
            //return base.ToString();
            string qha;
            if (qHeartAge.HasValue)
            {
                qha = qHeartAge.ToString();
            }
            else
            {
                qha = "over 84";
            }
            return reason.ToString() + ","
                + resultStatus.ToString() + ","
                + DataQuality.ToString() + ","
                + typical_score.ToString("0.0#####") + ","
                + score.ToString("0.0#####") + ","
                + qha;
        }

        public static string DummyString()
        {
            return "algorithmValidity,algorithmStatus," + QRiskCVDDataQuality.DummyString() + ",ageSexEthnicityHealthyScore,patientScore,QRISK3HeartAge";
        }
    }
}
