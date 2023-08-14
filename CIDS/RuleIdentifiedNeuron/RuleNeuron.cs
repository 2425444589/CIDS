using System;
using System.Text;

namespace CIDS.RuleIdentifiedNeuron
{
    //suppose to be identified neurons
    class RuleNeuron : IComparable<RuleNeuron>
    {


        public RuleBasic Receptor;


        public RuleBasic[] Channel;


        public RuleBasic[] Synapse;


        public int niche;


        public bool IsComplete = false;


        public string GenerateType = "";

        public int CoverNumber = 0;


        public int CorrectNumber = 0;


        public double accuracy = 0.0;

        public override string ToString()
        {
            int size = (Channel.Length + Synapse.Length + 5) * 120;
            StringBuilder result = new StringBuilder(size);
            result.Append("生态值:\n");
            result.Append(niche);
            result.Append("\n接收器:\n");
            result.Append(Receptor.ToString());
            result.Append("\n通道:\n");
            foreach (var chan in Channel)
            {
                result.Append(chan.ToString());
                result.Append('\n');
            }
            result.Append("\n树突:\n");
            foreach (var syn in Synapse)
            {
                result.Append(syn.ToString());
                result.Append('\n');
            }
            result.Append("类型");
            result.Append(GenerateType);
            result.Append('\n');
            result.Append("覆盖");
            result.Append(CoverNumber.ToString());
            result.Append('\n');
            result.Append("正确个数");
            result.Append(CorrectNumber.ToString());
            result.Append('\n');
            result.Append("正确率");
            result.Append(accuracy.ToString());
            result.Append('\n');

            return result.ToString();
        }


        public int CompareTo(RuleNeuron obj)
        {
            int comResult;

            if (this.CoverNumber == obj.CoverNumber & this.CorrectNumber == obj.CorrectNumber)
            {
                comResult = 0;
            }
            else
            {

                if (this.CoverNumber > obj.CoverNumber)
                {
                    comResult = -1;
                }

                else if (this.CoverNumber == obj.CoverNumber && this.CorrectNumber > obj.CorrectNumber)
                {
                    comResult = -1;
                }
                else
                {
                    comResult = 1;
                }
            }

            return comResult;
        }
    }


    class RuleNeuronBackUp
    {

        public RuleBasic Receptor;

        public RuleBasic[] Channel;

        public RuleBasic[] Synapse;

        public int niche;

        public bool IsComplete = false;


        public override string ToString()
        {
            int size = (Channel.Length + Synapse.Length + 1) * 120;
            StringBuilder result = new StringBuilder(size);
            result.Append("生态值:\n");
            result.Append(niche);
            result.Append("\n接收器:\n");
            result.Append(Receptor.ToString());
            result.Append("\n通道:\n");
            foreach (var chan in Channel)
            {
                result.Append(chan.ToString());
                result.Append('\n');
            }
            result.Append("\n树突:\n");
            foreach (var syn in Synapse)
            {
                result.Append(syn.ToString());
                result.Append('\n');
            }
            return result.ToString();
        }
    }
}
