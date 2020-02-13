using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model
{
    internal class FileChangeFrequency
    {
        public FileChangeFrequency(string filePath, int fileChanges)
        {
            FilePath = filePath;
            FileChanges = fileChanges;
        }

        internal string FilePath { get; private set; }
        internal int FileChanges { get; private set; }
    }
}
