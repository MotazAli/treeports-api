using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TreePorts.DTO;
using TreePorts.Interfaces.Repositories;
using TreePorts.Models;

namespace TreePorts.Repositories;

    public class CaptainRepository : ICaptainRepository
    {

        private TreePortsDBContext _context;

        public CaptainRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public  async Task<CaptainUser> DeleteUserAsync(long id)
        {
            var oldUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUser == null) return null;

            _context.Users.Remove(oldUser);
            return oldUser;
        }

        public async Task<List<CaptainUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        

        public async Task<List<CaptainUser>> GetUsersByAsync(Expression<Func<CaptainUser, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUser> GetUserByIdAsync(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<CaptainUser> InsertUserAsync(CaptainUser user)
        {
            user.CreationDate = DateTime.Now;
            var userAccount = user.UserAccounts.FirstOrDefault();
            if (userAccount != null)
                userAccount.CreationDate = user.CreationDate;
            
            var result = await _context.Users.AddAsync(user);
            return result.Entity;
        }

        public async Task<CaptainUser> UpdateUserAsync(CaptainUser user)
        {
            var oldUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (oldUser == null) return null;

            oldUser.FirstName = user.FirstName ?? oldUser.FirstName;
            oldUser.FamilyName = user.FamilyName ?? oldUser.FamilyName;
            oldUser.StcPay = user.StcPay ?? oldUser.StcPay;
            oldUser.VehiclePlateNumber = user.VehiclePlateNumber ?? oldUser.VehiclePlateNumber;
            oldUser.NbsherNationalNumberImage = user.NbsherNationalNumberImage ?? oldUser.NbsherNationalNumberImage;
            oldUser.NationalNumberExpDate = user.NationalNumberExpDate ?? oldUser.NationalNumberExpDate;
            oldUser.BirthDate = user.BirthDate ?? oldUser.BirthDate;

            oldUser.NationalNumber = user.NationalNumber ?? oldUser.NationalNumber;
            oldUser.CountryId = user.CountryId ?? oldUser.CountryId;
            oldUser.CityId = user.CityId ?? oldUser.CityId;
          
            oldUser.Gender = user.Gender ?? oldUser.Gender;
            oldUser.BirthDay = user.BirthDay ?? oldUser.BirthDay;
            oldUser.BirthMonth = user.BirthMonth ?? oldUser.BirthMonth;
            oldUser.BirthYear = user.BirthYear ?? oldUser.BirthYear;
            oldUser.Mobile = user.Mobile ?? oldUser.Mobile;
           
            oldUser.ResidenceExpireDay = user.ResidenceExpireDay ?? oldUser.ResidenceExpireDay;
            oldUser.ResidenceExpireMonth = user.ResidenceExpireMonth ?? oldUser.ResidenceExpireMonth;
            oldUser.ResidenceExpireYear = user.ResidenceExpireYear ?? oldUser.ResidenceExpireYear;
            oldUser.PersonalImageName = user.PersonalImageName ?? oldUser.PersonalImageName;
            oldUser.PersonalImageAndroidPath = user.PersonalImageAndroidPath ?? oldUser.PersonalImageAndroidPath;
            oldUser.NationalNumberImageName = user.NationalNumberImageName ?? oldUser.NationalNumberImageName;
            oldUser.NationalNumberImageAndroidPath = user.NationalNumberImageAndroidPath ?? oldUser.NationalNumberImageAndroidPath;
            oldUser.ResidenceCountryId = user.ResidenceCountryId ?? oldUser.ResidenceCountryId;
            oldUser.ResidenceCityId = user.ResidenceCityId ?? oldUser.ResidenceCityId;
            oldUser.ModifiedBy = user.ModifiedBy ?? oldUser.ModifiedBy;
            oldUser.ModificationDate = DateTime.Now;

            if (user.UserVehicles != null && user.UserVehicles.Count > 0)
            {
                oldUser.UserVehicles.Clear();
                oldUser.UserVehicles.Add(user.UserVehicles.FirstOrDefault());
            }

            _context.Entry<CaptainUser>(oldUser).State = EntityState.Modified;

            return oldUser;

        }


        public async Task<CaptainUserAccount> DeleteUserAccountAsync(long id)
        {
            var account = await _context.UserAccounts.FirstOrDefaultAsync(a => a.Id == id);
            account.ModificationDate = DateTime.Now;
            account.IsDeleted = true;
            _context.Entry<CaptainUserAccount>(account).State = EntityState.Modified;
            return account;
        }

        public async Task<List<CaptainUserAccount>> GetUsersAccountsAsync()
        {
            return await _context.UserAccounts.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<List<CaptainUserAccount>> GetUsersAccountsByAsync(Expression<Func<CaptainUserAccount, bool>> predicate)
        {
            return await _context.UserAccounts.Where(predicate).Include(u => u.User).ToListAsync();
        }

        public async Task<CaptainUserAccount> GetUserAccountByIdAsync(long id)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<CaptainUserAccount> InsertUserAccountAsync(CaptainUserAccount account)
        {
            account.CreationDate = DateTime.Now;
            var result = await _context.UserAccounts.AddAsync(account);
            return result.Entity;
        }

        public async Task<CaptainUserAccount> UpdateUserAccountAsync(CaptainUserAccount account)
        {
            var oldAccount = await _context.UserAccounts.FirstOrDefaultAsync(a => a.Id == account.Id);
            if (oldAccount == null) return null;


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


        public IQueryable<CaptainUserAccount> GetUserAccountByQuerable(Expression<Func<CaptainUserAccount, bool>> predicate)
        {
            return _context.UserAccounts.Where(predicate).Include(u => u.User);
        }

        public IQueryable<CaptainUserAccount> GetUserAccountByQuerable()
        {
            return _context.UserAccounts.Include(u => u.User);
        }


        public async Task<List<CaptainUserNewRequest>> GetUsersNewRequestsAsync()
        {
            return await _context.UserNewRequests.ToListAsync();
        }

        public async Task<CaptainUserNewRequest> GetUserNewRequestByIdAsync(long id)
        {
            return await _context.UserNewRequests.FirstOrDefaultAsync( u => u.Id == id );
        }

        public async Task<List<CaptainUserNewRequest>> GetUsersNewRequestsByAsync(Expression<Func<CaptainUserNewRequest, bool>> predicate)
        {
            return await _context.UserNewRequests.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserNewRequest> InsertUserNewRequestAsync(CaptainUserNewRequest userRequest)
        {
            userRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.UserNewRequests.AddAsync(userRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserNewRequest> UpdateUserNewRequestAsync(CaptainUserNewRequest userRequest)
        {
            var oldUserNewRequest = await _context.UserNewRequests.FirstOrDefaultAsync(u => u.Id == userRequest.Id);
            if (oldUserNewRequest == null) return null;

            oldUserNewRequest.OrderId = userRequest.OrderId;
            oldUserNewRequest.UserId = userRequest.UserId;
            oldUserNewRequest.AgentId = userRequest.AgentId;
            oldUserNewRequest.ModifiedBy = userRequest.ModifiedBy;
            oldUserNewRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserNewRequest>(oldUserNewRequest).State = EntityState.Modified;
            return oldUserNewRequest;

        }

        public async Task<CaptainUserNewRequest> DeleteUserNewRequestAsync(long id)
        {
            var oldUserNewRequest = await _context.UserNewRequests.FirstOrDefaultAsync(u => u.Id == id);
            _context.UserNewRequests.Remove(oldUserNewRequest);
            return oldUserNewRequest;
        }


        public async Task<List<CaptainUserNewRequest>> DeleteUserNewRequestByUserIdAsync(long id)
        {

            var oldUserNewRequests = await _context.UserNewRequests.Where(u => u.UserId == id).ToListAsync();
            if (oldUserNewRequests is null || oldUserNewRequests.Count == 0) return null;

            _context.UserNewRequests.RemoveRange(oldUserNewRequests);
            return oldUserNewRequests;
        }


        public async Task<List<CaptainUserAcceptedRequest>> GetUsersAcceptedRequestsAsync()
        {
            return await _context.UserAcceptedRequests.ToListAsync();
        }

        public async Task<CaptainUserAcceptedRequest> GetUserAcceptedRequestByIdAsync(long id)
        {
            return await _context.UserAcceptedRequests.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserAcceptedRequest>> GetUsersAcceptedRequestsByAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            return await _context.UserAcceptedRequests.Include(u=>u.User).Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserAcceptedRequest> InsertUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest)
        {
            userAcceptedRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.UserAcceptedRequests.AddAsync(userAcceptedRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserAcceptedRequest> UpdateUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest)
        {
            var oldUserAcceptedRequest = await _context.UserAcceptedRequests.FirstOrDefaultAsync(u => u.Id == userAcceptedRequest.Id);
            if (oldUserAcceptedRequest == null) return null;

            oldUserAcceptedRequest.OrderId = userAcceptedRequest.OrderId;
            oldUserAcceptedRequest.UserId = userAcceptedRequest.UserId;
            oldUserAcceptedRequest.ModifiedBy = userAcceptedRequest.ModifiedBy;
            oldUserAcceptedRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserAcceptedRequest>(oldUserAcceptedRequest).State = EntityState.Modified;
            return oldUserAcceptedRequest;
        }

        public async Task<CaptainUserAcceptedRequest> DeleteUserAcceptedRequestAsync(long id)
        {

            var oldUserAcceptedRequest = await _context.UserAcceptedRequests.FirstOrDefaultAsync(u => u.Id == id);
            _context.UserAcceptedRequests.Remove(oldUserAcceptedRequest);
            return oldUserAcceptedRequest;
        }

		public async Task<List<BoxType>> GetBoxTypesAsync()
        {
            return await _context.BoxTypes.ToListAsync();
        }

        public async Task<BoxType> GetBoxTypeByIdAsync(long id)
        {
            return await _context.BoxTypes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserBox>> GetUsersBoxesAsync()
        {
            return await _context.UserBoxs.ToListAsync();
        }

        public async Task<CaptainUserBox> GetUserBoxById(long id)
        {
            return await _context.UserBoxs.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserBox>> GetUsersBoxesByAsync(Expression<Func<CaptainUserBox, bool>> predicate)
        {
            return await _context.UserBoxs.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserBox> InsertUserBoxAsync(CaptainUserBox userBox)
        {
            userBox.CreationDate = DateTime.Now;
            var inserResult = await _context.UserBoxs.AddAsync(userBox);
            return inserResult.Entity;
        }

        public async Task<CaptainUserBox> UpdateUserBoxAsync(CaptainUserBox userBox)
        {
            var oldUserBox = await _context.UserBoxs.FirstOrDefaultAsync(u => u.Id == userBox.Id);
            if (oldUserBox == null) return null;

            oldUserBox.UserVehicleId = userBox.UserVehicleId;
            oldUserBox.BoxTypeId = userBox.BoxTypeId;
            oldUserBox.IsDeleted = userBox.IsDeleted;
            oldUserBox.ModifiedBy = userBox.ModifiedBy;
            oldUserBox.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserBox>(oldUserBox).State = EntityState.Modified;
            return oldUserBox;
        }

        public async Task<CaptainUserBox> DeleteUserBoxAsync(long id)
        {

            var oldUserBox = await _context.UserBoxs.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserBox == null) return null;

            oldUserBox.IsDeleted = true;
            _context.Entry<CaptainUserBox>(oldUserBox).State = EntityState.Modified;
            return oldUserBox;
        }

		public async Task<List<CaptainUserCurrentBalance>> GetUsersCurrentBalancesAsync()
        {
            return await _context.UserCurrentBalances.ToListAsync();
        }

        public async Task<CaptainUserCurrentBalance> GetUserCurrentBalanceByIdAsync(long id)
        {
            return await _context.UserCurrentBalances.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserCurrentBalance>> GetUsersCurrentBalancesByAsync(Expression<Func<CaptainUserCurrentBalance, bool>> predicate)
        {
            return await _context.UserCurrentBalances.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserCurrentBalance> InsertUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance)
        {
            userCurrentBalance.CreationDate = DateTime.Now;
            var inserResult = await _context.UserCurrentBalances.AddAsync(userCurrentBalance);
            return inserResult.Entity;
        }

        public async Task<CaptainUserCurrentBalance> UpdateUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance)
        {
            var oldUserCurrentBalance = await _context.UserCurrentBalances.FirstOrDefaultAsync(u => u.Id == userCurrentBalance.Id);
            if (oldUserCurrentBalance == null) return null;

            oldUserCurrentBalance.UserPaymentId = userCurrentBalance.UserPaymentId;
            oldUserCurrentBalance.PaymentStatusTypeId = userCurrentBalance.PaymentStatusTypeId;
            oldUserCurrentBalance.ModifiedBy = userCurrentBalance.ModifiedBy;
            oldUserCurrentBalance.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserCurrentBalance>(oldUserCurrentBalance).State = EntityState.Modified;
            return userCurrentBalance;
        }

        public async Task<CaptainUserCurrentBalance> DeleteUserCurrentBalanceAsync(long id)
        {
            var oldUserCurrentBalance = await _context.UserCurrentBalances.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserCurrentBalance == null) return null;

            _context.UserCurrentBalances.Remove(oldUserCurrentBalance);
            return oldUserCurrentBalance;
        }

        public async Task<List<CaptainUserCurrentLocation>> GetUsersCurrentLocationsAsync()
        {
            return await _context.UserCurrentLocations.ToListAsync();
        }

        public async Task<CaptainUserCurrentLocation> GetUserCurrentLocationByIdAsync(long id)
        {
            return await _context.UserCurrentLocations.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserCurrentLocation>> GetUsersCurrentLocationsByAsync(Expression<Func<CaptainUserCurrentLocation, bool>> predicate)
        {
            return await _context.UserCurrentLocations.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserCurrentLocation> InsertUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation)
        {

            var oldUserCurrentLocations = await _context.UserCurrentLocations.Where(l => l.UserId == userCurrentLocation.UserId).ToListAsync();
            if (oldUserCurrentLocations != null && oldUserCurrentLocations.Count > 0)
            {
                var oldUserCurrentLocation = oldUserCurrentLocations[0];
                if (oldUserCurrentLocations.Count > 1)
                {
                    var removedLocation = oldUserCurrentLocations.Where(l => l.ModificationDate == null);
                    _context.UserCurrentLocations.RemoveRange(removedLocation);
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
            var inserResult = await _context.UserCurrentLocations.AddAsync(userCurrentLocation);
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

        public async Task<CaptainUserCurrentLocation> UpdateUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation)
        {
            var oldUserCurrentLocation = await _context.UserCurrentLocations.FirstOrDefaultAsync(u => u.Id == userCurrentLocation.Id);
            if (oldUserCurrentLocation == null) return null;

            oldUserCurrentLocation.UserId = userCurrentLocation.UserId;
            oldUserCurrentLocation.Lat = userCurrentLocation.Lat;
            oldUserCurrentLocation.Long = userCurrentLocation.Long;
            oldUserCurrentLocation.ModifiedBy = userCurrentLocation.ModifiedBy;
            oldUserCurrentLocation.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserCurrentLocation>(oldUserCurrentLocation).State = EntityState.Modified;
            return oldUserCurrentLocation;
        }

        public async Task<CaptainUserCurrentLocation> DeleteUserCurrentLocationAsync(long id)
        {

            var oldUserCurrentLocation = await _context.UserCurrentLocations.Where(u => u.UserId == id).ToListAsync();
            if (oldUserCurrentLocation == null || oldUserCurrentLocation.Count() <= 0) return null;

            _context.UserCurrentLocations.RemoveRange(oldUserCurrentLocation);
            return oldUserCurrentLocation[0];

            /* var oldUserCurrentLocation = await _context.UserCurrentLocations.FirstOrDefaultAsync(u => u.Id == id);
             if (oldUserCurrentLocation == null || oldUserCurrentLocation?.Id <= 0) return null;

             _context.UserCurrentLocations.Remove(oldUserCurrentLocation);
             return oldUserCurrentLocation;*/
        }

        public async Task<List<CaptainUserCurrentStatus>> GetUsersCurrentStatusesAsync()
        {
            return await _context.UserCurrentStatuses.ToListAsync();
        }

        public async Task<CaptainUserCurrentStatus> GetUserCurrentStatusByIdAsync(long id)
        {
            return await _context.UserCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserCurrentStatus>> GetUsersCurrentStatusesByAsync(Expression<Func<CaptainUserCurrentStatus, bool>> predicate)
        {
            return await _context.UserCurrentStatuses.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserCurrentStatus> InsertUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus)
        {
            var currentDate = DateTime.Now;

            var oldUserCurrentStatus = await _context.UserCurrentStatuses.FirstOrDefaultAsync(u => u.UserId == userCurrentStatus.UserId);
            if (oldUserCurrentStatus != null && oldUserCurrentStatus.Id > 0)
            {
                CaptainUserStatusHistory statusHistory = new CaptainUserStatusHistory()
                {
                    UserId = oldUserCurrentStatus.UserId,
                    StatusTypeId = oldUserCurrentStatus.StatusTypeId,
                    CreationDate = currentDate,
                    ModificationDate = oldUserCurrentStatus.ModificationDate
                };


                oldUserCurrentStatus.UserId = userCurrentStatus.UserId;
                oldUserCurrentStatus.StatusTypeId = userCurrentStatus.StatusTypeId;
                oldUserCurrentStatus.IsCurrent = true;
                oldUserCurrentStatus.ModificationDate = currentDate;

                await _context.UserStatusHistories.AddAsync(statusHistory);
                _context.Entry<CaptainUserCurrentStatus>(oldUserCurrentStatus).State = EntityState.Modified;
                return oldUserCurrentStatus;

            }
            else
            {
                CaptainUserStatusHistory statusHistory = new CaptainUserStatusHistory()
                {
                    UserId = userCurrentStatus.UserId,
                    StatusTypeId = (long)StatusTypes.New,
                    CreationDate = currentDate,
                    ModificationDate = userCurrentStatus.ModificationDate
                };
                await _context.UserStatusHistories.AddAsync(statusHistory);
                userCurrentStatus.CreationDate = currentDate;
                var insertResult = await _context.UserCurrentStatuses.AddAsync(userCurrentStatus);
                return insertResult.Entity;
            }


            
        }

        public async Task<CaptainUserCurrentStatus> UpdateUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus)
        {
            var oldUserCurrentStatus = await _context.UserCurrentStatuses.FirstOrDefaultAsync(u => u.Id == userCurrentStatus.Id);
            if (oldUserCurrentStatus == null) return null;

            var currentDate = DateTime.Now;
            CaptainUserStatusHistory statusHistory = new CaptainUserStatusHistory()
            {
                UserId = oldUserCurrentStatus.UserId,
                StatusTypeId = oldUserCurrentStatus.StatusTypeId,
                CreationDate = currentDate,
                CreatedBy = 1
            };



            oldUserCurrentStatus.UserId = userCurrentStatus.UserId;
            oldUserCurrentStatus.StatusTypeId = userCurrentStatus.StatusTypeId;
            oldUserCurrentStatus.IsCurrent = userCurrentStatus.IsCurrent;
            oldUserCurrentStatus.ModifiedBy = userCurrentStatus.ModifiedBy;
            oldUserCurrentStatus.ModificationDate = currentDate;

            var insertResult = await _context.UserStatusHistories.AddAsync(statusHistory);
            _context.Entry<CaptainUserCurrentStatus>(oldUserCurrentStatus).State = EntityState.Modified;
            return oldUserCurrentStatus;
        }

        public async Task<CaptainUserCurrentStatus> DeleteUserCurrentStatusAsync(long id)
        {
            var oldUserCurrentStatus = await _context.UserCurrentStatuses.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserCurrentStatus == null) return null;

            _context.UserCurrentStatuses.Remove(oldUserCurrentStatus);
            return oldUserCurrentStatus;
        }

        public async Task<List<CaptainUserIgnoredPenalty>> GetUsersIgnoredPenaltiesAsync()
        {
            return await _context.UserIgnoredPenalties.ToListAsync();
        }

        public async Task<CaptainUserIgnoredPenalty> GetUserIgnoredPenaltyByIdAsync(long id)
        {
            return await _context.UserIgnoredPenalties.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserIgnoredPenalty>> GetUsersIgnoredPenaltiesByAsync(Expression<Func<CaptainUserIgnoredPenalty, bool>> predicate)
        {
            return await _context.UserIgnoredPenalties.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserIgnoredPenalty> InsertUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty)
        {
            userIgnoredPenalty.CreationDate = DateTime.Now;
            var inserResult = await _context.UserIgnoredPenalties.AddAsync(userIgnoredPenalty);
            return inserResult.Entity;
        }

        public async Task<CaptainUserIgnoredPenalty> UpdateUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty)
        {
            var oldUserIgnoredPenalty = await _context.UserIgnoredPenalties.FirstOrDefaultAsync(u => u.Id == userIgnoredPenalty.Id);
            if (oldUserIgnoredPenalty == null) return null;

            oldUserIgnoredPenalty.UserId = userIgnoredPenalty.UserId;
            oldUserIgnoredPenalty.SystemSettingId = userIgnoredPenalty.SystemSettingId;
            oldUserIgnoredPenalty.PenaltyStatusTypeId = userIgnoredPenalty.PenaltyStatusTypeId;
            oldUserIgnoredPenalty.ModifiedBy = userIgnoredPenalty.ModifiedBy;
            oldUserIgnoredPenalty.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserIgnoredPenalty>(oldUserIgnoredPenalty).State = EntityState.Modified;
            return oldUserIgnoredPenalty;
        }

        public async Task<CaptainUserIgnoredPenalty> DeleteUserIgnoredPenaltyAsync(long id)
        {
            var oldUserIgnoredPenalty = await _context.UserIgnoredPenalties.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserIgnoredPenalty == null) return null;

            _context.UserIgnoredPenalties.Remove(oldUserIgnoredPenalty);
            return oldUserIgnoredPenalty;
        }

        public async Task<List<CaptainUserIgnoredRequest>> GetUsersIgnoredRequestsAsync()
        {
            return await _context.UserIgnoredRequests.ToListAsync();
        }

        public async Task<CaptainUserIgnoredRequest> GetUserIgnoredRequestByIdAsync(long id)
        {
            return await _context.UserIgnoredRequests.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserIgnoredRequest>> GetUsersIgnoredRequestsByAsync(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate)
        {
            return await _context.UserIgnoredRequests.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserIgnoredRequest> InsertUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest)
        {
            userIgnoredRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.UserIgnoredRequests.AddAsync(userIgnoredRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserIgnoredRequest> UpdateUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest)
        {
            var oldUserIgnoredRequest = await _context.UserIgnoredRequests.FirstOrDefaultAsync(u => u.Id == userIgnoredRequest.Id);
            if (oldUserIgnoredRequest == null) return null;

            oldUserIgnoredRequest.UserId = userIgnoredRequest.UserId;
            oldUserIgnoredRequest.OrderId = userIgnoredRequest.OrderId;
            oldUserIgnoredRequest.AgentId = userIgnoredRequest.AgentId;
            oldUserIgnoredRequest.ModifiedBy = userIgnoredRequest.ModifiedBy;
            oldUserIgnoredRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserIgnoredRequest>(oldUserIgnoredRequest).State = EntityState.Modified;
            return userIgnoredRequest;
        }

        public async Task<CaptainUserIgnoredRequest> DeleteUserIgnoredRequestAsync(long id)
        {
            var oldUserIgnoredRequest = await _context.UserIgnoredRequests.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserIgnoredRequest == null) return null;

            _context.UserIgnoredRequests.Remove(oldUserIgnoredRequest);
            return oldUserIgnoredRequest;
        }

		public async Task<List<CaptainUserPayment>> GetUsersPaymentsAsync()
        {
            return await _context.UserPayments.ToListAsync();
        }
        
        public async Task<CaptainUserPayment> GetUserPaymentByIdAsync(long id)
        {
            return await _context.UserPayments.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserPayment>> GetUsersPaymentsByAsync(Expression<Func<CaptainUserPayment, bool>> predicate)
        {
            return await _context.UserPayments.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserPayment> InsertUserPaymentAsync(CaptainUserPayment userPayment)
        {
            userPayment.CreationDate = DateTime.Now;
            var inserResult = await _context.UserPayments.AddAsync(userPayment);
            return inserResult.Entity;
        }

        public async Task<CaptainUserPayment> UpdateUserPaymentAsync(CaptainUserPayment userPayment)
        {
            var oldUserPayment = await _context.UserPayments.FirstOrDefaultAsync(u => u.Id == userPayment.Id);
            if (oldUserPayment == null) return null;

            oldUserPayment.UserId = userPayment.UserId;
            oldUserPayment.OrderId = userPayment.OrderId;
            oldUserPayment.PaymentTypeId = userPayment.PaymentTypeId;
            oldUserPayment.SystemSettingId = userPayment.SystemSettingId;
            oldUserPayment.Value = userPayment.Value;
            oldUserPayment.StatusId = userPayment.StatusId;
            oldUserPayment.ModifiedBy = userPayment.ModifiedBy;
            oldUserPayment.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserPayment>(oldUserPayment).State = EntityState.Modified;


            CaptainUserPaymentHistory userPaymentHistory = new CaptainUserPaymentHistory()
            {
                UserId = oldUserPayment.UserId,
                OrderId = oldUserPayment.OrderId,
                PaymentTypeId = oldUserPayment.PaymentTypeId,
                StatusId = oldUserPayment.StatusId,
                SystemSettingId = oldUserPayment.SystemSettingId,
                Value = oldUserPayment.Value,
                CreationDate = DateTime.Now,
                CreatedBy = oldUserPayment.CreatedBy,
                ModifiedBy = oldUserPayment.ModifiedBy,
                ModificationDate = oldUserPayment.ModificationDate
            };
            var insertResult = await _context.UserPaymentHistories.AddAsync(userPaymentHistory);

            return oldUserPayment;
        }

        public async Task<CaptainUserPayment> DeleteUserPaymentAsync(long id)
        {
            var oldUserPayment = await _context.UserPayments.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserPayment == null) return null;

            _context.UserPayments.Remove(oldUserPayment);
            return oldUserPayment;
        }

        public async Task<List<CaptainUserRejectedRequest>> GetUsersRejectedRequestsAsync()
        {
            return await _context.UserRejectedRequests.ToListAsync();
        }

        public async Task<CaptainUserRejectedRequest> GetUserRejectedRequestByIdAsync(long id)
        {
            return await _context.UserRejectedRequests.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserRejectedRequest>> GetUsersRejectedRequestsByAsync(Expression<Func<CaptainUserRejectedRequest, bool>> predicate)
        {
            return await _context.UserRejectedRequests.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserRejectedRequest> InsertUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest)
        {
            userRejectedRequest.CreationDate = DateTime.Now;
            var inserResult = await _context.UserRejectedRequests.AddAsync(userRejectedRequest);
            return inserResult.Entity;
        }

        public async Task<CaptainUserRejectedRequest> UpdateUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest)
        {
            var oldUserRejectedRequest = await _context.UserRejectedRequests.FirstOrDefaultAsync(u => u.Id == userRejectedRequest.Id);
            if (oldUserRejectedRequest == null) return null;

            oldUserRejectedRequest.UserId = userRejectedRequest.UserId;
            oldUserRejectedRequest.OrderId = userRejectedRequest.OrderId;
            oldUserRejectedRequest.AgentId = userRejectedRequest.AgentId;
            oldUserRejectedRequest.ModifiedBy = userRejectedRequest.ModifiedBy;
            oldUserRejectedRequest.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserRejectedRequest>(oldUserRejectedRequest).State = EntityState.Modified;
            return oldUserRejectedRequest;
        }

        public async Task<CaptainUserRejectedRequest> DeleteUserRejectedRequestAsync(long id)
        {
            var oldUserRejectedRequest = await _context.UserRejectedRequests.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserRejectedRequest == null) return null;

            _context.UserRejectedRequests.Remove(oldUserRejectedRequest);
            return oldUserRejectedRequest;
        }

        public async Task<List<CaptainUserRejectPenalty>> GetUsersRejectPenaltiesAsync()
        {
            return await _context.UserRejectPenalties.ToListAsync();
        }

        public async Task<CaptainUserRejectPenalty> GetUserRejectPenaltyByIdAsync(long id)
        {
            return await _context.UserRejectPenalties.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserRejectPenalty>> GetUsersRejectPenaltiesByAsync(Expression<Func<CaptainUserRejectPenalty, bool>> predicate)
        {
            return await _context.UserRejectPenalties.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserRejectPenalty> InsertUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty)
        {
            userRejectPenalty.CreationDate = DateTime.Now;
            var inserResult = await _context.UserRejectPenalties.AddAsync(userRejectPenalty);
            return inserResult.Entity;
        }

        public async Task<CaptainUserRejectPenalty> UpdateUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty)
        {
            var oldUserRejectPenalty = await _context.UserRejectPenalties.FirstOrDefaultAsync(u => u.Id == userRejectPenalty.Id);
            if (oldUserRejectPenalty == null) return null;

            oldUserRejectPenalty.UserId = userRejectPenalty.UserId;
            oldUserRejectPenalty.SystemSettingId = userRejectPenalty.SystemSettingId;
            oldUserRejectPenalty.PenaltyStatusTypeId = userRejectPenalty.PenaltyStatusTypeId;
            oldUserRejectPenalty.ModifiedBy = userRejectPenalty.ModifiedBy;
            oldUserRejectPenalty.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserRejectPenalty>(oldUserRejectPenalty).State = EntityState.Modified;
            return oldUserRejectPenalty;
        }

        public async Task<CaptainUserRejectPenalty> DeleteUserRejectPenaltyAsync(long id)
        {
            var oldUserRejectPenalty = await _context.UserRejectPenalties.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserRejectPenalty == null) return null;

            _context.UserRejectPenalties.Remove(oldUserRejectPenalty);
            return oldUserRejectPenalty;
        }

        public async Task<List<CaptainUserShift>> GetUsersShiftsAsync()
        {
            return await _context.UserShifts.ToListAsync();
        }

        public async Task<CaptainUserShift> GetUserShiftByIdAsync(long id)
        {
            return await _context.UserShifts.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserShift>> GetUsersShiftsByAsync(Expression<Func<CaptainUserShift, bool>> predicate)
        {
            return await _context.UserShifts.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserShift> InsertUserShiftAsync(CaptainUserShift userShift)
        {
            userShift.CreationDate = DateTime.Now;
            var inserResult = await _context.UserShifts.AddAsync(userShift);
            return inserResult.Entity;
        }

        public async Task<CaptainUserShift> UpdateUserShiftAsync(CaptainUserShift userShift)
        {
            var oldUserShift = await _context.UserShifts.FirstOrDefaultAsync(u => u.Id == userShift.Id);
            if (oldUserShift == null) return null;

            oldUserShift.UserId = userShift.UserId;
            oldUserShift.EndHour = userShift.EndHour;
            oldUserShift.EndMinutes = userShift.EndMinutes;
            oldUserShift.StartHour = userShift.StartHour;
            oldUserShift.StartMinutes = userShift.StartMinutes;
            oldUserShift.ModifiedBy = userShift.ModifiedBy;
            oldUserShift.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserShift>(oldUserShift).State = EntityState.Modified;
            return oldUserShift;
        }

        public async Task<CaptainUserShift> DeleteUserShiftAsync(long id)
        {
            var oldUserShift = await _context.UserShifts.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserShift == null) return null;

            _context.UserShifts.Remove(oldUserShift);
            return oldUserShift;
        }

        public async Task<List<CaptainUserStatusHistory>> GetUsersStatusHistoriesAsync()
        {
            return await _context.UserStatusHistories.ToListAsync();
        }

        public async Task<CaptainUserStatusHistory> GetUserStatusHistoryByIdAsync(long id)
        {
            return await _context.UserStatusHistories.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CaptainUserStatusHistory>> GetUsersStatusHistoriesByAsync(Expression<Func<CaptainUserStatusHistory, bool>> predicate)
        {
            return await _context.UserStatusHistories.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserStatusHistory> InsertUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory)
        {
            userStatusHistory.CreationDate = DateTime.Now;
            var inserResult = await _context.UserStatusHistories.AddAsync(userStatusHistory);
            return inserResult.Entity;
        }

        public async Task<CaptainUserStatusHistory> UpdateUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory)
        {
            var oldUserStatusHistory = await _context.UserStatusHistories.FirstOrDefaultAsync(u => u.Id == userStatusHistory.Id);
            if (oldUserStatusHistory == null) return null;

            oldUserStatusHistory.UserId = userStatusHistory.UserId;
            oldUserStatusHistory.StatusTypeId = userStatusHistory.StatusTypeId;
            oldUserStatusHistory.ModifiedBy = userStatusHistory.ModifiedBy;
            oldUserStatusHistory.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserStatusHistory>(oldUserStatusHistory).State = EntityState.Modified;
            return oldUserStatusHistory;
        }

        public async Task<CaptainUserStatusHistory> DeleteUserStatusHistoryAsync(long id)
        {
            var oldUserStatusHistory = await _context.UserStatusHistories.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserStatusHistory == null) return null;

            _context.UserStatusHistories.Remove(oldUserStatusHistory);
            return oldUserStatusHistory;
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(long id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<CaptainUserVehicle>> GetUsersVehiclesAsync()
        {
            return await _context.UserVehicles.ToListAsync();
        }

        public async Task<CaptainUserVehicle> GetUserVehicleByIdAsync(long id)
        {
            return await _context.UserVehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<CaptainUserVehicle>> GetUsersVehiclesByAsync(Expression<Func<CaptainUserVehicle, bool>> predicate)
        {
            return await _context.UserVehicles.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserVehicle> InsertUserVehicleAsync(CaptainUserVehicle userVehicle)
        {
            userVehicle.CreationDate = DateTime.Now;
            var inserResult = await _context.UserVehicles.AddAsync(userVehicle);
            return inserResult.Entity;
        }

        public async Task<CaptainUserVehicle> UpdateUserVehicleAsync(CaptainUserVehicle userVehicle)
        {
            var oldUserVehicle = await _context.UserVehicles.FirstOrDefaultAsync(u => u.Id == userVehicle.Id);
            if (oldUserVehicle == null) return null;

            oldUserVehicle.UserId = userVehicle.UserId;
            oldUserVehicle.VehicleId = userVehicle.VehicleId;
            oldUserVehicle.PlateNumber = userVehicle.PlateNumber;
            oldUserVehicle.Model = userVehicle.Model;
            oldUserVehicle.VehicleImageName = userVehicle.VehicleImageName;
            oldUserVehicle.VehicleImageAndroidPath = userVehicle.VehicleImageAndroidPath;
            oldUserVehicle.LicenseImageName = userVehicle.LicenseImageName;
            oldUserVehicle.LicenseImageAndroidPath = userVehicle.LicenseImageAndroidPath;
            oldUserVehicle.LicenseNumber = userVehicle.LicenseNumber;
            oldUserVehicle.IsActive = userVehicle.IsActive;
            oldUserVehicle.IsDeleted = userVehicle.IsDeleted;
            oldUserVehicle.ModifiedBy = userVehicle.ModifiedBy;
            oldUserVehicle.ModificationDate = DateTime.Now;

            _context.Entry<CaptainUserVehicle>(oldUserVehicle).State = EntityState.Modified;
            return oldUserVehicle;
        }

        public async Task<CaptainUserVehicle> DeleteUserVehicleAsync(long id)
        {
            var oldUserVehicle = await _context.UserVehicles.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserVehicle == null) return null;

            oldUserVehicle.IsDeleted = true;
            _context.Entry<CaptainUserVehicle>(oldUserVehicle).State = EntityState.Modified;
            return oldUserVehicle;
        }


        public async Task<CaptainUser> GetUserNearestLocationAsync(string pickupLatitude, string pickupLongitude)
        {

            //var agentCoordinate = new GeoCoordinate(double.Parse(PickupLatitude), double.Parse(PickupLongitude));
            //var driversIgnoredRequests = await _context.UserIgnoredRequests.Where(u => u.OrderId == OrderID).Select(u => u.UserId).ToListAsync();
            //var driversRejectedRequests = await _context.UserRejectedRequests.Where(u => u.OrderId == OrderID).Select(u => u.UserId).ToListAsync();

            //driversIgnoredRequests.AddRange(driversRejectedRequests);


            var users = await _context.Users.FromSqlRaw("SelectNearestCaptain '" + pickupLatitude + "','" + pickupLongitude + "'").ToListAsync();
            if (users == null || users.Count <= 0) return null;

            var captain = users.FirstOrDefault();
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
        
        
        
        public async Task<List<CaptainUser>> GetCaptainsUsersNearToLocationAsync(string pickupLatitude, string pickupLongitude)
        {
            List<CaptainUser> result = new List<CaptainUser>(); 
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();
            var command = conn.CreateCommand();
            string query = "GetNearCaptains '" + pickupLatitude + "','" + pickupLongitude + "'";
            command.CommandText = query;
            using var reader = await command.ExecuteReaderAsync();
            while ( reader.Read())
            {
                var id = reader.GetInt64(reader.GetOrdinal("ID"));
                var firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                var familyName = reader.GetString(reader.GetOrdinal("FamilyName"));
                var lat = reader.GetDouble(reader.GetOrdinal("lat"));
                var lng = reader.GetDouble(reader.GetOrdinal("long"));

                CaptainUser user = new CaptainUser()
                {
                    Id = id,
                    FirstName = firstName,
                    FamilyName = familyName,
                    Lat =lat,
                    Long = lng
                };

                result.Add(user);

                
            }

            //reader.Close();
            return result;

            //var users = await _context.Users.FromSqlRaw("GetNearCaptains '" + PickupLatitude + "','" + PickupLongitude + "'").ToListAsync();
            //if (users == null || users.Count <= 0) return null;

            //return users;
        }
        
        
        public async Task<List<CaptainUserMessageHub>> GetUsersMessageHubsAsync()
        {
            return await _context.UserMessageHubs.ToListAsync();
        }

        public async Task<CaptainUserMessageHub> GetUserMessageHubByIdAsync(long id)
        {
            return await _context.UserMessageHubs.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<CaptainUserMessageHub>> GetUsersMessageHubsByAsync(Expression<Func<CaptainUserMessageHub, bool>> predicate)
        {
            return await _context.UserMessageHubs.Where(predicate).ToListAsync();
        }

        public async Task<CaptainUserMessageHub> InsertUserMessageHubAsync(long id, string connectionId)
        {


            var oldUserHub = await _context.UserMessageHubs.FirstOrDefaultAsync(h => h.UserId == id);
            if (oldUserHub != null && oldUserHub.Id > 0)
            {
                oldUserHub.ConnectionId = connectionId;
                oldUserHub.ModifiedBy = 1;
                oldUserHub.ModificationDate = DateTime.Now;
                _context.Entry<CaptainUserMessageHub>(oldUserHub).State = EntityState.Modified;
                return oldUserHub;
            }
            else 
            {
                CaptainUserMessageHub newHub = new CaptainUserMessageHub() { UserId = id, ConnectionId = connectionId, CreationDate = DateTime.Now, CreatedBy = 1 };
                var insertResult = await _context.UserMessageHubs.AddAsync(newHub);
                return insertResult.Entity;
            }

            
        }

        public async Task<CaptainUserMessageHub> UpdateUserMessageHubAsync(long id, string connectionId)
        {
            var oldUserHub = await _context.UserMessageHubs.FirstOrDefaultAsync(h => h.UserId == id);
            if (oldUserHub == null) return null;


            oldUserHub.ConnectionId = connectionId;
            oldUserHub.ModifiedBy = 1;
            oldUserHub.ModificationDate = DateTime.Now;
            _context.Entry<CaptainUserMessageHub>(oldUserHub).State = EntityState.Modified;
            return oldUserHub;
        }

        public async Task<CaptainUserNewRequest> DeleteUserNewRequestByOrderIdAsync(long id)
        {
            var oldUserNewRequests = await _context.UserNewRequests.FirstOrDefaultAsync(u => u.OrderId == id);
            if (oldUserNewRequests == null) return null;

            _context.UserNewRequests.Remove(oldUserNewRequests);
            return oldUserNewRequests;
        }

        public async Task<CaptainUserPayment> DeleteUserPaymentByOrderIdAsync(long id)
        {
            var oldUserPayment = await _context.UserPayments.FirstOrDefaultAsync(u => u.OrderId == id);
            if (oldUserPayment == null) return null;

            _context.UserPayments.Remove(oldUserPayment);
            return oldUserPayment;
        }
        public IQueryable<CaptainUserRejectedRequest> GetUserRejectedRequestByQuerable(Expression<Func<CaptainUserRejectedRequest, bool>> predicate)
        {
            var result = _context.UserRejectedRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts); 
          
            return result;
        }

        public IQueryable<CaptainUserAcceptedRequest> GetUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            var result = _context.UserAcceptedRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts)
                .Include(o => o.Order).ThenInclude(o => o.PaymentType).Include(o => o.User)
                .Include(o => o.Order).ThenInclude(o => o.Agent).Include(u => u.User).ThenInclude(c => c.City).Include(u => u.User).ThenInclude(c => c.Country)
                .Include(o => o.Order).ThenInclude(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
                .ThenInclude(u => u.User).ThenInclude(c => c.City)
                .Include(o => o.Order).ThenInclude(o => o.UserAcceptedRequests).ThenInclude(u => u.User).ThenInclude(c => c.UserAccounts)
                .ThenInclude(u => u.User).ThenInclude(c => c.Country)
                
                .Where(predicate);
           
            return result;

        }
        public IQueryable<CaptainUserIgnoredRequest> GetUserIgnoredRequestByQuerable(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate)
        {
            var result = _context.UserIgnoredRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts); 
           
            return result;

        }

		public IQueryable<CaptainUser> GetByQuerable()
		{
            return _context.UserAccounts.Include(u => u.User)
                .ThenInclude(u => u.City).Include(u => u.User).ThenInclude(u => u.Country)
                .Include(u => u.User).ThenInclude(u => u.UserAccounts).Select(c => c.User);

        }
       

		public List<CaptainUser> GetByStatusType(long? statusTypeId, List<CaptainUser> query)
		{
            if(statusTypeId != null)
			{
                var restult = _context.UserAccounts.Where(u => u.StatusTypeId == statusTypeId)
                    .Include(u => u.User).ThenInclude(u=> u.UserCurrentStatus).Include(u => u.User).ThenInclude(u => u.Country).Include(u =>u.User).ThenInclude(u => u.City).Select(c => c.User)
                    .ToList();
                return restult;
			}
            return query;
		}
        public IQueryable<CaptainUser> GetByStatusQuerableS(long? statusTypeId)
        {
            return _context.UserAccounts.Where(u => u.StatusTypeId == statusTypeId).Include(u => u.User)
                .ThenInclude(u => u.City).Include(u => u.User).ThenInclude(u => u.Country)
                .Include(u => u.User).ThenInclude(u => u.UserAccounts).Select(c => c.User);
            
        }



		public async Task<List<CaptainUserActivity>> GetUsersActivitiesAsync()
		{
			return await _context.UserActivities.ToListAsync();
		}

		public async Task<CaptainUserActivity> GetUserActivityByIdAsync(long id)
		{
			return await _context.UserActivities.FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task<List<CaptainUserActivity>> GetUsersActivitiesByAsync(Expression<Func<CaptainUserActivity, bool>> predicate)
		{
			return await _context.UserActivities.Where(predicate).ToListAsync();
		}

		public async Task<CaptainUserActivity> InsertUserActivityAsync(CaptainUserActivity userActivity)
		{
			var oldUserActivity = await _context.UserActivities.FirstOrDefaultAsync(u => u.UserId == userActivity.UserId && u.IsCurrent == true);
			if (oldUserActivity != null && oldUserActivity.Id > 0)
			{

				oldUserActivity.IsCurrent = false;
				oldUserActivity.ModificationDate = DateTime.Now;
				_context.Entry<CaptainUserActivity>(oldUserActivity).State = EntityState.Modified;

			}

			userActivity.IsCurrent = true;
			userActivity.CreationDate = DateTime.Now;
			var insert_result = await _context.UserActivities.AddAsync(userActivity);
			return insert_result.Entity;
		}



        public async Task<CaptainUserActivity> UpdateUserActivityAsync(CaptainUserActivity userActivity)
        {
            var oldUserActivity = await _context.UserActivities.FirstOrDefaultAsync(u => u.Id == userActivity.Id);
            if (oldUserActivity == null) return null;

            oldUserActivity.IsCurrent = false;
            oldUserActivity.ModificationDate = DateTime.Now;
            _context.Entry<CaptainUserActivity>(oldUserActivity).State = EntityState.Modified;
            return oldUserActivity;


        }

		public async Task<CaptainUserActivity> DeleteUserActivityAsync(long id)
		{
            var oldUserActivity = await _context.UserActivities.FirstOrDefaultAsync(u => u.Id == id);
            if (oldUserActivity == null) return null;

            
            _context.UserActivities.Remove(oldUserActivity);
            return oldUserActivity;
        }

		public object UserReportCount()
		{
            var totalUsers = _context.Users.Count();
            
            var newUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.New).Count();
            var readyUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Ready).Count();
            var workingUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Working).Count();
            var progressUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Progress).Count();
            var suspendedUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Suspended).Count();
            var stoppedUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Stopped).Count();
            var reviewingUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Reviewing).Count();
            var penaltyUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Penalty).Count();
            var incompleteUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Incomplete).Count();
            var completeUsers = _context.UserCurrentStatuses.Where(u => u.StatusTypeId == (long)StatusTypes.Complete).Count();
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

		public async Task<Bonus> GetBonusByCountryAsync(long? countryId)
		{
            var bonus = await _context.Bonuses.FirstOrDefaultAsync(b => b.CountryId == countryId);
            return bonus;
		}
        public async Task<CaptainUserBonus> InsertBonusAsync(CaptainUserBonus userBonus)
		{
            var result = await _context.UserBonuses.AddAsync(userBonus);
            return result.Entity;
		}

		public IQueryable<CaptainUserRejectedRequest> GetAllRejectedRequestByQuerable()
		{
            var result = _context.UserRejectedRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts);

            return result;
        }
       public async Task<List<Qrcode>> GetQRCodeByAsync(Expression<Func<Qrcode, bool>> predicate)
		{
            return await _context.Qrcodes.Where(predicate).ToListAsync();
           
		}

       
        public async Task<List<CaptainUserAcceptedRequest>> UserAcceptedRequestsAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            return await _context.UserAcceptedRequests.Include(u=>u.User).ThenInclude(u=>u.City)
                .Include(u => u.User).ThenInclude(u => u.Country).Where(predicate).ToListAsync();

        }

        public IQueryable<CaptainUserAcceptedRequest> GetAllAcceptedRequestByQuerable()
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
        }
      
        public IQueryable<CaptainUserIgnoredRequest> GetAllIgnoredRequestByQuerable()
		{
            var result = _context.UserIgnoredRequests.Include(u => u.User).ThenInclude(c => c.UserAccounts);//.Include(o => o.Order);

            return result;
        }

        public async Task<List<CaptainUserAcceptedRequest>> GetUserAcceptedRequestAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate)
        {
            return await _context.UserAcceptedRequests.Include(u=>u.User).ThenInclude(u=>u.City).
                Include(u => u.User).ThenInclude(u => u.Country).Where(predicate).ToListAsync();
        }


        public async Task<List<CaptainUserAccount>> GetActiveUsersAccountsPaginationAsync(int skip, int take)
        {
            return await _context.UserAccounts
                .Where(u => u.StatusTypeId != (long)StatusTypes.Reviewing)
                .OrderByDescending(u => u.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }


        public async Task<List<CaptainUserAccount>> GetReviewingUsersAccountsPaginationAsync(int skip, int take)
        {
            return await _context.UserAccounts
                .Where(u => u.StatusTypeId == (long)StatusTypes.Reviewing)
                .OrderByDescending(u => u.CreationDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }


    }

