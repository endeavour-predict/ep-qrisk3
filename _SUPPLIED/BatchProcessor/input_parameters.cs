//
// (c) 2008-23 ClinRisk Ltd.
// All rights reserved.
//
// No unauthorised copying, distribution, modification, creating derived works, 
// or even compilation allowed, unless express permission has been granted by 
// ClinRisk Ltd.

using CRStandardDefinitions;

namespace QRISK3BatchProcessor
{
    public enum inputValidity { 
        valid, 
        incorrect_no_of_parameters,
        row_id_not_integer,
        b_cvd_not_boolean,
        sex_not_0_or_1,
        age_not_integer,
        b_AF_not_boolean,
        b_atypicalantipsy_not_boolean,
        b_corticosteroids_not_boolean,
        b_impotence2_not_boolean,
        b_migraine_not_boolean,
        b_ra_not_boolean,
        b_renal_not_boolean,
        b_semi_not_boolean,
        b_sle_not_boolean,
        b_treatedhyp_not_boolean,
        diabetes_cat_not_in_range,
        bmi_not_null_or_double,
        ethnicity_not_in_range,
        fh_cvd_not_boolean,
        rati_not_null_or_double,
        sbp_not_null_or_double,
        sbps5_not_null_or_double,
        smoke_cat_not_in_range,
        town_not_null_or_double,
        postcode_format_incorrect,
        UNKNOWN_ERROR 
    };

    // this needs to be updated for QRISK3 parameters
    // Let's make it typesafe at the same time
    public class inputParameters
    {
        static private int required_number_of_fields = 24;
        /*
            row_id,
            b_cvd,
            sex,
            age,
            b_AF,
            b_atypicalantipsy,
            b_corticosteroids,
            b_impotence2,
            b_migraine,
            b_ra,
            b_renal,
            b_semi,
            b_sle,
            b_treatedhyp,
            diabetes_cat,
            bmi(nullable),
            ethnicity,
            fh_cvd,
            rati(nullable),
            sbp(nullable),
            sbps5(nullable),
            smoke_cat,
            town(nullable),
            postcode(nullable)

         */

        public int row_id;

        public bool b_cvd;
        public Gender sex;
        public int age;
        public bool b_AF;
        public bool b_atypicalantipsy;
        public bool b_corticosteroids;
        public bool b_impotence2;
        public bool b_migraine;
        public bool b_ra;
        public bool b_renal;
        public bool b_semi;
        public bool b_sle;
        public bool b_treatedhyp;
        public DiabetesCat diabetes_cat;
        public double? bmi;
        public Ethnicity ethnicity;
        public bool fh_cvd;
        public double? rati;
        public double? sbp;
        public double? sbps5;
        public SmokeCat smoke_cat;
        public double? town;

        public string postcode;
        
        public inputValidity validity;

        private void parseDouble(string field, out double? name, inputValidity error)
        {
            double tmp;
            name = null;

            if (field == "")
            {
                name = null;
                return;
            }

            if (!double.TryParse(field, out tmp))
            {
                validity = error;
            }
            else
            {
                name = tmp;
            }
        }

        private void parseBoolean(string field, out bool name, inputValidity error)
        {
            int tmp;
            name = false;
            if (!int.TryParse(field, out tmp))
            {
                validity = error;
            }
            else
            {
                switch (tmp)
                {
                    case 0:
                        name = false;
                        break;
                    case 1:
                        name = true;
                        break;
                    default:
                        validity = error;
                        break;
                }
            }
        }

        private void parseIntInRange(string field, out int name, int min, int max, inputValidity error)
        {
            int tmp;
            name = -1;
            if (!int.TryParse(field, out tmp))
            {
                validity = error;
            }
            else
            {
                if (tmp >= min && tmp <= max)
                {
                    name = tmp;
                }
                else
                {
                    validity = error;
                }
            }
        }

