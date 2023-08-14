
using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CIDS.DSCI.Prediction
{
    internal class PredictionWithEvaluateRules
    {

        internal static double PredictTest(ref Dictionary<string, Queue<RuleNeuron>> _model,
            ref EnvStruct.Environment _env, out int _unMatchNum, out int _conflictNum)
        {
            _unMatchNum = 0;
            _conflictNum = 0;
            InitialScore(ref _env.Labels, out Dictionary<string, int> Score);
            string SelectLabel;
            float correct = 0;
            foreach (DataRow instance in _env.DataOriginal.Rows)
            {
                foreach (var _label in _model.Keys)
                {
                    foreach (var _rule in _model[_label])
                    {
                        int obtained = RuleCover(_rule, instance, instance[_env.ClassName].ToString(), _label);

                        if (_rule.GenerateType == "search" && obtained > 0)
                        {
                            obtained = (int)(obtained * 0.1);
                            //Console.WriteLine(obtained);
                        }
                        Score[_label] = Score[_label] > obtained ? Score[_label] : obtained;

                        //if (obtained > 0)
                        //{
                        //     Score[_label] += obtained;
                        //    if (_rule.GenerateType=="basic")
                        //     {
                        //        Score[_label] = Score[_label] / 2;
                        //    }


                        // }
                    }
                }


                SelectLabel = LabelWinner(ref Score, out bool _unMatch, out bool _conflict);

                if (_unMatch) { _unMatchNum++; }
                if (_conflict) { _conflictNum++; }

                if (SelectLabel == instance[_env.ClassName].ToString())
                {
                    correct++;
                }

                RefreshScore(ref Score);
            }
            CalculateRuleAccuracy(ref _model);
            return (100 * correct / _env.InstanceTotalNum);
        }


        private static void InitialScore(ref HashSet<string> _labels, out Dictionary<string, int> _score)
        {
            _score = new Dictionary<string, int>();
            foreach (var _l in _labels)
            {
                _score.Add(_l, 0);
            }
        }


        private static void RefreshScore(ref Dictionary<string, int> _score)
        {
            foreach (var _key in _score.Keys)
            {
                _score[_key] = 0;
            }
        }


        private static int RuleCover(RuleNeuron _rule, DataRow _row, string _label, string _selectedLabel)
        {
            int answer = 0;

            bool cover = true;

            if (!FunctionMatch(_rule.Receptor, ref _row)) { return answer; }

            foreach (var _channel in _rule.Channel)
            {

                if (!FunctionMatch(_channel, ref _row))
                {
                    _channel.CoverNumber++;
                    if (_selectedLabel != _label) { _channel.CorrectNumber++; }
                    return answer;
                }

            }

            foreach (var _synapse in _rule.Synapse)
            {
                if (FunctionMatch(_synapse, ref _row))
                {

                    answer = answer > _synapse.Niche ? answer : _synapse.Niche;

                    if (cover)
                    {
                        _rule.CoverNumber++; cover = false;
                        if (_selectedLabel == _label) { _rule.CorrectNumber++; }
                    }
                    _synapse.CoverNumber++;
                    if (_selectedLabel == _label) { _synapse.CorrectNumber++; }


                }
            }
            return answer;
        }


        private static bool FunctionMatch(RuleBasic _fun, ref DataRow _row)
        {
            return PredictionFunction.Check(Convert.ToDouble(_row[_fun.Feature]), ref _fun);
        }


        private static string LabelWinner(ref Dictionary<string, int> _score, out bool _unMatch, out bool _conflict)
        {
            string answer = null;
            int old = 0;

            _conflict = false;
            foreach (var _label in _score.Keys)
            {
                if (_score[_label] > old)
                {
                    if (answer != null)
                    {
                        _conflict = true;
                    }
                    answer = _label;
                    old = _score[_label];
                }
            }
            _unMatch = false;

            if (answer == null)
            {
                _unMatch = true;
                Random rd = new Random();
                int index = rd.Next(_score.Keys.Count);
                answer = _score.Keys.ElementAt(index);
            }
            return answer;
        }


        public static void CalculateRuleAccuracy(ref Dictionary<string, Queue<RuleNeuron>> _model)
        {
            Dictionary<string, Queue<RuleNeuron>> changed = new Dictionary<string, Queue<RuleNeuron>>();
            foreach (var model in _model)
            {

                RuleNeuron[] rules = model.Value.ToArray();
                for (int i = 0; i < rules.Length; i++)
                {
                    if (rules[i].CoverNumber == 0)
                    {
                        rules[i].accuracy = 0;
                    }
                    else
                    {
                        rules[i].accuracy = 1.0 * rules[i].CorrectNumber / rules[i].CoverNumber;
                    }
                }
                Array.Sort(rules);
                Queue<RuleNeuron> rulesWithRecored = new Queue<RuleNeuron>();
                foreach (var individual in rules)
                {
                    rulesWithRecored.Enqueue(individual);
                }
                changed.Add(model.Key, rulesWithRecored);
            }
            _model = changed;
        }
    }
}
