using CIDS.DSCI.CIDSPre;
using CIDS.DSCI.Epoch;
using System;
using System.Collections.Generic;
using System.Linq;



namespace CIDS.DSCI.RuleBuilder
{
    static class RuleSubsumption
    {

        private static int CoveredInterestingNum(ref string _feature, ref int _start, ref int _end, ref Dictionary<string, PreAtomData[]> _data)
        {

            int count = 0;
            int span = _end - _start + 1;
            foreach (var id in Enumerable.Range(_start, span))
            {
                if (!_data[_feature][id].Select)
                {
                    count++;
                }
            }
            return count;
        }

        private static int ReCalculateBatch(ref Dictionary<string, int> _boundary, ref int _covered, ref string _label)
        {
            int ReBatch = 2 * _boundary[_label] - _covered;
            if (ReBatch <= 0) { return 0; }
            return (ReBatch + 2) / 2;
        }


        private static int CalculateImpurity(ref Dictionary<string, int> _boundary, ref string _label)
        {
            int impurity = 0;
            foreach (var _l in _boundary.Keys)
            {
                if (!_l.Equals(_label))
                {
                    impurity += _boundary[_l];
                }
            }
            return impurity;
        }


        private static int SearchPoint(ref int _location, ref int _end)
        {
            if (_location > _end)
            {
                return -1;
            }
            return 1;
        }


        private static void UpdateCover(ref Dictionary<string, PreAtomData[]> _data, ref string _feature, ref int _location,
            ref int _batch, ref int _impurity, ref int _interestNum)
        {

            if (_data[_feature][_location].RelativityLabel)
            {
                _batch--;
                if (!_data[_feature][_location].Select)
                {
                    _interestNum++;
                }
            }

            else
            {
                _impurity--;
            }
        }


        private static void FragmentDetermined(ref string _label, ref Dictionary<string, PreAtomData[]> _data, ref string _feature, int _location,
            int _end, int _batch, int _impurity, out int _interestNum, out int _answer)
        {

            _interestNum = 0;


            if (_location == _end)
            {

                UpdateCover(ref _data, ref _feature, ref _location, ref _batch, ref _impurity, ref _interestNum);
                _answer = _end;
                return;
            }

            int direction = SearchPoint(ref _location, ref _end);

            int front = _location;

            UpdateCover(ref _data, ref _feature, ref _location, ref _batch, ref _impurity, ref _interestNum);

            _location += direction;


            bool selected = false;


            while (!_location.Equals(_end))
            {

                //if (_impurity <= 0 & _batch <= 0)
                //{
                //    //值相同继续走
                //    if(_data[_feature][front].Value.Equals(_data[_feature][_location].Value))
                //    {
                //        continue;
                //    }
                //    //值不同 front是非目标类
                //    if (!_data[_feature][front].RelativityLabel.Equals(_label))
                //    {
                //        _answer = front;
                //        return;
                //    }
                //    //值不同 front是目标类 下一位不是
                //    if (!_data[_feature][_location].RelativityLabel.Equals(_label))
                //    {
                //        _answer = front;
                //        return;
                //    }
                //}


                if (_impurity <= 0 & _batch <= 0)
                {
                    //    //

                    if (_data[_feature][front].Conflict)
                    {

                        if (!_data[_feature][front].Value.Equals(_data[_feature][_location].Value))
                        {
                            _answer = front;
                            return;
                        }
                    }
                    else
                    {

                        if (!_data[_feature][front].RelativityLabel.Equals(_data[_feature][_location].RelativityLabel))
                        {
                            _answer = front;
                            return;
                        }

                        if (_data[_feature][front].Select)
                        {
                            selected = true;
                        }

                        if (selected)
                        {
                            if (!_data[_feature][front].Value.Equals(_data[_feature][_location].Value))
                            {
                                _answer = front;
                                return;
                            }
                        }
                    }
                }




                UpdateCover(ref _data, ref _feature, ref _location, ref _batch, ref _impurity, ref _interestNum);

                front = _location;

                _location += direction;

            }

            _answer = _end;
            return;
        }


        private static void MinusDuplicatedCoveredInterest(ref Dictionary<string, PreAtomData[]> _data, ref string _feature, ref int _point, ref int _interestNum)
        {
            if (_data[_feature][_point].RelativityLabel)
            {
                if (!_data[_feature][_point].Select)
                {
                    _interestNum--;
                }
            }
        }


