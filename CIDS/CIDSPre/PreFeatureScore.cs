using System;

namespace CIDS.DSCI.CIDSPre
{
    internal class PreFeatureScore : IComparable<PreFeatureScore>
    {

        internal string Feature;

        internal int Cover;


        internal PreFeatureScore(string _feature, int _value)
        {
            this.Feature = _feature;
            this.Cover = _value;
        }


        public int CompareTo(PreFeatureScore obj)
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
    }
}
