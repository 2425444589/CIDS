using System;
using System.Text;

namespace CIDS.DSCI.CIDSPre
{
    internal class PreAtomData : IComparable<PreAtomData>
    {

        internal double Value;
        internal string Label;
        internal int Index;
        internal bool Select;
        internal bool Conflict;
        internal bool RelativityLabel;



        public int CompareTo(PreAtomData obj)
        {
            int comResult;
            if (this.Value == obj.Value && this.Label == obj.Label)
            {
                comResult = 0;
            }
            else
            {
                if (this.Value > obj.Value)
                {
                    comResult = 1;
                }
                else if (this.Value == obj.Value && this.Label.CompareTo(obj.Label) > 0)
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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder(120);
            result.Append("行数: ");
            result.Append(this.Index.ToString());
            result.Append(" 值: ");
            result.Append(this.Value.ToString());
            result.Append(" 标签: ");
            result.Append(this.Label);
            result.Append(" 是否选中: ");
            result.Append(this.Select.ToString());
            result.Append(" 是否冲突: ");
            result.Append(this.Conflict.ToString());
            result.Append(" 相对类: ");
            result.Append(this.RelativityLabel.ToString());
            return result.ToString();
        }

        public PreAtomData(double _value, string _label, int _index)
        {
            this.Value = _value;
            this.Label = _label;
            this.Index = _index;
            this.Select = false;
        }
    }
}
