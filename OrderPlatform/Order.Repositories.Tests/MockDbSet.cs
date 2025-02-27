using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Order.Data.Entities;

namespace Order.Repositories.Tests;

public static class MockDbSet
{
    public static Mock<DbSet<T>> GetMockDbSet<T>(
        IEnumerable<T> enumerable,
        Action? providerCreateQueryCallback = null)
        where T : EntityBase
    {
        var data = enumerable.AsQueryable();
        var mockSet = new Mock<DbSet<T>>(MockBehavior.Strict);

        mockSet.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

        mockSet.As<IQueryable<T>>()
            .Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<T>(data.Provider, providerCreateQueryCallback));

        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);

        mockSet.Setup(m => m.AddAsync(It.IsAny<T>(), CancellationToken.None)).Returns<T>(default);
        mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).Returns<object[]>(objArray =>
        {
            var keyValue = (Guid)objArray[0];
            return new ValueTask<T?>(data.FirstOrDefault(d => d.Id == keyValue));
        });

        mockSet.Setup(m => m.Update(It.IsAny<T>())).Returns<T>(_ => default!);

        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Returns<T>(_ => default!);

        return mockSet;
    }
}

internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;
    private readonly Action? _createQueryCallback;

    internal TestAsyncQueryProvider(
        IQueryProvider inner,
        Action? createQueryCallback = null)
    {
        _inner = inner;
        _createQueryCallback = createQueryCallback;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        _createQueryCallback?.Invoke();
        return new TestAsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        _createQueryCallback?.Invoke();
        return new TestAsyncEnumerable<TElement>(expression);
    }

    public object? Execute(Expression expression)
    {
        return _inner.Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return _inner.Execute<TResult>(expression);
    }

    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
    {
        return new TestAsyncEnumerable<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        return Execute<TResult>(expression);
    }
}

internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public TestAsyncEnumerable(IEnumerable<T> enumerable)
        : base(enumerable)
    {
    }

    public TestAsyncEnumerable(Expression expression)
        : base(expression)
    {
    }

    public IAsyncEnumerator<T> GetEnumerator()
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}

internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    public void Dispose()
    {
        _inner.Dispose();
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(MoveNext(CancellationToken.None));
    }

    public T Current => _inner.Current;

    public Task<bool> MoveNext(CancellationToken cancellationToken)
    {
        return Task.FromResult(_inner.MoveNext());
    }

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        return new ValueTask();
    }
}