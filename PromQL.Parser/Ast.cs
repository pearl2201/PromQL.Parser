using System;
using System.Collections.Immutable;

namespace PromQL.Parser
{
    /// <summary>
    /// Root of all PromQL expressions.
    /// </summary>
    public interface Expr {}
    
    /// <summary>
    /// Expressions that may be used in conjunction with the `offset` keyword.
    /// </summary>
    public interface OffsettableExpr { }
    
    /// <summary>
    /// Expressions that may be used in the left-hand operand of binary expressions.
    /// </summary>
    public interface BinaryLhsExpr { }
    
    /// <summary>
    /// Expressions that may be used in subquery expressions.
    /// </summary>
    public interface SubqueryChildExpr { }
    
    /// <summary>
    /// Describes the cardinality relationship of two Vectors in a binary operation.
    /// </summary>
    public enum VectorMatchCardinality
    {
        OneToOne,
        ManyToOne,
        OneToMany
    }
    
    /// <summary>
    /// Represents an aggregation operation on a Vector.
    /// </summary>
    /// <param name="Operator">The used aggregation operation.</param>
    /// <param name="Expr">The Vector expression over which is aggregated.</param>
    /// <param name="Param">Parameter used by some aggregators.</param>
    /// <param name="GroupingLabels">The labels by which to group the Vector.</param>
    /// <param name="Without"> Whether to drop the given labels rather than keep them.</param>
    public record AggregateExpr(Operators.Aggregate Operator, Expr Expr, Expr Param, ImmutableArray<string> GroupingLabels, bool Without) : Expr, BinaryLhsExpr, SubqueryChildExpr;

    /// <summary>
    /// Represents a binary expression between two child expressions.
    /// </summary>
    /// <param name="LeftHandSide">The left-hand operand of the expression</param>
    /// <param name="RightHandSide">The right-hand operand of the expression</param>
    /// <param name="Operator">The operation of the expression</param>
    /// <param name="VectorMatching">The matching behavior for the operation to be applied if both operands are Vectors.</param>
    public record BinaryExpr(BinaryLhsExpr LeftHandSide, Expr RightHandSide, Operators.Binary Operator, VectorMatching VectorMatching) : Expr, SubqueryChildExpr;

    // TODO copy more docos from https://github.com/prometheus/prometheus/blob/7471208b5c8ff6b65b644adedf7eb964da3d50ae/promql/parser/ast.go

    /// <summary>
    /// VectorMatching describes how elements from two Vectors in a binary operation are supposed to be matched.
    /// </summary>
    /// <param name="MatchCardinality">The cardinality of the two Vectors.</param>
    /// <param name="MatchingLabels">Contains the labels which define equality of a pair of elements from the Vectors.</param>
    /// <param name="On">When true, includes the given label names from matching, rather than excluding them.</param>
    /// <param name="Include">Contains additional labels that should be included in the result from the side with the lower cardinality.</param>
    /// <param name="ReturnBool">If a comparison operator, return 0/1 rather than filtering.</param>
    public record VectorMatching(VectorMatchCardinality MatchCardinality, ImmutableArray<string> MatchingLabels,
        bool On, ImmutableArray<string> Include, bool ReturnBool)
    {
        public VectorMatching() : this(VectorMatchCardinality.OneToOne, ImmutableArray<string>.Empty, false,
            ImmutableArray<string>.Empty, false)
        {
        }

        public VectorMatching(bool returnBool) : this (VectorMatchCardinality.OneToOne, ImmutableArray<string>.Empty, false, ImmutableArray<string>.Empty, returnBool  )
        {
        }
    };

    /// <summary>
    /// A function call.
    /// </summary>
    /// <param name="Identifier">The function that was called.</param>
    /// <param name="Args">Arguments used in the call.</param>
    public record FunctionCall(FunctionIdentifier Identifier, ImmutableArray<Expr> Args) : Expr, BinaryLhsExpr, SubqueryChildExpr;
    public record ParenExpression(Expr Expr) : Expr, BinaryLhsExpr, SubqueryChildExpr;
    public record OffsetExpr(OffsettableExpr Expr, Duration Duration) : Expr, BinaryLhsExpr, SubqueryChildExpr;
    public record MatrixSelector(VectorSelector Vector, Duration Duration) : Expr, OffsettableExpr, BinaryLhsExpr;

    public record UnaryExpr(Operators.Unary Operator, Expr Expr) : Expr, BinaryLhsExpr, SubqueryChildExpr;
    
    public record VectorSelector : Expr, OffsettableExpr, SubqueryChildExpr, BinaryLhsExpr
    {
        public VectorSelector(MetricIdentifier metricIdentifier)
        {
            MetricIdentifier = metricIdentifier;
        }

        public VectorSelector(LabelMatchers labelMatchers)
        {
            LabelMatchers = labelMatchers;
        }
        
        public VectorSelector(MetricIdentifier metricIdentifier, LabelMatchers labelMatchers)
        {
            MetricIdentifier = metricIdentifier;
            LabelMatchers = labelMatchers;
        }
        
        public MetricIdentifier? MetricIdentifier { get; }
        public LabelMatchers? LabelMatchers { get; }
    } 

    public record LabelMatchers(ImmutableArray<LabelMatcher> Matchers);
    
    public record LabelMatcher(string LabelName, Operators.Match Operator, StringLiteral Value);

    public record MetricIdentifier(string Value);

    public record NumberLiteral(decimal Value) : Expr, BinaryLhsExpr;
    
    public record Duration(TimeSpan Value);

    public record StringLiteral(char Quote, string Value) : Expr;


    public record SubqueryExpr(SubqueryChildExpr Expr, Duration Range, Duration? Step) : Expr, BinaryLhsExpr, OffsettableExpr;
}
