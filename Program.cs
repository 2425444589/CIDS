using CIDS.Config;
using CIDS.DSCI;
using CIDS.DSCI.Support;
using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CIDS
{
    class Program
    {
        static void Main(string[] args)
        {
            TenFoldTrain();
            //SingleTrain();

        }

        static void TenFoldTrain()
        {

            //InterfaceEnv sample = new EnvPumpkingSeed();
            //InterfaceEnv sample = new EnvWhiteWineBinary();
            InterfaceEnv sample = new EnvEFIfull();
            //InterfaceEnv sample = new EnvDryBean();
            //InterfaceEnv sample = new EnvPistachio16();
            //InterfaceEnv sample = new EnvPistachio28();
            //InterfaceEnv sample = new EnvRaisin();
            //InterfaceEnv sample = new EnvRiceCammeo();
            //InterfaceEnv sample = new EnvPulsar();
            //InterfaceEnv sample = new EnvPulsarImbalance();
            //InterfaceEnv sample = new EnvFruit();
            //InterfaceEnv sample = new EnvFace();
            //InterfaceEnv sample = new EnvStress();
            //InterfaceEnv sample = new EnvIris();
            //InterfaceEnv sample = new EnvDiabete();
            //InterfaceEnv sample = new EnvMINIST();
            //InterfaceEnv sample = new EnvMSPET();
            //InterfaceEnv sample = new EnvFMINIST();
            //InterfaceEnv sample = new EnvGinger();
            //开始时间

            //总正确率
            double sumAccuracy = 0;
            ArrayList accuracys = new ArrayList();
            SupportTextManipulate supportSave = new SupportTextManipulate();
            foreach (int _id in Enumerable.Range(0, 10))
            {
                Console.WriteLine("group: " + _id.ToString());
                if (sample.GetEnv(_id, out EnvStruct.Environment _train, out EnvStruct.Environment _test))
                {
                    //InterfaceConfig settings = new ConfigPumpkingSeed();
                    //InterfaceConfig settings = new ConfigWhiteWineBinary();
                    InterfaceConfig settings = new ConfigEFIfull();
                    //InterfaceConfig settings = new ConfigDryBean();
                    //InterfaceConfig settings = new ConfigPistachio16();
                    //InterfaceConfig settings = new ConfigPistachio28();
                    //InterfaceConfig settings = new ConfigRaisin();
                    //InterfaceConfig settings = new ConfigRiceCammeo();
                    //InterfaceConfig settings = new ConfigPulsar();
                    //InterfaceConfig settings = new ConfigPulsarImbalanced();
                    //InterfaceConfig settings = new ConfigFruit();
                    //InterfaceConfig settings = new ConfigFace();
                    //InterfaceConfig settings = new ConfigStress();
                    //InterfaceConfig settings = new ConfigIris();
                    //InterfaceConfig settings = new ConfigDiabete();
                    //InterfaceConfig settings = new ConfigMINIST();
                    //InterfaceConfig settings = new ConfigMSPET();
                    //InterfaceConfig settings = new ConfigFMINIST();
                    //InterfaceConfig settings = new ConfigGinger();


                    settings.GetConfig(out ConfigTrain tconfig);
                    ASCSEnter ascs = new ASCSEnter();
                    //ascs.Train(ref _train, ref tconfig, out Dictionary<string, Queue<RuleNeuron>> Model);
                    //double testAccuracy = ascs.Test(ref Model, ref _test);
                    ascs.TrainPlus(ref _train, ref tconfig, out Dictionary<string, Queue<RuleNeuron>> Model, ref _test);
                    double testAccuracy = ascs.TestPlus(ref Model, ref _test, out int _unMatch, out int _conflictNum);
                    accuracys.Add(testAccuracy);
                    sumAccuracy += testAccuracy;
                    Console.WriteLine("Complete");
                    Console.WriteLine(supportSave.SupportSave(ref Model, ref testAccuracy, ref _unMatch, ref _conflictNum, ref sample, _id));
                }

            }

            //正确率集合
            foreach (var result in accuracys)
            {
                Console.Write(result.ToString());
                Console.Write(" ");
            }
            Console.WriteLine("");
            sumAccuracy = sumAccuracy / accuracys.Count;
            Console.WriteLine("Arverage accuracy: " + sumAccuracy.ToString());

        }

        static void SingleTrain()
        {

            //InterfaceEnv sample = new EnvCIFAR10();
            //InterfaceEnv sample = new EnvSingleStress();
            //InterfaceEnv sample = new EnvSingleMusic();
            InterfaceEnv sample = new EnvSingleMNIST();
            //总正确率
            double sumAccuracy = 0;
            ArrayList accuracys = new ArrayList();
            SupportTextManipulate supportSave = new SupportTextManipulate();


            if (sample.GetEnv(0, out EnvStruct.Environment _train, out EnvStruct.Environment _test))
            {
                //InterfaceConfig settings = new ConfigCIFAR10();
                //InterfaceConfig settings = new ConfigStressSingle();
                //InterfaceConfig settings = new ConfigMusicSingle();
                InterfaceConfig settings = new ConfigMINIST();
                settings.GetConfig(out ConfigTrain tconfig);
                ASCSEnter ascs = new ASCSEnter();
                //ascs.Train(ref _train, ref tconfig, out Dictionary<string, Queue<RuleNeuron>> Model);
                //double testAccuracy = ascs.Test(ref Model, ref _test);
                ascs.TrainPlus(ref _train, ref tconfig, out Dictionary<string, Queue<RuleNeuron>> Model, ref _test);
                double testAccuracy = ascs.TestPlus(ref Model, ref _test, out int _unMatch, out int _conflictNum);
                accuracys.Add(testAccuracy);
                sumAccuracy += testAccuracy;
                Console.WriteLine("Complete");
                Console.WriteLine(supportSave.SupportSave(ref Model, ref testAccuracy, ref _unMatch, ref _conflictNum, ref sample, 0));
            }




            foreach (var result in accuracys)
            {
                Console.Write(result.ToString());
                Console.Write(" ");
            }
            Console.WriteLine("");
            sumAccuracy = sumAccuracy / accuracys.Count;
            Console.WriteLine("Arverage accuracy: " + sumAccuracy.ToString());

        }

        public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            //秒
            return ts3.TotalSeconds.ToString();
        }

    }
}
