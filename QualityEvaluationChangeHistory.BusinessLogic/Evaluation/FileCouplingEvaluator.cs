using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation
{
    public class FileCouplingEvaluator
    {
        private const int COMBINATION_SIZE = 2;
        private const int TOP_FILES_TO_LOOK_AT = 20;

        private readonly List<GitCommit> _gitCommits;
        private readonly List<FileChangeFrequency> _fileChangeFrequencies;
        private readonly List<string> _fileNames;
        private readonly CombinationEvaluator _combinationEvaluator;

        public FileCouplingEvaluator(List<GitCommit> gitCommits, List<FileChangeFrequency> fileChangeFrequencies)
        {
            _combinationEvaluator = new CombinationEvaluator();
            _gitCommits = gitCommits;
            _fileChangeFrequencies = InitFileChangeFrequencies(fileChangeFrequencies);
            _fileNames = InitFileNames();

        }

        private List<FileChangeFrequency> InitFileChangeFrequencies(List<FileChangeFrequency> fileChangeFrequencies)
        {
            return fileChangeFrequencies
                .OrderByDescending(x => x.FileChanges)
                .Take(TOP_FILES_TO_LOOK_AT)
                .ToList();
        }

        private List<string> InitFileNames()
        {
            return _fileChangeFrequencies
                .Select(x => x.FilePath)
                .ToList();
        }

        public List<FileCouple> CalculateFileCouples()
        {
            List<FileCouple> fileCouples = new List<FileCouple>();

            foreach (List<int> combination in GetCombinations())
            {
                FileCouple fileCouple = new FileCouple();
                List<string> fileNames = GetFileNamesFromNumbers(combination);
                fileCouple.FileNames.AddRange(fileNames);

                foreach(GitCommit gitCommit in _gitCommits)
                {
                    if (gitCommit.ContainsFileNames(fileNames))
                        fileCouple.GitCommits.Add(gitCommit.SHA);
                }

                fileCouples.Add(fileCouple);
            }

            return fileCouples;
        }

        private List<string> GetFileNamesFromNumbers(List<int> combination)
        {
            List<string> fileNames = new List<string>();

            foreach (int fileNumber in combination)
                fileNames.Add(_fileNames[fileNumber]);

            return fileNames;
        }

        private List<List<int>> GetCombinations()
        {
            return _combinationEvaluator
                            .GetCombinations(_fileNames.Count, COMBINATION_SIZE)
                            .ToList();
        }
    }
}
