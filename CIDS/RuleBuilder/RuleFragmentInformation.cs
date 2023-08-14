using System;
using System.Collections.Generic;
using System.Text;

namespace CIDS.DSCI.RuleBuilder
{
    class RuleFragmentInformation : IComparable<RuleFragmentInformation>
    {

        internal int Start;

        internal int End;

        internal string Feature;

        internal int Interest;
        internal int Cover;

        internal HashSet<int> CoveredInstances;


        internal RuleFragmentInformation(ref int _start, ref int _end, ref string _feature,
            ref int _interest, ref int _cover)
        {
            this.Start = _start;
            this.End = _end;
            this.Feature = _feature;
            this.Interest = _interest;
            this.Cover = _cover;
        }

        internal RuleFragmentInformation(int _start, int _end, int _interest, ref string _feature)
        {
            this.Start = _start;
            this.End = _end;
            this.Interest = _interest;
            this.Feature = _feature;
            this.CoveredInstances = new HashSet<int>();
        }

        internal RuleFragmentInformation()
        {
        }

        public int CompareTo(RuleFragmentInformation obj)
        {
            int comResult;
            if (this.Interest == obj.Interest & this.Cover == obj.Cover)
            {
                comResult = 0;
            }
            else
            {

                if (this.Interest > obj.Interest)
                {
                    comResult = -1;
                }

                else if (this.Interest == obj.Interest && this.Cover > obj.Cover)
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


        public override string ToString()
        {

            StringBuilder result = new StringBuilder(120);
            result.Append(" 特征: ");
            result.Append(this.Feature);
            result.Append(" 起始点: ");
            result.Append(this.Start);
            result.Append(" 终止点: ");
            result.Append(this.End);
            result.Append(" 覆盖兴趣点: ");
            result.Append(this.Interest);
            result.Append(" 覆盖点: ");
            result.Append(this.Cover);
            return result.ToString();
        }
    }
}
