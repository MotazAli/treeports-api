using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TreePorts.Models;

namespace TreePorts.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Bookkeeping>> GetBookkeepingAsync();
        Task<Bookkeeping> GetBookkeepingByIdAsync(long id);
        Task<List<Bookkeeping>> GetBookkeepingByAsync(Expression<Func<Bookkeeping, bool>> predicate);
        Task<object> GetBookkeepingPaginationByAsync(Expression<Func<Bookkeeping, bool>> predicate,int skip ,int take);
        Task<Bookkeeping> InsertBookkeepingAsync(Bookkeeping bookkeeping);
        Task<Bookkeeping> UpdateBookkeepingAsync(Bookkeeping bookkeeping);
        Task<Bookkeeping> DeleteBookkeepingAsync(long id);

        Task<List<Bookkeeping>> UntransferredBookkeepingByAsync(Expression<Func<Bookkeeping, bool>> predicate);

        Task<List<DepositType>> GetDepositTypesAsync();
        Task<DepositType> GetDepositTypeByIdAsync(long id);
        Task<List<DepositType>> GetDepositTypeByAsync(Expression<Func<DepositType, bool>> predicate);
        Task<DepositType> InsertDepositTypeAsync(DepositType depositType);
        Task<DepositType> UpdateDepositTypeAsync(DepositType depositType);
        Task<DepositType> DeleteDepositTypeAsync(long id);


        Task<List<Transfer>> GetTransfersAsync();
        Task<Transfer> GetTransferByIdAsync(long id);
        Task<List<Transfer>> GetTransfersByAsync(Expression<Func<Transfer, bool>> predicate);
        Task<Transfer> InsertTransferAsync(Transfer transfer);
        Task<Transfer> UpdateTransferAsync(Transfer transfer);
        Task<Transfer> DeleteTransferAsync(long id);


    }
}
