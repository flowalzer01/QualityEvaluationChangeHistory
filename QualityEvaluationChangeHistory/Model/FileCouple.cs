using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model
{
    internal class FileCouple
    {
        public FileCouple()
        {
            GitCommits = new List<string>();
            FileNames = new List<string>();
        }

        internal List<string> GitCommits { get; set; }
        internal List<string> FileNames { get; set; }

    }
}
