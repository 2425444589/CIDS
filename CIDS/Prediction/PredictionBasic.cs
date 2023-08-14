using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace CIDS.DSCI.Prediction
{
    static class PredictionBasic
    {

        internal static double PredictTest(ref Dictionary<string, Queue<RuleNeuron>> _model, ref EnvStruct.Environment _env)
        {
            InitialScore(ref _env.Labels, out Dictionary<string, int> Score);
            string SelectLabel;
            float correct = 0;
            foreach (DataRow instance in _env.DataOriginal.Rows)
            {
                foreach (var _label in _model.Keys)
                {
                    foreach (var _rule in _model[_label])
                    {
                        int obtained = RuleCover(_rule, instance);
                        Score[_label] = Score[_label] > obtained ? Score[_label] : obtained;
                    }
                }

                SelectLabel = LabelWinner(ref Score);
                if (SelectLabel == instance[_env.ClassName].ToString())
                {
                    correct++;
                }

                RefreshScore(ref Score);
            }
            return (100 * correct / _env.InstanceTotalNum);
        }

        private static int RuleCover(RuleNeuron _rule, DataRow _row)
        {
            int answer = 0;

            if (!FunctionMatch(_rule.Receptor, ref _row)) { return answer; }

            foreach (var _channel in _rule.Channel)
            {

                if (!FunctionMatch(_channel, ref _row)) { return answer; }
            }

            foreach (var _synapse in _rule.Synapse)
            {
                if (FunctionMatch(_synapse, ref _row))
                {

                    answer = answer > _synapse.Niche ? answer : _synapse.Niche;
                }
            }
            return answer;
        }


        private static bool FunctionMatch(RuleBasic _fun, ref DataRow _row)
        {
            return PredictionFunction.Check(Convert.ToDouble(_row[_fun.Feature]), ref _fun);
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


        private static string LabelWinner(ref Dictionary<string, int> _score)
        {
            string answer = null;
            int old = 0;
            foreach (var _label in _score.Keys)
            {
                if (_score[_label] > old)
                {
                    answer = _label;
                    old = _score[_label];
                }
            }

            if (answer == null)
            {
                Random rd = new Random();
                int index = rd.Next(_score.Keys.Count);
                answer = _score.Keys.ElementAt(index);
            }
            return answer;
        }
    }
}
