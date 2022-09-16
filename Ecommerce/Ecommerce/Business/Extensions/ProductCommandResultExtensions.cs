// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.


using Ecommerce.Domain;

namespace Ecommerce.Business.Extensions
{
    public static class ProductCommandResultExtensions
    {
        public static CommandResult ToResultSucced(this Product product)
        {
            return new CommandResult(product.IsValid, product.Identity.Value);
        }
        
        public static CommandResult ToResultFailed(this Product product)
        {
            return new CommandResult(product.IsValid, Guid.Empty, product.Failures);
        }

    }
}