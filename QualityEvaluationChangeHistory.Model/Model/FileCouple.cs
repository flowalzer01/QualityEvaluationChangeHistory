using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    public class FileCouple
    {
        public FileCouple()
        {
            GitCommits = new List<string>();
            FileNames = new List<string>();
        }

        public List<string> GitCommits { get; set; }
        public List<string> FileNames { get; set; }
    }
}
