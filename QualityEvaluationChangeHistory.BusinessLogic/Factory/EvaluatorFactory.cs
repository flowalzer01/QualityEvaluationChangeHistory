using QualityEvaluationChangeHistory.BusinessLogic.Evaluation;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface;
using QualityEvaluationChangeHistory.BusinessLogic.WareHouse.Evaluation;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Factory
{
    public class EvaluatorFactory
    {
        private readonly bool _fromWareHouse;
        private readonly string _wareHouseProjectPath;

        public EvaluatorFactory(bool fromWareHouse, string wareHouseProjectPath)
        {
            _fromWareHouse = fromWareHouse;
            _wareHouseProjectPath = wareHouseProjectPath;
        }

        public IFileChangeFrequencyEvaluator GetFileChangeFrequencyEvaluator()
        {
            if (_fromWareHouse)
                return new FileChangeFrequencyWareHouse(_wareHouseProjectPath);
            else
                return new FileChangeFrequencyEvaluator();
        }

        public IFileMetricOverTimeEvaluator GetFileMetricOverTimeEvaluator()
        {
            if (_fromWareHouse)
                return new FileMetricOverTimeWareHouse(_wareHouseProjectPath);
            else
                return new FileMetricOverTimeEvaluator();
        }

        public IFileMetricOverFileChangeFrequencyEvaluator GetFileMetricOverFileChangeFrequencyEvaluator()
        {
            if (_fromWareHouse)
                return new FileMetricOverFileChangeFrequencyWareHouse(_wareHouseProjectPath);
            else
                return new FileMetricOverFileChangeFrequencyEvaluator();
        }
    }
}
