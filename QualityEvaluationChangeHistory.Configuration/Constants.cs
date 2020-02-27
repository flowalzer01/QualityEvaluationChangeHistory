using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QualityEvaluationChangeHistory.Configuration
{
    public class Constants
    {
        public const bool DataFromWareHouse = true;
        public const bool EvaluationDataFromWareHouse = true;
        public const string WareHouseProjectName = @"Heidelpay";
        public const string RepositoryPath = @"C:\Users\walzeflo\source\repos\heidelpayDotNET";
        public static string SolutionPath = @"C:\Users\walzeflo\source\repos\heidelpayDotNET\heidelpayDotNET.sln";

        public const string WareHousePath = @"C:\Users\walzeflo\source\repos\WareHouse";
        public const string FileMetricFileName = "FileMetric.json";

        /// <summary>
        /// the following constants are only considerded when evaluation data is not from warehouse
        /// if evaluation data comes from warehouse, it is just read as it is.
        /// </summary>
        public const int FileChangeFrequencyNumberOfFiles = 50;
        public const int FileCouplingCombinationSize = 2;
        public const int FileCouplingFilesToLookAt = 20;
        public const int FileMetricsOverTimeNumberOfFiles = 10;

        public static string WareHouseProjectPath => Path.Combine(WareHousePath, WareHouseProjectName);

    }
}
