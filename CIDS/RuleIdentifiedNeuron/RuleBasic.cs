using System.Text;

namespace CIDS.RuleIdentifiedNeuron
{
    class RuleBasic
    {

        public byte Id;

        public double Upper;

        public double Lower;

        public string Feature;

        public int Niche;

        public int CoverNumber = 0;


        public int CorrectNumber = 0;




        internal RuleBasic(ref string _feature)
        {
            this.Feature = _feature;
        }


        public override string ToString()
        {

            StringBuilder result = new StringBuilder(120);
            result.Append(" 特征: ");
            result.Append(this.Feature);
            result.Append(" 函数: ");
            switch (Id)
            {
                case 0:
                    result.Append(" 大于>=");
                    result.Append(Lower);
                    break;
                case 1:
                    result.Append(" 小于<=");
                    result.Append(Upper);
                    break;
                case 2:
                    result.Append(" 在于");
                    result.Append('[');
                    result.Append(Lower);
                    result.Append("  ");
                    result.Append(Upper);
                    result.Append(']');
                    break;
                case 3:
                    result.Append(" 不在于");
                    result.Append('[');
                    result.Append(Lower);
                    result.Append("  ");
                    result.Append(Upper);
                    result.Append(']');
                    break;
                case 4:
                    result.Append("全真");
                    break;
                case 5:
                    result.Append(" 大于>=");
                    result.Append(Upper);
                    break;
                case 6:
                    result.Append(" 小于<=");
                    result.Append(Lower);
                    break;
                default:
                    result.Append("异常");
                    break;
            }

            if (this.Niche > 0)
            {
                result.Append("  生态量: ");
                result.Append(this.Niche);
                result.Append(" 覆盖量: ");
                result.Append(this.CoverNumber);
                result.Append("正确数: ");
                result.Append(this.CorrectNumber);

            }
            return result.ToString();
        }
    }

    class RuleBasicBackUP
    {

        public byte Id;

        public double Upper;

        public double Lower;

        public string Feature;

        public int Niche;




        internal RuleBasicBackUP(ref string _feature)
        {
            this.Feature = _feature;
        }


        public override string ToString()
        {

            StringBuilder result = new StringBuilder(120);
            result.Append(" 特征: ");
            result.Append(this.Feature);
            result.Append(" 函数: ");
            switch (Id)
            {
                case 0:
                    result.Append(" 大于>=");
                    result.Append(Lower);
                    break;
                case 1:
                    result.Append(" 小于<=");
                    result.Append(Upper);
                    break;
                case 2:
                    result.Append(" 在于");
                    result.Append('[');
                    result.Append(Lower);
                    result.Append("  ");
                    result.Append(Upper);
                    result.Append(']');
                    break;
                case 3:
                    result.Append(" 不在于");
                    result.Append('[');
                    result.Append(Lower);
                    result.Append("  ");
                    result.Append(Upper);
                    result.Append(']');
                    break;
                case 4:
                    result.Append("全真");
                    break;
                case 5:
                    result.Append(" 大于>=");
                    result.Append(Upper);
                    break;
                case 6:
                    result.Append(" 小于<=");
                    result.Append(Lower);
                    break;
                default:
                    result.Append("异常");
                    break;
            }

            if (this.Niche > 0)
            {
                result.Append("  生态量: ");
                result.Append(this.Niche);
            }
            return result.ToString();
        }
    }
}
