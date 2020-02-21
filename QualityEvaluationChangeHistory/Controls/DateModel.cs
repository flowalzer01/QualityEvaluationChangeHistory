using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Controls
{
    public class DateModel
    {
        public DateModel(DateTime dateTime, double value)
        {
            DateTime = dateTime;
            Value = value;
        }

        public System.DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
