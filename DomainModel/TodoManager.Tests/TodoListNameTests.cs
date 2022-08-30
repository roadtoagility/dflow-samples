
using TodoManager.Tests.Data;
using TodoManagerApp.Domain;
using Xunit;

namespace TodoManager.Tests;

public class TodoListNameTests
{
    [Theory]
    [ClassData(typeof(ValidListNameProvider))]
    public void Create_Valid_TodoListName(string input, TodoListName expected)
    {
        var listName = TodoListName.From(input);
        Assert.Equal(expected,listName);
    }
    
    [Theory]
    [ClassData(typeof(InvalidListNameProvider))]
    public void Create_Invalid_TodoListName(string input, bool expected, int failureCount)
    {
        var listName = TodoListName.From(input);
        Assert.Equal(expected,listName.ValidationStatus.IsValid);
        Assert.Equal(failureCount,listName.ValidationStatus.Failures.Count);
    }
}