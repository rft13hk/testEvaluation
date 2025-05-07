using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.Shared;
public class SaleItemsScenariosTest
{
    public async Task<Ambev.DeveloperEvaluation.Domain.Entities.User> CreateFackeUser(string user = "user01")
    {
        var fackeUser = new Ambev.DeveloperEvaluation.Domain.Entities.User();
        fackeUser.Id = Guid.NewGuid();
        fackeUser.Username = user;
        fackeUser.Email = "test@test.com";
        fackeUser.CreatedAt = DateTime.UtcNow;
        fackeUser.UpdatedAt = DateTime.UtcNow;

        return await Task.FromResult(fackeUser);
    }

    public async Task<Ambev.DeveloperEvaluation.Domain.Entities.Costumer> CreateFackeCostumer(
        Ambev.DeveloperEvaluation.Domain.Entities.User fackeUser, 
        string costumer = "costumer01"
        )
    {
        var fackeCostumer = new Ambev.DeveloperEvaluation.Domain.Entities.Costumer()
        {
            Id = Guid.NewGuid(),
            UserId = fackeUser.Id,
            Users = fackeUser,
            CostumerName = costumer,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow
        };

        return await Task.FromResult(fackeCostumer);
    }

    public async Task<Ambev.DeveloperEvaluation.Domain.Entities.Branch> CreateFackeBranch(
        Ambev.DeveloperEvaluation.Domain.Entities.User fackeUser, 
        string branch = "branch01"
        )
    {
        var fackeBranche = new Ambev.DeveloperEvaluation.Domain.Entities.Branch()
        {
            Id = Guid.NewGuid(),
            BranchName = branch,
            UserId = fackeUser.Id,
            User = fackeUser,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow
        };

        return await Task.FromResult(fackeBranche);

    }

    public async Task<Ambev.DeveloperEvaluation.Domain.Entities.Product> CreateFackeProduct(
        Ambev.DeveloperEvaluation.Domain.Entities.User fackeUser,
        string productCode = "001",
        string productName = "Product 001",
        decimal price = 123.45m
    )
    {
        var fackeProduct = new Ambev.DeveloperEvaluation.Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            ProductCode = productCode,
            ProductName = productName,
            Price = price,
            UserId = fackeUser.Id,
            User = fackeUser
        };

        return await Task.FromResult(fackeProduct);        
    }

    public async Task<Ambev.DeveloperEvaluation.Domain.Entities.Sale> CreateFackeSale(
        Ambev.DeveloperEvaluation.Domain.Entities.Costumer fackeCostumer,
        Ambev.DeveloperEvaluation.Domain.Entities.Branch fackeBranche,
        Ambev.DeveloperEvaluation.Domain.Entities.User fackeUser,
        string saleNumber = "001"
    )
    {
        var fackeSale = new Ambev.DeveloperEvaluation.Domain.Entities.Sale()
        {
            Id = Guid.NewGuid(),
            SaleNumber = saleNumber,
            SaleDate = DateTime.UtcNow,
            CostumerId = fackeCostumer.Id,
            Costumer = fackeCostumer,
            BranchId = fackeBranche.Id,
            Branch = fackeBranche,
            UserId = fackeUser.Id,
            User = fackeUser
        };

        return await Task.FromResult(fackeSale);

    }

    public async Task<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem> CreateFackeSaleItem(
        Ambev.DeveloperEvaluation.Domain.Entities.Sale fackeSale,
        Ambev.DeveloperEvaluation.Domain.Entities.Product fackeProduct,
        Ambev.DeveloperEvaluation.Domain.Entities.User fackeUser,        
        int quantity = 1
        )
    {
        var fackeSaleItem = new Ambev.DeveloperEvaluation.Domain.Entities.SaleItem()
        {
            Id = Guid.NewGuid(),
            SaleId = fackeSale.Id,
            Sale = fackeSale,
            SaleDate = DateTime.UtcNow,
            ProductId = fackeProduct.Id,
            Product = fackeProduct,
            Price = fackeProduct.Price,
            Quantity = quantity,
            UserId = fackeUser.Id,
            User = fackeUser
        };

        return await Task.FromResult(fackeSaleItem);            
    }

    public async Task<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem> CreateFackeSaleItem(
        string saleNumber = "001",
        string user = "user01",
        string costumer = "costumer01",
        string branch = "branch01",

        string productCode = "001",
        string productName = "Product 001",
        decimal price = 123.45m,
        int quantity = 1       
    )
    {
        var _user = await CreateFackeUser(user);
        var _costumer = await CreateFackeCostumer(_user, costumer);
        var _branch = await CreateFackeBranch(_user, branch);
        var _product = await CreateFackeProduct(_user, productCode, productName, price);
        var _sale = await CreateFackeSale(_costumer, _branch, _user, saleNumber);
        var _saleItem = await CreateFackeSaleItem(_sale, _product, _user, quantity);

        return _saleItem;
        
    }

}