using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Fitshirt.Infrastructure.Repositories.Designs;
using Fitshirt.Infrastructure.Repositories.Users;

namespace Fitshirt.Domain.Features.Designs;

public class DesignDomain : IDesignDomain
{
    private readonly IDesignRepository _designRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IShieldRepository _shieldRepository;
    private readonly IUserRepository _userRepository;

    public DesignDomain(IDesignRepository designRepository, IColorRepository colorRepository, IShieldRepository shieldRepository, IUserRepository userRepository)
    {
        _designRepository = designRepository;
        _colorRepository = colorRepository;
        _shieldRepository = shieldRepository;
        _userRepository = userRepository;
    }

    public Task<bool> AddAsync(Design entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int d, Design entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingDesign = await _designRepository.GetByIdAsync(id);
        if (existingDesign == null)
        {
            throw new NotFoundEntityIdException(nameof(Design), id);
        }

        return await _designRepository.DeleteAsync(id);
    }

    public async Task<bool> AddDesignAsync(Design design, ICollection<int> shieldId)
    {
        var user = await _userRepository.GetByIdAsync(design.UserId);

        if (user == null)
        {
            throw new NotFoundEntityIdException(nameof(User), design.UserId);
        }

        var shield = await _shieldRepository.GetByIdAsync(design.ShieldId);
        if (shield == null)
        {
            throw new NotFoundEntityIdException(nameof(DesignShield), design.ShieldId);
        }

        return await _designRepository.AddAsync(design);
    }

    public async Task<bool> UpdateDesignAsync(int id, Design design)
    {
        var existingDesign = await _designRepository.GetByIdAsync(id);

        if (existingDesign == null)
        {
            throw new NotFoundEntityIdException(nameof(Design), id);
        }
        
        var user = await _userRepository.GetByIdAsync(design.UserId);

        if (user == null)
        {
            throw new NotFoundEntityIdException(nameof(User), design.UserId);
        }

        var colorPrimary = await _colorRepository.GetByIdAsync(design.PrimaryColorId);

        if (colorPrimary == null)
        {
            throw new NotFoundEntityIdException(nameof(Color), design.PrimaryColorId);
        }
        
        var colorSecondary = await _colorRepository.GetByIdAsync(design.SecondaryColorId);

        if (colorSecondary == null)
        {
            throw new NotFoundEntityIdException(nameof(Color), design.SecondaryColorId);
        }
        
        var colorTertiary = await _colorRepository.GetByIdAsync(design.TertiaryColorId);

        if (colorTertiary == null)
        {
            throw new NotFoundEntityIdException(nameof(Color), design.TertiaryColorId);
        }

        var shield = await _shieldRepository.GetByIdAsync(design.ShieldId);
        if (shield == null)
        {
            throw new NotFoundEntityIdException(nameof(DesignShield), design.ShieldId);
        }

        return await _designRepository.UpdateAsync(id, design);
    }
}