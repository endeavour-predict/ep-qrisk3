using CRStandardDefinitions;
using System.ComponentModel.DataAnnotations;

namespace QRisk3.Engine.Tests.Models
{


    
    public class QRiskInputModel
    {

        public QRiskInputModel()
        {
                
        }

        /// <summary>
        /// Create a QRisk Input model from a row of "Test Pack" data
        /// </summary>
        /// <param name="row">Row of data from an Oxford Test Pack</param>
        public QRiskInputModel(string[] row)
        {
            // col 0 = row id            
            this.CVD = row[1] == "1";
            if (row[2] == "0") this.sex = Gender.Female;
            if (row[2] == "1") this.sex = Gender.Male;
            this.age = Int32.Parse(row[3]);
            this.atrialFibrillation = row[4] == "1";
            this.atypicalAntipsychoticMedication = row[5] == "1";
            this.systemicCorticosteroids = row[6] == "1";
            this.impotence = row[7] == "1";
            this.migraines = row[8] == "1";
            this.rheumatoidArthritis = row[9] == "1";
            this.chronicRenalDisease = row[10] == "1";
            this.severeMentalIllness = row[11] == "1";
            this.systemicLupusErythematosus = row[12] == "1";
            this.bloodPressureTreatment = row[13] == "1";
            this.diabetesStatus = (DiabetesCat)Enum.Parse(typeof(DiabetesCat), row[14]);
            this.BMI = Double.Parse(row[15]);
            this.ethnicity = (Ethnicity)Enum.Parse(typeof(Ethnicity), row[16]);
            this.familyHistoryCHD = row[17] == "1";
            this.cholesterolRatio = Double.Parse(row[18]);
            this.systolicBloodPressureMean = Double.Parse(row[19]);
            this.systolicBloodPressureStDev = Double.Parse(row[20]);
            this.smokingStatus = (SmokeCat)Enum.Parse(typeof(SmokeCat), row[21]);
            this.townsendScore = Double.Parse(row[22]);
        }

        /// <summary>
        /// Patient has a diagnosis of CVD recorded at any time prior to the search date.
        /// </summary>    
        /// <example>false</example>
        public bool CVD { get; set; }

        /// <summary>
        /// Assigned sex at birth.
        /// </summary>        
        /// <example>Female</example>
        [Required]
        public CRStandardDefinitions.Gender sex { get; set; }

        /// <summary>
        /// Patients age in years calculated on the search date.
        /// </summary>
        /// <example>45</example>
        
        [Required]
        public int age { get; set; }

        /// <summary>
        /// Atrial fibrillation at any time prior to the search date.
        /// </summary>
        /// <example>false</example>
        public bool atrialFibrillation { get; set; }

        /// <summary>
        /// On atypical antipsychotic medication?
        /// Second generation ‘atypical’ antipsychotic - prescribed two or more issues in the previous 6 months(includes amisulpride, aripiprazole, clozapine, lurasidone, olanzapine, paliperidone, quetiapine, risperidone, sertindole, or zotepine).
        /// </summary>
        /// <example>false</example>
        public bool atypicalAntipsychoticMedication { get; set; }

        /// <summary>
        /// On regular steroid tablets?	
        /// Systemic corticosteroids – prescribed two or more issues in the previous 6 months.
        /// </summary>
        /// <example>false</example>
        public bool systemicCorticosteroids { get; set; }

        /// <summary>
        /// A diagnosis of, or treatment for, erectile dysfunction,at any time prior to the search date?	
        /// </summary>
        /// <example>false</example>
        public bool impotence { get; set; }

        /// <summary>        
        /// Diagnosis of migraine at any time prior to the search date?
        /// </summary>
        /// <example>false</example>
        public bool migraines { get; set; }

        /// <summary>
        ///  Rheumatoid arthritis at any time prior to the search date?
        /// </summary>
        /// <example>false</example>
        public bool rheumatoidArthritis { get; set; }

        /// <summary>
        /// Chronic renal disease at any time prior to the search date?
        /// </summary>
        /// <example>false</example>
        public bool chronicRenalDisease { get; set; }

        /// <summary>
        /// Diagnosis of severe mental illness (psychosis, severe depression, manic depression, schizophrenia) at any time prior to the search date?
        /// </summary>
        /// <example>false</example>
        public bool severeMentalIllness { get; set; }

        /// <summary>
        /// Diagnosis of systemic lupus erythematosus (SLE) at any time prior to the search date?
        /// </summary>
        /// <example>false</example>
        public bool systemicLupusErythematosus { get; set; }

        /// <summary>
        /// On blood pressure treatment?
        /// Diagnosis of hypertension at any time in the patient’s records AND On antihypertensive treatment if 1+ script in last 6 months prior to the search date.
        /// Antihypertensive treatment includes the following: 
        /// Thiazides, Beta blockers, ACE inhibitors, Angiotensin II Antagonists, Calcium Channel Blockers.
        /// Patients should only be included if both criteria are satisfied i.e.on treatment and have a diagnosis of hypertension.
        /// </summary>
        /// <example>false</example>
        public bool bloodPressureTreatment { get; set; }

        /// <summary>
        /// Diabetes status
        /// </summary>
        /// <example>None</example>
        public CRStandardDefinitions.DiabetesCat diabetesStatus { get; set; }

        /// <summary>
        /// Body Mass Index (kg/m^2).
        /// Acceptable/Credible Range: 20 to 40.
        /// The most recently recorded body mass index for the patient recorded prior to the search date recorded in the last 5 years.
        /// </summary>
        /// <example>29.1</example>  
        
        public double? BMI { get; set; }

        /// <summary>
        /// Ethnic group, chosen from the 17 categories used by QRisk3.
        /// </summary>
        /// <example>OtherWhiteBackground</example>
        public CRStandardDefinitions.Ethnicity ethnicity { get; set; }

        /// <summary>
        /// Family history of coronary heart disease in a first degree relative under the age of 60 years recorded before the search date?
        /// </summary>
        /// <example>false</example>
        public bool familyHistoryCHD { get; set; }

        /// <summary>
        /// Cholesterol/HDL ratio.
        /// Acceptable/Credible Range: 1 to 12 
        /// The most recent ratio of total serum cholesterol/HDL recorded in the last 5 years
        /// </summary>
        /// <example>4.1</example>        
        public double? cholesterolRatio { get; set; }

        /// <summary>
        /// Latest Systolic Blood Pressure mean reading (mmHg). 
        /// </summary>
        /// <example>140.4</example>        
        public Double? systolicBloodPressureMean { get; set; }

        /// <summary>
        /// Standard Deviation for the systolicBloodPressureMean value
        /// </summary>
        /// <example>3</example>        
        public Double? systolicBloodPressureStDev { get; set; }

        /// <summary>
        /// Most recent confirmed smoking status.
        /// </summary>
        /// <example>NonSmoker</example>
        public CRStandardDefinitions.SmokeCat smokingStatus { get; set; }

        /// <summary>
        /// Townsend score. 
        /// The Townsend score associated with the output area of a patient’s postcode.
        /// See: https://statistics.ukdataservice.ac.uk/dataset/2011-uk-townsend-deprivation-scores#:~:text=The%20Townsend%20Deprivation%20Index%20is,is%20available%20for%20that%20area).
        /// </summary>
        /// <example>0</example>        
        public double? townsendScore { get; set; }


    }
}
