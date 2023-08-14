using System;
using System.Text;

namespace CIDS.DSCI.CIDSPre
{

    internal class PreFragmentInformation : IComparable<PreFragmentInformation>
    {

        internal int Start;


        internal int End;


        internal int Cover;


        internal string Feature;


        public int CompareTo(PreFragmentInformation obj)
        {
            int comResult;

            if (this.Cover == obj.Cover)
            {
                comResult = 0;
            }
            else
            {

                if (this.Cover < obj.Cover)
                {
                    comResult = 1;
                }
                else
                {
                    comResult = -1;
                }
            }

            return comResult;
        }


        internal PreFragmentInformation(int _start, int _end, int _cover, ref string _feature)
        {
            this.Start = _start;
            this.End = _end;
            this.Cover = _cover;
            this.Feature = _feature;
        }


        public override string ToString()
        {

            StringBuilder result = new StringBuilder(120);
            result.Append(" 特征: ");
            result.Append(this.Feature);
            result.Append(" 起始点: ");
            result.Append(this.Start.ToString());
            result.Append(" 终止点: ");
            result.Append(this.End.ToString());
            result.Append(" 覆盖点: ");
            result.Append(this.Cover.ToString());
            return result.ToString();
        }
    }
}
