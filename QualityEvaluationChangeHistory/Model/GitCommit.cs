using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model
{
    [DataContract]
    public class GitCommit
    {
        public GitCommit(string sha, string message, string author)
        {
            SHA = sha;
            Message = message;
            Author = author;
            PatchEntryChanges = new List<GitPatchEntryChange>();
        }

        [DataMember]
        public string Author { get; private set; }

        [DataMember]
        public string SHA { get; private set; }

        [DataMember]
        public string Message { get; private set; }

        [DataMember]
        public List<GitPatchEntryChange> PatchEntryChanges { get; private set; }

        internal bool ContainsFileNames(List<string> fileNames)
        {
            return IntersectFileNames(fileNames).Count() == fileNames.Count;
        }

        private IEnumerable<string> IntersectFileNames(List<string> fileNames)
        {
            return PatchEntryChanges
                .Select(x => x.Path)
                .Intersect(fileNames);
        }
    }
}
