using ArchiMetrics.Analysis;
using ArchiMetrics.Analysis.Common.Metrics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation
{
    public class FileMetricOverTimeEvaluator : IFileMetricOverTimeEvaluator
    {
        public List<FileMetricOverTime> GetFileMetricOverTime(IEnumerable<GitCommit> gitCommits, IEnumerable<string> filePaths)
        {
            List<FileMetricOverTime> fileMetricsOverTime = new List<FileMetricOverTime>();

            foreach(string filePath in filePaths)
            {
                FileMetricOverTime fileMetricOverTime = new FileMetricOverTime(filePath);

                foreach (GitCommit gitCommit in gitCommits)
                {
                    if (gitCommit.ContainsFileNames(new List<string>() { filePath }))
                    {
                        SyntaxTree syntaxTree = GetSyntaxTree(GetFileContentForFilePath(filePath, gitCommit));

                        if (syntaxTree.Length == 0)
                            continue;

                        INamespaceMetric metric = GetFileMetric(syntaxTree);

                        fileMetricOverTime.FileMetricForCommits.Add(
                            new FileMetricForCommit(gitCommit.SHA,
                                gitCommit.Time,
                                metric.MaintainabilityIndex,
                                metric.CyclomaticComplexity,
                                metric.LinesOfCode));
                    }
                }
                fileMetricsOverTime.Add(fileMetricOverTime);
            }

            return fileMetricsOverTime;
        }

        private static INamespaceMetric GetFileMetric(SyntaxTree syntaxTree)
        {
            List<SyntaxTree> syntaxTrees = new List<SyntaxTree>() { syntaxTree };
            CodeMetricsCalculator metricsCalculator = new CodeMetricsCalculator();
            var task = metricsCalculator.Calculate(syntaxTrees);
            return task.Result.Single();
        }

        private SyntaxTree GetSyntaxTree(string text)
        {
            return CSharpSyntaxTree.ParseText(text);
        }

        private static string GetFileContentForFilePath(string filePath, GitCommit gitCommit)
        {
            return gitCommit
                .PatchEntryChanges
                .Single(x => x.Path == filePath)
                .FileContent;
        }
    }
}
