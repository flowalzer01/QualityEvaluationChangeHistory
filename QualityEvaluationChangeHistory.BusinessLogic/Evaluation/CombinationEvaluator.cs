using Combinatorics.Collections;
using System.Collections.Generic;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation
{
    public class CombinationEvaluator
    {
        public CombinationEvaluator()
        {
        }


        public IEnumerable<List<int>> GetCombinations(int elements, int combinationSize)
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
