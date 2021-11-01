namespace PromQL.Parser
{
    public static class Operators
    {
        /// <remarks>
        /// Taken from https://github.com/prometheus/prometheus/blob/4414351576ac27754d9eec71c271171d5c020677/pkg/labels/matcher.go#L24
        /// </remarks>
        public enum Match
        {
            Equal,
            NotEqual,
            Regexp,
            NotRegexp
        }

        public enum Binary
        {
            Pow,
            Mul,
            Div,
            Mod,
            Atan2,
            Add,
            Sub,
            Eql,
            Gte,
            Gtr,
            Lte,
            Lss,
            Neq,
            And,
            Unless,
            Or
        }
    
        public enum Aggregate
        {
            Avg, 
            Bottomk, 
            Count, 
            CountValues, 
            Group, 
            Max, 
            Min, 
            Quantile, 
            Stddev, 
            Stdvar, 
            Sum, 
            Topk,
        }

        public enum Unary
        {
            /// <summary>
            /// Aka plus ('+')
            /// </summary>
            Add,
            /// <summary>
            /// Aka minus ('-')
            /// </summary>
            Sub
        }
    }
}
