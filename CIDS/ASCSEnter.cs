using CIDS.Config;
using CIDS.DSCI.CIDSPre;
using CIDS.DSCI.Epoch;
using CIDS.DSCI.Prediction;
using CIDS.DSCI.RuleBuilder;
using CIDS.DSCI.Support;
using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CIDS.DSCI
{
    class ASCSEnter
    {

        readonly PreCIDSInit ASCSBegin = new PreCIDSInit();

        readonly PreEnterFeatureDetermine ASCSFeatureEnter = new PreEnterFeatureDetermine();

        readonly Random random = new Random();

        public bool Train(ref EnvStruct.Environment _envTrain, ref ConfigTrain _config, out Dictionary<string, Queue<RuleNeuron>> Model)
        {

            ASCSBegin.InitSetting(out Dictionary<string, PreAtomData[]> TrainSet, out Dictionary<string, int[]> TrainSearchTable,
                out Dictionary<string, HashSet<int>> Interest, out Dictionary<string, int> Boundarys, ref _envTrain, ref _config);

            ASCSBegin.ModelInit(ref _envTrain.Labels, out Model);

            bool CeaseUnFull = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var _label in _envTrain.Labels)
            {
                string label = _label;
                ASCSBegin.LabelSetting(ref label, ref TrainSet);
                ASCSFeatureEnter.IdentifyEnterFeatures(ref TrainSet, ref _envTrain, ref _config, ref label, out string[] Selected,
                    out Dictionary<string, Queue<PreFragmentInformation>> GoodCandidate);

                foreach (var _feature in Selected)
                {
                    foreach (var _candidate in GoodCandidate[_feature])
                    {
                        if (RuleSubsumption.SubsumptionExtension(ref TrainSet, _candidate, ref label, ref _envTrain.InstanceTotalNum, ref Boundarys, out RuleFragmentInformation represented))
                        {

                            if (EpochBasic.EpochBuildRule(ref _envTrain, ref TrainSearchTable, ref _config, ref TrainSet, represented, ref _config.Boundary, ref Interest, ref label, out RuleNeuron rule))
                            {
                                Model[_label].Enqueue(rule);
                            }
                        }
                    }
                }

                int dead = 0;

                while (Interest[_label].Count > 0 & dead < _config.DeadCount)
                {
                    int interestId = EpochBasic.EpochRandomSelectPoint(random, ref Interest, ref label);
                    RuleSubsumption.SubsumptionDilate(ref TrainSet, ref TrainSearchTable, ref interestId, ref label, ref _envTrain.InstanceTotalNum, ref Boundarys, ref Selected, out RuleFragmentInformation[] represented);
                    foreach (var fragment in represented)
                    {
                        if (EpochBasic.EpochBuildRule(ref _envTrain, ref TrainSearchTable, ref _config, ref TrainSet, fragment, ref _config.Boundary, ref Interest, ref label, out RuleNeuron rule))
                        {
                            dead = 0;
                            Model[_label].Enqueue(rule);
                            break;
                        }
                        dead++;
                    }
                    if (dead == _config.DeadCount)
                    {
                        CeaseUnFull = true;
                    }
                }

            }
            watch.Stop();

            TimeSpan span = watch.Elapsed;
            SupportASCS support = new SupportASCS();

            support.CountModelSize(ref Model);
            support.PrintEnvironment(ref _envTrain, "训练集");
            if (CeaseUnFull)
            {
                Console.WriteLine("未达成完美训练");
            }
            else
            {
                Console.WriteLine("训练集完全覆盖");
            }
            Console.WriteLine("总训练时长" + span.TotalSeconds + "s");
            Console.WriteLine("训练正确率");
            Console.WriteLine(PredictionBasic.PredictTest(ref Model, ref _envTrain).ToString() + "%");
            return true;
        }

        public double Test(ref Dictionary<string, Queue<RuleNeuron>> _model, ref EnvStruct.Environment _envTest)
        {
            double accuracy = PredictionBasic.PredictTest(ref _model, ref _envTest);
            SupportASCS support = new SupportASCS();
            Console.WriteLine("======================");
            support.PrintEnvironment(ref _envTest, "测试集");
            Console.WriteLine("测试正确率");
            Console.WriteLine(accuracy.ToString() + "%");
            Console.WriteLine("======================");
            return accuracy;
        }



        public bool TrainPlus(ref EnvStruct.Environment _envTrain, ref ConfigTrain _config, out Dictionary<string, Queue<RuleNeuron>> Model, ref EnvStruct.Environment _envTest)
        {

            ASCSBegin.InitSetting(out Dictionary<string, PreAtomData[]> TrainSet, out Dictionary<string, int[]> TrainSearchTable,
                out Dictionary<string, HashSet<int>> Interest, out Dictionary<string, int> Boundarys, ref _envTrain, ref _config);

            ASCSBegin.ModelInit(ref _envTrain.Labels, out Model);

            bool CeaseUnFull = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var _label in _envTrain.Labels)
            {
                Console.WriteLine(_label + "Begin");
                string label = _label;
                ASCSBegin.LabelSetting(ref label, ref TrainSet);

                ASCSFeatureEnter.IdentifyEnterFeatures(ref TrainSet, ref _envTrain, ref _config, ref label, out string[] Selected,
                    out Dictionary<string, Queue<PreFragmentInformation>> GoodCandidate);

                foreach (var _feature in Selected)
                {
                    foreach (var _candidate in GoodCandidate[_feature])
                    {
                        if (RuleSubsumption.SubsumptionExtension(ref TrainSet, _candidate, ref label, ref _envTrain.InstanceTotalNum, ref Boundarys, out RuleFragmentInformation represented))
                        {

                            if (EpochBasic.EpochBuildRule(ref _envTrain, ref TrainSearchTable, ref _config, ref TrainSet, represented, ref _config.Boundary, ref Interest, ref label, out RuleNeuron rule))
                            {
                                rule.GenerateType = "basic";
                                Model[_label].Enqueue(rule);
                            }
                        }
                    }
                }

                int dead = 0;

                while (Interest[_label].Count > 0 & dead < _config.DeadCount)
                {
                    int interestId = EpochBasic.EpochRandomSelectPoint(random, ref Interest, ref label);
                    RuleSubsumption.SubsumptionDilate(ref TrainSet, ref TrainSearchTable, ref interestId, ref label, ref _envTrain.InstanceTotalNum, ref Boundarys, ref Selected, out RuleFragmentInformation[] represented);
                    foreach (var fragment in represented)
                    {
                        if (EpochBasic.EpochBuildRule(ref _envTrain, ref TrainSearchTable, ref _config, ref TrainSet, fragment, ref _config.Boundary, ref Interest, ref label, out RuleNeuron rule))
                        {
                            dead = 0;
                            rule.GenerateType = "search";
                            Model[_label].Enqueue(rule);
                            break;
                        }
                        dead++;
                    }
                    if (dead == _config.DeadCount)
                    {
                        CeaseUnFull = true;
                    }
                }

                TestPlus(ref Model, ref _envTest, out int unMatcR, out int conflitR);
            }
            watch.Stop();

            TimeSpan span = watch.Elapsed;
            SupportASCS support = new SupportASCS();

            //support.PrintModel(ref Model);
            support.CountModelSize(ref Model);
            support.PrintEnvironment(ref _envTrain, "训练集");
            if (CeaseUnFull)
            {
                Console.WriteLine("未达成完美训练");
            }
            else
            {
                Console.WriteLine("训练集完全覆盖");
            }
            Console.WriteLine("总训练时长" + span.TotalSeconds + "s");
            Console.WriteLine("训练正确率");
            Console.WriteLine(PredictionBasic.PredictTest(ref Model, ref _envTrain).ToString() + "%");
            return true;
        }
        public double TestPlus(ref Dictionary<string, Queue<RuleNeuron>> _model, ref EnvStruct.Environment _envTest, out int _unMatch, out int _conflictNum)
        {
            double accuracy = PredictionWithEvaluateRules.PredictTest(ref _model, ref _envTest, out _unMatch, out _conflictNum);
            SupportASCS support = new SupportASCS();
            Console.WriteLine("======================");
            support.PrintEnvironment(ref _envTest, "测试集");
            Console.WriteLine("测试正确率");
            Console.WriteLine(accuracy.ToString() + "%");
            Console.WriteLine("======================");
            //support.PrintModel(ref _model);
            return accuracy;
        }
    }
}
