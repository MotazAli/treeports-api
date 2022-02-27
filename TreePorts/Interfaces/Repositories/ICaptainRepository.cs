
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TreePorts.DTO.Records;
using TreePorts.Models;

namespace TreePorts.Interfaces.Repositories;

    public interface ICaptainRepository
    {
        Task<IEnumerable<CaptainUser>> GetCaptainUsersAsync();
        Task<CaptainUser?> GetCaptainUserByIdAsync(string id);

        Task<CaptainUserAccount?> GetCaptainUserAccountNearestLocationAsync(string pickupLatitude ,string pickupLongitude);
        Task<IEnumerable<NearCaptainUser>> GetCaptainsUsersNearToLocationAsync(string pickupLatitude ,string pickupLongitude);
        Task<IEnumerable<CaptainUser>> GetCaptainUsersByAsync(Expression<Func<CaptainUser, bool>> predicate);
        Task<CaptainUser> InsertCaptainUserAsync(CaptainUser user);
        Task<CaptainUser?> UpdateCaptainUserAsync(CaptainUser user);
        Task<CaptainUser?> DeleteCaptainUserAsync(string id);


        Task<IEnumerable<CaptainUserAccount>> GetCaptainUsersAccountsAsync();
        Task<CaptainUserAccount?> GetCaptainUserAccountByIdAsync(string id);
        Task<IEnumerable<CaptainUserAccount>> GetCaptainUsersAccountsByAsync(Expression<Func<CaptainUserAccount, bool>> predicate);
        Task<CaptainUserAccount> InsertCaptainUserAccountAsync(CaptainUserAccount account);
        Task<CaptainUserAccount?> UpdateCaptainUserAccountAsync(CaptainUserAccount account);
        Task<CaptainUserAccount?> DeleteCaptainUserAccountAsync(string id);
        IQueryable<CaptainUserAccount> GetCaptainUserAccountByQuerable(Expression<Func<CaptainUserAccount, bool>> predicate);
        IQueryable<CaptainUserAccount> GetCaptainUserAccountByQuerable();



        Task<IEnumerable<CaptainUserMessageHub>> GetCaptainUsersMessageHubsAsync();
        Task<CaptainUserMessageHub?> GetCaptainUserMessageHubByIdAsync(long id);
        Task<IEnumerable<CaptainUserMessageHub>> GetCaptainUsersMessageHubsByAsync(Expression<Func<CaptainUserMessageHub, bool>> predicate);
        Task<CaptainUserMessageHub> InsertCaptainUserMessageHubByCaptainUserAccountIdAsync(string id,string connectionId);
        Task<CaptainUserMessageHub?> UpdateCaptainUserMessageHubByCaptainUserAccountIdAsync(string id, string connectionId);



        Task<IEnumerable<CaptainUserNewRequest>> GetCaptainUsersNewRequestsAsync();
        Task<CaptainUserNewRequest?> GetCaptainUserNewRequestByIdAsync(long id);
        Task<IEnumerable<CaptainUserNewRequest>> GetCaptainUsersNewRequestsByAsync(Expression<Func<CaptainUserNewRequest, bool>> predicate);
        Task<CaptainUserNewRequest> InsertCaptainUserNewRequestAsync(CaptainUserNewRequest userRequest);
        Task<CaptainUserNewRequest?> UpdateCaptainUserNewRequestAsync(CaptainUserNewRequest userRequest);
        Task<CaptainUserNewRequest?> DeleteCaptainUserNewRequestAsync(long id);
        Task<CaptainUserNewRequest?> DeleteCaptainUserNewRequestByOrderIdAsync(long id);
        Task<IEnumerable<CaptainUserNewRequest>?> DeleteCaptainUserNewRequestByUserIdAsync(string id);

        Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUsersAcceptedRequestsAsync();
        Task<CaptainUserAcceptedRequest?> GetCaptainUserAcceptedRequestByIdAsync(long id);
        Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUsersAcceptedRequestsByAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);
        Task<CaptainUserAcceptedRequest> InsertCaptainUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest);
        Task<CaptainUserAcceptedRequest?> UpdateCaptainUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest);
        Task<CaptainUserAcceptedRequest?> DeleteCaptainUserAcceptedRequestAsync(long id);




		Task<IEnumerable<BoxType>> GetBoxTypesAsync();
        Task<BoxType?> GetBoxTypeByIdAsync(long id);
        Task<IEnumerable<CaptainUserBox>> GetCaptainUsersBoxesAsync();
        Task<CaptainUserBox?> GetCaptainUserBoxById(long id);
        Task<IEnumerable<CaptainUserBox>> GetCaptainUsersBoxesByAsync(Expression<Func<CaptainUserBox, bool>> predicate);
        Task<CaptainUserBox> InsertCaptainUserBoxAsync(CaptainUserBox userBox);
        Task<CaptainUserBox?> UpdateCaptainUserBoxAsync(CaptainUserBox userBox);
        Task<CaptainUserBox?> DeleteCaptainUserBoxAsync(long id);


		Task<IEnumerable<CaptainUserActivity>> GetCaptainUsersActivitiesAsync();
        Task<CaptainUserActivity?> GetCaptainUserActivityByIdAsync(long id);
        Task<IEnumerable<CaptainUserActivity>> GetCaptainUsersActivitiesByAsync(Expression<Func<CaptainUserActivity, bool>> predicate);
        Task<CaptainUserActivity> InsertCaptainUserActivityAsync(CaptainUserActivity userActivity);
        Task<CaptainUserActivity?> UpdateCaptainUserActivityAsync(CaptainUserActivity userActivity);
        Task<CaptainUserActivity?> DeleteCaptainUserActivityAsync(long id);




        Task<IEnumerable<CaptainUserCurrentBalance>> GetCaptainUsersCurrentBalancesAsync();
        Task<CaptainUserCurrentBalance?> GetCaptainUserCurrentBalanceByIdAsync(long id);
        Task<IEnumerable<CaptainUserCurrentBalance>> GetCaptainUsersCurrentBalancesByAsync(Expression<Func<CaptainUserCurrentBalance, bool>> predicate);
        Task<CaptainUserCurrentBalance> InsertCaptainUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance);
        Task<CaptainUserCurrentBalance?> UpdateCaptainUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance);
        Task<CaptainUserCurrentBalance?> DeleteCaptainUserCurrentBalanceAsync(long id);


        Task<IEnumerable<CaptainUserCurrentLocation>> GetCaptainUsersCurrentLocationsAsync();
        Task<CaptainUserCurrentLocation?> GetCaptainUserCurrentLocationByIdAsync(long id);
        Task<IEnumerable<CaptainUserCurrentLocation>> GetCaptainUsersCurrentLocationsByAsync(Expression<Func<CaptainUserCurrentLocation, bool>> predicate);
        Task<CaptainUserCurrentLocation> InsertCaptainUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation);
        Task<CaptainUserCurrentLocation?> UpdateCaptainUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation);
        Task<CaptainUserCurrentLocation?> DeleteCaptainUserCurrentLocationByCaptainUserAccountIdAsync(string id);


        Task<IEnumerable<CaptainUserCurrentStatus>> GetCaptainUsersCurrentStatusesAsync();
        Task<CaptainUserCurrentStatus?> GetCaptainUserCurrentStatusByIdAsync(long id);
        Task<IEnumerable<CaptainUserCurrentStatus>> GetCaptainUsersCurrentStatusesByAsync(Expression<Func<CaptainUserCurrentStatus, bool>> predicate);
        Task<CaptainUserCurrentStatus> InsertCaptainUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus);
        Task<CaptainUserCurrentStatus?> UpdateCaptainUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus);
        Task<CaptainUserCurrentStatus?> DeleteCaptainUserCurrentStatusAsync(long id);


        Task<IEnumerable<CaptainUserIgnoredPenalty>> GetCaptainUsersIgnoredPenaltiesAsync();
        Task<CaptainUserIgnoredPenalty?> GetCaptainUserIgnoredPenaltyByIdAsync(long id);
        Task<IEnumerable<CaptainUserIgnoredPenalty>> GetCaptainUsersIgnoredPenaltiesByAsync(Expression<Func<CaptainUserIgnoredPenalty, bool>> predicate);
        Task<CaptainUserIgnoredPenalty> InsertCaptainUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty);
        Task<CaptainUserIgnoredPenalty?> UpdateCaptainUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty);
        Task<CaptainUserIgnoredPenalty?> DeleteCaptainUserIgnoredPenaltyAsync(long id);

        
        Task<IEnumerable<CaptainUserIgnoredRequest>> GetCaptainUsersIgnoredRequestsAsync();
        Task<CaptainUserIgnoredRequest?> GetCaptainUserIgnoredRequestByIdAsync(long id);
        Task<IEnumerable<CaptainUserIgnoredRequest>> GetCaptainUsersIgnoredRequestsByAsync(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate);
        Task<CaptainUserIgnoredRequest> InsertCaptainUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest);
        Task<CaptainUserIgnoredRequest?> UpdateCaptainUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest);
        Task<CaptainUserIgnoredRequest?> DeleteCaptainUserIgnoredRequestAsync(long id);


        Task<IEnumerable<CaptainUserPayment>> GetCaptainUsersPaymentsAsync();
        Task<CaptainUserPayment?> GetCaptainUserPaymentByIdAsync(long id);
        Task<IEnumerable<CaptainUserPayment>> GetCaptainUsersPaymentsByAsync(Expression<Func<CaptainUserPayment, bool>> predicate);
        Task<CaptainUserPayment> InsertCaptainUserPaymentAsync(CaptainUserPayment userPayment);
        Task<CaptainUserPayment?> UpdateCaptainUserPaymentAsync(CaptainUserPayment userPayment);
        Task<CaptainUserPayment?> DeleteCaptainUserPaymentAsync(long id);
        Task<CaptainUserPayment?> DeleteCaptainUserPaymentByOrderIdAsync(long id);


       /* Task<IEnumerable<CaptainUserRejectedRequest>> GetCaptainUsersRejectedRequestsAsync();
        Task<CaptainUserRejectedRequest?> GetCaptainUserRejectedRequestByIdAsync(long id);
        Task<IEnumerable<CaptainUserRejectedRequest>> GetCaptainUsersRejectedRequestsByAsync(Expression<Func<CaptainUserRejectedRequest, bool>> predicate);
        Task<CaptainUserRejectedRequest> InsertCaptainUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest);
        Task<CaptainUserRejectedRequest?> UpdateCaptainUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest);
        Task<CaptainUserRejectedRequest?> DeleteCaptainUserRejectedRequestAsync(long id);


        Task<IEnumerable<CaptainUserRejectPenalty>> GetCaptainUsersRejectPenaltiesAsync();
        Task<CaptainUserRejectPenalty?> GetCaptainUserRejectPenaltyByIdAsync(long id);
        Task<IEnumerable<CaptainUserRejectPenalty>> GetCaptainUsersRejectPenaltiesByAsync(Expression<Func<CaptainUserRejectPenalty, bool>> predicate);
        Task<CaptainUserRejectPenalty> InsertCaptainUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty);
        Task<CaptainUserRejectPenalty?> UpdateCaptainUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty);
        Task<CaptainUserRejectPenalty?> DeleteCaptainUserRejectPenaltyAsync(long id);
*/
        Task<IEnumerable<CaptainUserShift>> GetCaptainUsersShiftsAsync();
        Task<CaptainUserShift?> GetCaptainUserShiftByIdAsync(long id);
        Task<IEnumerable<CaptainUserShift>> GetCaptainUsersShiftsByAsync(Expression<Func<CaptainUserShift, bool>> predicate);
        Task<CaptainUserShift> InsertCaptainUserShiftAsync(CaptainUserShift userShift);
        Task<CaptainUserShift?> UpdateCaptainUserShiftAsync(CaptainUserShift userShift);
        Task<CaptainUserShift?> DeleteCaptainUserShiftAsync(long id);


        Task<IEnumerable<CaptainUserStatusHistory>> GetCaptainUsersStatusHistoriesAsync();
        Task<CaptainUserStatusHistory?> GetCaptainUserStatusHistoryByIdAsync(long id);
        Task<IEnumerable<CaptainUserStatusHistory>> GetCaptainUsersStatusHistoriesByAsync(Expression<Func<CaptainUserStatusHistory, bool>> predicate);
        Task<CaptainUserStatusHistory> InsertCaptainUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory);
        Task<CaptainUserStatusHistory?> UpdateCaptainUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory);
        Task<CaptainUserStatusHistory?> DeleteCaptainUserStatusHistoryAsync(long id);



        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<Vehicle?> GetVehicleByIdAsync(long id);
        Task<IEnumerable<CaptainUserVehicle>> GetCaptainUsersVehiclesAsync();
        Task<CaptainUserVehicle?> GetCaptainUserVehicleByIdAsync(long id);
        Task<IEnumerable<CaptainUserVehicle>> GetCaptainUsersVehiclesByAsync(Expression<Func<CaptainUserVehicle, bool>> predicate);
        Task<CaptainUserVehicle> InsertCaptainUserVehicleAsync(CaptainUserVehicle userVehicle);
        Task<CaptainUserVehicle?> UpdateCaptainUserVehicleAsync(CaptainUserVehicle userVehicle);
        Task<CaptainUserVehicle?> DeleteCaptainUserVehicleAsync(long id);

        //IQueryable<CaptainUserRejectedRequest> GetCaptainUserRejectedRequestByQuerable(Expression<Func<CaptainUserRejectedRequest, bool>> predicate);
        IQueryable<CaptainUserAcceptedRequest> GetCaptainUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);
        IQueryable<CaptainUserIgnoredRequest> GetCaptainUserIgnoredRequestByQuerable(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate);

       // int GetAllRejectedRequestByQuerable();
        //IQueryable<CaptainUserAcceptedRequest> GetAllAcceptedRequestByQuerable();
        //int GetAllIgnoredRequestByQuerable();

        //IQueryable<CaptainUserRejectedRequest> GetAllRejectedRequestByQuerable();
        //IQueryable<CaptainUserIgnoredRequest> GetAllIgnoredRequestByQuerable();
        //IQueryable<CaptainUser> GetByQuerable();
        //IEnumerable<CaptainUser> GetCaptainUserByStatusType(long? statusTypeId, IEnumerable<CaptainUser> query);
        //IQueryable<CaptainUser> GetCaptainUserByStatusQuerableS(long? statusTypeId);
        Object UserReportCount();

        Task<Bonus?> GetBonusByCountryAsync(long? countryId);
        Task<CaptainUserBonus> InsertCaptainUserBonusAsync(CaptainUserBonus userBonus);
        Task<IEnumerable<OrderQrcode>> GetOrderQRCodeByAsync(Expression<Func<OrderQrcode, bool>> predicate);
        Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUserAcceptedRequestAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);

        Task<IEnumerable<CaptainUserAccount>> GetActiveCaptainUsersAccountsPaginationAsync(int skip, int take);
        Task<IEnumerable<CaptainUserAccount>> GetReviewingCaptainUsersAccountsPaginationAsync(int skip, int take);

    }

