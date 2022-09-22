// Copyright (C) 2022  Road to Agility
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Library General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Library General Public License for more details.
//
// You should have received a copy of the GNU Library General Public
// License along with this library; if not, write to the
// Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
// Boston, MA  02110-1301, USA.
//

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Events.DomainEvents;

namespace Ecommerce.Domain.Events
{
    public class ProductCreatedEvent : DomainEvent
    {
        public ProductCreatedEvent(ProductId id, ProductName name, ProductDescription description
            , ProductWeight weight, VersionId version)
            : base(DateTime.Now, version)
        {
            Id = id;
            Name = name;
            Description = description;
            Weight = weight;
        }

        public ProductId Id { get; }
        public ProductName Name { get; }
        public ProductDescription Description { get; }
        public ProductWeight Weight { get; }

        public static ProductCreatedEvent For(Product product)
        {
            return new ProductCreatedEvent(product.Identity,product.Name
                , product.Description,product.Weight,product.Version);
        }
        
        public JsonNode ToOutbox()
        {
            return new JsonObject
            {
                { "Id", this.Id.Value },
                { "Name", this.Name.Value },
                { "Description", this.Description.Value },
                { "Weight", this.Weight.Value },
                { "When", When }
            };
        }        
    }
}