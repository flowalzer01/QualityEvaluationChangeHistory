using QualityEvaluationChangeHistory.Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation
{
    public class FileChangeFrequencyEvaluator
    {
        private const int NumberOfFilesToShow = 50;

        public List<FileChangeFrequency> GetFileChangeFrequencies(List<GitCommit> gitCommits)
        {
            Dictionary<string, FileChangeFrequency> fileChangeFrequencyDictionary = new Dictionary<string, FileChangeFrequency>();

            foreach (GitCommit gitCommit in gitCommits)
            {
                foreach (GitPatchEntryChange gitPatchEntryChange in gitCommit.PatchEntryChanges)
                {
                    if (!fileChangeFrequencyDictionary.ContainsKey(gitPatchEntryChange.Path))
                        fileChangeFrequencyDictionary[gitPatchEntryChange.Path] = new FileChangeFrequency(gitPatchEntryChange.Path, 1, gitPatchEntryChange.LinesAdded, gitPatchEntryChange.LinesDeleted);
                    else
                    {
                        FileChangeFrequency fileChangeFrequency = fileChangeFrequencyDictionary[gitPatchEntryChange.Path];
                        fileChangeFrequency.FileChanges++;
                        fileChangeFrequency.LinesAdded += gitPatchEntryChange.LinesAdded;
                        fileChangeFrequency.LinesDeleted += gitPatchEntryChange.LinesDeleted;
                    }
                }
            }

            return fileChangeFrequencyDictionary
                .Select(x => x.Value)
                .OrderByDescending(x => x.FileChanges)
                .Take(NumberOfFilesToShow)
                .ToList();
        }
    }
}
