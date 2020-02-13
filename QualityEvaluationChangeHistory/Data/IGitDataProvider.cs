﻿using QualityEvaluationChangeHistory.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Data
{
    public interface IGitDataProvider
    {
        List<GitCommit> GetCommits();
    }
}
