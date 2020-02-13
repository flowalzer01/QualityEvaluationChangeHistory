using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    public class FileChangeFrequency
    {
        public FileChangeFrequency(string filePath, int fileChanges)
        {
            FilePath = filePath;
            FileChanges = fileChanges;
        }

        public string FilePath { get; private set; }
        public int FileChanges { get; private set; }
    }
}
