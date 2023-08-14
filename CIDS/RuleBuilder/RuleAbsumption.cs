using CIDS.DSCI.Epoch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIDS.DSCI.RuleBuilder
{
    static class RuleAbsumption
    {

        internal static bool AbsumptionExclude(ref int _max, ref int _threshold, ref HashSet<int> _dataNegative,
            ref Dictionary<string, EpochSubData[]> _data, out Queue<RuleFragmentInformation> _candidate)
        {
            Queue<RuleFragmentInformation> CandidateQ = new Queue<RuleFragmentInformation>();
            foreach (var _feature in _data.Keys)
            {
                //策略生成
                ContinueSearch(_data[_feature], ref _threshold, _feature, ref CandidateQ);
            }
            return SetAndIsResultEmpty(_max, ref _dataNegative, ref CandidateQ, out _candidate);
        }


        private static bool SetAndIsResultEmpty(int _max, ref HashSet<int> _dataNegative, ref Queue<RuleFragmentInformation> _candidateQ, out Queue<RuleFragmentInformation> _candidates)
        {
            RuleFragmentInformation[] _candidateA = _candidateQ.ToArray();
            Array.Sort(_candidateA);
            _candidates = new Queue<RuleFragmentInformation>();
            foreach (var fragment in _candidateA)
            {

                if ((_max == 0) || _dataNegative.Count == 0)
                {
                    break;
                }

                if (_dataNegative.Overlaps(fragment.CoveredInstances))
                {
                    _candidates.Enqueue(fragment);
                    _dataNegative.ExceptWith(fragment.CoveredInstances);
                    _max--;
                }
            }

            return !_candidates.Count.Equals(0);
        }


        private static void SetCoveredInstanceId(ref EpochSubData[] _data, ref RuleFragmentInformation _candidate)
        {
            foreach (var id in Enumerable.Range(_candidate.Start, _candidate.Interest))
            {
                _candidate.CoveredInstances.Add(_data[id].Index);
            }
        }


        private static void ContinueSearch(EpochSubData[] _data, ref int _threshold, string _feature,
            ref Queue<RuleFragmentInformation> _candidate)
        {

            int link = 1;

            int FrontIndex = -1;

            int Size = _data.Length - 1;

            if (Size < 1) { return; }

            foreach (var id in Enumerable.Range(1, Size))
            {

                FrontIndex++;

                if (id == Size)
                {

                    if (_data[FrontIndex].Conflict & _data[id].Conflict)
                    {
                        return;
                    }

                    if ((!_data[FrontIndex].Conflict) & _data[id].Conflict)
                    {

                        if (link >= _threshold & (!_data[FrontIndex].RelativityLabel))
                        {

                            RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                            SetCoveredInstanceId(ref _data, ref Fragment);
                            _candidate.Enqueue(Fragment);
                        }
                        return;
                    }

                    if (_data[FrontIndex].Conflict & (!_data[id].Conflict))
                    {

                        link = 1;
                        if (link >= _threshold & (!_data[FrontIndex].RelativityLabel))
                        {

                            RuleFragmentInformation Fragment = new RuleFragmentInformation(id, id, link, ref _feature);
                            SetCoveredInstanceId(ref _data, ref Fragment);
                            _candidate.Enqueue(Fragment);
                        }
                        return;
                    }

                    if ((!_data[FrontIndex].RelativityLabel) & (!_data[id].RelativityLabel))
                    {
                        link++;
                        if (link >= _threshold)
                        {
                            RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link + 1, id, link, ref _feature);
                            SetCoveredInstanceId(ref _data, ref Fragment);
                            _candidate.Enqueue(Fragment);
                        }
                        return;
                    }

                    if (link >= _threshold & (!_data[FrontIndex].RelativityLabel))
                    {
                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        SetCoveredInstanceId(ref _data, ref Fragment);
                        _candidate.Enqueue(Fragment);
                        return;
                    }

                    link = 1;
                    if (link >= _threshold & (!_data[id].RelativityLabel))
                    {
                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id, id, link, ref _feature);
                        SetCoveredInstanceId(ref _data, ref Fragment);
                        _candidate.Enqueue(Fragment);
                    }
                    return;
                }


                if (_data[FrontIndex].Conflict & _data[id].Conflict)
                {
                    continue;
                }

                if ((!_data[FrontIndex].Conflict) & _data[id].Conflict)
                {

                    if (link >= _threshold & (!_data[FrontIndex].RelativityLabel))
                    {

                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        SetCoveredInstanceId(ref _data, ref Fragment);
                        _candidate.Enqueue(Fragment);
                    }
                    continue;
                }
                if (_data[FrontIndex].Conflict & (!_data[id].Conflict))
                {

                    link = 1;
                    continue;
                }


                if ((!_data[FrontIndex].RelativityLabel) & (!_data[id].RelativityLabel))
                {
                    link++;
                    continue;
                }

                if ((!_data[FrontIndex].RelativityLabel) & _data[id].RelativityLabel)
                {

                    if (link >= _threshold)
                    {

                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        SetCoveredInstanceId(ref _data, ref Fragment);
                        _candidate.Enqueue(Fragment);
                    }
                    continue;
                }

                if (_data[FrontIndex].RelativityLabel & (!_data[id].RelativityLabel))
                {

                    link = 1;
                    continue;
                }

            }
        }
    }
}
