// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.Domain.BusinessObjects;

namespace TodoManagerApp.Domain;

public class Todo: BaseEntity<TodoId>
{
    public TodoDescription Description { get; }
    
    public TodoStatus IsDone { get; }

    public Todo(TodoDescription description, TodoStatus isDone, TodoId identity, VersionId version) 
        : base(identity, version)
    {
        Description = description;
        IsDone = isDone;
        
        AppendValidationResult(identity.ValidationStatus.Failures);
        AppendValidationResult(description.ValidationStatus.Failures);
        AppendValidationResult(isDone.ValidationStatus.Failures);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Identity;
        yield return Description;
    }
   
    public static Todo From(TodoDescription description, TodoStatus isDone, TodoId id, VersionId version)
    {
        return new Todo(description, isDone, id, version);
    }
    
    public static Todo New(TodoDescription description, TodoId id)
    {
        return From(description, TodoStatus.NotDone, id, VersionId.New());
    }
}