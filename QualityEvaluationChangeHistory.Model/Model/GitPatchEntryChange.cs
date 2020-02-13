using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    [DataContract]
    public class GitPatchEntryChange
    {
        public GitPatchEntryChange()
        {

        }

        public GitPatchEntryChange(string path, int linesAdded, int linesDeleted)
        {
            Path = path;
            LinesAdded = linesAdded;
            LinesDeleted = linesDeleted;
        }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public int LinesAdded { get; set; }

        [DataMember]
        public int LinesDeleted { get; set; }
    }
}
