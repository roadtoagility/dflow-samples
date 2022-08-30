using System.Collections;
using System.Collections.Generic;
using TodoManagerApp.Domain;

namespace TodoManager.Tests.Data;

public class ValidListNameProvider : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { "My Todos", TodoListName.From("My Todos") }
    };

    public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class InvalidListNameProvider : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { "", false, 1 }
    };

    public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}