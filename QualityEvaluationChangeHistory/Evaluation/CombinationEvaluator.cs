using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace QualityEvaluationChangeHistory.Evaluation
{
    internal class CombinationEvaluator
    {
        public CombinationEvaluator()
        {
        }


        internal IEnumerable<List<int>> GetCombinations(int elements, int combinationSize)
        {
            Combinations<int> combinations = new Combinations<int>(GetElements(elements), combinationSize);

            foreach (List<int> combination in combinations)
                yield return combination;
        }

        private static List<int> GetElements(int elements)
        {
            List<int> integers = new List<int>(elements);

            for (int i = 0; i < elements; i++)
                integers.Add(i);

            return integers;
        }

        private static void Print(Combinations<int> combinations)
        {
            foreach (List<int> combination in combinations)
                System.Diagnostics.Debug.WriteLine(string.Join(",", combination));
        }
    }
}
