using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ExpenseStatistics.Dto;
using ExpenseStatistics.Domain.Entities;
using ExpenseStatistics.Repositories;

namespace ExpenseStatistics.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IValidator<CreateTransactionDto> _validator;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public TransactionService(
            TransactionRepository transactionRepository,
            IValidator<CreateTransactionDto> validator,
            IMapper mapper,
            IMemoryCache cache)
        {
            _transactionRepository = transactionRepository;
            _validator = validator;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Transaction> CreateAsync(CreateTransactionDto dto, Guid userId)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var transaction = _mapper.Map<Transaction>(dto);
            transaction.Id = Guid.NewGuid();
            transaction.UserId = userId;

            await _transactionRepository.AddAsync(transaction);
            _cache.Remove($"statistics_{userId}");

            return transaction;
        }

        public async Task<Transaction> GetByIdAsync(Guid id, Guid userId)
        {
            return await _transactionRepository.GetByIdAsync(id, userId);
        }
    }
}