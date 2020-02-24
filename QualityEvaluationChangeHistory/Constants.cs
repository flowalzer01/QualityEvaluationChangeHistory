﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QualityEvaluationChangeHistory
{
    public class Constants
    {
        public const bool DataFromWareHouse = true;
        public const string WareHouseProjectName = @"Heidelpay";
        public const string RepositoryPath = @"C:\Users\walzeflo\source\repos\heidelpayDotNET";
        public const string WareHousePath = @"C:\Users\walzeflo\source\repos\WareHouse";

        public const int FileChangeFrequencyNumberOfFiles = 50;
        public const int FileCouplingCombinationSize = 2;
        public const int FileCouplingFilesToLookAt = 20;

        public static string WareHouseProjectPath => Path.Combine(WareHousePath, WareHouseProjectName);
    }
}
