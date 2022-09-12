// Copyright (C) 2020  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using DFlow.Domain.Validation;
using DFlow.Persistence;
using TodoManagerApp.Domain;
using TodoManagerApp.Persistence.Repositories;

namespace TodoManagerApp.Business;

public sealed class CommandResult : ExecutionResult
{
    public CommandResult(bool isSucceed, IReadOnlyList<Failure> violations) 
        : this(isSucceed, -1,  violations)
    {
    }
    public CommandResult(bool isSucceed, int id) 
        : this(isSucceed, ImmutableArray<Failure>.Empty)
    {
        Id = id;
    }
    public CommandResult(bool isSucceed, int id, IReadOnlyList<Failure> violations) 
        : base(isSucceed, violations)
    {
        Id = id;
    }
    
    public int Id { get; }
}