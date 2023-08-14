using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace CIDS.Environment
{
    class EnvFunctions
    {

        internal void PrintData(ref DataTable _dt)
        {
            string temp;

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                temp = "";
                for (int j = 0; j < _dt.Columns.Count; j++)
                {
                    temp += _dt.Rows[i][j] + " ";
                }
                Console.WriteLine(temp);
            }
        }


        private bool ReadCSVToDataTableHasTitle(ref string _filename, ref string _spliter, out DataTable _dt)
        {
            _dt = new DataTable();

            bool firstRow = true;

            string[] information;

            string eachLine;

            DataColumn featureName;

            DataRow instanceValue;
            try
            {
                using (FileStream fs = new FileStream(_filename, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    {

                        while ((eachLine = sr.ReadLine()) != null)
                        {
                            information = eachLine.Split(new string[] { _spliter }, StringSplitOptions.None);

                            if (firstRow)
                            {
                                firstRow = false;
                                foreach (var feature in information)
                                {
                                    featureName = new DataColumn(feature);
                                    _dt.Columns.Add(featureName);
                                }
                                continue;
                            }

                            instanceValue = _dt.Rows.Add(information);
                        }
                    }
                }

                return true;
            }
            catch
            {

                return false;
            }
        }


        private bool FileExist(ref string _address, ref string _dataName)
        {
            if (File.Exists(_address))
            {
                Console.WriteLine(String.Concat(_dataName, " found successfully"));
                return true;
            }
            Console.WriteLine(String.Concat(_dataName + " does not exist"));
            return false;
        }

        private void SetFeatureInformation(ref DataTable _dt, ref string _labelName, out int _featureNum, out HashSet<string> _featureNames)
        {

            _featureNames = new HashSet<string>();

            _featureNum = _dt.Columns.Count - 1;
            foreach (DataColumn feature in _dt.Columns)
            {
                _featureNames.Add(feature.ColumnName);
            }

            _featureNames.Remove(_labelName);
        }



        private void SetInstanceInformation(ref DataTable _df, ref string _labelName, out Dictionary<string, int> _instanceEachNum, out HashSet<string> _labelNames, out int _labelNum, out int _instanceTotal)
        {

            _labelNames = new HashSet<string>();

            _instanceEachNum = new Dictionary<string, int>();

            string label;

            foreach (DataRow row in _df.Rows)
            {

                label = row[_labelName].ToString();

                if (!_labelNames.Contains(label))
                {
                    _labelNames.Add(label);
                    _instanceEachNum.Add(label, 0);
                }

                _instanceEachNum[label]++;
            }

            _labelNum = _labelNames.Count;

            _instanceTotal = _df.Rows.Count;
        }



        public bool InitializeEnvironment(ref string _address, ref string _dataName, ref string _spliter, ref string _labelName, out EnvStruct.Environment _env)
        {

            _env = new EnvStruct.Environment();

            if (!FileExist(ref _address, ref _dataName))
            {
                return false;
            }

            if (!ReadCSVToDataTableHasTitle(ref _address, ref _spliter, out _env.DataOriginal))
            {
                return false;
            }

            SetFeatureInformation(ref _env.DataOriginal, ref _labelName, out _env.FeatureNum, out _env.FeatureNames);

            SetInstanceInformation(ref _env.DataOriginal, ref _labelName, out _env.InstanceEachLabelNum, out _env.Labels, out _env.LabelNum, out _env.InstanceTotalNum);
            _env.ClassName = _labelName;
            return true;
        }


        private bool DictionaryExist(ref string _address, ref string _dataName)
        {
            if (Directory.Exists(_address))
            {
                Console.WriteLine(String.Concat(_dataName + " found successfully"));
                return true;
            }
            Console.WriteLine(String.Concat(_dataName + " does not exist"));
            return false;
        }


        public bool InitializeEnvironmentTenFolders(ref string _address, ref string _dataName, ref string _spliter, ref string _labelName, int _testId, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {

            _envTrain = new EnvStruct.Environment();
            _envTest = new EnvStruct.Environment();
            if (!DictionaryExist(ref _address, ref _dataName))
            {
                return false;
            }

            DirectoryInfo root = new DirectoryInfo(_address);
            FileInfo[] files = root.GetFiles();


            bool IsFirst = true;

            foreach (var _f in files)
            {
                string _target = _f.FullName;
                int _id = int.Parse(_f.Name.Split('.')[0]);
                if (!ReadCSVToDataTableHasTitle(ref _target, ref _spliter, out DataTable DataOriginal))
                {
                    return false;
                }

                if (_testId == _id)
                {
                    _envTest.DataOriginal = DataOriginal;
                }
                else
                {
                    if (IsFirst)
                    {
                        _envTrain.DataOriginal = DataOriginal;
                        IsFirst = false;
                    }
                    else
                    {

                        _envTrain.DataOriginal.Merge(DataOriginal);
                    }
                }
            }


            SetFeatureInformation(ref _envTrain.DataOriginal, ref _labelName, out _envTrain.FeatureNum, out _envTrain.FeatureNames);

            SetInstanceInformation(ref _envTrain.DataOriginal, ref _labelName, out _envTrain.InstanceEachLabelNum, out _envTrain.Labels,
                out _envTrain.LabelNum, out _envTrain.InstanceTotalNum);

            _envTrain.ClassName = _labelName;


            SetFeatureInformation(ref _envTest.DataOriginal, ref _labelName, out _envTest.FeatureNum, out _envTest.FeatureNames);

            SetInstanceInformation(ref _envTest.DataOriginal, ref _labelName, out _envTest.InstanceEachLabelNum, out _envTest.Labels,
                out _envTest.LabelNum, out _envTest.InstanceTotalNum);

            _envTest.ClassName = _labelName;
            return true;
        }


        public bool InitializeEnvironmentOneRun(ref string _address, ref string _dataName, ref string _spliter, ref string _labelName, out EnvStruct.Environment _envTrain, out EnvStruct.Environment _envTest)
        {

            _envTrain = new EnvStruct.Environment();
            _envTest = new EnvStruct.Environment();
            if (!DictionaryExist(ref _address, ref _dataName))
            {
                return false;
            }

            DirectoryInfo root = new DirectoryInfo(_address);
            FileInfo[] files = root.GetFiles();


            foreach (var _f in files)
            {
                string _target = _f.FullName;

                string _use = _f.Name.Split('.')[0];
                if (!ReadCSVToDataTableHasTitle(ref _target, ref _spliter, out DataTable DataOriginal))
                {
                    return false;
                }

                if (_use == "test")
                {
                    _envTest.DataOriginal = DataOriginal;
                }
                else
                {
                    _envTrain.DataOriginal = DataOriginal;

                }
            }


            SetFeatureInformation(ref _envTrain.DataOriginal, ref _labelName, out _envTrain.FeatureNum, out _envTrain.FeatureNames);

            SetInstanceInformation(ref _envTrain.DataOriginal, ref _labelName, out _envTrain.InstanceEachLabelNum, out _envTrain.Labels,
                out _envTrain.LabelNum, out _envTrain.InstanceTotalNum);

            _envTrain.ClassName = _labelName;


            SetFeatureInformation(ref _envTest.DataOriginal, ref _labelName, out _envTest.FeatureNum, out _envTest.FeatureNames);

            SetInstanceInformation(ref _envTest.DataOriginal, ref _labelName, out _envTest.InstanceEachLabelNum, out _envTest.Labels,
                out _envTest.LabelNum, out _envTest.InstanceTotalNum);

            _envTest.ClassName = _labelName;
            return true;
        }
    }
}
