using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Data.Data;
using KhumaloCraft.Data.Entities;
using KhumaloCraft.Data.Repositories.Interfaces;

namespace KhumaloCraft.Data.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
  private readonly ApplicationDbContext _dbContext;

  public OrderRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<Order>> GetAllOrdersAsync()
  {
    return await _dbContext.Orders
      .Include(o => o.OrderItems)
      .ThenInclude(oi => oi.Product)
      .Include(o => o.User)
      .ToListAsync();
  }

  public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
  {
    return await _dbContext.Orders
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
        .Where(o => o.UserId == userId)
        .ToListAsync();
  }

  public async Task<Order?> GetOrderByIdAsync(int orderId)
  {
    return await _dbContext.Orders.FirstOrDefaultAsync(p => p.OrderId == orderId);
  }

  public async Task AddOrderAsync(Order order)
  {
    await _dbContext.Orders.AddAsync(order);
  }

  public async Task CancelOrderAsync(Order order)
  {
    _dbContext.Orders.Remove(order);
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
