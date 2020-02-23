using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    public class FileChangeFrequency
    {
        public FileChangeFrequency(string filePath, int fileChanges, int linesAdded, int linesDeleted)
        {
            FilePath = filePath;
            FileChanges = fileChanges;
            LinesAdded = linesAdded;
            LinesDeleted = linesDeleted;
        }

        public string FilePath { get; }
        public int FileChanges { get; set; }
        public int LinesAdded { get; set; }
        public int LinesDeleted { get; set; }
        public int LinesChanged => LinesAdded + LinesDeleted;
    }
}
