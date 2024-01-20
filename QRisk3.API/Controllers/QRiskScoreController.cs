using Microsoft.AspNetCore.Mvc;
using QRisk_API.Models;
using QRisk_API.Services;
using QRISK3Engine;

namespace QRisk_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class QRiskScoreController : ControllerBase
    {
        public string apiVersion { get { return "QRisk API v0.12.0"; } }

        public QRiskScoreController()
        {
            
        }


        /// <summary>
        /// Return the 10 Year QRisk score
        /// </summary>        
        /// <response code="200">200 is returned if a Prediction score has been calculated.</response>
        /// <response code="400">400 is returned if input cannot be processed (for example if the Age is out of range). Details will be in the returned response body.</response>                
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "GetQRiskScore")]    
        public ActionResult<Prediction> Post(QRiskInputModel inputModel)
        {            
            var CalculatorVersion = QRiskCVDAlgorithmCalculator.version();
            var ApiVersion = apiVersion;            

            if (string.IsNullOrEmpty(CalculatorVersion))
            {
                return Problem("Config missing: CalculatorVersion");
            }
            if (string.IsNullOrEmpty(ApiVersion))
            {
                return Problem("Config missing: ApiVersion");
            }
            
            

            // This validation block is a direct copy/paste from the ClinRisk calculator validation
            // we prefer to validate here in the API and throw a 400, rather than calling the calculator when we know it can't run a Prediction score
            if (inputModel.age < QRiskCVDAlgorithmCalculator.minAge || inputModel.age > QRiskCVDAlgorithmCalculator.maxAge)
            {
                return BadRequest("Cannot run prediction. Age is out of range.");
            }
            // does not apply if already had a CVD event
            if (inputModel.CVD)
            {
                return BadRequest("Cannot run prediction. CVD is true.");
            }

            bool performedSBPCalc;
            DataQuality SBPListQuality;
            Prediction outputModel;
            var calculationService = new CalculationService();
            calculationService.PerformQRiskCalculation(inputModel, CalculatorVersion, ApiVersion,                                                        
                                                        out performedSBPCalc, out SBPListQuality, out outputModel);
            
            if (performedSBPCalc)
            {
                outputModel.Quality.Add(SBPListQuality);
            }

            return outputModel;
        }

        

    }
}