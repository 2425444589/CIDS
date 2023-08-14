using CIDS.RuleIdentifiedNeuron;

namespace CIDS.DSCI.Prediction
{
    static class PredictionFunction
    {

        private static bool FunctionLess(ref double _input, ref double _condition)
        {
            return _input <= _condition;
        }


        private static bool FunctionLarger(ref double _input, ref double _condition)
        {
            return _input >= _condition;
        }

        internal static bool Check(double _input, ref RuleBasic _base)
        {

            return _base.Id switch
            {

                0 => FunctionLarger(ref _input, ref _base.Lower),

                1 => FunctionLess(ref _input, ref _base.Upper),

                5 => FunctionLarger(ref _input, ref _base.Upper),

                6 => FunctionLess(ref _input, ref _base.Lower),

                2 => (Check(0, ref _input, ref _base.Lower) & Check(1, ref _input, ref _base.Upper)),



                3 => (Check(0, ref _input, ref _base.Upper) || Check(1, ref _input, ref _base.Lower)),

                4 => true,
                _ => false,
            };
        }

        /// <summary>
        /// 计算条件是否符合 符合为真 反之假
        /// </summary>
        /// <param name="_id">函数编号</param>
        /// <param name="_input">目标数据值</param>
        /// <param name="_value">条件值</param>
        /// <returns></returns>
        internal static bool Check(byte _id, ref double _input, ref double _value)
        {
            return _id switch
            {
                //0 大于
                0 => FunctionLarger(ref _input, ref _value),
                //1 小于
                1 => FunctionLess(ref _input, ref _value),
                //其他
                _ => false,
            };
        }
    }
}
