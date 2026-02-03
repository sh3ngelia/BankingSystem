using AutoMapper;
using BankingSystem.Domain.Entities;
using BankingSystem.Application.DTOs.Account;

namespace BankingSystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountResponseDto>();
            CreateMap<CreateAccountDto, Account>();

            // Project 
            // builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Service
            // var dto = _mapper.Map<AccountResponseDto>(account);
        }
    }
}