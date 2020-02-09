using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model
{
    internal class GitPatchEntryChange
    {
        public GitPatchEntryChange(string path, int linesAdded, int linesDeleted)
        {
            Path = path;
            LinesAdded = linesAdded;
            LinesDeleted = linesDeleted;
        }

        internal string Path { get; private set; }
        internal int LinesAdded { get; private set; }
        internal int LinesDeleted { get; private set; }
    }
}
