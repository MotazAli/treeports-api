using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreePorts.Interfaces.Repositories
{
    public interface ISystemRepository
    {
        Task<List<SystemSetting>> GetSystemSettingsAsync(CancellationToken cancellationToken);
        //Task<List<RejectPerType>> GetRejectPerTypesAsync(,CancellationToken cancellationToken);
        Task<List<IgnorPerType>> GetIgnorPerTypesAsync(CancellationToken cancellationToken);
        Task<List<PenaltyPerType>> GetPenaltyPerTypesAsync(CancellationToken cancellationToken);

        Task<List<PenaltyStatusType>> GetPeniltyStatusTypesAsync(CancellationToken cancellationToken);

        Task<SystemSetting?> GetSystemSettingByIdAsync(long id,CancellationToken cancellationToken);

        Task<SystemSetting?> GetCurrentSystemSettingAsync(CancellationToken cancellationToken);
        Task<List<SystemSetting>> GetSystemSettingsByAsync(Expression<Func<SystemSetting, bool>> predicate,CancellationToken cancellationToken);
        Task<SystemSetting> InsertSystemSettingAsync(SystemSetting systemSetting,CancellationToken cancellationToken);

        Task<SystemSetting?> DeleteSystemSettingAsync(long id,CancellationToken cancellationToken);

        Task<CaptainUserIgnoredPenalty?> GetCaptainUserIgnoredPenaltyByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
        //Task<CaptainUserRejectPenalty?> GetCaptainUserRejectedPenaltyByCaptainUserAccountIdAsync(long captainUserAccountId,CancellationToken cancellationToken);

        Task<List<CaptainUserIgnoredPenalty>?> GetCaptainUserIgnoredPenaltiesByCaptainUserAccountIdAsync(string captainUserAccountId,CancellationToken cancellationToken);
        //Task<List<CaptainUserRejectPenalty>?> GetCaptainUserRejectedPenaltiesByCaptainUserAccountIdAsync(long captainUserAccountId,CancellationToken cancellationToken);


        Task<CaptainUserIgnoredPenalty?> InsertCaptainUserIgnoredPenaltyAsync(string captainUserAccountId,CancellationToken cancellationToken);
        //Task<CaptainUserRejectPenalty?> InsertCaptainUserRejectedPenaltyAsync(string captainUserAccountId,CancellationToken cancellationToken);

        Task<CaptainUserIgnoredPenalty?> UpdateCaptainUserIgnoredPenaltyAsync(string captainUserAccountId, long penaltyStatusType,CancellationToken cancellationToken);
        //Task<CaptainUserRejectPenalty?> UpdateCaptainUserRejectedPenaltyAsync(string captainUserAccountId, long penaltyStatusType,CancellationToken cancellationToken);


        Task<List<Shift>> GetShiftsAsync(CancellationToken cancellationToken);
        Task<Shift?> GetShiftByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<Shift>> GetShiftsByAsync(Expression<Func<Shift, bool>> predicate,CancellationToken cancellationToken);
        Task<List<Shift>> GetShiftsByShiftDateAsync(Shift shift,CancellationToken cancellationToken);
        Task<Shift> InsertShiftAsync(Shift shift,CancellationToken cancellationToken);
        Task<Shift?> UpdateShiftAsync(Shift shift,CancellationToken cancellationToken);
        Task<Shift?> DeleteShiftAsync(long id,CancellationToken cancellationToken);

        Task<List<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken);
        Task<Vehicle?> GetVehicleByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<Vehicle>> GetVehiclesByAsync(Expression<Func<Vehicle, bool>> predicate,CancellationToken cancellationToken);

        Task<List<BoxType>> GetBoxTypesAsync(CancellationToken cancellationToken);
        Task<BoxType?> GetBoxTypeByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<BoxType>> GetBoxTypesByAsync(Expression<Func<BoxType, bool>> predicate,CancellationToken cancellationToken);


        Task<List<ContactMessage>> GetContactMessagesAsync(CancellationToken cancellationToken);
        Task<ContactMessage?> GetContactMessageByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<ContactMessage>> GetContactMessagesByAsync(Expression<Func<ContactMessage, bool>> predicate,CancellationToken cancellationToken);
        Task<ContactMessage> InsertContactMessageAsync(ContactMessage contactMessage,CancellationToken cancellationToken);
        Task<ContactMessage?> UpdateContactMessageAsync(ContactMessage contactMessage,CancellationToken cancellationToken);
        Task<ContactMessage?> DeleteContactMessageAsync(long id,CancellationToken cancellationToken);

        Task<List<PromotionType>> GetPromotionTypesAsync(CancellationToken cancellationToken);
        Task<PromotionType?> GetPromotionTypeByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<PromotionType>> GetPromotionTypesByAsync(Expression<Func<PromotionType, bool>> predicate,CancellationToken cancellationToken);
        Task<PromotionType> InsertPromotionTypeAsync(PromotionType promotionType,CancellationToken cancellationToken);
        Task<PromotionType?> UpdatePromotionTypeAsync(PromotionType promotionType,CancellationToken cancellationToken);
        Task<PromotionType?> DeletePromotionTypeAsync(long id,CancellationToken cancellationToken);


        Task<List<Promotion>> GetPromotionsAsync(CancellationToken cancellationToken);
        Task<Promotion?> GetPromotionByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<Promotion>> GetPromotionsPaginationAsync(int skip, int take,CancellationToken cancellationToken);
        Task<List<Promotion>> GetPromotionsByAsync(Expression<Func<Promotion, bool>> predicate,CancellationToken cancellationToken);
        Task<Promotion> InsertPromotionAsync(Promotion promotion,CancellationToken cancellationToken);
        Task<Promotion?> UpdatePromotionAsync(Promotion promotion,CancellationToken cancellationToken);
        Task<Promotion?> DeletePromotionAsync(long id,CancellationToken cancellationToken);


        Task<List<AndroidVersion>> GetAndroidVersionsAsync(CancellationToken cancellationToken);
        Task<AndroidVersion?> GetAndroidVersionByIdAsync(long id,CancellationToken cancellationToken);
        Task<List<AndroidVersion>> GetAndroidVersionsPaginationAsync(int skip,int take,CancellationToken cancellationToken);
        Task<AndroidVersion?> GetCurrentAndroidVersionAsync(CancellationToken cancellationToken);

        Task<List<AndroidVersion>> GetAndroidVersionsByAsync(Expression<Func<AndroidVersion, bool>> predicate,CancellationToken cancellationToken);
        Task<AndroidVersion> InsertAndroidVersionAsync(AndroidVersion androidVersion,CancellationToken cancellationToken);
        Task<AndroidVersion?> UpdateAndroidVersionAsync(AndroidVersion androidVersion,CancellationToken cancellationToken);
        Task<AndroidVersion?> DeleteAndroidVersionAsync(long id,CancellationToken cancellationToken);

    }
}
