// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;
using DFlow.Business.Cqrs;
using DFlow.Domain.Validation;

namespace Ecommerce.Business;

public sealed class CommandResult : ExecutionResult
{
    public CommandResult(bool isSucceed, IReadOnlyList<Failure> violations)
    :base(isSucceed, violations)
    {

    }
    public CommandResult(bool isSucceed, Guid id) 
        : this(isSucceed, ImmutableArray<Failure>.Empty)
    {
        Id = id;
        
    }
    public CommandResult(bool isSucceed, Guid id, IReadOnlyList<Failure> violations) 
        : base(isSucceed, violations)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}