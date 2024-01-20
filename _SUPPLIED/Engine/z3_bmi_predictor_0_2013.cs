/* 
 * (c) 2013 ClinRisk Ltd.  All rights reserved.
 * This file has been auto-generated.
 * XML source: z3_bmi_predictor_0_2013.xml
 * STATA dta time stamp: 6 Nov 2012 10:19
 * .NET file create date: Fri 15 Mar 2013 13:49:34 GMT
 */

using System;
using CRStandardDefinitions;

namespace ClinRiskAutogenerated 
{
	public class z3_bmi_predictor_0_2013 {
		/* bmi */

		static private double bmi_female_raw(
			int age,int b_cvd,int b_treatedhyp,int b_type1,int b_type2,int ethrisk,int smoke_cat
		)
		{

			/* The conditional arrays */

			double[] Iethrisk = {
				0,
				0,
				-0.9103719691820829600000000,
				0.8182942924074451100000000,
				-0.4719542429441248600000000,
				-1.6728183067932849000000000,
				1.6134045591382604000000000,
				2.0296066625563971000000000,
				-3.6137925009637200000000000,
				0.0466861073536137190000000
			};
			double[] Ismoke = {
				0,
				0.4439349699436318500000000,
				-0.5461097940001868900000000,
				-0.2124353499830781700000000,
				0.2020539960274210500000000
			};

			/* Applying the fractional polynomial transforms */
			/* (which includes scaling)                      */

			double dage = age;
			dage=dage/10;
			double age_1 = dage;
			double age_2 = Math.Pow(dage,2);

			/* The normalisation coefficients */

			double mage_1 = 5.361288547515869;
			double mage_2 = 28.743415832519531;

			/* Centring the continuous variables */

			age_1 = age_1 - mage_1;
			age_2 = age_2 - mage_2;

			/* Start of Sum */
			double a=0;

			/* The conditional sums */

			a += Iethrisk[ethrisk];
			a += Ismoke[smoke_cat];

			/* The continuous coefficients */

			double cage_1 = 2.2629294845423957000000000;
			double cage_2 = -0.2073641822836017600000000;

			/* Sum from continuous values */

			a += age_1 * cage_1;
			a += age_2 * cage_2;

			/* The boolean coefficients */

			double cb_cvd = 0.2213782029006499100000000;
			double cb_treatedhyp = 1.9430209286180171000000000;
			double cb_type1 = 1.5389895450065985000000000;
			double cb_type2 = 3.0388348411614095000000000;

			/* Sum from boolean values */

			a += b_cvd * cb_cvd;
			a += b_treatedhyp * cb_treatedhyp;
			a += b_type1 * cb_type1;
			a += b_type2 * cb_type2;

			/* The interaction coefficients */


			/* Sum from interaction terms */


			/* Calculate the score itself */
			double constant =  26.9941362710623910000000000;
			double score = a + constant;
			return score;
		}

        // Comments on ranges for integers should be autogenerated
        // age must be in range (25,99)
		static public Double bmi_female(
			int age,bool b_cvd,bool b_treatedhyp,DiabetesCat diabetes_cat,Ethnicity ethnicity,SmokeCat smoke_cat
		)
		{
            // put age within allowed bounds
            if (age < 25)
                age = 25;
            if (age > 99)
                age = 99;
			Double score;
			double tmp = bmi_female_raw(
                age,
                Utilities.boolToInt(b_cvd),
                Utilities.boolToInt(b_treatedhyp),
                Utilities.diabetescatToType1(diabetes_cat),
                Utilities.diabetescatToType2(diabetes_cat),
                Utilities.ethnicityToEthrisk(ethnicity),
                Utilities.smokecatToInt(smoke_cat)
            );
			tmp = Math.Round(tmp*1000000.0)/1000000.0;
			score = tmp;

			return score;
		}

		/* End of bmi */

	}
}
