namespace PromQL.Parser
{
    /// <summary>
    /// Functions available in PromQL.
    /// </summary>
    /// <remarks>
    /// Primarily taken from https://github.com/prometheus/prometheus/blob/main/web/ui/module/codemirror-promql/src/grammar/promql.grammar#L121-L188.
    /// More authoritative source would be https://github.com/prometheus/prometheus/blob/7471208b5c8ff6b65b644adedf7eb964da3d50ae/promql/parser/functions.go.
    /// </remarks>
    public enum FunctionIdentifier
    {
        AbsentOverTime,
        Absent,
        Abs,
        Acos,
        Acosh,
        Asin,
        Asinh,
        Atan,
        Atanh,
        AvgOverTime,
        Ceil,
        Changes,
        Clamp,
        ClampMax,
        ClampMin,
        Cos,
        Cosh,
        CountOverTime,
        DaysInMonth,
        DayOfMonth,
        DayOfWeek,
        Deg,
        Delta,
        Deriv,
        Exp,
        Floor,
        HistogramQuantile,
        HoltWinters,
        Hour,
        Idelta,
        Increase,
        Irate,
        LabelReplace,
        LabelJoin,
        LastOverTime,
        Ln,
        Log10,
        Log2,
        MaxOverTime,
        MinOverTime,
        Minute,
        Month,
        Pi,
        PredictLinear,
        PresentOverTime,
        QuantileOverTime,
        Rad,
        Rate,
        Resets,
        Round,
        Scalar,
        Sgn,
        Sin,
        Sinh,
        Sort,
        SortDesc,
        Sqrt,
        StddevOverTime,
        StdvarOverTime,
        SumOverTime,
        Tan,
        Tanh,
        Timestamp,
        Time,
        Vector,
        Year
    }
}
