﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShardingCore.Exceptions;
using ShardingCore.Extensions;
using ShardingCore.Sharding.Abstractions;
using ShardingCore.Sharding.StreamMergeEngines.Abstractions;
using ShardingCore.Sharding.StreamMergeEngines.Abstractions.AbstractEnsureExpressionMergeEngines;

namespace ShardingCore.Sharding.StreamMergeEngines.AggregateMergeEngines
{
    /*
    * @Author: xjm
    * @Description:
    * @Date: 2021/8/18 14:46:20
    * @Ver: 1.0
    * @Email: 326308290@qq.com
    */
    public class SumAsyncInMemoryMergeEngine<TEntity, TEnsureResult> : AbstractEnsureMethodCallSelectorInMemoryAsyncMergeEngine<TEntity, TEnsureResult,TEnsureResult>
    {
        public SumAsyncInMemoryMergeEngine(MethodCallExpression methodCallExpression, IShardingDbContext shardingDbContext) : base(methodCallExpression, shardingDbContext)
        {
        }

        public override TEnsureResult MergeResult()
        {
            if (typeof(decimal) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<decimal>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(decimal?) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<decimal?>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(int) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<int>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(int?) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<int?>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(long) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<long>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(long?) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<long?>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(double) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<double>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(double?) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<double?>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(float) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<float>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(float?) == typeof(TEnsureResult))
            {
                var result = base.Execute(queryable => ((IQueryable<float?>)queryable).Sum());
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
#if !EFCORE2
            throw new ShardingCoreException(
                $"not support {GetMethodCallExpression().Print()} result {typeof(TEnsureResult)}");
#endif
#if EFCORE2
            throw new ShardingCoreException(
                $"not support {GetMethodCallExpression()} result {typeof(TEnsureResult)}");
#endif
        }

        public override async Task<TEnsureResult> MergeResultAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (typeof(decimal) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<decimal>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(decimal?) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<decimal?>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(int) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<int>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(int?) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<int?>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(long) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<long>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(long?) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<long?>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(double) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<double>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(double?) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<double?>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(float) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<float>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }
            if (typeof(float?) == typeof(TEnsureResult))
            {
                var result = await base.ExecuteAsync(queryable => ((IQueryable<float?>)queryable).SumAsync(cancellationToken), cancellationToken);
                if (result.IsEmpty())
                    return default;
                var sum = result.Sum();
                return ConvertSum(sum);
            }

#if !EFCORE2
            throw new ShardingCoreException(
                $"not support {GetMethodCallExpression().Print()} result {typeof(TEnsureResult)}");
#endif
#if EFCORE2
            throw new ShardingCoreException(
                $"not support {GetMethodCallExpression()} result {typeof(TEnsureResult)}");
#endif
        }
        private TEnsureResult ConvertSum<TNumber>(TNumber number)
        {
            if (number == null)
                return default;
            var convertExpr = Expression.Convert(Expression.Constant(number), typeof(TEnsureResult));
            return Expression.Lambda<Func<TEnsureResult>>(convertExpr).Compile()();
        }
    }
}
