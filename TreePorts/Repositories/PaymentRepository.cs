using System.Linq.Expressions;

namespace TreePorts.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly TreePortsDBContext _context;
        public PaymentRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<Bookkeeping?> DeleteBookkeepingAsync(long id, CancellationToken cancellationToken)
        {
            var oldBookkeeping = await this.GetBookkeepingByIdAsync(id,cancellationToken);
            if (oldBookkeeping == null) return null;

            _context.Bookkeepings.Remove(oldBookkeeping);
            return oldBookkeeping;
        }

        public async Task<DepositType?> DeleteDepositTypeAsync(long id,CancellationToken cancellationToken)
        {
            var oldDepositType = await this.GetDepositTypeByIdAsync(id,cancellationToken);
            if (oldDepositType == null) return null;

            _context.DepositTypes.Remove(oldDepositType);
            return oldDepositType;
        }

        public async Task<Transfer?> DeleteTransferAsync(long id, CancellationToken cancellationToken)
        {
            var oldTransfer = await this.GetTransferByIdAsync(id,cancellationToken);
            if (oldTransfer == null) return null;

            _context.Transfers.Remove(oldTransfer);
            return oldTransfer;
        }

        public async Task<List<Bookkeeping>> GetBookkeepingAsync(CancellationToken cancellationToken)
        {
            return await _context.Bookkeepings.ToListAsync(cancellationToken);
        }

        public async Task<List<Bookkeeping>> GetBookkeepingByAsync(Expression<Func<Bookkeeping, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Bookkeepings.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<Bookkeeping?> GetBookkeepingByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Bookkeepings.FirstOrDefaultAsync(b => b.Id == id,cancellationToken);
        }

        public async Task<List<DepositType>> GetDepositTypeByAsync(Expression<Func<DepositType, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.DepositTypes.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<DepositType?> GetDepositTypeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.DepositTypes.FirstOrDefaultAsync(b => b.Id == id,cancellationToken);
        }

        public async Task<List<DepositType>> GetDepositTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.DepositTypes.ToListAsync(cancellationToken);
        }

        public async Task<List<Transfer>> GetTransfersByAsync(Expression<Func<Transfer, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Transfers.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<Transfer?> GetTransferByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Transfers.FirstOrDefaultAsync(b => b.Id == id,cancellationToken);
        }

        public async Task<List<Transfer>> GetTransfersAsync(CancellationToken cancellationToken)
        {
            return await _context.Transfers.ToListAsync(cancellationToken);
        }

        public async Task<Bookkeeping> InsertBookkeepingAsync(Bookkeeping bookkeeping, CancellationToken cancellationToken)
        {
            bookkeeping.CreationDate = DateTime.Now;
            var inserResult = await _context.Bookkeepings.AddAsync(bookkeeping,cancellationToken);
            return inserResult.Entity;
        }

        public async Task<DepositType> InsertDepositTypeAsync(DepositType depositType, CancellationToken cancellationToken)
        {
            depositType.CreationDate = DateTime.Now;
            var inserResult = await _context.DepositTypes.AddAsync(depositType,cancellationToken);
            return inserResult.Entity;
        }

        public async Task<Transfer> InsertTransferAsync(Transfer transfer, CancellationToken cancellationToken)
        {
            transfer.CreationDate = DateTime.Now;
            var inserResult = await _context.Transfers.AddAsync(transfer,cancellationToken);
            return inserResult.Entity;
        }

        public async Task<Bookkeeping?> UpdateBookkeepingAsync(Bookkeeping bookkeeping, CancellationToken cancellationToken)
        {
            var oldBookkeeping = await this.GetBookkeepingByIdAsync(bookkeeping.Id,cancellationToken);
            if (oldBookkeeping == null) return null;

            oldBookkeeping.CaptainUserAccountId = bookkeeping.CaptainUserAccountId;
            oldBookkeeping.OrderId = bookkeeping.OrderId;
            oldBookkeeping.DepositTypeId = bookkeeping.DepositTypeId;
            oldBookkeeping.Value = bookkeeping.Value;
            oldBookkeeping.ModificationDate = DateTime.Now;

            _context.Entry<Bookkeeping>(oldBookkeeping).State = EntityState.Modified;
            return oldBookkeeping;

        }

        public async Task<DepositType?> UpdateDepositTypeAsync(DepositType depositType, CancellationToken cancellationToken)
        {
            var oldDepositType = await this.GetDepositTypeByIdAsync(depositType.Id,cancellationToken);
            if (oldDepositType == null) return null;

            oldDepositType.Type = depositType.Type;
            oldDepositType.ArabicType = depositType.ArabicType;
            oldDepositType.ModificationDate = DateTime.Now;

            _context.Entry<DepositType>(oldDepositType).State = EntityState.Modified;
            return oldDepositType;
        }

        public async Task<Transfer?> UpdateTransferAsync(Transfer transfer, CancellationToken cancellationToken)
        {
            var oldTransfer = await this.GetTransferByIdAsync(transfer.Id,cancellationToken);
            if (oldTransfer == null) return null;

            oldTransfer.BookkeepingId = transfer.BookkeepingId;
            oldTransfer.ModificationDate = DateTime.Now;

            _context.Entry<Transfer>(oldTransfer).State = EntityState.Modified;
            return oldTransfer;
        }


        public async Task<object> GetBookkeepingPaginationByAsync(Expression<Func<Bookkeeping, bool>> predicate, int skip ,int take, CancellationToken cancellationToken)
        {

            var query = await _context.Bookkeepings.Where(predicate).ToListAsync(cancellationToken);

            return (from bookkeeping in query
                          join order in _context.Orders on bookkeeping.OrderId equals order.Id
                          join agent in _context.Agents on order.AgentId equals agent.Id
                          join depositType in _context.DepositTypes on bookkeeping.DepositTypeId equals depositType.Id
                          select new { AgentId = agent.Id ,AgentName = agent.Fullname, AgentImage = agent.Image ,DepositType = depositType, Bookkeeping = bookkeeping }).Skip(skip).Take(take).ToList();
        }



        public async Task<List<Bookkeeping>> UntransferredBookkeepingByAsync(Expression<Func<Bookkeeping, bool>> predicate, CancellationToken cancellationToken)
        {
            var query = await _context.Bookkeepings.Where(predicate).ToListAsync(cancellationToken);
            //var ids = await _context.Transfers.Where(t => t.BookkeepingId)


            return (from bookkeeping in query
                    join transfer in _context.Transfers on bookkeeping.Id equals transfer.BookkeepingId into resultjoin
                    where !resultjoin.Select(r => r.BookkeepingId).ToList().Contains(bookkeeping.Id) 
                    select bookkeeping
                    ).ToList();
        }



    }
}
