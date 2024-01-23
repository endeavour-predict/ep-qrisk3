using ep_service;
using ep_models;

namespace EP_QRisk3
{
    public class Controller
    {
        /// <summary>
        /// Get Score(s) for EPInputModel
        /// </summary>
        /// <param name="EPInputModel"></param>
        /// <returns></returns>
        public PredictionModel GetScore(EPInputModel EPInputModel)
        {            
            return new PredictionService().GetScore(EPInputModel);
        }
    }
}
