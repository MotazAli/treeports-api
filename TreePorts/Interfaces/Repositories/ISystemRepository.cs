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
        Task<List<SystemSetting>> GetSystemSettingsAsync();
        //Task<List<RejectPerType>> GetRejectPerTypesAsync();
        Task<List<IgnorPerType>> GetIgnorPerTypesAsync();
        Task<List<PenaltyPerType>> GetPenaltyPerTypesAsync();

        Task<List<PenaltyStatusType>> GetPeniltyStatusTypesAsync();

        Task<SystemSetting?> GetSystemSettingByIdAsync(long id);

        Task<SystemSetting?> GetCurrentSystemSettingAsync();
        Task<List<SystemSetting>> GetSystemSettingsByAsync(Expression<Func<SystemSetting, bool>> predicate);
        Task<SystemSetting> InsertSystemSettingAsync(SystemSetting systemSetting);

        Task<SystemSetting?> DeleteSystemSettingAsync(long id);

        Task<CaptainUserIgnoredPenalty?> GetCaptainUserIgnoredPenaltyByCaptainUserAccountIdAsync(string captainUserAccountId);
        //Task<CaptainUserRejectPenalty?> GetCaptainUserRejectedPenaltyByCaptainUserAccountIdAsync(long captainUserAccountId);

        Task<List<CaptainUserIgnoredPenalty>?> GetCaptainUserIgnoredPenaltiesByCaptainUserAccountIdAsync(string captainUserAccountId);
        //Task<List<CaptainUserRejectPenalty>?> GetCaptainUserRejectedPenaltiesByCaptainUserAccountIdAsync(long captainUserAccountId);


        Task<CaptainUserIgnoredPenalty?> InsertCaptainUserIgnoredPenaltyAsync(string captainUserAccountId);
        //Task<CaptainUserRejectPenalty?> InsertCaptainUserRejectedPenaltyAsync(string captainUserAccountId);

        Task<CaptainUserIgnoredPenalty?> UpdateCaptainUserIgnoredPenaltyAsync(string captainUserAccountId, long penaltyStatusType);
        //Task<CaptainUserRejectPenalty?> UpdateCaptainUserRejectedPenaltyAsync(string captainUserAccountId, long penaltyStatusType);


        Task<List<Shift>> GetShiftsAsync();
        Task<Shift?> GetShiftByIdAsync(long id);
        Task<List<Shift>> GetShiftsByAsync(Expression<Func<Shift, bool>> predicate);
        Task<List<Shift>> GetShiftsByShiftDateAsync(Shift shift);
        Task<Shift> InsertShiftAsync(Shift shift);
        Task<Shift?> UpdateShiftAsync(Shift shift);
        Task<Shift?> DeleteShiftAsync(long id);

        Task<List<Vehicle>> GetVehiclesAsync();
        Task<Vehicle?> GetVehicleByIdAsync(long id);
        Task<List<Vehicle>> GetVehiclesByAsync(Expression<Func<Vehicle, bool>> predicate);

        Task<List<BoxType>> GetBoxTypesAsync();
        Task<BoxType?> GetBoxTypeByIdAsync(long id);
        Task<List<BoxType>> GetBoxTypesByAsync(Expression<Func<BoxType, bool>> predicate);


        Task<List<ContactMessage>> GetContactMessagesAsync();
        Task<ContactMessage?> GetContactMessageByIdAsync(long id);
        Task<List<ContactMessage>> GetContactMessagesByAsync(Expression<Func<ContactMessage, bool>> predicate);
        Task<ContactMessage> InsertContactMessageAsync(ContactMessage contactMessage);
        Task<ContactMessage?> UpdateContactMessageAsync(ContactMessage contactMessage);
        Task<ContactMessage?> DeleteContactMessageAsync(long id);

        Task<List<PromotionType>> GetPromotionTypesAsync();
        Task<PromotionType?> GetPromotionTypeByIdAsync(long id);
        Task<List<PromotionType>> GetPromotionTypesByAsync(Expression<Func<PromotionType, bool>> predicate);
        Task<PromotionType> InsertPromotionTypeAsync(PromotionType promotionType);
        Task<PromotionType?> UpdatePromotionTypeAsync(PromotionType promotionType);
        Task<PromotionType?> DeletePromotionTypeAsync(long id);


        Task<List<Promotion>> GetPromotionsAsync();
        Task<Promotion?> GetPromotionByIdAsync(long id);
        Task<List<Promotion>> GetPromotionsPaginationAsync(int skip, int take);
        Task<List<Promotion>> GetPromotionsByAsync(Expression<Func<Promotion, bool>> predicate);
        Task<Promotion> InsertPromotionAsync(Promotion promotion);
        Task<Promotion?> UpdatePromotionAsync(Promotion promotion);
        Task<Promotion?> DeletePromotionAsync(long id);


        Task<List<AndroidVersion>> GetAndroidVersionsAsync();
        Task<AndroidVersion?> GetAndroidVersionByIdAsync(long id);
        Task<List<AndroidVersion>> GetAndroidVersionsPaginationAsync(int skip,int take);
        Task<AndroidVersion?> GetCurrentAndroidVersionAsync();

        Task<List<AndroidVersion>> GetAndroidVersionsByAsync(Expression<Func<AndroidVersion, bool>> predicate);
        Task<AndroidVersion> InsertAndroidVersionAsync(AndroidVersion androidVersion);
        Task<AndroidVersion?> UpdateAndroidVersionAsync(AndroidVersion androidVersion);
        Task<AndroidVersion?> DeleteAndroidVersionAsync(long id);

    }
}
