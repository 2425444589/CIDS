using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace CIDS.Environment
{
    class EnvStruct
    {
        [Obfuscation(Exclude = true)]
        internal struct Environment
        {

            internal int FeatureNum;


            internal HashSet<string> Labels;


            internal HashSet<string> FeatureNames;


            internal DataTable DataOriginal;


            internal int InstanceTotalNum;


            internal Dictionary<string, int> InstanceEachLabelNum;


            internal string ClassName;


            internal int LabelNum;
        }
    }
}
