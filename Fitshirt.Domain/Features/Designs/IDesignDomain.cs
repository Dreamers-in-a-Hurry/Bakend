using Fitshirt.Domain.Features.Common;
using Fitshirt.Infrastructure.Models.Designs;

namespace Fitshirt.Domain.Features.Designs;

public interface IDesignDomain : IBaseDomain<Design>
{
    Task<bool> AddDesignAsync(Design design, ICollection<int> sizeIds);
    Task<bool> UpdateDesignAsync(int id, Design design, ICollection<int> sizeIds);
}