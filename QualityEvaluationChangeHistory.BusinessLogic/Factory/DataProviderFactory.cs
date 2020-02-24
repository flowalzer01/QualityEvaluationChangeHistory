using QualityEvaluationChangeHistory.BusinessLogic.Data;
using QualityEvaluationChangeHistory.BusinessLogic.Data.Interface;
using QualityEvaluationChangeHistory.BusinessLogic.WareHouse.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Factory
{
    public class DataProviderFactory
    {
        private readonly bool _fromWareHouse;
        private readonly string _wareHouseProjectPath;
        private readonly string _repositoryPath;

        public DataProviderFactory(bool fromWareHouse, string wareHouseProjectPath, string repositoryPath)
        {
            _fromWareHouse = fromWareHouse;
            _wareHouseProjectPath = wareHouseProjectPath;
            _repositoryPath = repositoryPath;
        }

        public IGitDataProvider GetGitDataProvider()
        {
            if (_fromWareHouse)
                return new GitDataFromWareHouseProvider(_wareHouseProjectPath);
            else
                return new GitDataRepositoryProvider(_repositoryPath);
        }
    }
}
