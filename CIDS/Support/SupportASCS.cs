using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections.Generic;
using System.Data;


namespace CIDS.DSCI.Support
{
    internal class SupportASCS
    {

        public void PrintModel(ref Dictionary<string, Queue<RuleNeuron>> _model)
        {
            int count = 0;
            foreach (var model in _model)
            {
                count += model.Value.Count;
                Console.WriteLine("========================================");
                Console.WriteLine(model.Key + "   " + model.Value.Count.ToString());
                foreach (var rule in model.Value)
                {
                    Console.WriteLine(rule.ToString());
                }
            }
            foreach (var model in _model)
            {
                Console.WriteLine("标签" + model.Key + ":" + model.Value.Count.ToString());
            }
            Console.WriteLine("总规则数量" + count.ToString());
        }


        public void PrintModelSymple(ref Dictionary<string, Queue<RuleNeuron>> _model)
        {
            int count = 0;
            foreach (var model in _model)
            {
                count += model.Value.Count;
                Console.WriteLine("标签" + model.Key + ":" + model.Value.Count.ToString());
            }
            Console.WriteLine("总规则数量" + count.ToString());
        }

        public void PrintEnvironment(ref EnvStruct.Environment _env, string _type)
        {
            Console.WriteLine(_type + "总数据数:" + _env.InstanceTotalNum.ToString());
            foreach (var num in _env.InstanceEachLabelNum)
            {
                Console.WriteLine(num.Key + ":" + num.Value.ToString());
            }
        }

        public void PrintInstance(DataRow _row)
        {
            string answer = "";
            foreach (var value in _row.ItemArray)
            {
                answer += " " + value.ToString();
            }
            Console.WriteLine(answer);
        }

        public void CountModelSize(ref Dictionary<string, Queue<RuleNeuron>> _model)
        {
            int count = 0;
            foreach (var model in _model)
            {
                count += model.Value.Count;
                Console.WriteLine("标签" + model.Key + ":" + model.Value.Count.ToString());
            }
            Console.WriteLine("总规则数量" + count.ToString());
        }
    }
}
