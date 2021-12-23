
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TreePorts.Models;

namespace TreePorts.Interfaces.Repositories;

    public interface ICaptainRepository
    {
        Task<List<CaptainUser>> GetUsersAsync();
        Task<CaptainUser> GetUserByIdAsync(long id);

        Task<CaptainUser> GetUserNearestLocationAsync(string pickupLatitude ,string pickupLongitude);
        Task<List<CaptainUser>> GetCaptainsUsersNearToLocationAsync(string pickupLatitude ,string pickupLongitude);
        Task<List<CaptainUser>> GetUsersByAsync(Expression<Func<CaptainUser, bool>> predicate);
        Task<CaptainUser> InsertUserAsync(CaptainUser user);
        Task<CaptainUser> UpdateUserAsync(CaptainUser user);
        Task<CaptainUser> DeleteUserAsync(long id);


        Task<List<CaptainUserAccount>> GetUsersAccountsAsync();
        Task<CaptainUserAccount> GetUserAccountByIdAsync(long id);
        Task<List<CaptainUserAccount>> GetUsersAccountsByAsync(Expression<Func<CaptainUserAccount, bool>> predicate);
        Task<CaptainUserAccount> InsertUserAccountAsync(CaptainUserAccount account);
        Task<CaptainUserAccount> UpdateUserAccountAsync(CaptainUserAccount account);
        Task<CaptainUserAccount> DeleteUserAccountAsync(long id);
        IQueryable<CaptainUserAccount> GetUserAccountByQuerable(Expression<Func<CaptainUserAccount, bool>> predicate);
        IQueryable<CaptainUserAccount> GetUserAccountByQuerable();



        Task<List<CaptainUserMessageHub>> GetUsersMessageHubsAsync();
        Task<CaptainUserMessageHub> GetUserMessageHubByIdAsync(long id);
        Task<List<CaptainUserMessageHub>> GetUsersMessageHubsByAsync(Expression<Func<CaptainUserMessageHub, bool>> predicate);
        Task<CaptainUserMessageHub> InsertUserMessageHubAsync(long id,string connectionId);
        Task<CaptainUserMessageHub> UpdateUserMessageHubAsync(long id, string connectionId);



        Task<List<CaptainUserNewRequest>> GetUsersNewRequestsAsync();
        Task<CaptainUserNewRequest> GetUserNewRequestByIdAsync(long id);
        Task<List<CaptainUserNewRequest>> GetUsersNewRequestsByAsync(Expression<Func<CaptainUserNewRequest, bool>> predicate);
        Task<CaptainUserNewRequest> InsertUserNewRequestAsync(CaptainUserNewRequest userRequest);
        Task<CaptainUserNewRequest> UpdateUserNewRequestAsync(CaptainUserNewRequest userRequest);
        Task<CaptainUserNewRequest> DeleteUserNewRequestAsync(long id);
        Task<CaptainUserNewRequest> DeleteUserNewRequestByOrderIdAsync(long id);
        Task<List<CaptainUserNewRequest>> DeleteUserNewRequestByUserIdAsync(long id);

        Task<List<CaptainUserAcceptedRequest>> GetUsersAcceptedRequestsAsync();
        Task<CaptainUserAcceptedRequest> GetUserAcceptedRequestByIdAsync(long id);
        Task<List<CaptainUserAcceptedRequest>> GetUsersAcceptedRequestsByAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);
        Task<CaptainUserAcceptedRequest> InsertUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest);
        Task<CaptainUserAcceptedRequest> UpdateUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest);
        Task<CaptainUserAcceptedRequest> DeleteUserAcceptedRequestAsync(long id);




		Task<List<BoxType>> GetBoxTypesAsync();
        Task<BoxType> GetBoxTypeByIdAsync(long id);
        Task<List<CaptainUserBox>> GetUsersBoxesAsync();
        Task<CaptainUserBox> GetUserBoxById(long id);
        Task<List<CaptainUserBox>> GetUsersBoxesByAsync(Expression<Func<CaptainUserBox, bool>> predicate);
        Task<CaptainUserBox> InsertUserBoxAsync(CaptainUserBox userBox);
        Task<CaptainUserBox> UpdateUserBoxAsync(CaptainUserBox userBox);
        Task<CaptainUserBox> DeleteUserBoxAsync(long id);


		Task<List<CaptainUserActivity>> GetUsersActivitiesAsync();
        Task<CaptainUserActivity> GetUserActivityByIdAsync(long id);
        Task<List<CaptainUserActivity>> GetUsersActivitiesByAsync(Expression<Func<CaptainUserActivity, bool>> predicate);
        Task<CaptainUserActivity> InsertUserActivityAsync(CaptainUserActivity userActivity);
        Task<CaptainUserActivity> UpdateUserActivityAsync(CaptainUserActivity userActivity);
        Task<CaptainUserActivity> DeleteUserActivityAsync(long id);




        Task<List<CaptainUserCurrentBalance>> GetUsersCurrentBalancesAsync();
        Task<CaptainUserCurrentBalance> GetUserCurrentBalanceByIdAsync(long id);
        Task<List<CaptainUserCurrentBalance>> GetUsersCurrentBalancesByAsync(Expression<Func<CaptainUserCurrentBalance, bool>> predicate);
        Task<CaptainUserCurrentBalance> InsertUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance);
        Task<CaptainUserCurrentBalance> UpdateUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance);
        Task<CaptainUserCurrentBalance> DeleteUserCurrentBalanceAsync(long id);


        Task<List<CaptainUserCurrentLocation>> GetUsersCurrentLocationsAsync();
        Task<CaptainUserCurrentLocation> GetUserCurrentLocationByIdAsync(long id);
        Task<List<CaptainUserCurrentLocation>> GetUsersCurrentLocationsByAsync(Expression<Func<CaptainUserCurrentLocation, bool>> predicate);
        Task<CaptainUserCurrentLocation> InsertUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation);
        Task<CaptainUserCurrentLocation> UpdateUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation);
        Task<CaptainUserCurrentLocation> DeleteUserCurrentLocationAsync(long id);


        Task<List<CaptainUserCurrentStatus>> GetUsersCurrentStatusesAsync();
        Task<CaptainUserCurrentStatus> GetUserCurrentStatusByIdAsync(long id);
        Task<List<CaptainUserCurrentStatus>> GetUsersCurrentStatusesByAsync(Expression<Func<CaptainUserCurrentStatus, bool>> predicate);
        Task<CaptainUserCurrentStatus> InsertUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus);
        Task<CaptainUserCurrentStatus> UpdateUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus);
        Task<CaptainUserCurrentStatus> DeleteUserCurrentStatusAsync(long id);


        Task<List<CaptainUserIgnoredPenalty>> GetUsersIgnoredPenaltiesAsync();
        Task<CaptainUserIgnoredPenalty> GetUserIgnoredPenaltyByIdAsync(long id);
        Task<List<CaptainUserIgnoredPenalty>> GetUsersIgnoredPenaltiesByAsync(Expression<Func<CaptainUserIgnoredPenalty, bool>> predicate);
        Task<CaptainUserIgnoredPenalty> InsertUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty);
        Task<CaptainUserIgnoredPenalty> UpdateUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty);
        Task<CaptainUserIgnoredPenalty> DeleteUserIgnoredPenaltyAsync(long id);

        
        Task<List<CaptainUserIgnoredRequest>> GetUsersIgnoredRequestsAsync();
        Task<CaptainUserIgnoredRequest> GetUserIgnoredRequestByIdAsync(long id);
        Task<List<CaptainUserIgnoredRequest>> GetUsersIgnoredRequestsByAsync(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate);
        Task<CaptainUserIgnoredRequest> InsertUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest);
        Task<CaptainUserIgnoredRequest> UpdateUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest);
        Task<CaptainUserIgnoredRequest> DeleteUserIgnoredRequestAsync(long id);


        Task<List<CaptainUserPayment>> GetUsersPaymentsAsync();
        Task<CaptainUserPayment> GetUserPaymentByIdAsync(long id);
        Task<List<CaptainUserPayment>> GetUsersPaymentsByAsync(Expression<Func<CaptainUserPayment, bool>> predicate);
        Task<CaptainUserPayment> InsertUserPaymentAsync(CaptainUserPayment userPayment);
        Task<CaptainUserPayment> UpdateUserPaymentAsync(CaptainUserPayment userPayment);
        Task<CaptainUserPayment> DeleteUserPaymentAsync(long id);
        Task<CaptainUserPayment> DeleteUserPaymentByOrderIdAsync(long id);


        Task<List<CaptainUserRejectedRequest>> GetUsersRejectedRequestsAsync();
        Task<CaptainUserRejectedRequest> GetUserRejectedRequestByIdAsync(long id);
        Task<List<CaptainUserRejectedRequest>> GetUsersRejectedRequestsByAsync(Expression<Func<CaptainUserRejectedRequest, bool>> predicate);
        Task<CaptainUserRejectedRequest> InsertUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest);
        Task<CaptainUserRejectedRequest> UpdateUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest);
        Task<CaptainUserRejectedRequest> DeleteUserRejectedRequestAsync(long id);


        Task<List<CaptainUserRejectPenalty>> GetUsersRejectPenaltiesAsync();
        Task<CaptainUserRejectPenalty> GetUserRejectPenaltyByIdAsync(long id);
        Task<List<CaptainUserRejectPenalty>> GetUsersRejectPenaltiesByAsync(Expression<Func<CaptainUserRejectPenalty, bool>> predicate);
        Task<CaptainUserRejectPenalty> InsertUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty);
        Task<CaptainUserRejectPenalty> UpdateUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty);
        Task<CaptainUserRejectPenalty> DeleteUserRejectPenaltyAsync(long id);

        Task<List<CaptainUserShift>> GetUsersShiftsAsync();
        Task<CaptainUserShift> GetUserShiftByIdAsync(long id);
        Task<List<CaptainUserShift>> GetUsersShiftsByAsync(Expression<Func<CaptainUserShift, bool>> predicate);
        Task<CaptainUserShift> InsertUserShiftAsync(CaptainUserShift userShift);
        Task<CaptainUserShift> UpdateUserShiftAsync(CaptainUserShift userShift);
        Task<CaptainUserShift> DeleteUserShiftAsync(long id);


        Task<List<CaptainUserStatusHistory>> GetUsersStatusHistoriesAsync();
        Task<CaptainUserStatusHistory> GetUserStatusHistoryByIdAsync(long id);
        Task<List<CaptainUserStatusHistory>> GetUsersStatusHistoriesByAsync(Expression<Func<CaptainUserStatusHistory, bool>> predicate);
        Task<CaptainUserStatusHistory> InsertUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory);
        Task<CaptainUserStatusHistory> UpdateUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory);
        Task<CaptainUserStatusHistory> DeleteUserStatusHistoryAsync(long id);



        Task<List<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(long id);
        Task<List<CaptainUserVehicle>> GetUsersVehiclesAsync();
        Task<CaptainUserVehicle> GetUserVehicleByIdAsync(long id);
        Task<List<CaptainUserVehicle>> GetUsersVehiclesByAsync(Expression<Func<CaptainUserVehicle, bool>> predicate);
        Task<CaptainUserVehicle> InsertUserVehicleAsync(CaptainUserVehicle userVehicle);
        Task<CaptainUserVehicle> UpdateUserVehicleAsync(CaptainUserVehicle userVehicle);
        Task<CaptainUserVehicle> DeleteUserVehicleAsync(long id);

        IQueryable<CaptainUserRejectedRequest> GetUserRejectedRequestByQuerable(Expression<Func<CaptainUserRejectedRequest, bool>> predicate);
        IQueryable<CaptainUserAcceptedRequest> GetUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);
        IQueryable<CaptainUserIgnoredRequest> GetUserIgnoredRequestByQuerable(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate);

       // int GetAllRejectedRequestByQuerable();
        IQueryable<CaptainUserAcceptedRequest> GetAllAcceptedRequestByQuerable();
        //int GetAllIgnoredRequestByQuerable();

        IQueryable<CaptainUserRejectedRequest> GetAllRejectedRequestByQuerable();
        IQueryable<CaptainUserIgnoredRequest> GetAllIgnoredRequestByQuerable();
        IQueryable<CaptainUser> GetByQuerable();
        List<CaptainUser> GetByStatusType(long? statusTypeId, List<CaptainUser> query);
        IQueryable<CaptainUser> GetByStatusQuerableS(long? statusTypeId);
        Object UserReportCount();

        Task<Bonus> GetBonusByCountryAsync(long? countryId);
        Task<CaptainUserBonus> InsertBonusAsync(CaptainUserBonus userBonus);
        Task<List<Qrcode>> GetQRCodeByAsync(Expression<Func<Qrcode, bool>> predicate);
        Task<List<CaptainUserAcceptedRequest>> GetUserAcceptedRequestAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);

        Task<List<CaptainUserAccount>> GetActiveUsersAccountsPaginationAsync(int skip, int take);
        Task<List<CaptainUserAccount>> GetReviewingUsersAccountsPaginationAsync(int skip, int take);

    }

