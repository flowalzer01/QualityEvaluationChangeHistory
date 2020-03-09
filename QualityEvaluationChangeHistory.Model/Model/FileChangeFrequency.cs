using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    [DataContract]
    public class FileChangeFrequency
    {
        public FileChangeFrequency()
        {

        }

        public FileChangeFrequency(string filePath, int fileChanges, int linesAdded, int linesDeleted)
        {
            FilePath = filePath;
            FileChanges = fileChanges;
            LinesAdded = linesAdded;
            LinesDeleted = linesDeleted;
        }

        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public int FileChanges { get; set; }

        [DataMember]
        public int LinesAdded { get; set; }

        [DataMember]
        public int LinesDeleted { get; set; }

        [DataMember]
        public int LinesChanged => LinesAdded + LinesDeleted;
    }
}
