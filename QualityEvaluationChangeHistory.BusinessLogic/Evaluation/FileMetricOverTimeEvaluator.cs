using ArchiMetrics.Analysis;
using ArchiMetrics.Analysis.Common.Metrics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation
{
    public class FileMetricOverTimeEvaluator
    {
        public FileMetricOverTime GetFileMetricOverTime(List<GitCommit> gitCommits, string filePath)
        {
            FileMetricOverTime fileMetricOverTime = new FileMetricOverTime(filePath);

            foreach (GitCommit gitCommit in gitCommits)
            {
                if (gitCommit.ContainsFileNames(new List<string>() { filePath }))
                {
                    SyntaxTree syntaxTree = GetSyntaxTree(GetFileContentForFilePath(filePath, gitCommit));
                    INamespaceMetric metric = GetFileMetric(syntaxTree);

                    fileMetricOverTime.FileMetricForCommits.Add(
                        new FileMetricForCommit(gitCommit.SHA,
                            gitCommit.Time,
                            metric.MaintainabilityIndex,
                            metric.CyclomaticComplexity,
                            metric.LinesOfCode));
                }
            }


            return fileMetricOverTime;
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
