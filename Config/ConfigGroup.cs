using System.Collections.Generic;

namespace CIDS.Config
{

    class ConfigPumpkingSeed : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("CERCEVELIK", 0.5f);
            _con.Boundary.Add("URGUP_SIVRISI", 0.6f);

            _con.AbsumptionLimitationThreshold = 5;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.005f;

            _con.BeginFeatureMax = 8;

            _con.AbsumptionMin = 55;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }


    class ConfigWhiteWineBinary : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.6f);
            _con.Boundary.Add("1", 0.6f);

            _con.AbsumptionLimitationThreshold = 12;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.0015f;

            _con.BeginFeatureMax = 6;

            _con.AbsumptionMin = 1;

            _con.DeadCount = 500;
        }
    }



    class ConfigEFIfull : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("smile", 0.6f);
            _con.Boundary.Add("neutral", 0.6f);

            _con.AbsumptionLimitationThreshold = 15;

            _con.DataRate = 0.05f;

            _con.EnterFeatureBatchRate = 0.03f;

            _con.BeginFeatureMax = 30;

            _con.AbsumptionMin = 10;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }



    class ConfigCIFAR10 : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.7f);
            _con.Boundary.Add("1", 0.7f);
            _con.Boundary.Add("2", 0.7f);
            _con.Boundary.Add("3", 0.7f);
            _con.Boundary.Add("4", 0.7f);
            _con.Boundary.Add("5", 0.7f);
            _con.Boundary.Add("6", 0.7f);
            _con.Boundary.Add("7", 0.7f);
            _con.Boundary.Add("8", 0.7f);
            _con.Boundary.Add("9", 0.7f);

            _con.AbsumptionLimitationThreshold = 15;

            _con.DataRate = 0.005f;

            _con.EnterFeatureBatchRate = 0.005f;

            _con.BeginFeatureMax = 30;

            _con.AbsumptionMin = 50;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.3f;
        }
    }


    class ConfigDryBean : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("HOROZ", 0.6f);
            _con.Boundary.Add("CALI", 0.6f);
            _con.Boundary.Add("BOMBAY", 0.6f);
            _con.Boundary.Add("SIRA", 0.6f);
            _con.Boundary.Add("BARBUNYA", 0.6f);
            _con.Boundary.Add("SEKER", 0.6f);
            _con.Boundary.Add("DERMASON", 0.6f);

            _con.AbsumptionLimitationThreshold = 5;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.01f;

            _con.BeginFeatureMax = 8;

            _con.AbsumptionMin = 15;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.08f;
        }
    }


    class ConfigPistachio16 : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("Siit_Pistachio", 0.6f);
            _con.Boundary.Add("Kirmizi_Pistachio", 0.6f);

            _con.AbsumptionLimitationThreshold = 10;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.01f;

            _con.BeginFeatureMax = 6;

            _con.AbsumptionMin = 20;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.3f;
        }
    }


    class ConfigPistachio28 : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("Siirt_Pistachio", 0.6f);
            _con.Boundary.Add("Kirmizi_Pistachio", 0.6f);

            _con.AbsumptionLimitationThreshold = 10;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.02f;

            _con.BeginFeatureMax = 7;

            _con.AbsumptionMin = 20;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.35f;
        }
    }


    class ConfigRaisin : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("Kecimen", 0.6f);
            _con.Boundary.Add("Besni", 0.6f);

            _con.AbsumptionLimitationThreshold = 10;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.01f;

            _con.BeginFeatureMax = 3;

            _con.AbsumptionMin = 15;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.17f;
        }
    }


    class ConfigRiceCammeo : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("Osmancik", 0.6f);
            _con.Boundary.Add("Cammeo", 0.6f);

            _con.AbsumptionLimitationThreshold = 5;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.01f;

            _con.BeginFeatureMax = 3;

            _con.AbsumptionMin = 15;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.05f;
        }
    }


    class ConfigPulsar : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.4f);
            _con.Boundary.Add("1", 0.7f);

            _con.AbsumptionLimitationThreshold = 10;

            _con.DataRate = 0.008f;

            _con.EnterFeatureBatchRate = 0.005f;

            _con.BeginFeatureMax = 5;

            _con.AbsumptionMin = 10;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.25f;
        }
    }


    class ConfigPulsarImbalanced : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.4f);
            _con.Boundary.Add("1", 0.7f);

            _con.AbsumptionLimitationThreshold = 10;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.01f;

            _con.BeginFeatureMax = 30;

            _con.AbsumptionMin = 15;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }


    class ConfigFruit : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("BERHI", 0.6f);
            _con.Boundary.Add("SAFAVI", 0.6f);
            _con.Boundary.Add("SOGAY", 0.6f);
            _con.Boundary.Add("ROTANA", 0.6f);
            _con.Boundary.Add("IRAQI", 0.6f);
            _con.Boundary.Add("DEGLET", 0.6f);
            _con.Boundary.Add("DOKOL", 0.6f);

            _con.AbsumptionLimitationThreshold = 5;

            _con.DataRate = 0.015f;

            _con.EnterFeatureBatchRate = 0.02f;

            _con.BeginFeatureMax = 5;

            _con.AbsumptionMin = 12;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.2f;
        }
    }


    class ConfigIris : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("setosa", 0.55f);
            _con.Boundary.Add("versicolor", 0.55f);
            _con.Boundary.Add("virginica", 0.55f);

            _con.AbsumptionLimitationThreshold = 1;

            _con.DataRate = 0.05f;

            _con.EnterFeatureBatchRate = 0.01f;

            _con.BeginFeatureMax = 3;

            _con.AbsumptionMin = 12;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }

    class ConfigDiabete : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.55f);
            _con.Boundary.Add("1", 0.55f);

            _con.AbsumptionLimitationThreshold = 8;

            _con.DataRate = 0.1f;

            _con.EnterFeatureBatchRate = 0.03f;

            _con.BeginFeatureMax = 7;

            _con.AbsumptionMin = 15;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }

    class ConfigMINIST : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.6f);
            _con.Boundary.Add("1", 0.6f);
            _con.Boundary.Add("2", 0.6f);
            _con.Boundary.Add("3", 0.6f);
            _con.Boundary.Add("4", 0.6f);
            _con.Boundary.Add("5", 0.6f);
            _con.Boundary.Add("6", 0.6f);
            _con.Boundary.Add("7", 0.6f);
            _con.Boundary.Add("8", 0.6f);
            _con.Boundary.Add("9", 0.6f);

            _con.AbsumptionLimitationThreshold = 8;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.03f;

            _con.BeginFeatureMax = 7;

            _con.AbsumptionMin = 50;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }

    class ConfigMSPET : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.6f);
            _con.Boundary.Add("1", 0.6f);

            _con.AbsumptionLimitationThreshold = 8;

            _con.DataRate = 0.002f;

            _con.EnterFeatureBatchRate = 0.003f;

            _con.BeginFeatureMax = 15;

            _con.AbsumptionMin = 15;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }



    class ConfigFMINIST : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.6f);
            _con.Boundary.Add("1", 0.6f);
            _con.Boundary.Add("2", 0.6f);
            _con.Boundary.Add("3", 0.6f);
            _con.Boundary.Add("4", 0.6f);
            _con.Boundary.Add("5", 0.6f);
            _con.Boundary.Add("6", 0.6f);
            _con.Boundary.Add("7", 0.6f);
            _con.Boundary.Add("8", 0.6f);
            _con.Boundary.Add("9", 0.6f);

            _con.AbsumptionLimitationThreshold = 8;

            _con.DataRate = 0.01f;

            _con.EnterFeatureBatchRate = 0.03f;

            _con.BeginFeatureMax = 15;

            _con.AbsumptionMin = 50;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }



    class ConfigGinger : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.6f);
            _con.Boundary.Add("1", 0.6f);
            _con.Boundary.Add("2", 0.6f);
            _con.Boundary.Add("3", 0.6f);
            _con.Boundary.Add("4", 0.6f);
            _con.Boundary.Add("5", 0.6f);
            _con.Boundary.Add("6", 0.6f);
            _con.Boundary.Add("7", 0.6f);
            _con.Boundary.Add("8", 0.6f);


            _con.AbsumptionLimitationThreshold = 5;

            _con.DataRate = 0.08f;
            _con.EnterFeatureBatchRate = 0.03f;

            _con.BeginFeatureMax = 25;

            _con.AbsumptionMin = 40;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.2f;
        }
    }

    class ConfigFace : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("Stressful", 0.5f);
            _con.Boundary.Add("Calm", 0.6f);

            _con.AbsumptionLimitationThreshold = 8;

            _con.DataRate = 0.05f;

            _con.EnterFeatureBatchRate = 0.03f;

            _con.BeginFeatureMax = 3;

            _con.AbsumptionMin = 2;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }


    class ConfigStress : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.6f);
            _con.Boundary.Add("1", 0.6f);

            _con.AbsumptionLimitationThreshold = 8;

            _con.DataRate = 0.005f;

            _con.EnterFeatureBatchRate = 0.003f;

            _con.BeginFeatureMax = 15;

            _con.AbsumptionMin = 50;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }

    class ConfigStressSingle : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("0", 0.6f);
            _con.Boundary.Add("1", 0.6f);

            _con.AbsumptionLimitationThreshold = 8;

            _con.DataRate = 0.005f;

            _con.EnterFeatureBatchRate = 0.003f;

            _con.BeginFeatureMax = 20;

            _con.AbsumptionMin = 50;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.1f;
        }
    }

    class ConfigMusicSingle : InterfaceConfig
    {
        public void GetConfig(out ConfigTrain _con)
        {
            _con = new ConfigTrain();

            _con.Boundary = new Dictionary<string, float>();
            _con.Boundary.Add("blues", 0.6f);
            _con.Boundary.Add("classical", 0.6f);
            _con.Boundary.Add("country", 0.6f);
            _con.Boundary.Add("disco", 0.6f);
            _con.Boundary.Add("hiphop", 0.6f);
            _con.Boundary.Add("jazz", 0.6f);
            _con.Boundary.Add("metal", 0.6f);
            _con.Boundary.Add("pop", 0.6f);
            _con.Boundary.Add("reggae", 0.6f);
            _con.Boundary.Add("rock", 0.6f);

            _con.AbsumptionLimitationThreshold = 12;

            _con.DataRate = 0.025f;

            _con.EnterFeatureBatchRate = 0.005f;

            _con.BeginFeatureMax = 15;

            _con.AbsumptionMin = 35;

            _con.DeadCount = 500;

            _con.SubsumptionSizeThreshold = 0.2f;
        }
    }
}