        public inputParameters(string s)
        {
            string[] fields = s.Split(',');
            // in this context, nulls are empty strings ""
            // if more than one parameter is invalid, we return the last one tested that was shown to be invalid
            validity = inputValidity.valid;
            if (fields.GetLength(0) != inputParameters.required_number_of_fields)
            {
                validity = inputValidity.incorrect_no_of_parameters;
                return;
            }

            int fieldno = 0;
            int tmp;

            // int row_id;
            if (!int.TryParse(fields[fieldno], out row_id))
            {
                validity = inputValidity.row_id_not_integer;
            }
            fieldno++;

            // bool b_cvd;
            parseBoolean(fields[fieldno], out b_cvd, inputValidity.b_cvd_not_boolean);
            fieldno++;

            // Gender sex;
            if (!int.TryParse(fields[fieldno], out tmp))
            {
                validity = inputValidity.sex_not_0_or_1;
            }
            else
            {
                switch (tmp)
                {
                    case 0:
                        sex = Gender.Female;
                        break;
                    case 1:
                        sex = Gender.Male;
                        break;
                    default:
                        validity = inputValidity.sex_not_0_or_1;
                        break;
                }
            }
            fieldno++;

            // int age;
            if (!int.TryParse(fields[fieldno], out age))
            {
                validity = inputValidity.age_not_integer;
            }
            fieldno++;

            // bool b_AF;
            parseBoolean(fields[fieldno], out b_AF, inputValidity.b_AF_not_boolean);
            fieldno++;

            // bool b_atypicalantipsy;
            parseBoolean(fields[fieldno], out b_atypicalantipsy, inputValidity.b_atypicalantipsy_not_boolean);
            fieldno++;

            // bool b_corticosteroids;
            parseBoolean(fields[fieldno], out b_corticosteroids, inputValidity.b_corticosteroids_not_boolean);
            fieldno++;

            // bool b_impotence2;
            parseBoolean(fields[fieldno], out b_impotence2, inputValidity.b_impotence2_not_boolean);
            fieldno++;

            // bool b_migraine;
            parseBoolean(fields[fieldno], out b_migraine, inputValidity.b_migraine_not_boolean);
            fieldno++;
            
            // bool b_ra;
            parseBoolean(fields[fieldno], out b_ra, inputValidity.b_ra_not_boolean);
            fieldno++;
            
            // bool b_renal;
            parseBoolean(fields[fieldno], out b_renal, inputValidity.b_renal_not_boolean);
            fieldno++;
            
            // bool b_semi;
            parseBoolean(fields[fieldno], out b_semi, inputValidity.b_semi_not_boolean);
            fieldno++;
            
            // bool b_sle;
            parseBoolean(fields[fieldno], out b_sle, inputValidity.b_sle_not_boolean);
            fieldno++;
            
            // bool b_treatedhyp;
            parseBoolean(fields[fieldno], out b_treatedhyp, inputValidity.b_treatedhyp_not_boolean);
            fieldno++;

            // DiabetesCat diabetes_cat;
            parseIntInRange(fields[fieldno], out tmp, 0, 2, inputValidity.diabetes_cat_not_in_range);
            diabetes_cat = (DiabetesCat)tmp;
            fieldno++;

            // double? bmi;
            parseDouble(fields[fieldno], out bmi, inputValidity.bmi_not_null_or_double);
            fieldno++;

            // Ethnicity ethnicity;
            parseIntInRange(fields[fieldno], out tmp, 0, 17, inputValidity.ethnicity_not_in_range);
            ethnicity = (Ethnicity)tmp;
            fieldno++;
            
            // bool fh_cvd;
            parseBoolean(fields[fieldno], out fh_cvd, inputValidity.fh_cvd_not_boolean);
            fieldno++;

            // double? rati;
            parseDouble(fields[fieldno], out rati, inputValidity.rati_not_null_or_double);
            fieldno++;

            // double? sbp;
            parseDouble(fields[fieldno], out sbp, inputValidity.sbp_not_null_or_double);
            fieldno++;

            // double? sbps5;
            parseDouble(fields[fieldno], out sbps5, inputValidity.sbps5_not_null_or_double);
            fieldno++;

            // SmokeCat smoke_cat;
            parseIntInRange(fields[fieldno], out tmp, 0, 5, inputValidity.smoke_cat_not_in_range);
            smoke_cat = (SmokeCat)tmp;
            fieldno++;
            
            // double? town;
            parseDouble(fields[fieldno], out town, inputValidity.town_not_null_or_double);
            fieldno++;

            // string postcode;

            // validate postcode
            // empty string, or 7 chars long
            // Better as follows:
            // One of four types:
            //   An__nAA
            //   Ann_nAA
            //   AAn_nAA
            //   AAnnnAA
            string rPostcode = CRStandardDefinitions.Utilities.validateAndRegularisePostcode(fields[fieldno]);

            // if "" pass through as ""
            // if postcode valid, use validated one
            if (fields[fieldno] == "")
            {
                postcode = "";
            }
            else if (rPostcode != "postcode_invalid")
            {
                postcode = rPostcode;
            }
            else
            {
                validity = inputValidity.postcode_format_incorrect;
            }
            fieldno++;
        }
    }
}
