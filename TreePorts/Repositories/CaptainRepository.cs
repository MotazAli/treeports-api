using System.Linq.Expressions;
using TreePorts.DTO;
using TreePorts.DTO.Records;

namespace TreePorts.Repositories;

    public class CaptainRepository : ICaptainRepository
    {

        private TreePortsDBContext _context;

        public CaptainRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public  async Task<CaptainUser?> DeleteCaptainUserAsync(string id)
        {
            var oldUser = await _context.CaptainUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUser == null) return null;

            _context.CaptainUsers.Remove(oldUser);
            return oldUser;
        }

        public async Task<IEnumerable<CaptainUser>> GetCaptainUsersAsync()
        {
            return await _context.CaptainUsers.ToListAsync();
        }

        

        public async Task<IEnumerable<CaptainUser>> GetCaptainUsersByAsync(Expression<Func<CaptainUser, bool>> predicate)
        {
            return await _context.CaptainUsers.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUser?> GetCaptainUserByIdAsync(string id)
        {
            return await _context.CaptainUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<CaptainUser> InsertCaptainUserAsync(CaptainUser user)
        {
            /*user.CreationDate = DateTime.Now;
            var userAccount = user.UserAccounts.FirstOrDefault();
            if (userAccount != null)
                userAccount.CreationDate = user.CreationDate;*/

            
            user.Id = Guid.NewGuid().ToString();
            var result = await _context.CaptainUsers.AddAsync(user);
            return result.Entity;
        }

        public async Task<CaptainUser?> UpdateCaptainUserAsync(CaptainUser user)
        {
            var oldUser = await _context.CaptainUsers.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (oldUser == null) return null;

            oldUser.FirstName = user.FirstName ?? oldUser.FirstName;
            oldUser.LastName = user.LastName ?? oldUser.LastName;
            oldUser.StcPay = user.StcPay ?? oldUser.StcPay;
            oldUser.VehiclePlateNumber = user.VehiclePlateNumber ?? oldUser.VehiclePlateNumber;
            oldUser.NbsherNationalNumberImage = user.NbsherNationalNumberImage ?? oldUser.NbsherNationalNumberImage;
            oldUser.BirthDate = user.BirthDate ?? oldUser.BirthDate;

            oldUser.NationalNumber = user.NationalNumber ?? oldUser.NationalNumber;
            oldUser.CountryId = user.CountryId ?? oldUser.CountryId;
            oldUser.CityId = user.CityId ?? oldUser.CityId;
          
            oldUser.Gender = user.Gender ?? oldUser.Gender;
            oldUser.Mobile = user.Mobile ?? oldUser.Mobile;
           
            oldUser.NationalNumberExpireDate = user.NationalNumberExpireDate ?? oldUser.NationalNumberExpireDate;
            oldUser.ResidenceCountryId = user.ResidenceCountryId ?? oldUser.ResidenceCountryId;
            oldUser.ResidenceCityId = user.ResidenceCityId ?? oldUser.ResidenceCityId;
            oldUser.ModifiedBy = user.ModifiedBy ?? oldUser.ModifiedBy;
            oldUser.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUser>(oldUser).State = EntityState.Modified;

            return oldUser;

        }


        public async Task<CaptainUserAccount?> DeleteCaptainUserAccountAsync(string id)
        {
            var account = await _context.CaptainUserAccounts.FirstOrDefaultAsync(a => a.Id == id);
            if (account == null) return null;

            account.ModificationDate = DateTime.Now;
            account.IsDeleted = true;
            _context.Entry<CaptainUserAccount>(account).State = EntityState.Modified;
            return account;
        }

        public async Task<IEnumerable<CaptainUserAccount>> GetCaptainUsersAccountsAsync()
        {
            return await _context.CaptainUserAccounts.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<CaptainUserAccount>> GetCaptainUsersAccountsByAsync(Expression<Func<CaptainUserAccount, bool>> predicate)
        {
            return await _context.CaptainUserAccounts.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserAccount?> GetCaptainUserAccountByIdAsync(string id)
        {
            return await _context.CaptainUserAccounts.FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<CaptainUserAccount> InsertCaptainUserAccountAsync(CaptainUserAccount account)
        {
            account.Id = Guid.NewGuid().ToString();
            account.CreationDate = DateTime.Now;
            var result = await _context.CaptainUserAccounts.AddAsync(account);
            return result.Entity;
        }

        public async Task<CaptainUserAccount?> UpdateCaptainUserAccountAsync(CaptainUserAccount account)
        {
            var oldAccount = await _context.CaptainUserAccounts.FirstOrDefaultAsync(a => a.Id == account.Id);
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


        public IQueryable<CaptainUserAccount> GetCaptainUserAccountByQuerable(Expression<Func<CaptainUserAccount, bool>> predicate)
        {
            return _context.CaptainUserAccounts.Where(predicate);
        }

        public IQueryable<CaptainUserAccount> GetCaptainUserAccountByQuerable()
        {
            return _context.CaptainUserAccounts;
        }


        public async Task<IEnumerable<CaptainUserNewRequest>> GetCaptainUsersNewRequestsAsync()
        {
            return await _context.CaptainUserNewRequests.ToListAsync();
        }

        public async Task<CaptainUserNewRequest?> GetCaptainUserNewRequestByIdAsync(long id)
        {
            return await _context.CaptainUserNewRequests.FirstOrDefaultAsync( u => u.Id == id );
        }

        public async Task<IEnumerable<CaptainUserNewRequest>> GetCaptainUsersNewRequestsByAsync(Expression<Func<CaptainUserNewRequest, bool>> predicate)
        {
            return await _context.CaptainUserNewRequests.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserNewRequest> InsertCaptainUserNewRequestAsync(CaptainUserNewRequest userRequest)
        {
            userRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserNewRequests.AddAsync(userRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserNewRequest?> UpdateCaptainUserNewRequestAsync(CaptainUserNewRequest userRequest)
        {
            var oldUserNewRequest = await _context.CaptainUserNewRequests.FirstOrDefaultAsync(u => u.Id == userRequest.Id);
            if (oldUserNewRequest == null) return null;

            oldUserNewRequest.OrderId = userRequest.OrderId;
            oldUserNewRequest.CaptainUserAccountId = userRequest.CaptainUserAccountId;
            oldUserNewRequest.AgentId = userRequest.AgentId;
            oldUserNewRequest.ModifiedBy = userRequest.ModifiedBy;
            oldUserNewRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserNewRequest>(oldUserNewRequest).State = EntityState.Modified;
            return oldUserNewRequest;

        }

        public async Task<CaptainUserNewRequest?> DeleteCaptainUserNewRequestAsync(long id)
        {
            var oldUserNewRequest = await _context.CaptainUserNewRequests.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserNewRequest == null) return null;

            _context.CaptainUserNewRequests.Remove(oldUserNewRequest);
            return oldUserNewRequest;
        }


        public async Task<IEnumerable<CaptainUserNewRequest>?> DeleteCaptainUserNewRequestByUserIdAsync(string id)
        {

            var oldUserNewRequests = await _context.CaptainUserNewRequests.Where(u => u.CaptainUserAccountId == id).ToListAsync();
            if (oldUserNewRequests is null || oldUserNewRequests.Count == 0) return null;

            _context.CaptainUserNewRequests.RemoveRange(oldUserNewRequests);
            return oldUserNewRequests;
        }


        public async Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUsersAcceptedRequestsAsync()
        {
            return await _context.CaptainUserAcceptedRequests.ToListAsync();
        }

        public async Task<CaptainUserAcceptedRequest?> GetCaptainUserAcceptedRequestByIdAsync(long id)
        {
            return await _context.CaptainUserAcceptedRequests.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUsersAcceptedRequestsByAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            return await _context.CaptainUserAcceptedRequests.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserAcceptedRequest> InsertCaptainUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest)
        {
            userAcceptedRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserAcceptedRequests.AddAsync(userAcceptedRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserAcceptedRequest?> UpdateCaptainUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest)
        {
            var oldUserAcceptedRequest = await _context.CaptainUserAcceptedRequests.FirstOrDefaultAsync(u => u.Id == userAcceptedRequest.Id);
            if (oldUserAcceptedRequest == null) return null;

            oldUserAcceptedRequest.OrderId = userAcceptedRequest.OrderId;
            oldUserAcceptedRequest.CaptainUserAccountId = userAcceptedRequest.CaptainUserAccountId;
            oldUserAcceptedRequest.ModifiedBy = userAcceptedRequest.ModifiedBy;
            oldUserAcceptedRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserAcceptedRequest>(oldUserAcceptedRequest).State = EntityState.Modified;
            return oldUserAcceptedRequest;
        }

        public async Task<CaptainUserAcceptedRequest?> DeleteCaptainUserAcceptedRequestAsync(long id)
        {

            var oldUserAcceptedRequest = await _context.CaptainUserAcceptedRequests.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserAcceptedRequest == null) return null;

            _context.CaptainUserAcceptedRequests.Remove(oldUserAcceptedRequest);
            return oldUserAcceptedRequest;
        }

		public async Task<IEnumerable<BoxType>> GetBoxTypesAsync()
        {
            return await _context.BoxTypes.ToListAsync();
        }

        public async Task<BoxType?> GetBoxTypeByIdAsync(long id)
        {
            return await _context.BoxTypes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserBox>> GetCaptainUsersBoxesAsync()
        {
            return await _context.CaptainUserBoxs.ToListAsync();
        }

        public async Task<CaptainUserBox?> GetCaptainUserBoxById(long id)
        {
            return await _context.CaptainUserBoxs.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserBox>> GetCaptainUsersBoxesByAsync(Expression<Func<CaptainUserBox, bool>> predicate)
        {
            return await _context.CaptainUserBoxs.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserBox> InsertCaptainUserBoxAsync(CaptainUserBox userBox)
        {
            userBox.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserBoxs.AddAsync(userBox);
            return inserResult.Entity;
        }

        public async Task<CaptainUserBox?> UpdateCaptainUserBoxAsync(CaptainUserBox userBox)
        {
            var oldUserBox = await _context.CaptainUserBoxs.FirstOrDefaultAsync(u => u.Id == userBox.Id);
            if (oldUserBox == null) return null;

            oldUserBox.CaptainUserVehicleId = userBox.CaptainUserVehicleId;
            oldUserBox.BoxTypeId = userBox.BoxTypeId;
            oldUserBox.IsDeleted = userBox.IsDeleted;
            oldUserBox.ModifiedBy = userBox.ModifiedBy;
            oldUserBox.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserBox>(oldUserBox).State = EntityState.Modified;
            return oldUserBox;
        }

        public async Task<CaptainUserBox?> DeleteCaptainUserBoxAsync(long id)
        {

            var oldUserBox = await _context.CaptainUserBoxs.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserBox == null) return null;

            oldUserBox.IsDeleted = true;
            _context.Entry<CaptainUserBox>(oldUserBox).State = EntityState.Modified;
            return oldUserBox;
        }

		public async Task<IEnumerable<CaptainUserCurrentBalance>> GetCaptainUsersCurrentBalancesAsync()
        {
            return await _context.CaptainUserCurrentBalances.ToListAsync();
        }

        public async Task<CaptainUserCurrentBalance?> GetCaptainUserCurrentBalanceByIdAsync(long id)
        {
            return await _context.CaptainUserCurrentBalances.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserCurrentBalance>> GetCaptainUsersCurrentBalancesByAsync(Expression<Func<CaptainUserCurrentBalance, bool>> predicate)
        {
            return await _context.CaptainUserCurrentBalances.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserCurrentBalance> InsertCaptainUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance)
        {
            userCurrentBalance.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserCurrentBalances.AddAsync(userCurrentBalance);
            return inserResult.Entity;
        }

        public async Task<CaptainUserCurrentBalance?> UpdateCaptainUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance)
        {
            var oldUserCurrentBalance = await _context.CaptainUserCurrentBalances.FirstOrDefaultAsync(u => u.Id == userCurrentBalance.Id);
            if (oldUserCurrentBalance == null) return null;

            oldUserCurrentBalance.CaptainUserPaymentId = userCurrentBalance.CaptainUserPaymentId;
            oldUserCurrentBalance.PaymentStatusTypeId = userCurrentBalance.PaymentStatusTypeId;
            oldUserCurrentBalance.ModifiedBy = userCurrentBalance.ModifiedBy;
            oldUserCurrentBalance.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserCurrentBalance>(oldUserCurrentBalance).State = EntityState.Modified;
            return userCurrentBalance;
        }

        public async Task<CaptainUserCurrentBalance?> DeleteCaptainUserCurrentBalanceAsync(long id)
        {
            var oldUserCurrentBalance = await _context.CaptainUserCurrentBalances.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserCurrentBalance == null) return null;

            _context.CaptainUserCurrentBalances.Remove(oldUserCurrentBalance);
            return oldUserCurrentBalance;
        }

        public async Task<IEnumerable<CaptainUserCurrentLocation>> GetCaptainUsersCurrentLocationsAsync()
        {
            return await _context.CaptainUserCurrentLocations.ToListAsync();
        }

        public async Task<CaptainUserCurrentLocation?> GetCaptainUserCurrentLocationByIdAsync(long id)
        {
            return await _context.CaptainUserCurrentLocations.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserCurrentLocation>> GetCaptainUsersCurrentLocationsByAsync(Expression<Func<CaptainUserCurrentLocation, bool>> predicate)
        {
            return await _context.CaptainUserCurrentLocations.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserCurrentLocation> InsertCaptainUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation)
        {

            var oldUserCurrentLocations = await _context.CaptainUserCurrentLocations.Where(l => l.CaptainUserAccountId == userCurrentLocation.CaptainUserAccountId).ToListAsync();
            if (oldUserCurrentLocations != null && oldUserCurrentLocations.Count > 0)
            {
                var oldUserCurrentLocation = oldUserCurrentLocations[0];
                if (oldUserCurrentLocations.Count > 1)
                {
                    var removedLocation = oldUserCurrentLocations.Where(l => l.ModificationDate == null);
                    _context.CaptainUserCurrentLocations.RemoveRange(removedLocation);
                    await _context.SaveChangesAsync();
                }
                //oldUserCurrentLocation.UserId = userCurrentLocation.UserId;
                oldUserCurrentLocation.Lat = userCurrentLocation.Lat;
                oldUserCurrentLocation.Long = userCurrentLocation.Long;
                //oldUserCurrentLocation.ModifiedBy = userCurrentLocation.ModifiedBy;
                oldUserCurrentLocation.ModificationDate = DateTime.Now;

                _context.Entry<CaptainUserCurrentLocation>(oldUserCurrentLocation).State = EntityState.Modified;
                return oldUserCurrentLocation;
            }

            userCurrentLocation.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserCurrentLocations.AddAsync(userCurrentLocation);
            return inserResult.Entity;


            /* var oldUserCurrentLocation = await _context.UserCurrentLocations.FirstOrDefaultAsync(l => l.UserId == userCurrentLocation.UserId);
             if (oldUserCurrentLocation?.Id > 0) {
                 //oldUserCurrentLocation.UserId = userCurrentLocation.UserId;
                 oldUserCurrentLocation.Lat = userCurrentLocation.Lat;
                 oldUserCurrentLocation.Long = userCurrentLocation.Long;
                 //oldUserCurrentLocation.ModifiedBy = userCurrentLocation.ModifiedBy;
                 oldUserCurrentLocation.ModificationDate = DateTime.Now;

                 _context.Entry<CaptainUserCurrentLocation>(oldUserCurrentLocation).State = EntityState.Modified;
                 return oldUserCurrentLocation;
             }

             userCurrentLocation.CreationDate = DateTime.Now;
             var inserResult = await _context.UserCurrentLocations.AddAsync(userCurrentLocation);
             return inserResult.Entity;*/
        }

        public async Task<CaptainUserCurrentLocation?> UpdateCaptainUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation)
        {
            var oldUserCurrentLocation = await _context.CaptainUserCurrentLocations.FirstOrDefaultAsync(u => u.Id == userCurrentLocation.Id);
            if (oldUserCurrentLocation == null) return null;

            oldUserCurrentLocation.CaptainUserAccountId = userCurrentLocation.CaptainUserAccountId;
            oldUserCurrentLocation.Lat = userCurrentLocation.Lat;
            oldUserCurrentLocation.Long = userCurrentLocation.Long;
            oldUserCurrentLocation.ModifiedBy = userCurrentLocation.ModifiedBy;
            oldUserCurrentLocation.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserCurrentLocation>(oldUserCurrentLocation).State = EntityState.Modified;
            return oldUserCurrentLocation;
        }

        public async Task<CaptainUserCurrentLocation?> DeleteCaptainUserCurrentLocationByCaptainUserAccountIdAsync(string id)
        {

            var oldUserCurrentLocation = await _context.CaptainUserCurrentLocations.Where(u => u.CaptainUserAccountId == id).ToListAsync();
            if (oldUserCurrentLocation == null || oldUserCurrentLocation.Count() <= 0) return null;

            _context.CaptainUserCurrentLocations.RemoveRange(oldUserCurrentLocation);
            return oldUserCurrentLocation[0];

            /* var oldUserCurrentLocation = await _context.UserCurrentLocations.FirstOrDefaultAsync(u => u.Id == id);
             if (oldUserCurrentLocation == null || oldUserCurrentLocation?.Id <= 0) return null;

             _context.UserCurrentLocations.Remove(oldUserCurrentLocation);
             return oldUserCurrentLocation;*/
        }

        public async Task<IEnumerable<CaptainUserCurrentStatus>> GetCaptainUsersCurrentStatusesAsync()
        {
            return await _context.CaptainUserCurrentStatuses.ToListAsync();
        }

        public async Task<CaptainUserCurrentStatus?> GetCaptainUserCurrentStatusByIdAsync(long id)
        {
            return await _context.CaptainUserCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserCurrentStatus>> GetCaptainUsersCurrentStatusesByAsync(Expression<Func<CaptainUserCurrentStatus, bool>> predicate)
        {
            return await _context.CaptainUserCurrentStatuses.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserCurrentStatus> InsertCaptainUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus)
        {
            var currentDate = DateTime.Now;

            var oldUserCurrentStatus = await _context.CaptainUserCurrentStatuses.FirstOrDefaultAsync(u => u.CaptainUserAccountId == userCurrentStatus.CaptainUserAccountId);
            if (oldUserCurrentStatus != null && oldUserCurrentStatus.Id > 0)
            {
                CaptainUserStatusHistory statusHistory = new CaptainUserStatusHistory()
                {
                    CaptainUserAccountId = oldUserCurrentStatus.CaptainUserAccountId,
                    StatusTypeId = oldUserCurrentStatus.StatusTypeId,
                    CreationDate = currentDate,
                    ModificationDate = oldUserCurrentStatus.ModificationDate
                };


                oldUserCurrentStatus.CaptainUserAccountId = userCurrentStatus.CaptainUserAccountId;
                oldUserCurrentStatus.StatusTypeId = userCurrentStatus.StatusTypeId;
                oldUserCurrentStatus.IsCurrent = true;
                oldUserCurrentStatus.ModificationDate = currentDate;

                await _context.CaptainUserStatusHistories.AddAsync(statusHistory);
                _context.Entry<CaptainUserCurrentStatus>(oldUserCurrentStatus).State = EntityState.Modified;
                return oldUserCurrentStatus;

            }
            else
            {
                CaptainUserStatusHistory statusHistory = new CaptainUserStatusHistory()
                {
                    CaptainUserAccountId = userCurrentStatus.CaptainUserAccountId,
                    StatusTypeId = (long)StatusTypes.New,
                    CreationDate = currentDate,
                    ModificationDate = userCurrentStatus.ModificationDate
                };
                await _context.CaptainUserStatusHistories.AddAsync(statusHistory);
                userCurrentStatus.CreationDate = currentDate;
                var insertResult = await _context.CaptainUserCurrentStatuses.AddAsync(userCurrentStatus);
                return insertResult.Entity;
            }


            
        }

        public async Task<CaptainUserCurrentStatus?> UpdateCaptainUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus)
        {
            var oldUserCurrentStatus = await _context.CaptainUserCurrentStatuses.FirstOrDefaultAsync(u => u.Id == userCurrentStatus.Id);
            if (oldUserCurrentStatus == null) return null;

            var currentDate = DateTime.Now;
            CaptainUserStatusHistory statusHistory = new CaptainUserStatusHistory()
            {
                CaptainUserAccountId = oldUserCurrentStatus.CaptainUserAccountId,
                StatusTypeId = oldUserCurrentStatus.StatusTypeId,
                CreationDate = currentDate
            };



            oldUserCurrentStatus.CaptainUserAccountId = userCurrentStatus.CaptainUserAccountId;
            oldUserCurrentStatus.StatusTypeId = userCurrentStatus.StatusTypeId;
            oldUserCurrentStatus.IsCurrent = userCurrentStatus.IsCurrent;
            oldUserCurrentStatus.ModifiedBy = userCurrentStatus.ModifiedBy;
            oldUserCurrentStatus.ModificationDate = currentDate;

            var insertResult = await _context.CaptainUserStatusHistories.AddAsync(statusHistory);
            _context.Entry<CaptainUserCurrentStatus>(oldUserCurrentStatus).State = EntityState.Modified;
            return oldUserCurrentStatus;
        }

        public async Task<CaptainUserCurrentStatus?> DeleteCaptainUserCurrentStatusAsync(long id)
        {
            var oldUserCurrentStatus = await _context.CaptainUserCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserCurrentStatus == null) return null;

            _context.CaptainUserCurrentStatuses.Remove(oldUserCurrentStatus);
            return oldUserCurrentStatus;
        }

        public async Task<IEnumerable<CaptainUserIgnoredPenalty>> GetCaptainUsersIgnoredPenaltiesAsync()
        {
            return await _context.CaptainUserIgnoredPenalties.ToListAsync();
        }

        public async Task<CaptainUserIgnoredPenalty?> GetCaptainUserIgnoredPenaltyByIdAsync(long id)
        {
            return await _context.CaptainUserIgnoredPenalties.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserIgnoredPenalty>> GetCaptainUsersIgnoredPenaltiesByAsync(Expression<Func<CaptainUserIgnoredPenalty, bool>> predicate)
        {
            return await _context.CaptainUserIgnoredPenalties.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserIgnoredPenalty> InsertCaptainUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty)
        {
            userIgnoredPenalty.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserIgnoredPenalties.AddAsync(userIgnoredPenalty);
            return inserResult.Entity;
        }

        public async Task<CaptainUserIgnoredPenalty?> UpdateCaptainUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty)
        {
            var oldUserIgnoredPenalty = await _context.CaptainUserIgnoredPenalties.FirstOrDefaultAsync(u => u.Id == userIgnoredPenalty.Id);
            if (oldUserIgnoredPenalty == null) return null;

            oldUserIgnoredPenalty.CaptainUserAccountId = userIgnoredPenalty.CaptainUserAccountId;
            oldUserIgnoredPenalty.SystemSettingId = userIgnoredPenalty.SystemSettingId;
            oldUserIgnoredPenalty.PenaltyStatusTypeId = userIgnoredPenalty.PenaltyStatusTypeId;
            oldUserIgnoredPenalty.ModifiedBy = userIgnoredPenalty.ModifiedBy;
            oldUserIgnoredPenalty.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserIgnoredPenalty>(oldUserIgnoredPenalty).State = EntityState.Modified;
            return oldUserIgnoredPenalty;
        }

        public async Task<CaptainUserIgnoredPenalty?> DeleteCaptainUserIgnoredPenaltyAsync(long id)
        {
            var oldUserIgnoredPenalty = await _context.CaptainUserIgnoredPenalties.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserIgnoredPenalty == null) return null;

            _context.CaptainUserIgnoredPenalties.Remove(oldUserIgnoredPenalty);
            return oldUserIgnoredPenalty;
        }

        public async Task<IEnumerable<CaptainUserIgnoredRequest>> GetCaptainUsersIgnoredRequestsAsync()
        {
            return await _context.CaptainUserIgnoredRequests.ToListAsync();
        }

        public async Task<CaptainUserIgnoredRequest?> GetCaptainUserIgnoredRequestByIdAsync(long id)
        {
            return await _context.CaptainUserIgnoredRequests.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserIgnoredRequest>> GetCaptainUsersIgnoredRequestsByAsync(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate)
        {
            return await _context.CaptainUserIgnoredRequests.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserIgnoredRequest> InsertCaptainUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest)
        {
            userIgnoredRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserIgnoredRequests.AddAsync(userIgnoredRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserIgnoredRequest?> UpdateCaptainUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest)
        {
            var oldUserIgnoredRequest = await _context.CaptainUserIgnoredRequests.FirstOrDefaultAsync(u => u.Id == userIgnoredRequest.Id);
            if (oldUserIgnoredRequest == null) return null;

            oldUserIgnoredRequest.CaptainUserAccountId = userIgnoredRequest.CaptainUserAccountId;
            oldUserIgnoredRequest.OrderId = userIgnoredRequest.OrderId;
            oldUserIgnoredRequest.AgentId = userIgnoredRequest.AgentId;
            oldUserIgnoredRequest.ModifiedBy = userIgnoredRequest.ModifiedBy;
            oldUserIgnoredRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserIgnoredRequest>(oldUserIgnoredRequest).State = EntityState.Modified;
            return userIgnoredRequest;
        }

        public async Task<CaptainUserIgnoredRequest?> DeleteCaptainUserIgnoredRequestAsync(long id)
        {
            var oldUserIgnoredRequest = await _context.CaptainUserIgnoredRequests.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserIgnoredRequest == null) return null;

            _context.CaptainUserIgnoredRequests.Remove(oldUserIgnoredRequest);
            return oldUserIgnoredRequest;
        }

		public async Task<IEnumerable<CaptainUserPayment>> GetCaptainUsersPaymentsAsync()
        {
            return await _context.CaptainUserPayments.ToListAsync();
        }
        
        public async Task<CaptainUserPayment?> GetCaptainUserPaymentByIdAsync(long id)
        {
            return await _context.CaptainUserPayments.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserPayment>> GetCaptainUsersPaymentsByAsync(Expression<Func<CaptainUserPayment, bool>> predicate)
        {
            return await _context.CaptainUserPayments.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserPayment> InsertCaptainUserPaymentAsync(CaptainUserPayment userPayment)
        {
            userPayment.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserPayments.AddAsync(userPayment);
            return inserResult.Entity;
        }

        public async Task<CaptainUserPayment?> UpdateCaptainUserPaymentAsync(CaptainUserPayment userPayment)
        {
            var oldUserPayment = await _context.CaptainUserPayments.FirstOrDefaultAsync(u => u.Id == userPayment.Id);
            if (oldUserPayment == null) return null;

            oldUserPayment.CaptainUserAccountId = userPayment.CaptainUserAccountId;
            oldUserPayment.OrderId = userPayment.OrderId;
            oldUserPayment.PaymentTypeId = userPayment.PaymentTypeId;
            oldUserPayment.SystemSettingId = userPayment.SystemSettingId;
            oldUserPayment.Value = userPayment.Value;
            oldUserPayment.PaymentStatusTypeId = userPayment.PaymentStatusTypeId;
            oldUserPayment.ModifiedBy = userPayment.ModifiedBy;
            oldUserPayment.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserPayment>(oldUserPayment).State = EntityState.Modified;


            CaptainUserPaymentHistory userPaymentHistory = new CaptainUserPaymentHistory()
            {
                CaptainUserAccountId = oldUserPayment.CaptainUserAccountId,
                OrderId = oldUserPayment.OrderId,
                PaymentTypeId = oldUserPayment.PaymentTypeId,
                PaymentStatusTypeId = oldUserPayment.PaymentStatusTypeId,
                SystemSettingId = oldUserPayment.SystemSettingId,
                Value = oldUserPayment.Value,
                CreationDate = DateTime.Now,
                CreatedBy = oldUserPayment.CreatedBy,
                ModifiedBy = oldUserPayment.ModifiedBy,
                ModificationDate = oldUserPayment.ModificationDate
            };
            var insertResult = await _context.CaptainUserPaymentHistories.AddAsync(userPaymentHistory);

            return oldUserPayment;
        }

        public async Task<CaptainUserPayment?> DeleteCaptainUserPaymentAsync(long id)
        {
            var oldUserPayment = await _context.CaptainUserPayments.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserPayment == null) return null;

            _context.CaptainUserPayments.Remove(oldUserPayment);
            return oldUserPayment;
        }

       /* public async Task<IEnumerable<CaptainUserRejectedRequest>> GetCaptainUsersRejectedRequestsAsync()
        {
            return await _context.CaptainUserRejectedRequests.ToListAsync();
        }

        public async Task<CaptainUserRejectedRequest?> GetCaptainUserRejectedRequestByIdAsync(long id)
        {
            return await _context.CaptainUserRejectedRequests.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserRejectedRequest>> GetCaptainUsersRejectedRequestsByAsync(Expression<Func<CaptainUserRejectedRequest, bool>> predicate)
        {
            return await _context.CaptainUserRejectedRequests.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserRejectedRequest> InsertCaptainUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest)
        {
            userRejectedRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserRejectedRequests.AddAsync(userRejectedRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserRejectedRequest?> UpdateCaptainUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest)
        {
            var oldUserRejectedRequest = await _context.CaptainUserRejectedRequests.FirstOrDefaultAsync(u => u.Id == userRejectedRequest.Id);
            if (oldUserRejectedRequest == null) return null;

            oldUserRejectedRequest.UserId = userRejectedRequest.UserId;
            oldUserRejectedRequest.OrderId = userRejectedRequest.OrderId;
            oldUserRejectedRequest.AgentId = userRejectedRequest.AgentId;
            oldUserRejectedRequest.ModifiedBy = userRejectedRequest.ModifiedBy;
            oldUserRejectedRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserRejectedRequest>(oldUserRejectedRequest).State = EntityState.Modified;
            return oldUserRejectedRequest;
        }

        public async Task<CaptainUserRejectedRequest?> DeleteCaptainUserRejectedRequestAsync(long id)
        {
            var oldUserRejectedRequest = await _context.CaptainUserRejectedRequests.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserRejectedRequest == null) return null;

            _context.CaptainUserRejectedRequests.Remove(oldUserRejectedRequest);
            return oldUserRejectedRequest;
        }

        public async Task<IEnumerable<CaptainUserRejectPenalty>> GetCaptainUsersRejectPenaltiesAsync()
        {
            return await _context.CaptainUserRejectPenalties.ToListAsync();
        }

        public async Task<CaptainUserRejectPenalty?> GetCaptainUserRejectPenaltyByIdAsync(long id)
        {
            return await _context.CaptainUserRejectPenalties.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserRejectPenalty>> GetCaptainUsersRejectPenaltiesByAsync(Expression<Func<CaptainUserRejectPenalty, bool>> predicate)
        {
            return await _context.CaptainUserRejectPenalties.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserRejectPenalty> InsertCaptainUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty)
        {
            userRejectPenalty.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserRejectPenalties.AddAsync(userRejectPenalty);
            return inserResult.Entity;
        }

        public async Task<CaptainUserRejectPenalty?> UpdateCaptainUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty)
        {
            var oldUserRejectPenalty = await _context.CaptainUserRejectPenalties.FirstOrDefaultAsync(u => u.Id == userRejectPenalty.Id);
            if (oldUserRejectPenalty == null) return null;

            oldUserRejectPenalty.UserId = userRejectPenalty.UserId;
            oldUserRejectPenalty.SystemSettingId = userRejectPenalty.SystemSettingId;
            oldUserRejectPenalty.PenaltyStatusTypeId = userRejectPenalty.PenaltyStatusTypeId;
            oldUserRejectPenalty.ModifiedBy = userRejectPenalty.ModifiedBy;
            oldUserRejectPenalty.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserRejectPenalty>(oldUserRejectPenalty).State = EntityState.Modified;
            return oldUserRejectPenalty;
        }

        public async Task<CaptainUserRejectPenalty?> DeleteCaptainUserRejectPenaltyAsync(long id)
        {
            var oldUserRejectPenalty = await _context.CaptainUserRejectPenalties.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserRejectPenalty == null) return null;

            _context.CaptainUserRejectPenalties.Remove(oldUserRejectPenalty);
            return oldUserRejectPenalty;
        }
*/
        public async Task<IEnumerable<CaptainUserShift>> GetCaptainUsersShiftsAsync()
        {
            return await _context.CaptainUserShifts.ToListAsync();
        }

        public async Task<CaptainUserShift?> GetCaptainUserShiftByIdAsync(long id)
        {
            return await _context.CaptainUserShifts.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserShift>> GetCaptainUsersShiftsByAsync(Expression<Func<CaptainUserShift, bool>> predicate)
        {
            return await _context.CaptainUserShifts.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserShift> InsertCaptainUserShiftAsync(CaptainUserShift userShift)
        {
            userShift.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserShifts.AddAsync(userShift);
            return inserResult.Entity;
        }

        public async Task<CaptainUserShift?> UpdateCaptainUserShiftAsync(CaptainUserShift userShift)
        {
            var oldUserShift = await _context.CaptainUserShifts.FirstOrDefaultAsync(u => u.Id == userShift.Id);
            if (oldUserShift == null) return null;

            oldUserShift.CaptainUserAccountId = userShift.CaptainUserAccountId;
            oldUserShift.EndHour = userShift.EndHour;
            oldUserShift.EndMinutes = userShift.EndMinutes;
            oldUserShift.StartHour = userShift.StartHour;
            oldUserShift.StartMinutes = userShift.StartMinutes;
            oldUserShift.ModifiedBy = userShift.ModifiedBy;
            oldUserShift.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserShift>(oldUserShift).State = EntityState.Modified;
            return oldUserShift;
        }

        public async Task<CaptainUserShift?> DeleteCaptainUserShiftAsync(long id)
        {
            var oldUserShift = await _context.CaptainUserShifts.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserShift == null) return null;

            _context.CaptainUserShifts.Remove(oldUserShift);
            return oldUserShift;
        }

        public async Task<IEnumerable<CaptainUserStatusHistory>> GetCaptainUsersStatusHistoriesAsync()
        {
            return await _context.CaptainUserStatusHistories.ToListAsync();
        }

        public async Task<CaptainUserStatusHistory?> GetCaptainUserStatusHistoryByIdAsync(long id)
        {
            return await _context.CaptainUserStatusHistories.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<CaptainUserStatusHistory>> GetCaptainUsersStatusHistoriesByAsync(Expression<Func<CaptainUserStatusHistory, bool>> predicate)
        {
            return await _context.CaptainUserStatusHistories.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserStatusHistory> InsertCaptainUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory)
        {
            userStatusHistory.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserStatusHistories.AddAsync(userStatusHistory);
            return inserResult.Entity;
        }

        public async Task<CaptainUserStatusHistory?> UpdateCaptainUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory)
        {
            var oldUserStatusHistory = await _context.CaptainUserStatusHistories.FirstOrDefaultAsync(u => u.Id == userStatusHistory.Id);
            if (oldUserStatusHistory == null) return null;

            oldUserStatusHistory.CaptainUserAccountId = userStatusHistory.CaptainUserAccountId;
            oldUserStatusHistory.StatusTypeId = userStatusHistory.StatusTypeId;
            oldUserStatusHistory.ModifiedBy = userStatusHistory.ModifiedBy;
            oldUserStatusHistory.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserStatusHistory>(oldUserStatusHistory).State = EntityState.Modified;
            return oldUserStatusHistory;
        }

        public async Task<CaptainUserStatusHistory?> DeleteCaptainUserStatusHistoryAsync(long id)
        {
            var oldUserStatusHistory = await _context.CaptainUserStatusHistories.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserStatusHistory == null) return null;

            _context.CaptainUserStatusHistories.Remove(oldUserStatusHistory);
            return oldUserStatusHistory;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(long id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<CaptainUserVehicle>> GetCaptainUsersVehiclesAsync()
        {
            return await _context.CaptainUserVehicles.ToListAsync();
        }

        public async Task<CaptainUserVehicle?> GetCaptainUserVehicleByIdAsync(long id)
        {
            return await _context.CaptainUserVehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<CaptainUserVehicle>> GetCaptainUsersVehiclesByAsync(Expression<Func<CaptainUserVehicle, bool>> predicate)
        {
            return await _context.CaptainUserVehicles.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserVehicle> InsertCaptainUserVehicleAsync(CaptainUserVehicle userVehicle)
        {
            userVehicle.CreationDate = DateTime.Now;
            var inserResult = await _context.CaptainUserVehicles.AddAsync(userVehicle);
            return inserResult.Entity;
        }

        public async Task<CaptainUserVehicle?> UpdateCaptainUserVehicleAsync(CaptainUserVehicle userVehicle)
        {
            var oldUserVehicle = await _context.CaptainUserVehicles.FirstOrDefaultAsync(u => u.Id == userVehicle.Id);
            if (oldUserVehicle == null) return null;

            oldUserVehicle.CaptainUserAccountId = userVehicle.CaptainUserAccountId;
            oldUserVehicle.VehicleId = userVehicle.VehicleId;
            oldUserVehicle.PlateNumber = userVehicle.PlateNumber;
            oldUserVehicle.Model = userVehicle.Model;
            oldUserVehicle.VehicleImage = userVehicle.VehicleImage;
            oldUserVehicle.LicenseImage = userVehicle.LicenseImage;
            oldUserVehicle.LicenseNumber = userVehicle.LicenseNumber;
            oldUserVehicle.IsActive = userVehicle.IsActive;
            oldUserVehicle.IsDeleted = userVehicle.IsDeleted;
            oldUserVehicle.ModifiedBy = userVehicle.ModifiedBy;
            oldUserVehicle.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserVehicle>(oldUserVehicle).State = EntityState.Modified;
            return oldUserVehicle;
        }

        public async Task<CaptainUserVehicle?> DeleteCaptainUserVehicleAsync(long id)
        {
            var oldUserVehicle = await _context.CaptainUserVehicles.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserVehicle == null) return null;

            oldUserVehicle.IsDeleted = true;
            _context.Entry<CaptainUserVehicle>(oldUserVehicle).State = EntityState.Modified;
            return oldUserVehicle;
        }


        public async Task<CaptainUserAccount?> GetCaptainUserAccountNearestLocationAsync(string pickupLatitude, string pickupLongitude)
        {

            //var agentCoordinate = new GeoCoordinate(double.Parse(PickupLatitude), double.Parse(PickupLongitude));
            //var driversIgnoredRequests = await _context.UserIgnoredRequests.Where(u => u.OrderId == OrderID).Select(u => u.UserId).ToListAsync();
            //var driversRejectedRequests = await _context.UserRejectedRequests.Where(u => u.OrderId == OrderID).Select(u => u.UserId).ToListAsync();

            //driversIgnoredRequests.AddRange(driversRejectedRequests);


            var users = await _context.CaptainUserAccounts.FromSqlRaw("SelectNearestCaptain '" + pickupLatitude + "','" + pickupLongitude + "'").ToListAsync();
            if (users?.Count <= 0) return null;

            var captain = users?.FirstOrDefault();
            return captain;
        }


            /*public async Task<CaptainUser> GetUserNearestLocationAsync( string pickupLatitude, string pickupLongitude)
        {

            //var agentCoordinate = new GeoCoordinate(double.Parse(PickupLatitude), double.Parse(PickupLongitude));
            //var driversIgnoredRequests = await _context.UserIgnoredRequests.Where(u => u.OrderId == OrderID).Select(u => u.UserId).ToListAsync();
            //var driversRejectedRequests = await _context.UserRejectedRequests.Where(u => u.OrderId == OrderID).Select(u => u.UserId).ToListAsync();

            //driversIgnoredRequests.AddRange(driversRejectedRequests);


            var users = await _context.Users.FromSqlRaw("SelectNearestCaptain '" + pickupLatitude + "','" + pickupLongitude + "'").ToListAsync();
            if (users == null || users.Count <= 0) return null;

            var captain = users.FirstOrDefault();
            return captain;

            var driversIgnoredPenalty = await _context.UserIgnoredPenalties.Where(u => u.PenaltyStatusTypeId == (long)PenaltyStatusTypes.New).Select(u => u.UserId).ToListAsync();
            var driversRejectedPenalty = await _context.UserRejectPenalties.Where(u => u.PenaltyStatusTypeId == (long)PenaltyStatusTypes.New).Select(u => u.UserId).ToListAsync();

            driversRejectedPenalty.AddRange(driversIgnoredPenalty);



            var RunningOrdersIds = await _context.RunningOrders.Select(r => r.UserId).ToListAsync();

            //  Geo nearestLocation = new Geo();
            var nearestLocation = new DistanceDetails();
            if (driversRejectedPenalty != null && driversRejectedPenalty.Count > 0)
            {



                //nearestLocation = (from userLocation in _context.UserCurrentLocations
                //                   join runningOrder in _context.RunningOrders
                //                   on userLocation.UserId equals runningOrder.UserId into table
                //                   where !table.Select(s => s.UserId).Contains(userLocation.UserId)
                //                    && !driversIgnoredRequests.Contains(userLocation.UserId)
                //                   select new Geo((long)userLocation.UserId, userLocation.Lat, userLocation.Long))
                //                   .OrderBy(g => g.GetDistanceTo(agentCoordinate)).
                //                   FirstOrDefault();

                //nearestLocation = _context.UserCurrentLocations.Select(u => new Geo((long)u.UserId, u.Lat, u.Long))
                //   .Where(u => !RunningOrdersIds.Contains(u.UserID) && !driversIgnoredRequests.Contains(u.UserID) )
                //   .OrderBy(g => g.GetDistanceTo(agentCoordinate))
                //   .FirstOrDefault();
                nearestLocation = _context.UserCurrentLocations.Select(u => new DistanceDetails { UserId = u.UserId, UserLat = u.Lat,UserLong =  u.Long,
                    PickedLat = pickupLatitude, PickedLong = pickupLongitude })
                  .Where(u => !RunningOrdersIds.Contains(u.UserId) && !driversRejectedPenalty.Contains(u.UserId))
                  .OrderBy(g => g.CalculateDistance())
                  .FirstOrDefault();

                //nearestLocation = _context.UserCurrentLocations.Select(u => new Geo((long)u.UserId, u.Lat, u.Long))
                //   .Where(u => !driversIgnoredRequests.Contains(u.UserID))
                //   .OrderBy(g => g.GetDistanceTo(agentCoordinate))
                //   .First();
            }
            else
            {

                

                //nearestLocation = (from userLocation in _context.UserCurrentLocations
                //                   join runningOrder in _context.RunningOrders
                //                   on userLocation.UserId equals runningOrder.UserId into table
                //                   where !table.Select(s => s.UserId).Contains(userLocation.UserId)
                //                   select new Geo((long)userLocation.UserId, userLocation.Lat, userLocation.Long))
                //                   .OrderBy(g => g.GetDistanceTo(agentCoordinate)).
                //                   FirstOrDefault();

                nearestLocation = _context.UserCurrentLocations.ToList().Select(u => new DistanceDetails
                {
                    UserId = u.UserId,
                    UserLat = u.Lat,
                    UserLong = u.Long,
                    PickedLat = pickupLatitude,
                    PickedLong = pickupLongitude
                })
                    .Where( u => !RunningOrdersIds.Contains(u.UserId))
                    .OrderBy(g => g.CalculateDistance())
                    .FirstOrDefault();


                //nearestLocation = _context.UserCurrentLocations.ToList().Select(u => new Geo((long)u.UserId, u.Lat, u.Long))
                //   .OrderBy(g => g.GetDistanceTo(agentCoordinate))
                //   .First();
            }


            if (nearestLocation == null || nearestLocation.UserId == 0) return null;//throw new NotFoundException();

            var driver = await _context.Users.FirstOrDefaultAsync(u => u.Id == nearestLocation.UserId);
            return driver;


            //var DriversIgnored = await _unitOfWork.UserRepository.GetUserIgnoredRequestBy(u => u.OrderId == order.Id);
            //var list = DriversIgnored.Select(u => u.Id).ToList();
            //var agentCoordinate = new Geo(0, order.PickupLocationLat, order.PickupLocationLong);

            //var drivers = await _unitOfWork.UserRepository.GetAllUsersCurrentLocations();
            //var nearst = drivers.Select(u => new Geo((long)u.UserId, u.Lat, u.Long)).Where(u => !list.Contains(u.UserID)).OrderBy(g => g.GetDistanceTo(agentCoordinate)).First();

        }
*/
        
        
        
        public async Task<IEnumerable<NearCaptainUser>> GetCaptainsUsersNearToLocationAsync(string pickupLatitude, string pickupLongitude)
        {
            List<NearCaptainUser> result = new (); 
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();
            var command = conn.CreateCommand();
            string query = "GetNearCaptains '" + pickupLatitude + "','" + pickupLongitude + "'";
            command.CommandText = query;
            using var reader = await command.ExecuteReaderAsync();
            while ( reader.Read())
            {
                var id = reader.GetString(reader.GetOrdinal("ID"));
                var firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                var familyName = reader.GetString(reader.GetOrdinal("FamilyName"));
                var lat = reader.GetDouble(reader.GetOrdinal("lat"));
                var lng = reader.GetDouble(reader.GetOrdinal("long"));

                NearCaptainUser user = new(
                
                    Id : id,
                    FirstName : firstName,
                    LastName : familyName,
                    Lat :lat.ToString(),
                    Long : lng.ToString()
                );

                result.Add(user);

                
            }

            //reader.Close();
            return result;

            //var users = await _context.Users.FromSqlRaw("GetNearCaptains '" + PickupLatitude + "','" + PickupLongitude + "'").ToListAsync();
            //if (users == null || users.Count <= 0) return null;

            //return users;
        }
        
        
        public async Task<IEnumerable<CaptainUserMessageHub>> GetCaptainUsersMessageHubsAsync()
        {
            return await _context.CaptainUserMessageHubs.ToListAsync();
        }

        public async Task<CaptainUserMessageHub?> GetCaptainUserMessageHubByIdAsync(long id)
        {
            return await _context.CaptainUserMessageHubs.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<CaptainUserMessageHub>> GetCaptainUsersMessageHubsByAsync(Expression<Func<CaptainUserMessageHub, bool>> predicate)
        {
            return await _context.CaptainUserMessageHubs.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserMessageHub> InsertCaptainUserMessageHubByCaptainUserAccountIdAsync(string id, string connectionId)
        {


            var oldUserHub = await _context.CaptainUserMessageHubs.FirstOrDefaultAsync(h => h.CaptainUserAccountId == id);
            if (oldUserHub != null && oldUserHub.Id > 0)
            {
                oldUserHub.ConnectionId = connectionId;
                oldUserHub.ModificationDate = DateTime.Now;
                _context.Entry<CaptainUserMessageHub>(oldUserHub).State = EntityState.Modified;
                return oldUserHub;
            }
            else 
            {
                CaptainUserMessageHub newHub = new CaptainUserMessageHub() { CaptainUserAccountId = id, ConnectionId = connectionId, CreationDate = DateTime.Now};
                var insertResult = await _context.CaptainUserMessageHubs.AddAsync(newHub);
                return insertResult.Entity;
            }

            
        }

        public async Task<CaptainUserMessageHub?> UpdateCaptainUserMessageHubByCaptainUserAccountIdAsync(string id, string connectionId)
        {
            var oldUserHub = await _context.CaptainUserMessageHubs.FirstOrDefaultAsync(h => h.CaptainUserAccountId == id);
            if (oldUserHub == null) return null;


            oldUserHub.ConnectionId = connectionId;
            oldUserHub.ModificationDate = DateTime.Now;
            _context.Entry<CaptainUserMessageHub>(oldUserHub).State = EntityState.Modified;
            return oldUserHub;
        }

        public async Task<CaptainUserNewRequest?> DeleteCaptainUserNewRequestByOrderIdAsync(long id)
        {
            var oldUserNewRequests = await _context.CaptainUserNewRequests.FirstOrDefaultAsync(u => u.OrderId == id);
            if (oldUserNewRequests == null) return null;

            _context.CaptainUserNewRequests.Remove(oldUserNewRequests);
            return oldUserNewRequests;
        }

        public async Task<CaptainUserPayment?> DeleteCaptainUserPaymentByOrderIdAsync(long id)
        {
            var oldUserPayment = await _context.CaptainUserPayments.FirstOrDefaultAsync(u => u.OrderId == id);
            if (oldUserPayment == null) return null;

            _context.CaptainUserPayments.Remove(oldUserPayment);
            return oldUserPayment;
        }
    /*public IQueryable<CaptainUserRejectedRequest> GetCaptainUserRejectedRequestByQuerable(Expression<Func<CaptainUserRejectedRequest, bool>> predicate)
    {
        var result = _context.CaptainUserRejectedRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts); 

        return result;
    }*/

    public IQueryable<CaptainUserAcceptedRequest> GetCaptainUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
    {
        var result = _context.CaptainUserAcceptedRequests
            //.Include(u => u.User).ThenInclude(c => c.UserAccounts)
            //.Include(o => o.Order).ThenInclude(o => o.PaymentType).Include(o => o.User)
            //.Include(o => o.Order).ThenInclude(o => o.Agent).Include(u => u.User).ThenInclude(c => c.City).Include(u => u.User).ThenInclude(c => c.Country)
            //.Include(o => o.Order).ThenInclude(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
            //.ThenInclude(u => u.User).ThenInclude(c => c.City)
            //.Include(o => o.Order).ThenInclude(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
            //.ThenInclude(u => u.User).ThenInclude(c => c.Country)

            .Where(predicate);

        return result;

    }
    public IQueryable<CaptainUserIgnoredRequest> GetCaptainUserIgnoredRequestByQuerable(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate)
        {
            var result = _context.CaptainUserIgnoredRequests.Where(predicate); 
           
            return result;

        }

		/*public IQueryable<CaptainUser> GetByQuerable()
		{
            return _context.CaptainUserAccounts.Include(u => u.User)
                .ThenInclude(u => u.City).Include(u => u.User).ThenInclude(u => u.Country)
                .Include(u => u.User).ThenInclude(u => u.UserAccounts).Select(c => c.User);

        }*/
       

		/*public IEnumerable<CaptainUser> GetCaptainUserByStatusType(long? statusTypeId, IEnumerable<CaptainUser> query)
		{
            if(statusTypeId != null)
			{
                var restult = _context.CaptainUserAccounts.Where(u => u.StatusTypeId == statusTypeId)
                    .Include(u => u.User).ThenInclude(u=> u.UserCurrentStatus).Include(u => u.User).ThenInclude(u => u.Country).Include(u =>u.User).ThenInclude(u => u.City).Select(c => c.User)
                    .ToList();
                return restult;
			}
            return query;
		}*/
    /*    public IQueryable<CaptainUser> GetByStatusQuerableS(long? statusTypeId)
        {
            return _context.UserAccounts.Where(u => u.StatusTypeId == statusTypeId).Include(u => u.User)
                .ThenInclude(u => u.City).Include(u => u.User).ThenInclude(u => u.Country)
                .Include(u => u.User).ThenInclude(u => u.UserAccounts).Select(c => c.User);
            
        }

*/

		public async Task<IEnumerable<CaptainUserActivity>> GetCaptainUsersActivitiesAsync()
		{
			return await _context.CaptainUserActivities.ToListAsync();
		}

		public async Task<CaptainUserActivity?> GetCaptainUserActivityByIdAsync(long id)
		{
			return await _context.CaptainUserActivities.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<IEnumerable<CaptainUserActivity>> GetCaptainUsersActivitiesByAsync(Expression<Func<CaptainUserActivity, bool>> predicate)
		{
			return await _context.CaptainUserActivities.Where(predicate).ToListAsync();
		}

		public async Task<CaptainUserActivity> InsertCaptainUserActivityAsync(CaptainUserActivity userActivity)
		{
			var oldUserActivity = await _context.CaptainUserActivities.FirstOrDefaultAsync(u => u.CaptainUserAccountId == userActivity.CaptainUserAccountId && u.IsCurrent == true);
			if (oldUserActivity != null && oldUserActivity.Id > 0)
			{

				oldUserActivity.IsCurrent = false;
				oldUserActivity.ModificationDate = DateTime.Now;
				_context.Entry<CaptainUserActivity>(oldUserActivity).State = EntityState.Modified;

			}

			userActivity.IsCurrent = true;
			userActivity.CreationDate = DateTime.Now;
			var insert_result = await _context.CaptainUserActivities.AddAsync(userActivity);
			return insert_result.Entity;
		}



        public async Task<CaptainUserActivity?> UpdateCaptainUserActivityAsync(CaptainUserActivity userActivity)
        {
            var oldUserActivity = await _context.CaptainUserActivities.FirstOrDefaultAsync(u => u.Id == userActivity.Id);
            if (oldUserActivity == null) return null;

            oldUserActivity.IsCurrent = false;
            oldUserActivity.ModificationDate = DateTime.Now;
            _context.Entry<CaptainUserActivity>(oldUserActivity).State = EntityState.Modified;
            return oldUserActivity;


        }

		public async Task<CaptainUserActivity?> DeleteCaptainUserActivityAsync(long id)
		{
            var oldUserActivity = await _context.CaptainUserActivities.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserActivity == null) return null;

            
            _context.CaptainUserActivities.Remove(oldUserActivity);
            return oldUserActivity;
        }

		public object UserReportCount()
		{
            var totalUsers = _context.CaptainUsers.Count();
            
            var newUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.New).Count();
            var readyUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Ready).Count();
            var workingUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Working).Count();
            var progressUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Progress).Count();
            var suspendedUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Suspended).Count();
            var stoppedUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Stopped).Count();
            var reviewingUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Reviewing).Count();
            var penaltyUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Penalty).Count();
            var incompleteUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Incomplete).Count();
            var completeUsers = _context.CaptainUserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Complete).Count();
            return new
            {
                UsersCount = totalUsers,
                NewCount = newUsers,
                ReadyCount = readyUsers,
                WorkingCount = workingUsers,
                ProgressCount = progressUsers,
                SuspendedCount = suspendedUsers,
                StoppedCount = stoppedUsers,
                ReviewingCount = reviewingUsers,
                PenaltyCount = penaltyUsers,
                InCompleteCount = incompleteUsers,
                CompleteCount = completeUsers

            };
        }

		public async Task<Bonus?> GetBonusByCountryAsync(long? countryId)
		{
            var bonus = await _context.Bonuses.FirstOrDefaultAsync(b => b.CountryId == countryId);
            return bonus;
		}
        public async Task<CaptainUserBonus> InsertCaptainUserBonusAsync(CaptainUserBonus userBonus)
		{
            var result = await _context.CaptainUserBonuses.AddAsync(userBonus);
            return result.Entity;
		}

		/*public IQueryable<CaptainUserRejectedRequest> GetAllRejectedRequestByQuerable()
		{
            var result = _context.CaptainUserRejectedRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts);

            return result;
        }*/
       public async Task<IEnumerable<OrderQrcode>> GetOrderQRCodeByAsync(Expression<Func<OrderQrcode, bool>> predicate)
		{
            return await _context.OrderQrcodes.Where(predicate).ToListAsync();
           
		}

       
        public async Task<IEnumerable<CaptainUserAcceptedRequest>> CaptainUserAcceptedRequestsAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            return await _context.CaptainUserAcceptedRequests.Where(predicate).ToListAsync();

        }

       /* public IQueryable<CaptainUserAcceptedRequest> GetAllAcceptedRequestByQuerable()
		{
            var result = _context.UserAcceptedRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts)
                .Include(o => o.Order).ThenInclude(o => o.PaymentType).Include(o => o.User)
                .Include(o => o.Order).ThenInclude(o => o.Agent).Include(u => u.User).ThenInclude(c => c.City).Include(u => u.User).ThenInclude(c => c.Country)
                .Include(o => o.Order).ThenInclude(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
                .ThenInclude(u => u.User).ThenInclude(c => c.City)
                .Include(o => o.Order).ThenInclude(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
                .ThenInclude(u => u.User).ThenInclude(c => c.Country)
                 
                ;

            return result;
        }*/
      
        /*public IQueryable<CaptainUserIgnoredRequest> GetAllIgnoredRequestByQuerable()
		{
            var result = _context.UserIgnoredRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts);//.Include(o => o.Order);

            return result;
        }*/

        public async Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUserAcceptedRequestAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            return await _context.CaptainUserAcceptedRequests.Where(predicate).ToListAsync();
        }


        public async Task<IEnumerable<CaptainUserAccount>> GetActiveCaptainUsersAccountsPaginationAsync(int skip, int take)
        {
            return await _context.CaptainUserAccounts
                .Where(u => u.StatusTypeId != (long)StatusTypes.Reviewing)
                .OrderByDescending(u => u.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }


        public async Task<IEnumerable<CaptainUserAccount>> GetReviewingCaptainUsersAccountsPaginationAsync(int skip, int take)
        {
            return await _context.CaptainUserAccounts
                .Where(u => u.StatusTypeId == (long)StatusTypes.Reviewing)
                .OrderByDescending(u => u.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }


    }