        internal static bool SubsumptionExtension(ref Dictionary<string, PreAtomData[]> _data, PreFragmentInformation _raw, ref string _label,
             ref int _instanceNum, ref Dictionary<string, int> _boundarys, out RuleFragmentInformation _answer)
        {

            int InterestNum = CoveredInterestingNum(ref _raw.Feature, ref _raw.Start, ref _raw.End, ref _data);
            if (InterestNum == 0)
            {
                _answer = null;
                return false;
            }

            int batch = ReCalculateBatch(ref _boundarys, ref _raw.Cover, ref _label);
            int impurity = CalculateImpurity(ref _boundarys, ref _label);


            int cease = _instanceNum - 1;
            FragmentDetermined(ref _label, ref _data, ref _raw.Feature, _raw.End, cease, batch, impurity, out int frontCovered, out int frontEnd);

            FragmentDetermined(ref _label, ref _data, ref _raw.Feature, _raw.Start, 0, batch, impurity, out int backCovered, out int backEnd);

            InterestNum += backCovered + frontCovered;

            MinusDuplicatedCoveredInterest(ref _data, ref _raw.Feature, ref _raw.Start, ref InterestNum);
            MinusDuplicatedCoveredInterest(ref _data, ref _raw.Feature, ref _raw.End, ref InterestNum);

            if (InterestNum == 0)
            {
                _answer = new RuleFragmentInformation();
                return false;
            }
            int Cover = frontEnd - backEnd + 1;

            _answer = new RuleFragmentInformation(ref backEnd, ref frontEnd, ref _raw.Feature, ref InterestNum, ref Cover);
            return true;
        }



        private static void SetCoveredInstanceId(ref Dictionary<string, PreAtomData[]> _dataFull, ref EpochSubData[] _data, ref RuleFragmentInformation _candidate)
        {

            int unselected = 0;
            foreach (var id in Enumerable.Range(_candidate.Start, _candidate.Interest))
            {
                _candidate.CoveredInstances.Add(_data[id].Index);
                if (!_dataFull[_candidate.Feature][_data[id].Id].Select)
                {
                    unselected++;
                }
            }
            _candidate.Interest = unselected;
        }


        private static void ContinueSearch(ref Dictionary<string, PreAtomData[]> _dataFull, EpochSubData[] _data, ref int _threshold, string _feature,
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

                        if (link >= _threshold & _data[FrontIndex].RelativityLabel)
                        {

                            RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                            SetCoveredInstanceId(ref _dataFull, ref _data, ref Fragment);
                            _candidate.Enqueue(Fragment);
                        }
                        return;
                    }

                    if (_data[FrontIndex].Conflict & (!_data[id].Conflict))
                    {

                        link = 1;
                        if (link >= _threshold & _data[FrontIndex].RelativityLabel)
                        {

                            RuleFragmentInformation Fragment = new RuleFragmentInformation(id, id, link, ref _feature);
                            SetCoveredInstanceId(ref _dataFull, ref _data, ref Fragment);
                            _candidate.Enqueue(Fragment);
                        }
                        return;
                    }

                    if (_data[FrontIndex].RelativityLabel & _data[id].RelativityLabel)
                    {
                        link++;
                        if (link >= _threshold)
                        {
                            RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link + 1, id, link, ref _feature);
                            SetCoveredInstanceId(ref _dataFull, ref _data, ref Fragment);
                            _candidate.Enqueue(Fragment);
                        }
                        return;
                    }

                    if (link >= _threshold & _data[FrontIndex].RelativityLabel)
                    {
                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        SetCoveredInstanceId(ref _dataFull, ref _data, ref Fragment);
                        _candidate.Enqueue(Fragment);
                        return;
                    }

                    link = 1;
                    if (link >= _threshold & _data[id].RelativityLabel)
                    {
                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id, id, link, ref _feature);
                        SetCoveredInstanceId(ref _dataFull, ref _data, ref Fragment);
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

