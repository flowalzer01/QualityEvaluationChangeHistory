using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model
{
    [DataContract]
    public class GitPatchEntryChange
    {
        public GitPatchEntryChange(string path, int linesAdded, int linesDeleted)
        {
            Path = path;
            LinesAdded = linesAdded;
            LinesDeleted = linesDeleted;
        }

        [DataMember]
        public string Path { get; private set; }

        [DataMember]
        public int LinesAdded { get; private set; }

        [DataMember]
        public int LinesDeleted { get; private set; }
    }
}
