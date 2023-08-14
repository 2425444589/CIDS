namespace CIDS.Environment
{

    interface InterfaceEnv
    {
        bool GetEnv(int _id, out EnvStruct.Environment envTrain, out EnvStruct.Environment envTest);


        string GetProblemDescribe();

        string GetName();
    }
}