                    if (link >= _threshold & _data[FrontIndex].RelativityLabel)
                    {

                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        SetCoveredInstanceId(ref _dataFull, ref _data, ref Fragment);
                        _candidate.Enqueue(Fragment);
                    }
                    continue;
                }

                if (_data[FrontIndex].Conflict & (!_data[id].Conflict))
                {

                    link = 1;
                    continue;
                }


                if (_data[FrontIndex].RelativityLabel & _data[id].RelativityLabel)
                {
                    link++;
                    continue;
                }

                if (_data[FrontIndex].RelativityLabel & (!_data[id].RelativityLabel))
                {

                    if (link >= _threshold)
                    {
                        RuleFragmentInformation Fragment = new RuleFragmentInformation(id - link, FrontIndex, link, ref _feature);
                        SetCoveredInstanceId(ref _dataFull, ref _data, ref Fragment);
                        _candidate.Enqueue(Fragment);
                    }
                    continue;
                }

                if ((!_data[FrontIndex].RelativityLabel) & _data[id].RelativityLabel)
                {

                    link = 1;
                    continue;
                }

            }
        }


        private static bool SetAndIsResultEmpty(ref HashSet<int> _dataPositive, ref Queue<RuleFragmentInformation> _candidateQ, out Queue<RuleFragmentInformation> _candidates, out HashSet<int> covered)
        {
            RuleFragmentInformation[] _candidateA = _candidateQ.ToArray();
            Array.Sort(_candidateA);
            _candidates = new Queue<RuleFragmentInformation>();

            covered = new HashSet<int>();
            foreach (var fragment in _candidateA)
            {

                if (_dataPositive.Count == 0)
                {
                    break;
                }

                if (_dataPositive.Overlaps(fragment.CoveredInstances))
                {
                    _candidates.Enqueue(fragment);
                    _dataPositive.ExceptWith(fragment.CoveredInstances);

                    covered.UnionWith(fragment.CoveredInstances);

                }
                //else { _candidates.Enqueue(fragment); }
            }
            return !_candidates.Count.Equals(0);
        }


        internal static bool SbsumptionInclude(ref Dictionary<string, PreAtomData[]> _dataFull, ref float _thresholdRate, ref HashSet<int> _dataPositive,
            ref Dictionary<string, EpochSubData[]> _data, out Queue<RuleFragmentInformation> _candidate, out HashSet<int> _positiveCovered)
        {
            int _threshold = (int)Math.Ceiling(_dataPositive.Count * _thresholdRate);
            Queue<RuleFragmentInformation> CandidateQ = new Queue<RuleFragmentInformation>();
            foreach (var _feature in _data.Keys)
            {

                ContinueSearch(ref _dataFull, _data[_feature], ref _threshold, _feature, ref CandidateQ);
            }
            return SetAndIsResultEmpty(ref _dataPositive, ref CandidateQ, out _candidate, out _positiveCovered);
        }



        internal static void SubsumptionDilate(ref Dictionary<string, PreAtomData[]> _data, ref Dictionary<string, int[]> _searchTable, ref int _interestNum, ref string _label,
            ref int _instanceNum, ref Dictionary<string, int> _boundarys, ref string[] TopFeatures, out RuleFragmentInformation[] _answer)
        {

            Queue<RuleFragmentInformation> _find = new Queue<RuleFragmentInformation>();


            int cease = _instanceNum - 1;

            foreach (var _feature in TopFeatures)
            {
                int impurity = CalculateImpurity(ref _boundarys, ref _label);
                int batch = _boundarys[_label];
                string feature = _feature;

                FragmentDetermined(ref _label, ref _data, ref feature, _searchTable[_feature][_interestNum], cease, batch, impurity, out int frontCovered, out int frontEnd);

                FragmentDetermined(ref _label, ref _data, ref feature, _searchTable[_feature][_interestNum], 0, batch, impurity, out int backCovered, out int backEnd);

                int InterestNum = backCovered + frontCovered;

                MinusDuplicatedCoveredInterest(ref _data, ref feature, ref _searchTable[_feature][_interestNum], ref InterestNum);
                int Cover = frontEnd - backEnd + 1;

                RuleFragmentInformation candidate = new RuleFragmentInformation(ref backEnd, ref frontEnd, ref feature, ref InterestNum, ref Cover);
                _find.Enqueue(candidate);
            }
            _answer = _find.ToArray();
            Array.Sort(_answer);
        }

    }
}
