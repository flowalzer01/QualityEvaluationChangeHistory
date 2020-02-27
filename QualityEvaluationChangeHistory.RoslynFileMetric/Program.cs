using ArchiMetrics.Analysis;
using ArchiMetrics.Analysis.Common.Metrics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using QualityEvaluationChangeHistory.Configuration;
using QualityEvaluationChangeHistory.RoslynFileMetric.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityEvaluationChangeHistory.RoslynFileMetric
{
    public class Program
    {
        private static readonly string SolutionFilePath = Constants.SolutionPath;

        public static void Main(string[] args)
        {
            FileMetricsWriter fileMetricsWriter = new FileMetricsWriter();
            fileMetricsWriter.PrepareWareHouse();

            Task<List<FileMetric>> task = GetFileMetrics();
            List<FileMetric> fileMetrics = task.Result;

            fileMetricsWriter.WriteToFile(fileMetrics);

            Console.WriteLine("\nWrote to ware house - you can now run the other programm!");
            Console.ReadKey();
        }

        private static async Task<List<FileMetric>> GetFileMetrics()
        {
            Dictionary<string, FileMetric> fileMetricsDictionary = new Dictionary<string, FileMetric>();

            using (var workspace = MSBuildWorkspace.Create())
            {
                Solution solution = await workspace.OpenSolutionAsync(SolutionFilePath);

                foreach (var project in solution.Projects)
                {
                    CodeMetricsCalculator metricsCalculator = new CodeMetricsCalculator();
                    IEnumerable<INamespaceMetric> metrics = await metricsCalculator.Calculate(project, solution);

                    foreach (INamespaceMetric namespaceMetric in metrics)
                    {
                        foreach (ITypeMetric metric in namespaceMetric.TypeMetrics)
                        {
                            if (metric.MemberMetrics.Select(x => x.CodeFile).Where(x => !string.IsNullOrEmpty(x)).Distinct().Count() > 1)
                                Console.WriteLine("More than one file name...");

                            string codeFile = metric.
                                MemberMetrics
                                .FirstOrDefault(x => !string.IsNullOrEmpty(x.CodeFile))?
                                .CodeFile;

                            if (string.IsNullOrEmpty(codeFile))
                                continue;

                            Document document = project
                                .Documents
                                .SingleOrDefault(x => x.FilePath
                                .EndsWith(codeFile));

                            if (!fileMetricsDictionary.ContainsKey(document.FilePath))
                                fileMetricsDictionary[document.FilePath] = GetFileMetric(metric, document.FilePath);
                            else
                            {
                                Console.WriteLine($"{document.FilePath} already in the dictionary");
                                PrintMetric(fileMetricsDictionary[document.FilePath]);
                                PrintMetric(GetFileMetric(metric, document.FilePath));
                            }
                        }
                    }
                }
            }

            return fileMetricsDictionary
                .Select(x => x.Value)
                .ToList();
        }

        private static void PrintMetric(FileMetric fileMetric)
        {
            Console.WriteLine($"cyc: {fileMetric.CyclomaticComplexity}, loc: {fileMetric.LinesOfCode}, mindex: {fileMetric.MaintainabilityIndex}");
        }

        private static FileMetric GetFileMetric(ITypeMetric metric, string filePath)
        {
            return new FileMetric(filePath, metric.CyclomaticComplexity, metric.MaintainabilityIndex, metric.LinesOfCode);
        }
    }
}
