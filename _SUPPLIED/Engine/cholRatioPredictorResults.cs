//
// (c) 2010-13 ClinRisk Ltd.
// All rights reserved.
//
// No unauthorised copying, distribution, modification, creating derived works, 
// or even compilation allowed, unless express permission has been granted by 
// ClinRisk Ltd.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace cholRatioPredictor
{
    public class CholRatioPredictorResults
    {
        public enum Validity { VALID, INVALID };
        public enum ReasonInvalid { 
            OK,
            SEX_MUST_BE_0_OR_1,
            AGE_MUST_BE_BETWEEN_25_AND_84,
            HAD_A_CVD_EVENT_MUST_BE_0_OR_1,
            REQUIRING_BLOOD_PRESSURE_TREATMENT_MUST_BE_0_OR_1,
            HAVING_DIABETES_MUST_BE_0_OR_1,
            ETHNICITY_MUST_BE_BETWEEN_1_AND_9,
            SMOKING_STATUS_MUST_BE_0_OR_1,
            SMOKING_STATUS_MUST_BE_BETWEEN_0_AND_4
        };
        // Always valid, as age is always brought within range
        public Validity validity = Validity.VALID;
        public double? cholRatio = null;
        public List<ReasonInvalid> ReasonsInvalid = new List<ReasonInvalid>();
        public override String ToString()
        {
            String s = validity.ToString() + ",";
            if (validity == Validity.INVALID)
            {
                foreach (ReasonInvalid r in ReasonsInvalid)
                {
                    s += r.ToString() + ":";
                }
                s = s.TrimEnd(':');
            }
            else
            {
                s += cholRatio;
            }
            return s;
        }
    }
}
