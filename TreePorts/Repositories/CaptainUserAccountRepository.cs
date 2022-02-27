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

        public async Task<CaptainUserAccount?> DeleteCaptainUserAccountAsync(string id)
        {
            var account = await _context.CaptainUserAccounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            if(account == null) return null;
            account.ModificationDate = DateTime.Now;
            account.IsDeleted = true;
            _context.Entry<CaptainUserAccount>(account).State = EntityState.Modified;
            return account;
        }

        public async Task<List<CaptainUserAccount>> GetCaptainUsesrAccountsAsync()
        {
            return await _context.CaptainUserAccounts.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<List<CaptainUserAccount>> GetCaptainUsersAccountsByAsync(Expression<Func<CaptainUserAccount, bool>> predicate)
        {
            return  await _context.CaptainUserAccounts.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserAccount?> GetCaptainUserAccountByIdAsync(string ID)
        {
            return await _context.CaptainUserAccounts.Where(a => a.Id == ID).FirstOrDefaultAsync();
        }

		
		public async Task<CaptainUserAccount> InsertCaptainUserAccountAsync(CaptainUserAccount account)
        {
            account.CreationDate = DateTime.Now;
            var result = await _context.CaptainUserAccounts.AddAsync(account);
            return result.Entity;
        }

        public async Task<CaptainUserAccount?> UpdateCaptainUserAccountAsync(CaptainUserAccount account)
        {
            var oldAccount = await _context.CaptainUserAccounts.Where(a => a.Id == account.Id).FirstOrDefaultAsync();
            if (oldAccount == null) return null;

            oldAccount.CaptainUserId = account.CaptainUserId;
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
            return  _context.CaptainUserAccounts.Where(predicate);
        }

        public IQueryable<CaptainUserAccount> GetByQuerable()
        {
            return _context.CaptainUserAccounts;
        }

        /* Filter User*/
    }
}
