using Fitshirt.Infrastructure.Models.Purchases;
using Fitshirt.Infrastructure.Repositories.Common;

namespace Fitshirt.Infrastructure.Repositories.Purchases;

public interface IPurchaseRepository : IBaseRepository<Purchase>
{
    
    Task<IReadOnlyCollection<Purchase>> GetPurchasesByUserId(int userId);
}