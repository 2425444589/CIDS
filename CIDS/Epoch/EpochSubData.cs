using System;


namespace CIDS.DSCI.Epoch

{
    internal class EpochSubData : IComparable<EpochSubData>
    {

        internal int Id;

        internal int Index;

        internal bool Conflict;

        internal bool RelativityLabel;



        public int CompareTo(EpochSubData obj)
        {
            int comResult;

            if (this.Id == obj.Id)
            {
                comResult = 0;
            }
            else
            {

                if (this.Id > obj.Id)
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

        internal EpochSubData(int _id)
        {
            this.Id = _id;
        }
    }
}
