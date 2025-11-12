using Dotnet.Mcp.Example.Core.Application.DTOs;
using Dotnet.Mcp.Example.Core.Application.Services;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost(
    "/api/contractors",
    async (
        CreateContractorRequest request,
        IContractorsService service,
        CancellationToken cancellationToken) =>
    {
        var contractor = await service.CreateAsync(
            request.Name,
            cancellationToken);
        
        return Results.Created(
            $"/api/contractors/{contractor.Id}",
            new ContractorResponse(contractor.Id, contractor.Name));
    });

app.MapPost(
    "/api/orders",
    async (
        CreateOrderRequest request,
        IOrdersService service,
        CancellationToken cancellationToken) =>
    {
        var orderId = await service.CreateOrderAsync(
            request.ContractorId,
            cancellationToken);
        
        return Results.Created(
            $"/api/orders/{orderId}",
            new CreateOrderResponse(orderId));
    });

app.MapPost(
    "/api/orders/positions",
    async (
        AddOrderPositionRequest request,
        IOrdersService service,
        CancellationToken cancellationToken) =>
    {
        await service.AddPositionAsync(
            request.OrderId,
            request.ProductName,
            request.UnitPrice,
            request.Quantity,
            cancellationToken);

        return Results.NoContent();
    });

app.MapGet(
    "/api/orders",
    async (
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken) =>
    {
        var orders = await dbContext
            .Set<Order>()
            .Include(o => o.Positions)
            .ToListAsync(cancellationToken);

        if (!orders.Any())
        {
            return Results.NotFound();
        }

        var orderDtos = orders.Select(order => new OrderDto
        {
            Id = order.Id,
            Number = order.Number,
            CreatedAt = order.CreatedAt,
            Status = order.Status.Value,
            ContractorId = order.ContractorId,
            Positions = order.Positions.Select(position => new OrderPositionDto
            {
                Id = position.Id,
                UnitPrice = position.UnitPrice,
                Quantity = position.Quantity
            }).ToList()
        }).ToList();
        
        return Results.Ok(orderDtos);
    });

app.MapPost(
    "/api/invoices",
    async (
        CreateInvoiceRequest request,
        IInvoicesService service,
        CancellationToken cancellationToken) =>
    {
        var invoiceId = await service.CreateInvoiceFromOrderAsync(
            request.OrderId,
            cancellationToken);

        return Results.Created($"/api/invoices/{invoiceId}", new CreateInvoiceResponse(invoiceId));
    });

app.MapDelete(
    "/api/orders/{id:guid}",
    async (
        Guid id,
        IOrdersService service,
        CancellationToken cancellationToken) =>
    {
        await service.DeleteOrderAsync(id, cancellationToken);
        return Results.NoContent();
    });

app.Run();

// DTOs
sealed record CreateContractorRequest(string Name);
sealed record ContractorResponse(Guid Id, string Name);

sealed record CreateOrderRequest(Guid ContractorId);
sealed record CreateOrderResponse(Guid OrderId);

sealed record AddOrderPositionRequest(Guid OrderId, string ProductName, decimal UnitPrice, decimal Quantity);

sealed record CreateInvoiceRequest(Guid OrderId);
sealed record CreateInvoiceResponse(Guid InvoiceId);
