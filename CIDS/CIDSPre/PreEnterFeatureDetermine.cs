using CIDS.Config;
using CIDS.Environment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIDS.DSCI.CIDSPre
{

    class PreEnterFeatureDetermine
    {

        private void DetermineThresholdInitBasic(ref float _thresholdRate, ref Dictionary<string, int> _EachLabelNum,
            ref string _label, ref HashSet<string> _features,
            out int _thresholds, out Dictionary<string, Queue<PreFragmentInformation>> _candidates,
            out Dictionary<string, int> _scores)
        {

            _thresholds = (int)Math.Ceiling(_EachLabelNum[_label] * _thresholdRate);

            _candidates = new Dictionary<string, Queue<PreFragmentInformation>>();

            _scores = new Dictionary<string, int>();

            foreach (var feature in _features)
            {
                _candidates.Add(feature, new Queue<PreFragmentInformation>());
                _scores.Add(feature, 0);
            }
        }


        private void TranslateScoreToArray(ref Dictionary<string, int> _scores, out PreFeatureScore[] _scoresArray)
        {
            _scoresArray = new PreFeatureScore[_scores.Keys.Count];
            int count = 0;
            foreach (var score in _scores)
            {
                PreFeatureScore answer = new PreFeatureScore(score.Key, score.Value);
                _scoresArray[count] = answer;
                count++;
            }
        }


        private void ContinueSearch(ref Dictionary<string, PreAtomData[]> _data, ref int _threshold, ref string _feature,
            ref Dictionary<string, Queue<PreFragmentInformation>> _candidate)
        {

            int link = 1;

            int FrontIndex = -1;

            int Size = _data[_feature].Length - 1;

            if (Size < 1) { return; }

            foreach (var id in Enumerable.Range(1, Size))
            {

                FrontIndex++;

                if (id == Size)
                {

                    if (_data[_feature][FrontIndex].Conflict & _data[_feature][id].Conflict)
                    {
                        return;
                    }

                    if ((!_data[_feature][FrontIndex].Conflict) & _data[_feature][id].Conflict)
                    {

                        if (link >= _threshold & _data[_feature][FrontIndex].RelativityLabel)
                        {

                            PreFragmentInformation Fragment = new PreFragmentInformation(id - link, FrontIndex, link, ref _feature);
                            _candidate[_feature].Enqueue(Fragment);
                        }
                        return;
                    }

                    if (_data[_feature][FrontIndex].Conflict & (!_data[_feature][id].Conflict))
                    {

                        link = 1;
                        if (link >= _threshold & _data[_feature][id].RelativityLabel)
                        {

                            PreFragmentInformation Fragment = new PreFragmentInformation(id, id, link, ref _feature);
                            _candidate[_feature].Enqueue(Fragment);
                        }
                        return;
                    }

                    if (_data[_feature][FrontIndex].RelativityLabel & _data[_feature][id].RelativityLabel)
                    {
                        link++;
                        if (link >= _threshold)
                        {
                            PreFragmentInformation Fragment = new PreFragmentInformation(id - link + 1, id, link, ref _feature);
                            _candidate[_feature].Enqueue(Fragment);
                        }
                        return;
                    }

                    if (link >= _threshold & _data[_feature][FrontIndex].RelativityLabel)
                    {
                        PreFragmentInformation Fragment = new PreFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        _candidate[_feature].Enqueue(Fragment);
                        return;
                    }

                    link = 1;
                    if (link >= _threshold & _data[_feature][id].RelativityLabel)
                    {
                        PreFragmentInformation Fragment = new PreFragmentInformation(id, id, link, ref _feature);
                        _candidate[_feature].Enqueue(Fragment);
                    }
                    return;
                }

                if (_data[_feature][FrontIndex].Conflict & _data[_feature][id].Conflict)
                {
                    continue;
                }

                if ((!_data[_feature][FrontIndex].Conflict) & _data[_feature][id].Conflict)
                {

                    if (link >= _threshold & _data[_feature][FrontIndex].RelativityLabel)
                    {

                        PreFragmentInformation Fragment = new PreFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        _candidate[_feature].Enqueue(Fragment);
                    }
                    continue;
                }

                if (_data[_feature][FrontIndex].Conflict & (!_data[_feature][id].Conflict))
                {

                    link = 1;
                    continue;
                }


                if (_data[_feature][FrontIndex].RelativityLabel & _data[_feature][id].RelativityLabel)
                {
                    link++;
                    continue;
                }

                if (_data[_feature][FrontIndex].RelativityLabel & (!_data[_feature][id].RelativityLabel))
                {

                    if (link >= _threshold)
                    {
                        PreFragmentInformation Fragment = new PreFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        _candidate[_feature].Enqueue(Fragment);
                    }
                    continue;
                }

                if ((!_data[_feature][FrontIndex].RelativityLabel) & _data[_feature][id].RelativityLabel)
                {
                    link = 1;
                    continue;
                }

            }
        }


        private void SelectFeatures(ref Dictionary<string, Queue<PreFragmentInformation>> _candidate, ref Dictionary<string, int> _scores, ref int _max, out string[] _selected)
        {
            foreach (var feature in _candidate.Keys)
            {
                foreach (var inf in _candidate[feature])
                {
                    _scores[feature] += inf.Cover;
                }
            }

            TranslateScoreToArray(ref _scores, out PreFeatureScore[] _scoresArray);
            Array.Sort(_scoresArray);

            _selected = new string[_max];
            foreach (var id in Enumerable.Range(0, _max))
            {
                _selected[id] = _scoresArray[id].Feature;
            }
        }



        public void IdentifyEnterFeatures(ref Dictionary<string, PreAtomData[]> _data, ref EnvStruct.Environment _env, ref ConfigTrain _config,
            ref string _label, out string[] _selected, out Dictionary<string, Queue<PreFragmentInformation>> _candidate)
        {

            this.DetermineThresholdInitBasic(ref _config.EnterFeatureBatchRate, ref _env.InstanceEachLabelNum, ref _label, ref _env.FeatureNames,
                out int Threshold, out _candidate, out Dictionary<string, int> Scores);

            string CurrentFeature;
            foreach (var feature in _env.FeatureNames)
            {
                CurrentFeature = feature;
                this.ContinueSearch(ref _data, ref Threshold, ref CurrentFeature, ref _candidate);
            }

            this.SelectFeatures(ref _candidate, ref Scores, ref _config.BeginFeatureMax, out _selected);
        }

    }
}

