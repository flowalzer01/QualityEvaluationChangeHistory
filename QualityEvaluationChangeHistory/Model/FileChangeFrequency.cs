using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model
{
    internal class FileChangeFrequency
    {
        internal string FilePath { get; set; }
        internal int FileChanges { get; set; }
    }
}
