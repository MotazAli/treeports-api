using Microsoft.EntityFrameworkCore;
using TreePorts.DTO;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Repositories
{
    public class CaptainUserAccountRepository : ICaptainUserAccountRepository
    {

        private TreePortsDBContext _context;

        public CaptainUserAccountRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<CaptainUserAccount> Delete(long ID)
        {
            var account = await _context.UserAccounts.Where(a => a.Id == ID).FirstOrDefaultAsync();
            account.ModificationDate = DateTime.Now;
            account.IsDeleted = true;
            _context.Entry<CaptainUserAccount>(account).State = EntityState.Modified;
            return account;
        }

        public async Task<List<CaptainUserAccount>> GetAll()
        {
            return await _context.UserAccounts.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<List<CaptainUserAccount>> GetBy(Expression<Func<CaptainUserAccount, bool>> predicate)
        {
            return  await _context.UserAccounts.Where(predicate).Include(u => u.User).ToListAsync();
        }

        public async Task<CaptainUserAccount> GetByID(long ID)
        {
            return await _context.UserAccounts.Where(a => a.Id == ID).FirstOrDefaultAsync();
        }

		
		public async Task<CaptainUserAccount> Insert(CaptainUserAccount account)
        {
            account.CreationDate = DateTime.Now;
            var result = await _context.UserAccounts.AddAsync(account);
            return result.Entity;
        }

        public async Task<CaptainUserAccount> Update(CaptainUserAccount account)
        {
            var oldAccount = await _context.UserAccounts.Where(a => a.Id == account.Id).FirstOrDefaultAsync();

            oldAccount.UserId = account.UserId;
            oldAccount.StatusTypeId = account.StatusTypeId;
            oldAccount.Mobile = account.Mobile;
            oldAccount.PasswordHash = account.PasswordHash;
            oldAccount.PasswordSalt = account.PasswordSalt;
            oldAccount.Password = account.Password;
            oldAccount.Token = account.Token;
            oldAccount.ModifiedBy = account.ModifiedBy;
            oldAccount.ModificationDate = account.ModificationDate;
            _context.Entry<CaptainUserAccount>(oldAccount).State = EntityState.Modified;

            return oldAccount;
        }

        
		public IQueryable<CaptainUserAccount> GetByQuerable(Expression<Func<CaptainUserAccount, bool>> predicate)
		{
            return  _context.UserAccounts.Where(predicate).Include(u => u.User);
        }

        public IQueryable<CaptainUserAccount> GetByQuerable()
        {
            return _context.UserAccounts.Include(u => u.User);
        }

        /* Filter User*/
    }
}
