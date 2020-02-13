using QualityEvaluationChangeHistory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.Evaluation
{
    internal class FileChangeFrequencyEvaluator
    {
        internal List<FileChangeFrequency> GetFileChangeFrequencies(List<GitCommit> gitCommits)
        {
            Dictionary<string, int> fileChangeFrequencyDictionary = new Dictionary<string, int>();
            List<FileChangeFrequency> fileChangeFrequencies = new List<FileChangeFrequency>();

            foreach (GitCommit gitCommit in gitCommits)
            {
                foreach (GitPatchEntryChange gitPatchEntryChange in gitCommit.PatchEntryChanges)
                {
                    if (!fileChangeFrequencyDictionary.ContainsKey(gitPatchEntryChange.Path))
                        fileChangeFrequencyDictionary[gitPatchEntryChange.Path] = 1;
                    else
                        fileChangeFrequencyDictionary[gitPatchEntryChange.Path]++;
                }
            }

            foreach(var entry in fileChangeFrequencyDictionary)
                fileChangeFrequencies.Add(new FileChangeFrequency(entry.Key, entry.Value));

            return fileChangeFrequencies
                .OrderByDescending(x => x.FileChanges)
                .ToList();
        }
    }
}
