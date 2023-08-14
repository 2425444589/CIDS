using CIDS.Config;
using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CIDS.DSCI.CIDSPre
{
    class PreCIDSInit
    {

        private Dictionary<string, PreAtomData[]> DataTranslater(ref HashSet<string> _features, ref DataTable _dt, ref string _labelName, ref int _instanceSize)
        {

            Dictionary<string, PreAtomData[]> DecomposedData = new Dictionary<string, PreAtomData[]>();


            foreach (var feature in _features)
            {

                PreAtomData[] Atom = new PreAtomData[_instanceSize];

                foreach (int id in Enumerable.Range(0, _instanceSize))
                {

                    PreAtomData instance = new PreAtomData(double.Parse(_dt.Rows[id][feature].ToString()),
                        _dt.Rows[id][_labelName].ToString(), id);
                    Atom[id] = instance;
                }

                Array.Sort(Atom);
                DecomposedData.Add(feature, Atom);
            }

            return DecomposedData;
        }


        private Dictionary<string, int[]> DataSearchTable(ref Dictionary<string, PreAtomData[]> _data, ref int _instanceSize)
        {

            Dictionary<string, int[]> SearchTable = new Dictionary<string, int[]>();

            int count;

            foreach (var feature in _data)
            {

                int[] inf = new int[_instanceSize];
                SearchTable.Add(feature.Key, inf);
                count = 0;
                foreach (var instance in feature.Value)
                {

                    SearchTable[feature.Key][instance.Index] = count;
                    count++;
                }
            }
            return SearchTable;
        }



        private Dictionary<string, HashSet<int>> DataInterestSet(ref Dictionary<string, PreAtomData[]> _data, ref HashSet<string> _labels)
        {

            PreAtomData[] _instances = _data[_data.Keys.FirstOrDefault()];

            Dictionary<string, HashSet<int>> Interest = new Dictionary<string, HashSet<int>>();
            foreach (var label in _labels)
            {
                HashSet<int> ids = new HashSet<int>();
                Interest.Add(label, ids);
            }
            foreach (var instance in _instances)
            {
                Interest[instance.Label].Add(instance.Index);
            }
            return Interest;
        }

        private Dictionary<string, int> DataBoundary(ref float _batchSize, ref HashSet<string> _labels, ref Dictionary<string, int> _instanceEachNum)
        {
            Dictionary<string, int> Batches = new Dictionary<string, int>();
            int temp;
            foreach (var label in _labels)
            {

                temp = (int)Math.Ceiling(_instanceEachNum[label] * _batchSize);
                Batches.Add(label, temp);
            }
            return Batches;
        }


        internal void ModelInit(ref HashSet<string> _labels, out Dictionary<string, Queue<RuleNeuron>> Model)
        {

            Model = new Dictionary<string, Queue<RuleNeuron>>();
            foreach (var label in _labels)
            {
                Queue<RuleNeuron> Rules = new Queue<RuleNeuron>();
                Model.Add(label, Rules);
            }
        }


        internal void InitSetting(out Dictionary<string, PreAtomData[]> _trainset, out Dictionary<string, int[]> _searchTable, out Dictionary<string, HashSet<int>> _interest,
            out Dictionary<string, int> _batches, ref EnvStruct.Environment _env, ref ConfigTrain _config)
        {

            _trainset = this.DataTranslater(ref _env.FeatureNames, ref _env.DataOriginal, ref _env.ClassName, ref _env.InstanceTotalNum);

            _searchTable = this.DataSearchTable(ref _trainset, ref _env.InstanceTotalNum);

            _interest = this.DataInterestSet(ref _trainset, ref _env.Labels);

            _batches = this.DataBoundary(ref _config.DataRate, ref _env.Labels, ref _env.InstanceEachLabelNum);
        }



        private bool IsLabelTarget(ref string _target, ref string _label)
        {
            if (_target == _label)
            {
                return true;
            }
            return false;
        }


        private void SetConflictAtomData(ref string _label, ref Dictionary<string, PreAtomData[]> _data)
        {

            int FrontId;

            int Size;

            int Same;

            bool Conflict;

            bool CurrentLabel;
            foreach (var _feature in _data.Keys)
            {

                FrontId = -1;

                _data[_feature][0].RelativityLabel = this.IsLabelTarget(ref _label, ref _data[_feature][0].Label);

                _data[_feature][0].Conflict = false;

                Size = _data[_feature].Length - 1;

                if (Size < 1) { return; }

                Same = 1;


                Conflict = false;

                foreach (var id in Enumerable.Range(1, Size))
                {

                    FrontId++;

                    CurrentLabel = this.IsLabelTarget(ref _label, ref _data[_feature][id].Label);

                    _data[_feature][id].RelativityLabel = CurrentLabel;

                    _data[_feature][id].Conflict = false;

                    if (Conflict)
                    {

                        if (!_data[_feature][id].Value.Equals(_data[_feature][FrontId].Value))
                        {

                            Same = 1;
                            Conflict = false;
                            continue;
                        }


                        _data[_feature][id].Conflict = true;
                        continue;
                    }

                    if ((_data[_feature][id].Value == _data[_feature][FrontId].Value) & (!CurrentLabel.Equals(_data[_feature][FrontId].RelativityLabel)))
                    {
                        Conflict = true;

                        Same++;

                        foreach (var count in Enumerable.Range(0, Same))
                        {
                            _data[_feature][id - count].Conflict = true;
                        }
                        continue;
                    }


                    if (_data[_feature][id].Value == _data[_feature][FrontId].Value)
                    {

                        Same++;
                    }

                    else
                    {
                        Same = 1;
                    }

                }
            }

        }


        internal void LabelSetting(ref string _label, ref Dictionary<string, PreAtomData[]> _data)
        {

            this.SetConflictAtomData(ref _label, ref _data);
        }


        internal void ResetSelectState(ref Dictionary<string, PreAtomData[]> _data)
        {
            foreach (var _key in _data.Keys)
            {
                foreach (var id in Enumerable.Range(0, _data[_key].Length))
                {
                    _data[_key][id].Select = false;
                }
            }
        }

    }
}
