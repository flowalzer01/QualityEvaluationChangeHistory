using LibGit2Sharp;
using QualityEvaluationChangeHistory.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QualityEvaluationChangeHistory.Data
{
    internal class GitDataProvider
    {
        private readonly Repository _repository;

        public GitDataProvider(string repositoryPath)
        {
            _repository = new Repository(repositoryPath);
        }

        internal List<GitCommit> GetCommits()
        {
            List<Commit> commits = GetCommitsInternal();
            List<GitCommit> gitCommits = new List<GitCommit>();

            for (int i = 0; i < commits.Count - 1; i++)
            {
                Tree currentTree = commits[i].Tree;
                Tree previousTree = commits[i + 1].Tree;

                Patch patch = _repository.Diff.Compare<Patch>(currentTree, previousTree);

                Console.WriteLine($"{commits[i].MessageShort}");
                GitCommit gitCommit = new GitCommit(commits[i].Sha, commits[i].MessageShort, commits[i].Author.Name);

                foreach (PatchEntryChanges pec in patch)
                {
                    Console.WriteLine("{0} = {1} ({2}+ and {3}-)",
                        pec.Path,
                        pec.LinesAdded + pec.LinesDeleted,
                        pec.LinesAdded,
                        pec.LinesDeleted);

                    gitCommit.PatchEntryChanges.Add(new GitPatchEntryChange(pec.Path, pec.LinesAdded, pec.LinesDeleted));
                }
                Console.WriteLine();

                gitCommits.Add(gitCommit);
            }

            return gitCommits;
        }

        private List<Commit> GetCommitsInternal()
        {
            return _repository.Commits.ToList();
        }
    }
}
