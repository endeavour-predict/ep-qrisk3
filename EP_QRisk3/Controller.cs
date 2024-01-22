using ep_service;
using ep_models;

namespace EP_QRisk3
{
    public class Controller
    {
        public PredictionModel GetScore(EPInputModel EPInputModel)
        {            
            return new PredictionService().GetScore(EPInputModel);
        }
    }
}
