
namespace CIDS.Environment
{
    ///////////////////////////////////////////
    /// Ten folders cross-validation ///    //
    ///////////////////////////////////

    class EnvPumpkingSeed : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Pumpking";


        private string Name = "Punmpking_Seed";


        private string Spliter = ",";

        private const string Describe = "Pumpkin Seed: link  https://www.kaggle.com/datasets/muratkokludataset/pumpkin-seeds-dataset ,the original dataset is used, only the format is translated from arff to csv";


        private string ClassLabel = "Class";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }


        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvWhiteWineBinary : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\WinQuality";


        private string Name = "White_Wine_Binary";


        private string Spliter = ",";


        private const string Describe = "Wine Quality Data Set: link  https://archive.ics.uci.edu/ml/datasets/Wine+Quality ,only utilize the white wine dataset. Duplicated instances have removed. Task is for classifictation say quality 0-5 is 0 and quality 6-10 is 1";


        private string ClassLabel = "quality";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }

    }

    class EnvEFIfull : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\EFI_full";


        private string Name = "EFI";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "2048";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }

    }

    class EnvDryBean : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\DryBean";

        private string Name = "Dry_Bean";
        private string Spliter = ",";

        private const string Describe = "Dry_Bean link  https://www.kaggle.com/datasets/muratkokludataset/dry-bean-dataset ,the original dataset is used, only the format is translated from arff to csv";

        private string ClassLabel = "Class";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }


        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvPistachio16 : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Pistachio16";


        private string Name = "Pistachio16";


        private string Spliter = ",";


        private const string Describe = "Pistachio16 link https://www.kaggle.com/datasets/muratkokludataset/pistachio-dataset ,the original dataset is used, only the format is translated from arff to csv";


        private string ClassLabel = "Class";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }


        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }

    class EnvPistachio28 : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Pistachio28";

        private string Name = "Pistachio28";

        private string Spliter = ",";

        private const string Describe = "Pistachio16 link https://www.kaggle.com/datasets/muratkokludataset/pistachio-dataset ,the original dataset is used, only the format is translated from arff to csv";

        private string ClassLabel = "Class";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvRaisin : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Raisin";

        private string Name = "Raisin";

        private string Spliter = ",";

        private const string Describe = "Raisin link https://www.kaggle.com/datasets/muratkokludataset/raisin-dataset ,the original dataset is used, only the format is translated from arff to csv";

        private string ClassLabel = "Class";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }

    class EnvRiceCammeo : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\RiceCammeo";

        private string Name = "RiceCammeo";

        private string Spliter = ",";

        private const string Describe = "Raisin link https://www.kaggle.com/datasets/muratkokludataset/raisin-dataset ,the original dataset is used, only the format is translated from arff to csv";

        private string ClassLabel = "Class";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvPulsar : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Pulsar";

        private string Name = "Pulsar";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "2048";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }

    class EnvFruit : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Fruit";

        private string Name = "Fruit";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "Class";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvPulsarImbalance : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\PulsarImbalance";

        private string Name = "PulsarImbalance";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "2048";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();

        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }

    class EnvFace : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\face";


        private string Name = "face";


        private string Spliter = ",";


        private const string Describe = "";


        private string ClassLabel = "Labels";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }


        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvStress : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\stress";


        private string Name = "stress";


        private string Spliter = ",";


        private const string Describe = "";


        private string ClassLabel = "512";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvIris : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Iris";


        private string Name = "Iris";


        private string Spliter = ",";


        private const string Describe = "";


        private string ClassLabel = "species";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvDiabete : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Diabete";


        private string Name = "Diabete";


        private string Spliter = ",";


        private const string Describe = "";


        private string ClassLabel = "Outcome";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvMINIST : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\MINIST";


        private string Name = "MINIST";


        private string Spliter = ",";


        private const string Describe = "";


        private string ClassLabel = "64";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvMSPET : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\mspet";

        private string Name = "MSPET";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "64";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }


        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }


    class EnvFMINIST : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\FMINST";

        private string Name = "FMINST";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "64";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }


        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }

    class EnvGinger : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TenFolders\Ginger";

        private string Name = "Ginger";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "64";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();


        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentTenFolders(ref Address, ref Name, ref Spliter, ref ClassLabel, _id, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }
    }
    //////////////////////////
    /// Single model///    //
    /////////////////////////


    class EnvCIFAR10 : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TrainTest\CIFAR10";


        private string Name = "CIFAR10";


        private string Spliter = ",";


        private const string Describe = "";


        private string ClassLabel = "768";


        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentOneRun(ref Address, ref Name, ref Spliter, ref ClassLabel, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }

    }

    class EnvSingleStress : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TrainTest\Stress";

        private string Name = "StressSingle";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "512";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentOneRun(ref Address, ref Name, ref Spliter, ref ClassLabel, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }

    }



    class EnvSingleMusic : InterfaceEnv
    {
        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TrainTest\music";

        private string Name = "MUSIC";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "label";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentOneRun(ref Address, ref Name, ref Spliter, ref ClassLabel, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }

    }


    class EnvSingleMNIST : InterfaceEnv
    {

        private string Address = @System.Environment.CurrentDirectory + @"\Environment\TrainTest\MNIST";

        private string Name = "MNIST";

        private string Spliter = ",";

        private const string Describe = "";

        private string ClassLabel = "64";

        private readonly EnvFunctions EnvEnter = new EnvFunctions();



        public bool GetEnv(int _id, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {
            return EnvEnter.InitializeEnvironmentOneRun(ref Address, ref Name, ref Spliter, ref ClassLabel, out _envTrain, out _envTest);
        }

        public string GetProblemDescribe()
        {
            return Describe;
        }

        public string GetName()
        {
            return Name;
        }

    }

}
