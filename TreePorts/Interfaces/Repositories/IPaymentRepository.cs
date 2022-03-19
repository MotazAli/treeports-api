using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TreePorts.Models;

namespace TreePorts.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Bookkeeping>> GetBookkeepingAsync(CancellationToken cancellationToken);
        Task<Bookkeeping?> GetBookkeepingByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<Bookkeeping>> GetBookkeepingByAsync(Expression<Func<Bookkeeping, bool>> predicate,CancellationToken cancellationToken);
        Task<object> GetBookkeepingPaginationByAsync(Expression<Func<Bookkeeping, bool>> predicate,int skip ,int take,CancellationToken cancellationToken);
        Task<Bookkeeping> InsertBookkeepingAsync(Bookkeeping bookkeeping,CancellationToken cancellationToken);
        Task<Bookkeeping?> UpdateBookkeepingAsync(Bookkeeping bookkeeping,CancellationToken cancellationToken);
        Task<Bookkeeping?> DeleteBookkeepingAsync(long id,CancellationToken cancellationToken);

        Task<List<Bookkeeping>> UntransferredBookkeepingByAsync(Expression<Func<Bookkeeping, bool>> predicate,CancellationToken cancellationToken);

        Task<List<DepositType>> GetDepositTypesAsync(CancellationToken cancellationToken);
        Task<DepositType?> GetDepositTypeByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<DepositType>> GetDepositTypeByAsync(Expression<Func<DepositType, bool>> predicate,CancellationToken cancellationToken);
        Task<DepositType> InsertDepositTypeAsync(DepositType depositType,CancellationToken cancellationToken);
        Task<DepositType?> UpdateDepositTypeAsync(DepositType depositType,CancellationToken cancellationToken);
        Task<DepositType?> DeleteDepositTypeAsync(long id,CancellationToken cancellationToken);


        Task<List<Transfer>> GetTransfersAsync(CancellationToken cancellationToken);
        Task<Transfer?> GetTransferByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<Transfer>> GetTransfersByAsync(Expression<Func<Transfer, bool>> predicate,CancellationToken cancellationToken);
        Task<Transfer> InsertTransferAsync(Transfer transfer,CancellationToken cancellationToken);
        Task<Transfer?> UpdateTransferAsync(Transfer transfer,CancellationToken cancellationToken);
        Task<Transfer?> DeleteTransferAsync(long id,CancellationToken cancellationToken);


    }
}
