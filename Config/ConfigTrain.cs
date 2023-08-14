using System.Collections.Generic;

namespace CIDS.Config
{
    class ConfigTrain
    {

        public Dictionary<string, float> Boundary;

        public float DataRate;

        public float EnterFeatureBatchRate;

        public int BeginFeatureMax;



        public int AbsumptionMin;

        public int AbsumptionLimitationThreshold;



        public float SubsumptionSizeThreshold = 0.1f;


        public int DeadCount;
    }
}
