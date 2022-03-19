using System.Linq.Expressions;
using TreePorts.DTO.Records;

namespace TreePorts.Interfaces.Repositories;

    public interface ICaptainRepository
    {
        Task<IEnumerable<CaptainUser>> GetCaptainUsersAsync(CancellationToken cancellationToken);
        Task<CaptainUser?> GetCaptainUserByIdAsync(string id,CancellationToken cancellationToken);

        Task<CaptainUserAccount?> GetCaptainUserAccountNearestLocationAsync(string pickupLatitude ,string pickupLongitude,CancellationToken cancellationToken);
        Task<IEnumerable<NearCaptainUser>> GetCaptainsUsersNearToLocationAsync(string pickupLatitude ,string pickupLongitude,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUser>> GetCaptainUsersByAsync(Expression<Func<CaptainUser, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUser> InsertCaptainUserAsync(CaptainUser user,CancellationToken cancellationToken);
        Task<CaptainUser?> UpdateCaptainUserAsync(CaptainUser user,CancellationToken cancellationToken);
        Task<CaptainUser?> DeleteCaptainUserAsync(string id,CancellationToken cancellationToken);


        Task<IEnumerable<CaptainUserAccount>> GetCaptainUsersAccountsAsync(CancellationToken cancellationToken);
        Task<CaptainUserAccount?> GetCaptainUserAccountByIdAsync(string id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserAccount>> GetCaptainUsersAccountsByAsync(Expression<Func<CaptainUserAccount, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserAccount> InsertCaptainUserAccountAsync(CaptainUserAccount account,CancellationToken cancellationToken);
        Task<CaptainUserAccount?> UpdateCaptainUserAccountAsync(CaptainUserAccount account,CancellationToken cancellationToken);
        Task<CaptainUserAccount?> DeleteCaptainUserAccountAsync(string id,CancellationToken cancellationToken);
        IQueryable<CaptainUserAccount> GetCaptainUserAccountByQuerable(Expression<Func<CaptainUserAccount, bool>> predicate);
        IQueryable<CaptainUserAccount> GetCaptainUserAccountByQuerable();



        Task<IEnumerable<CaptainUserMessageHub>> GetCaptainUsersMessageHubsAsync(CancellationToken cancellationToken);
        Task<CaptainUserMessageHub?> GetCaptainUserMessageHubByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserMessageHub>> GetCaptainUsersMessageHubsByAsync(Expression<Func<CaptainUserMessageHub, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserMessageHub> InsertCaptainUserMessageHubByCaptainUserAccountIdAsync(string id,string connectionId,CancellationToken cancellationToken);
        Task<CaptainUserMessageHub?> UpdateCaptainUserMessageHubByCaptainUserAccountIdAsync(string id, string connectionId,CancellationToken cancellationToken);



        Task<IEnumerable<CaptainUserNewRequest>> GetCaptainUsersNewRequestsAsync(CancellationToken cancellationToken);
        Task<CaptainUserNewRequest?> GetCaptainUserNewRequestByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserNewRequest>> GetCaptainUsersNewRequestsByAsync(Expression<Func<CaptainUserNewRequest, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserNewRequest> InsertCaptainUserNewRequestAsync(CaptainUserNewRequest userRequest,CancellationToken cancellationToken);
        Task<CaptainUserNewRequest?> UpdateCaptainUserNewRequestAsync(CaptainUserNewRequest userRequest,CancellationToken cancellationToken);
        Task<CaptainUserNewRequest?> DeleteCaptainUserNewRequestAsync(long id,CancellationToken cancellationToken);
        Task<CaptainUserNewRequest?> DeleteCaptainUserNewRequestByOrderIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserNewRequest>?> DeleteCaptainUserNewRequestByUserIdAsync(string id,CancellationToken cancellationToken);

        Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUsersAcceptedRequestsAsync(CancellationToken cancellationToken);
        Task<CaptainUserAcceptedRequest?> GetCaptainUserAcceptedRequestByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUsersAcceptedRequestsByAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserAcceptedRequest> InsertCaptainUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest,CancellationToken cancellationToken);
        Task<CaptainUserAcceptedRequest?> UpdateCaptainUserAcceptedRequestAsync(CaptainUserAcceptedRequest userAcceptedRequest,CancellationToken cancellationToken);
        Task<CaptainUserAcceptedRequest?> DeleteCaptainUserAcceptedRequestAsync(long id,CancellationToken cancellationToken);




		Task<IEnumerable<BoxType>> GetBoxTypesAsync(CancellationToken cancellationToken);
        Task<BoxType?> GetBoxTypeByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserBox>> GetCaptainUsersBoxesAsync(CancellationToken cancellationToken);
        Task<CaptainUserBox?> GetCaptainUserBoxById(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserBox>> GetCaptainUsersBoxesByAsync(Expression<Func<CaptainUserBox, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserBox> InsertCaptainUserBoxAsync(CaptainUserBox userBox,CancellationToken cancellationToken);
        Task<CaptainUserBox?> UpdateCaptainUserBoxAsync(CaptainUserBox userBox,CancellationToken cancellationToken);
        Task<CaptainUserBox?> DeleteCaptainUserBoxAsync(long id,CancellationToken cancellationToken);


		Task<IEnumerable<CaptainUserActivity>> GetCaptainUsersActivitiesAsync(CancellationToken cancellationToken);
        Task<CaptainUserActivity?> GetCaptainUserActivityByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserActivity>> GetCaptainUsersActivitiesByAsync(Expression<Func<CaptainUserActivity, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserActivity> InsertCaptainUserActivityAsync(CaptainUserActivity userActivity,CancellationToken cancellationToken);
        Task<CaptainUserActivity?> UpdateCaptainUserActivityAsync(CaptainUserActivity userActivity,CancellationToken cancellationToken);
        Task<CaptainUserActivity?> DeleteCaptainUserActivityAsync(long id,CancellationToken cancellationToken);




        Task<IEnumerable<CaptainUserCurrentBalance>> GetCaptainUsersCurrentBalancesAsync(CancellationToken cancellationToken);
        Task<CaptainUserCurrentBalance?> GetCaptainUserCurrentBalanceByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserCurrentBalance>> GetCaptainUsersCurrentBalancesByAsync(Expression<Func<CaptainUserCurrentBalance, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserCurrentBalance> InsertCaptainUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance,CancellationToken cancellationToken);
        Task<CaptainUserCurrentBalance?> UpdateCaptainUserCurrentBalanceAsync(CaptainUserCurrentBalance userCurrentBalance,CancellationToken cancellationToken);
        Task<CaptainUserCurrentBalance?> DeleteCaptainUserCurrentBalanceAsync(long id,CancellationToken cancellationToken);


        Task<IEnumerable<CaptainUserCurrentLocation>> GetCaptainUsersCurrentLocationsAsync(CancellationToken cancellationToken);
        Task<CaptainUserCurrentLocation?> GetCaptainUserCurrentLocationByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserCurrentLocation>> GetCaptainUsersCurrentLocationsByAsync(Expression<Func<CaptainUserCurrentLocation, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserCurrentLocation> InsertCaptainUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation,CancellationToken cancellationToken);
        Task<CaptainUserCurrentLocation?> UpdateCaptainUserCurrentLocationAsync(CaptainUserCurrentLocation userCurrentLocation,CancellationToken cancellationToken);
        Task<CaptainUserCurrentLocation?> DeleteCaptainUserCurrentLocationByCaptainUserAccountIdAsync(string id,CancellationToken cancellationToken);


        Task<IEnumerable<CaptainUserCurrentStatus>> GetCaptainUsersCurrentStatusesAsync(CancellationToken cancellationToken);
        Task<CaptainUserCurrentStatus?> GetCaptainUserCurrentStatusByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserCurrentStatus>> GetCaptainUsersCurrentStatusesByAsync(Expression<Func<CaptainUserCurrentStatus, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserCurrentStatus> InsertCaptainUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus,CancellationToken cancellationToken);
        Task<CaptainUserCurrentStatus?> UpdateCaptainUserCurrentStatusAsync(CaptainUserCurrentStatus userCurrentStatus,CancellationToken cancellationToken);
        Task<CaptainUserCurrentStatus?> DeleteCaptainUserCurrentStatusAsync(long id,CancellationToken cancellationToken);


        Task<IEnumerable<CaptainUserIgnoredPenalty>> GetCaptainUsersIgnoredPenaltiesAsync(CancellationToken cancellationToken);
        Task<CaptainUserIgnoredPenalty?> GetCaptainUserIgnoredPenaltyByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserIgnoredPenalty>> GetCaptainUsersIgnoredPenaltiesByAsync(Expression<Func<CaptainUserIgnoredPenalty, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserIgnoredPenalty> InsertCaptainUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty,CancellationToken cancellationToken);
        Task<CaptainUserIgnoredPenalty?> UpdateCaptainUserIgnoredPenaltyAsync(CaptainUserIgnoredPenalty userIgnoredPenalty,CancellationToken cancellationToken);
        Task<CaptainUserIgnoredPenalty?> DeleteCaptainUserIgnoredPenaltyAsync(long id,CancellationToken cancellationToken);

        
        Task<IEnumerable<CaptainUserIgnoredRequest>> GetCaptainUsersIgnoredRequestsAsync(CancellationToken cancellationToken);
        Task<CaptainUserIgnoredRequest?> GetCaptainUserIgnoredRequestByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserIgnoredRequest>> GetCaptainUsersIgnoredRequestsByAsync(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserIgnoredRequest> InsertCaptainUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest,CancellationToken cancellationToken);
        Task<CaptainUserIgnoredRequest?> UpdateCaptainUserIgnoredRequestAsync(CaptainUserIgnoredRequest userIgnoredRequest,CancellationToken cancellationToken);
        Task<CaptainUserIgnoredRequest?> DeleteCaptainUserIgnoredRequestAsync(long id,CancellationToken cancellationToken);


        Task<IEnumerable<CaptainUserPayment>> GetCaptainUsersPaymentsAsync(CancellationToken cancellationToken);
        Task<CaptainUserPayment?> GetCaptainUserPaymentByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserPayment>> GetCaptainUsersPaymentsByAsync(Expression<Func<CaptainUserPayment, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserPayment> InsertCaptainUserPaymentAsync(CaptainUserPayment userPayment,CancellationToken cancellationToken);
        Task<CaptainUserPayment?> UpdateCaptainUserPaymentAsync(CaptainUserPayment userPayment,CancellationToken cancellationToken);
        Task<CaptainUserPayment?> DeleteCaptainUserPaymentAsync(long id,CancellationToken cancellationToken);
        Task<CaptainUserPayment?> DeleteCaptainUserPaymentByOrderIdAsync(long id,CancellationToken cancellationToken);


       /* Task<IEnumerable<CaptainUserRejectedRequest>> GetCaptainUsersRejectedRequestsAsync(,CancellationToken cancellationToken);
        Task<CaptainUserRejectedRequest?> GetCaptainUserRejectedRequestByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserRejectedRequest>> GetCaptainUsersRejectedRequestsByAsync(Expression<Func<CaptainUserRejectedRequest, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserRejectedRequest> InsertCaptainUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest,CancellationToken cancellationToken);
        Task<CaptainUserRejectedRequest?> UpdateCaptainUserRejectedRequestAsync(CaptainUserRejectedRequest userRejectedRequest,CancellationToken cancellationToken);
        Task<CaptainUserRejectedRequest?> DeleteCaptainUserRejectedRequestAsync(long id,CancellationToken cancellationToken);


        Task<IEnumerable<CaptainUserRejectPenalty>> GetCaptainUsersRejectPenaltiesAsync(,CancellationToken cancellationToken);
        Task<CaptainUserRejectPenalty?> GetCaptainUserRejectPenaltyByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserRejectPenalty>> GetCaptainUsersRejectPenaltiesByAsync(Expression<Func<CaptainUserRejectPenalty, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserRejectPenalty> InsertCaptainUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty,CancellationToken cancellationToken);
        Task<CaptainUserRejectPenalty?> UpdateCaptainUserRejectPenaltyAsync(CaptainUserRejectPenalty userRejectPenalty,CancellationToken cancellationToken);
        Task<CaptainUserRejectPenalty?> DeleteCaptainUserRejectPenaltyAsync(long id,CancellationToken cancellationToken);
*/
        Task<IEnumerable<CaptainUserShift>> GetCaptainUsersShiftsAsync(CancellationToken cancellationToken);
        Task<CaptainUserShift?> GetCaptainUserShiftByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserShift>> GetCaptainUsersShiftsByAsync(Expression<Func<CaptainUserShift, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserShift> InsertCaptainUserShiftAsync(CaptainUserShift userShift,CancellationToken cancellationToken);
        Task<CaptainUserShift?> UpdateCaptainUserShiftAsync(CaptainUserShift userShift,CancellationToken cancellationToken);
        Task<CaptainUserShift?> DeleteCaptainUserShiftAsync(long id,CancellationToken cancellationToken);


        Task<IEnumerable<CaptainUserStatusHistory>> GetCaptainUsersStatusHistoriesAsync(CancellationToken cancellationToken);
        Task<CaptainUserStatusHistory?> GetCaptainUserStatusHistoryByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserStatusHistory>> GetCaptainUsersStatusHistoriesByAsync(Expression<Func<CaptainUserStatusHistory, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserStatusHistory> InsertCaptainUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory,CancellationToken cancellationToken);
        Task<CaptainUserStatusHistory?> UpdateCaptainUserStatusHistoryAsync(CaptainUserStatusHistory userStatusHistory,CancellationToken cancellationToken);
        Task<CaptainUserStatusHistory?> DeleteCaptainUserStatusHistoryAsync(long id,CancellationToken cancellationToken);



        Task<IEnumerable<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken);
        Task<Vehicle?> GetVehicleByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserVehicle>> GetCaptainUsersVehiclesAsync(CancellationToken cancellationToken);
        Task<CaptainUserVehicle?> GetCaptainUserVehicleByIdAsync(long id,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserVehicle>> GetCaptainUsersVehiclesByAsync(Expression<Func<CaptainUserVehicle, bool>> predicate,CancellationToken cancellationToken);
        Task<CaptainUserVehicle> InsertCaptainUserVehicleAsync(CaptainUserVehicle userVehicle,CancellationToken cancellationToken);
        Task<CaptainUserVehicle?> UpdateCaptainUserVehicleAsync(CaptainUserVehicle userVehicle,CancellationToken cancellationToken);
        Task<CaptainUserVehicle?> DeleteCaptainUserVehicleAsync(long id,CancellationToken cancellationToken);

        //IQueryable<CaptainUserRejectedRequest> GetCaptainUserRejectedRequestByQuerable(Expression<Func<CaptainUserRejectedRequest, bool>> predicate,CancellationToken cancellationToken);
        IQueryable<CaptainUserAcceptedRequest> GetCaptainUserAcceptedRequestByQuerable(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate);
        IQueryable<CaptainUserIgnoredRequest> GetCaptainUserIgnoredRequestByQuerable(Expression<Func<CaptainUserIgnoredRequest, bool>> predicate);

       // int GetAllRejectedRequestByQuerable(,CancellationToken cancellationToken);
        //IQueryable<CaptainUserAcceptedRequest> GetAllAcceptedRequestByQuerable(,CancellationToken cancellationToken);
        //int GetAllIgnoredRequestByQuerable(,CancellationToken cancellationToken);

        //IQueryable<CaptainUserRejectedRequest> GetAllRejectedRequestByQuerable(,CancellationToken cancellationToken);
        //IQueryable<CaptainUserIgnoredRequest> GetAllIgnoredRequestByQuerable(,CancellationToken cancellationToken);
        //IQueryable<CaptainUser> GetByQuerable(,CancellationToken cancellationToken);
        //IEnumerable<CaptainUser> GetCaptainUserByStatusType(long? statusTypeId, IEnumerable<CaptainUser> query,CancellationToken cancellationToken);
        //IQueryable<CaptainUser> GetCaptainUserByStatusQuerableS(long? statusTypeId,CancellationToken cancellationToken);
        Task<object> UserReportCount(CancellationToken cancellationToken);

        Task<Bonus?> GetBonusByCountryAsync(long? countryId,CancellationToken cancellationToken);
        Task<CaptainUserBonus> InsertCaptainUserBonusAsync(CaptainUserBonus userBonus,CancellationToken cancellationToken);
        Task<IEnumerable<OrderQrcode>> GetOrderQRCodeByAsync(Expression<Func<OrderQrcode, bool>> predicate,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserAcceptedRequest>> GetCaptainUserAcceptedRequestAsync(Expression<Func<CaptainUserAcceptedRequest, bool>> predicate,CancellationToken cancellationToken);

        Task<IEnumerable<CaptainUserAccount>> GetActiveCaptainUsersAccountsPaginationAsync(int skip, int take,CancellationToken cancellationToken);
        Task<IEnumerable<CaptainUserAccount>> GetReviewingCaptainUsersAccountsPaginationAsync(int skip, int take,CancellationToken cancellationToken);

    }

