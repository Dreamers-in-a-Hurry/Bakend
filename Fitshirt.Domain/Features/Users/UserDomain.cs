using AutoMapper;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Users;

namespace Fitshirt.Domain.Features.Users;

public class UserDomain : IUserDomain
{
    private readonly IUserRepository _userRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IRoleRepository _roleRepository;

    public UserDomain(IUserRepository userRepository, IServiceRepository serviceRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _serviceRepository = serviceRepository;
        _roleRepository = roleRepository;
    }

    public async Task<bool> AddAsync(User user)
    {
        var userWithSameEmail = await _userRepository.GetUserByEmailAsync(user.Email);
        if (userWithSameEmail != null)
        {
            throw new DuplicatedUserEmailException(user.Email);
        }

        var userWithSamePhoneNumber = await _userRepository.GetUserByPhoneNumberAsync(user.Cellphone);
        if (userWithSamePhoneNumber != null)
        {
            throw new DuplicatedUserCellphoneException(user.Cellphone);
        }

        var userWithSameUsername = await _userRepository.GetUserByUsernameAsync(user.Username);
        if (userWithSameUsername != null)
        {
            throw new DuplicatedUserUsernameException(user.Username);
        }

        if (IsAgeLowerThan18(user.BirthDate))
        {
            throw new ValidationException("User must be, at least, 18 years old");
        }

        var clientRole = await _roleRepository.GetClientRoleAsync();

        var freeService = await _serviceRepository.GetFreeServiceAsync();
        
        user.RoleId = clientRole!.Id;
        user.ServiceId = freeService!.Id;
        
        return await _userRepository.AddAsync(user);
    }

    public async Task<bool> UpdateAsync(int id, User user)
    {
        var userWithSameEmail = await _userRepository.GetUserByEmailAsync(user.Email);
        if (userWithSameEmail != null && user.Id != userWithSameEmail.Id)
        {
            throw new DuplicatedUserEmailException(user.Email);
        }

        var userWithSamePhoneNumber = await _userRepository.GetUserByPhoneNumberAsync(user.Cellphone);
        if (userWithSamePhoneNumber != null && user.Id != userWithSamePhoneNumber.Id)
        {
            throw new DuplicatedUserCellphoneException(user.Cellphone);
        }

        var userWithSameUsername = await _userRepository.GetUserByUsernameAsync(user.Username);
        if (userWithSameUsername != null && user.Id != userWithSameUsername.Id)
        {
            throw new DuplicatedUserUsernameException(user.Username);
        }
        
        if (IsAgeLowerThan18(user.BirthDate))
        {
            throw new ValidationException("User must be, at least, 18 years old");
        }

        return await _userRepository.UpdateAsync(id, user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    private bool IsAgeLowerThan18(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthDate.Year;
        
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age < 18;
    }
}