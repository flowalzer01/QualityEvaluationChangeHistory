using LibGit2Sharp;
using QualityEvaluationChangeHistory.BusinessLogic.Data.Interface;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Data
{
    public class GitDataRepositoryProvider : IGitDataProvider
    {
        private readonly Repository _repository;
        private int _emptyFiles = 0;
        private int _parsedFiles = 0;

        public GitDataRepositoryProvider(string repositoryPath)
        {
            _repository = new Repository(repositoryPath);
        }

        public List<GitCommit> GetCommits()
        {
            List<Commit> commits = GetCommitsInternal();
            List<GitCommit> gitCommits = new List<GitCommit>();

            for (int i = 0; i < commits.Count - 1; i++)
            {
                Tree currentTree = commits[i].Tree;
                Tree previousTree = commits[i + 1].Tree;

                Patch patch = _repository.Diff.Compare<Patch>(currentTree, previousTree);

                Console.WriteLine($"{commits[i].MessageShort}");
                GitCommit gitCommit = new GitCommit(commits[i].Sha, commits[i].MessageShort, commits[i].Author.Name, commits[i].Author.When);

                foreach (PatchEntryChanges pec in patch)
                {
                    Console.WriteLine("{0} = {1} ({2}+ and {3}-)",
                        pec.Path,
                        pec.LinesAdded + pec.LinesDeleted,
                        pec.LinesAdded,
                        pec.LinesDeleted);

                    string fileContent = GetFileContent(commits[i], pec.Path);
                    gitCommit.PatchEntryChanges.Add(new GitPatchEntryChange(pec.Path, pec.LinesAdded, pec.LinesDeleted, fileContent));
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

        public string GetFileContent(Commit commit, string fileName)
        {
            _parsedFiles++;

            if (!fileName.EndsWith(".cs"))
                return string.Empty;

            TreeEntry treeEntry = commit[fileName];

            if(treeEntry == null)
            {
                _emptyFiles++;
                return string.Empty;
            }

            var blob = (Blob)treeEntry.Target;

            var contentStream = blob.GetContentStream();

            using (var tr = new StreamReader(contentStream, Encoding.UTF8))
            {
                string content = tr.ReadToEnd();
                return content;
            }
        }
    }
}
