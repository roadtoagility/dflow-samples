// Copyright (C) 2023  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using DFlow.BusinessObjects;
using DomainModel.SimpleApp.Domain;
using Xunit;
using FluentAssertions;

namespace DomainModel.SimpleApp.Tests.Domain;

public class BusinessObjectTests
{
    [Fact]
    public void Create_Description_Valid()
    {
        string input = "description";
        Description desc = Description.From(input);
        desc.Value.Should().Be(input);
    }
    
    [Fact]
    public void Create_ProductName_Valid()
    {
        //setup - copnfiguração
        string input = "my name";
        
        //operação
        ProductName name = ProductName.From(input);
        
        // assert - verificação
        name.Value.Should().Be(input);
    }
    
    [Fact]
    public void Create_ProductName_equal()
    {
        //setup - copnfiguração
        string input = "my name";
        
        //operação
        ProductName name1 = ProductName.From(input);
        ProductName name2 = ProductName.From(input);
        
        // assert - verificação
        name1.Should().Be(name2);
    }
    
    [Theory]
    [InlineData("", false)]
    [InlineData("    ", false)]
    [InlineData(null, false)]
    public void Create_ProductName_Empty(string input, bool expected)
    {
        //operação
        ProductName name = ProductName.From(input);
        
        // assert - verificação
        name.ValidationStatus.IsValid.Should().Be(expected);
    }
  
    [Fact]
    public void Create_Weight_Valid()
    {
        //setup - copnfiguração
        double input = 2;
        
        //operação
        Weight weight = Weight.From(input);
        
        // assert - verificação
        weight.Value.Should().Be(input);
    }
    
    [Theory]
    [InlineData(-1,false)]
    [InlineData(-100,false)]
    [InlineData(-2393433.0909f,false)]
    public void Create_Weight_LessThanZero(int input, bool expected)
    {
        //operação
        Weight weight = Weight.From(input);
        
        // assert - verificação
        weight.ValidationStatus.IsValid.Should().Be(expected);
    }
    
    [Fact]
    public void Create_Product_Valid()
    {
        //setup - copnfiguração
        ProductName inputName = ProductName.From("my name");
        Weight inputWeight = Weight.From(2);
        Description inputDescription = Description.From("");
        
        //operação
        Product product = new Product(inputName,inputDescription
            , inputWeight, VersionId.New());
        
        // assert - verificação
        product.IsValid.Should().BeTrue();
        product.Identity.Should().Be(inputName);
    }
    
    [Theory]
    [InlineData(
        "descrição do produto",
        "",
        10,
        false
    )]
    [InlineData(
        "descrição do produto",
        "nome",
        -12,
        false
    )]
    public void Create_Product_Invalid(
        string descr, string name, double weight, bool expect)
    {
        //setup - copnfiguração
        ProductName inputName = ProductName.From(name);
        Weight inputWeight = Weight.From(weight);
        Description inputDescription = Description.From(descr);
        
        //operação
        Product product = new Product(inputName,inputDescription
            , inputWeight, VersionId.New());
        
        // assert - verificação
        product.IsValid.Should().BeFalse();
        product.Identity.Should().Be(inputName);
    }
    
    [Theory]
    [InlineData("my name","new name",10,"","new name")]
    public void Create_Product_NameUpdate(
        string inputName, 
        string inputNewName, 
        double inputWeight,
        string inputDescr, 
        string expected)
    {
        var inputProduct = Product.New(inputName, inputWeight, inputDescr);
        var newName = ProductName.From(inputNewName);
        var product = Product.NameUpdate(inputProduct,newName); 

        product.IsValid.Should().BeTrue();
        product.Identity.Should().Be(newName);



    }
}