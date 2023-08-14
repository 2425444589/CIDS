using CIDS.Config;
using CIDS.DSCI.CIDSPre;
using CIDS.DSCI.RuleBuilder;
using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIDS.DSCI.Epoch
{
    internal static class EpochBasic
    {

        internal static int EpochRandomSelectPoint(Random _random, ref Dictionary<string, HashSet<int>> _interest, ref string _label)
        {
            int cut = _random.Next(0, _interest[_label].Count);
            int value = _interest[_label].ElementAt(cut);
            return value;
        }


        private static void GetSelectedInstance(ref Dictionary<string, PreAtomData[]> _data, ref RuleFragmentInformation _presented,
           out HashSet<int> _dataPositive, out HashSet<int> _dataNegative)
        {
            _dataPositive = new HashSet<int>();

            _dataNegative = new HashSet<int>();
            foreach (var id in Enumerable.Range(_presented.Start, _presented.Cover))
            {
                if (_data[_presented.Feature][id].RelativityLabel)
                {
                    _dataPositive.Add(_data[_presented.Feature][id].Index);
                }
                else
                {
                    _dataNegative.Add(_data[_presented.Feature][id].Index);
                }
            }
        }


        private static void GetSubData(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, int[]> _searchTable,
            ref HashSet<int> _dataPositive, ref HashSet<int> _dataNegative, string _feature, out EpochSubData[] _subData)
        {

            Queue<EpochSubData> dataQ = new Queue<EpochSubData>();
            foreach (var id in _dataPositive)
            {
                EpochSubData instance = new EpochSubData(_searchTable[_feature][id]);

                instance.RelativityLabel = _data[_feature][instance.Id].RelativityLabel;

                instance.Index = id;
                dataQ.Enqueue(instance);
            }

            foreach (var id in _dataNegative)
            {

                EpochSubData instance = new EpochSubData(_searchTable[_feature][id]);

                instance.RelativityLabel = _data[_feature][instance.Id].RelativityLabel;

                instance.Index = id;
                dataQ.Enqueue(instance);
            }
            _subData = dataQ.ToArray();

            Array.Sort(_subData);

            SetConflictSubData(ref _data, ref _feature, ref _subData);
        }


        private static void SetConflictSubData(ref Dictionary<string, PreAtomData[]> _data, ref string _feature, ref EpochSubData[] _dataSub)
        {

            int FrontId;

            int Size;

            int Same;

            bool Conflict;



            FrontId = -1;


            _dataSub[0].Conflict = false;

            Size = _dataSub.Length - 1;

            if (Size < 1) { return; }

            Same = 1;


            Conflict = false;


            foreach (var id in Enumerable.Range(1, Size))
            {

                FrontId++;


                _dataSub[id].Conflict = false;

                if (Conflict)
                {

                    if (!_data[_feature][_dataSub[id].Id].Value.Equals(_data[_feature][_dataSub[FrontId].Id].Value))
                    {

                        Same = 1;
                        Conflict = false;
                        continue;
                    }


                    _dataSub[id].Conflict = true;
                    continue;
                }


                if ((_data[_feature][_dataSub[id].Id].Value == _data[_feature][_dataSub[FrontId].Id].Value
                    ) & (!_dataSub[id].RelativityLabel.Equals(_dataSub[FrontId].RelativityLabel)))
                {
                    Conflict = true;

                    Same++;

                    foreach (var count in Enumerable.Range(0, Same))
                    {
                        _dataSub[id - count].Conflict = true;
                    }
                    continue;
                }

                if (_data[_feature][_dataSub[id].Id].Value == _data[_feature][_dataSub[FrontId].Id].Value)
                {

                    Same++;
                }

                else
                {
                    Same = 1;
                }

            }

        }


        private static void ReviseData(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, EpochSubData[]> _dataSub, ref HashSet<int> _remain, ref int _instanceNum, out Dictionary<string, EpochSubData[]> _dataSubAfter)
        {
            int count;
            _dataSubAfter = new Dictionary<string, EpochSubData[]>();
            foreach (var _feature in _dataSub.Keys)
            {
                string feature = _feature;
                count = 0;
                EpochSubData[] NewData = new EpochSubData[_instanceNum];
                foreach (var _fragment in _dataSub[_feature])
                {

                    if (_fragment.RelativityLabel || _remain.Contains(_fragment.Index))
                    {
                        NewData[count] = _fragment;
                        count++;
                    }
                }

                SetConflictSubData(ref _data, ref feature, ref NewData);

                _dataSubAfter.Add(_feature, NewData);
            }
        }


        private static void ChangeSelectState(ref Dictionary<string, int[]> _searchTable, ref Dictionary<string, PreAtomData[]> _data, ref HashSet<int> _select)
        {
            foreach (var _feature in _data.Keys)
            {
                foreach (var id in _select)
                {

                    _data[_feature][_searchTable[_feature][id]].Select = true;
                }
            }
        }



        private static void ReceptorBuilder(ref Dictionary<string, PreAtomData[]> _data, ref RuleFragmentInformation _fragment, ref int _instanceNum, ref string _label,
            ref Dictionary<string, float> _boundary, out RuleBasic _condition)
        {
            _condition = new RuleBasic(ref _fragment.Feature);
            _condition.Niche = _fragment.End - _fragment.Start + 1;

            if (_fragment.Start == 0 & _fragment.End == _instanceNum)
            {
                _condition.Id = 4;
                return;
            }

            if (_fragment.End == _instanceNum)
            {
                _condition.Id = 0;
                _condition.Lower = LowValue(ref _data, ref _fragment.Start, ref _fragment.Feature, ref _label, ref _boundary);
                return;
            }

            if (_fragment.Start == 0)
            {
                _condition.Id = 1;
                _condition.Upper = UpValue(ref _data, ref _fragment.End, ref _fragment.Feature, ref _label, ref _boundary);
                return;
            }

            _condition.Id = 2;
            _condition.Upper = UpValue(ref _data, ref _fragment.End, ref _fragment.Feature, ref _label, ref _boundary);
            _condition.Lower = LowValue(ref _data, ref _fragment.Start, ref _fragment.Feature, ref _label, ref _boundary);
        }




        private static void ChannelBuilder(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, EpochSubData[]> _dataSub, RuleFragmentInformation _fragment,
             ref int _instanceNum, ref string _label, ref Dictionary<string, float> _boundary, out RuleBasic _condition)
        {
            _condition = new RuleBasic(ref _fragment.Feature);
            _condition.Niche = _fragment.End - _fragment.Start + 1;

            if (_fragment.Start == 0 & _fragment.End == _instanceNum)
            {
                _condition.Id = 4;
                return;
            }


            if (_fragment.End == _instanceNum)
            {
                _condition.Id = 6;
                _condition.Lower = LowValueSubData(ref _data, ref _dataSub, ref _fragment.Start, ref _fragment.Feature, ref _label, ref _boundary);
                return;
            }


            if (_fragment.Start == 0)
            {
                _condition.Id = 5;
                _condition.Upper = UpValueSubData(ref _data, ref _dataSub, ref _fragment.End, ref _fragment.Feature, ref _label, ref _boundary);
                return;
            }

            _condition.Id = 3;
            _condition.Upper = UpValueSubData(ref _data, ref _dataSub, ref _fragment.End, ref _fragment.Feature, ref _label, ref _boundary);
            _condition.Lower = LowValueSubData(ref _data, ref _dataSub, ref _fragment.Start, ref _fragment.Feature, ref _label, ref _boundary);
        }



        private static void SynapseBuilder(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, EpochSubData[]> _dataSub, RuleFragmentInformation _fragment,
             ref int _instanceNum, ref string _label, ref Dictionary<string, float> _boundary, out RuleBasic _condition)
        {
            _condition = new RuleBasic(ref _fragment.Feature);
            _condition.Niche = _fragment.End - _fragment.Start + 1;

            if (_fragment.Start == 0 & _fragment.End == _instanceNum)
            {
                _condition.Id = 4;

                return;
            }


            if (_fragment.End == _instanceNum)
            {
                _condition.Id = 0;
                _condition.Lower = LowValueSubData(ref _data, ref _dataSub, ref _fragment.Start, ref _fragment.Feature, ref _label, ref _boundary);
                return;
            }


            if (_fragment.Start == 0)
            {
                _condition.Id = 1;
                _condition.Upper = UpValueSubData(ref _data, ref _dataSub, ref _fragment.End, ref _fragment.Feature, ref _label, ref _boundary);
                return;
            }

            _condition.Id = 2;
            _condition.Upper = UpValueSubData(ref _data, ref _dataSub, ref _fragment.End, ref _fragment.Feature, ref _label, ref _boundary);
            _condition.Lower = LowValueSubData(ref _data, ref _dataSub, ref _fragment.Start, ref _fragment.Feature, ref _label, ref _boundary);
        }


        private static double LowValue(ref Dictionary<string, PreAtomData[]> _data, ref int _id, ref string _feature,
            ref string _label, ref Dictionary<string, float> _boundary)
        {
            double answer = _data[_feature][_id].Value - (_data[_feature][_id].Value - _data[_feature][_id - 1].Value) * _boundary[_label];
            return answer;
        }

        private static double LowValueSubData(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, EpochSubData[]> _dataSub, ref int _id, ref string _feature,
           ref string _label, ref Dictionary<string, float> _boundary)
        {
            double answer = _data[_feature][_dataSub[_feature][_id].Id].Value -
                (_data[_feature][_dataSub[_feature][_id].Id].Value - _data[_feature][_dataSub[_feature][_id - 1].Id].Value) * _boundary[_label];
            return answer;
        }


        private static double UpValue(ref Dictionary<string, PreAtomData[]> _data, ref int _id, ref string _feature,
            ref string _label, ref Dictionary<string, float> _boundary)
        {
            double answer = _data[_feature][_id].Value + (_data[_feature][_id + 1].Value - _data[_feature][_id].Value) * _boundary[_label];
            return answer;
        }


        private static double UpValueSubData(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, EpochSubData[]> _dataSub, ref int _id, ref string _feature,
            ref string _label, ref Dictionary<string, float> _boundary)
        {
            double answer = _data[_feature][_dataSub[_feature][_id].Id].Value + (_data[_feature][_dataSub[_feature][_id + 1].Id].Value - _data[_feature][_dataSub[_feature][_id].Id].Value) * _boundary[_label];
            return answer;
        }


        private static RuleNeuron RuleBuilder(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, EpochSubData[]> _AbdataSub, ref Dictionary<string, EpochSubData[]> _SbdataSub,
            ref RuleFragmentInformation _fragmentEnter,
            ref Queue<RuleFragmentInformation> _abFragment, ref Queue<RuleFragmentInformation> _sbFragment, ref string _label, ref Dictionary<string, float> _boundary)
        {

            RuleNeuron rule = new RuleNeuron();

            int instanceNum = _data.FirstOrDefault().Value.Length - 1;
            ReceptorBuilder(ref _data, ref _fragmentEnter, ref instanceNum, ref _label, ref _boundary, out rule.Receptor);

            Queue<RuleBasic> AbConditions = new Queue<RuleBasic>();
            instanceNum = _AbdataSub.FirstOrDefault().Value.Length - 1;
            foreach (var _fragment in _abFragment)
            {
                ChannelBuilder(ref _data, ref _AbdataSub, _fragment, ref instanceNum, ref _label, ref _boundary, out RuleBasic AbCondition);
                AbConditions.Enqueue(AbCondition);
            }
            rule.Channel = AbConditions.ToArray();


            Queue<RuleBasic> SbConditions = new Queue<RuleBasic>();
            instanceNum = _SbdataSub.FirstOrDefault().Value.Length - 1;
            foreach (var _fragment in _sbFragment)
            {
                SynapseBuilder(ref _data, ref _SbdataSub, _fragment, ref instanceNum, ref _label, ref _boundary, out RuleBasic SbCondition);
                SbConditions.Enqueue(SbCondition);
            }
            rule.Synapse = SbConditions.ToArray();
            return rule;
        }





        internal static bool EpochBuildRule(ref EnvStruct.Environment _env, ref Dictionary<string, int[]> _searchTable,
            ref ConfigTrain _config, ref Dictionary<string, PreAtomData[]> _data, RuleFragmentInformation _fragment, ref Dictionary<string, float> _boundary,
            ref Dictionary<string, HashSet<int>> _interest, ref string _label, out RuleNeuron rule)
        {

            GetSelectedInstance(ref _data, ref _fragment, out HashSet<int> _dataPositive, out HashSet<int> _dataNegative);


            int BeginNum = _interest[_label].Count;


            _env.FeatureNames.Remove(_fragment.Feature);

            Dictionary<string, EpochSubData[]> SubDataDictionary = new Dictionary<string, EpochSubData[]>();

            foreach (var _feature in _env.FeatureNames)
            {
                GetSubData(ref _data, ref _searchTable, ref _dataPositive, ref _dataNegative, _feature, out EpochSubData[] DataBlock);
                SubDataDictionary.Add(_feature, DataBlock);
            }

            RuleAbsumption.AbsumptionExclude(ref _config.AbsumptionLimitationThreshold, ref _config.AbsumptionMin, ref _dataNegative, ref SubDataDictionary, out Queue<RuleFragmentInformation> AbConditions);

            int InstanceNum = _dataNegative.Count + _dataPositive.Count;

            ReviseData(ref _data, ref SubDataDictionary, ref _dataNegative, ref InstanceNum, out Dictionary<string, EpochSubData[]> SubDataDictionaryAfter);

            RuleSubsumption.SbsumptionInclude(ref _data, ref _config.SubsumptionSizeThreshold, ref _dataPositive, ref SubDataDictionaryAfter, out Queue<RuleFragmentInformation> SbConditions, out HashSet<int> PositiveCovered);

            _env.FeatureNames.Add(_fragment.Feature);

            _interest[_label].ExceptWith(PositiveCovered);





            if (BeginNum > _interest[_label].Count)
            {
                rule = RuleBuilder(ref _data, ref SubDataDictionary, ref SubDataDictionaryAfter, ref _fragment, ref AbConditions, ref SbConditions, ref _label, ref _boundary);
                rule.niche = PositiveCovered.Count;

                ChangeSelectState(ref _searchTable, ref _data, ref PositiveCovered);
                return true;
            }
            rule = new RuleNeuron();
            return false;
        }
    }
}
