using CIDS.Environment;
using CIDS.RuleIdentifiedNeuron;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CIDS.DSCI.Support
{
    class SupportTextManipulate
    {

        private string SupportConvertModelToString(ref Dictionary<string, Queue<RuleNeuron>> _model, ref double _accuracy, ref int _unMatch, ref int _conflictNum)
        {
            string Result = "";
            Result += "正确率: " + _accuracy.ToString() + '\n';
            Result += "未覆盖:      " + _unMatch.ToString() + '\n';
            Result += "冲突覆盖:      " + _conflictNum.ToString() + '\n';
            int count = 0;
            foreach (var model in _model)
            {
                count += model.Value.Count;
                Result += "========================================" + '\n';
                Result += model.Key + "   " + model.Value.Count.ToString() + '\n';
                foreach (var rule in model.Value)
                {
                    Result += rule.ToString();
                }
            }
            return Result;
        }


        private string SupportGenerateName(ref InterfaceEnv _env, ref int id)
        {
            string name = @System.Environment.CurrentDirectory + @"\result\";
            System.DateTime current = new System.DateTime();
            name += DateTime.Now.ToLongDateString().ToString();
            name += "_" + current.Hour.ToString();
            name += "_" + current.Minute.ToString();
            name += "_" + current.Second.ToString();

            name += _env.GetName();
            name += id.ToString();
            name += ".txt";
            return name;
        }


        public bool SupportSave(ref Dictionary<string, Queue<RuleNeuron>> _model, ref double accuracy, ref int unMatch, ref int conflictNum, ref InterfaceEnv _env, int id)
        {
            try
            {
                string informaction = SupportConvertModelToString(ref _model, ref accuracy, ref unMatch, ref conflictNum);
                string name = SupportGenerateName(ref _env, ref id);
                using (FileStream fs = new FileStream(name, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.Write(informaction);
                        sw.Flush();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
