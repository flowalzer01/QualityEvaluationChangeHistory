using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model
{
    internal class GitCommit
    {
        public GitCommit(string sha, string message, string author)
        {
            SHA = sha;
            Message = message;
            Author = author;
            PatchEntryChanges = new List<GitPatchEntryChange>();
        }

        internal string Author { get; private set; }
        internal string SHA { get; private set; }
        internal string Message { get; private set; }
        internal List<GitPatchEntryChange> PatchEntryChanges { get; private set; }
    }
}
