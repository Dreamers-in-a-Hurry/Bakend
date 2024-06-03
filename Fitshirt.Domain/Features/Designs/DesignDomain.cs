using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Designs;
using Fitshirt.Infrastructure.Repositories.Users;

namespace Fitshirt.Domain.Features.Designs;

public class DesignDomain : IDesignDomain
{
    private readonly IDesignRepository _designRepository;
    private readonly IShieldRepository _shieldRepository;
    private readonly IUserRepository _userRepository;

    public DesignDomain(IDesignRepository designRepository, IShieldRepository shieldRepository, IUserRepository userRepository)
    {
        _designRepository = designRepository;
        _shieldRepository = shieldRepository;
        _userRepository = userRepository;
    }

    public Task<bool> AddAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync()
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

    public async Task<bool> AddPostAsync(Design design, ICollection<int> shieldId)
    {
        var user = await _userRepository.GetByIdAsync(design.UserId);

        if (user == null)
        {
            throw new NotFoundEntityIdException(nameof(User), design.UserId);
        }

        var shields = await _shieldRepository.GetShieldsByIdsAsync(shieldId);
        if (shields.Count != shieldId.Count)
        {
            var shieldsIdFoundInDatabase = shields.Select(s => s.Id).ToList();
            var shieldsNotFoundInDatabase = shieldId.Except(shieldsIdFoundInDatabase).ToList();
            throw new NotFoundInListException<int>(nameof(DesignShield), nameof(DesignShield.Id),shieldsNotFoundInDatabase);
        }

        design.DesignShields = shields.Select(s => new DesignShield()
        {
            TeamName = s
        }).ToList();
        return await _designRepository.AddAsync(design);
    }

    public async Task<bool> UpdatePostAsync()
    {
        throw new NotImplementedException();
    }
}