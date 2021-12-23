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
    public class SystemRepository : ISystemRepository
    {
        private TreePortsDBContext _context;

        public SystemRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<SystemSetting> DeleteSystemSettingAsync(long id)
        {
            var settings = await _context.SystemSettings.FirstOrDefaultAsync(c => c.Id == id);
            if (settings == null) return null;

            settings.IsDeleted = true;
            _context.Entry<SystemSetting>(settings).State = EntityState.Modified;
            return settings;
        }

        public async Task<List<SystemSetting>> GetSystemSettingsAsync()
        {
            return await _context.SystemSettings.ToListAsync();
        }

        public async Task<List<CaptainUserIgnoredPenalty>> GetUserIgnoredPenaltiesByUserIdAsync(long userId)
        {
            return await _context.UserIgnoredPenalties.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<List<SystemSetting>> GetSystemSettingsByAsync(Expression<Func<SystemSetting, bool>> predicate)
        {
            return await _context.SystemSettings.Where(predicate).ToListAsync();
        }

        public async Task<SystemSetting> GetSystemSettingByIdAsync(long id)
        {
             return await _context.SystemSettings.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SystemSetting> GetCurrentSystemSettingAsync()
        {
            return await _context.SystemSettings.FirstOrDefaultAsync(s => s.IsCurrent == true && s.IsDeleted == false );
        }

        public async Task<CaptainUserIgnoredPenalty> GetUserIgnoredPenaltyByUserIdAsync(long userId)
        {
            return await _context.UserIgnoredPenalties.FirstOrDefaultAsync(u => 
            u.UserId == userId && 
            (u.PenaltyStatusTypeId == (long) PenaltyStatusTypes.New ));
        }

        public async Task<CaptainUserRejectPenalty> GetUserRejectedPenaltyByUserIdAsync(long userId)
        {
            return await _context.UserRejectPenalties.FirstOrDefaultAsync(u => 
            u.UserId == userId &&
            (u.PenaltyStatusTypeId == (long)PenaltyStatusTypes.New ));
        }

        public async Task<List<IgnorPerType>> GetIgnorPerTypesAsync()
        {
            return await _context.IgnorPerTypes.ToListAsync();
        }

        public async Task<List<PenaltyPerType>> GetPenaltyPerTypesAsync()
        {
            return await _context.PenaltyPerTypes.ToListAsync();
        }

        public async Task<List<PenaltyStatusType>> GetPeniltyStatusTypesAsync()
        {
            return await _context.PenaltyStatusTypes.ToListAsync();
        }

        public async Task<List<RejectPerType>> GetRejectPerTypesAsync()
        {
            return await _context.RejectPerTypes.ToListAsync();
        }

        public async Task<List<CaptainUserRejectPenalty>> GetUserRejectedPenaltiesByUserIdAsync(long userId)
        {
            return await _context.UserRejectPenalties.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<SystemSetting> InsertSystemSettingAsync(SystemSetting systemSetting)
        {
            var oldSystem = await _context.SystemSettings.FirstOrDefaultAsync(s => s.IsCurrent == true );
            if (oldSystem != null) {
                oldSystem.IsCurrent = false;
                oldSystem.ModifiedBy = systemSetting.CreatedBy;
                oldSystem.ModificationDate = DateTime.Now;
                _context.Entry<SystemSetting>(oldSystem).State = EntityState.Modified;
            }

            systemSetting.Id = 0;
            systemSetting.IsCurrent = true;
            systemSetting.IsDeleted = false;
            systemSetting.CreationDate = DateTime.Now;
            var result = await _context.SystemSettings.AddAsync(systemSetting);
            return result.Entity;

        }

        public async Task<CaptainUserIgnoredPenalty> InsertUserIgnoredPenaltyAsync(long userId)
        {

            var system = await this.GetCurrentSystemSettingAsync();
            CaptainUserIgnoredPenalty penalty = new CaptainUserIgnoredPenalty()
            {
                UserId = userId,
                SystemSettingId = system.Id,
                PenaltyStatusTypeId =(long) PenaltyStatusTypes.New,
                CreationDate = DateTime.Now
            };

            var result = await _context.UserIgnoredPenalties.AddAsync(penalty);
            return result.Entity;

        }

        public async Task<CaptainUserRejectPenalty> InsertUserRejectedPenaltyAsync(long userId)
        {
            var system = await this.GetCurrentSystemSettingAsync();
            CaptainUserRejectPenalty penalty = new CaptainUserRejectPenalty()
            {
                UserId = userId,
                SystemSettingId = system.Id,
                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                CreationDate = DateTime.Now
            };

            var result = await _context.UserRejectPenalties.AddAsync(penalty);
            return result.Entity;
        }

        public async Task<CaptainUserIgnoredPenalty> UpdateUserIgnoredPenaltyAsync(long userId,long penaltyStatusType)
        {
            var oldPenalty = await _context.UserIgnoredPenalties.FirstOrDefaultAsync(u => u.UserId == userId && u.PenaltyStatusTypeId != (long)PenaltyStatusTypes.End );
            oldPenalty.PenaltyStatusTypeId = penaltyStatusType;

            _context.Entry<CaptainUserIgnoredPenalty>(oldPenalty).State = EntityState.Modified;
            return oldPenalty;

        }

        public async Task<CaptainUserRejectPenalty> UpdateUserRejectedPenaltyAsync(long userId, long penaltyStatusType)
        {
            var oldPenalty = await _context.UserRejectPenalties.FirstOrDefaultAsync(u => u.UserId == userId && u.PenaltyStatusTypeId != (long)PenaltyStatusTypes.End);
            oldPenalty.PenaltyStatusTypeId = penaltyStatusType;

            _context.Entry<CaptainUserRejectPenalty>(oldPenalty).State = EntityState.Modified;
            return oldPenalty;
        }

        public async Task<List<Shift>> GetShiftsAsync()
        {
            return await _context.Shifts.ToListAsync();
        
        }

        public async Task<Shift> GetShiftByIdAsync(long id)
        {
            return await _context.Shifts.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Shift>> GetShiftsByAsync(Expression<Func<Shift, bool>> predicate)
        {
            return await _context.Shifts.Where(predicate).ToListAsync();
        }

        public async Task<List<Shift>> GetShiftsByShiftDateAsync(Shift shift)
        {
            return await _context.Shifts.Where(s => s.Day == shift.Day &&
           s.Month == shift.Month && s.Year == shift.Year).ToListAsync();
        }

        public async Task<Shift> InsertShiftAsync(Shift shift)
        {
            var insertResult = await _context.Shifts.AddAsync(shift);
            return insertResult.Entity;
        }

        public async Task<Shift> UpdateShiftAsync(Shift shift)
        {
            var oldShift = await _context.Shifts.FirstOrDefaultAsync(u => u.Id == shift.Id);
            if (oldShift == null) return null;

            oldShift.StartHour = shift.StartHour;
            oldShift.StartMinutes = shift.StartMinutes;
            oldShift.EndHour = shift.EndHour;
            oldShift.EndMinutes = shift.EndMinutes;
            oldShift.Day = shift.Day;
            oldShift.Month = shift.Month;
            oldShift.Year = shift.Year;
            oldShift.ModifiedBy = shift.ModifiedBy;
            oldShift.ModificationDate = DateTime.Now;

            _context.Entry<Shift>(oldShift).State = EntityState.Modified;
            return oldShift;
        }

        public async Task<Shift> DeleteShiftAsync(long id)
        {
            var shift = await _context.Shifts.FirstOrDefaultAsync(c => c.Id == id);
            if (shift == null) return null;

            shift.IsDeleted = true;
            _context.Entry<Shift>(shift).State = EntityState.Modified;
            return shift;
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(long id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync( v => v.Id == id);
        }

        public async Task<List<Vehicle>> GetVehiclesByAsync(Expression<Func<Vehicle, bool>> predicate)
        {
            return await _context.Vehicles.Where(predicate).ToListAsync();
        }

        public async Task<List<BoxType>> GetBoxTypesAsync()
        {
            return await _context.BoxTypes.ToListAsync();
        }

        public async Task<BoxType> GetBoxTypeByIdAsync(long id)
        {
            return await _context.BoxTypes.FirstOrDefaultAsync( b => b.Id == id);
        }

        public async Task<List<BoxType>> GetBoxTypesByAsync(Expression<Func<BoxType, bool>> predicate)
        {
            return await _context.BoxTypes.Where(predicate).ToListAsync();
        }

        public async Task<List<ContactMessage>> GetContactMessagesAsync()
        {
            return await _context.ContactMessages.ToListAsync();
        }

        public async Task<ContactMessage> GetContactMessageByIdAsync(long id)
        {
            return await _context.ContactMessages.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<ContactMessage>> GetContactMessagesByAsync(Expression<Func<ContactMessage, bool>> predicate)
        {
            return await _context.ContactMessages.Where(predicate).ToListAsync();
        }

        public async Task<ContactMessage> InsertContactMessageAsync(ContactMessage contactMessage)
        {
            contactMessage.CreationDate = DateTime.Now;
            contactMessage.Status = (long)MessageStatusTypes.New;
            var insertResult = await _context.ContactMessages.AddAsync(contactMessage);
            return insertResult.Entity;
        }

        public async Task<ContactMessage> UpdateContactMessageAsync(ContactMessage contactMessage)
        {
            var oldMessage = await _context.ContactMessages.FirstOrDefaultAsync(c => c.Id == contactMessage.Id);
            if (oldMessage == null) return null;

            oldMessage.Name = contactMessage.Name;
            oldMessage.Subject = contactMessage.Subject;
            oldMessage.Email = contactMessage.Email;
            oldMessage.Message = contactMessage.Message;
            oldMessage.Status = contactMessage.Status;
            oldMessage.IsDeleted = contactMessage.IsDeleted;
            oldMessage.ModificationDate = DateTime.Now;
            oldMessage.ModifiedBy = contactMessage.ModifiedBy;

            _context.Entry<ContactMessage>(oldMessage).State = EntityState.Modified;
            return oldMessage;
        }

        public async Task<ContactMessage> DeleteContactMessageAsync(long id)
        {
            var oldMessage = await _context.ContactMessages.FirstOrDefaultAsync(c => c.Id == id);
            if (oldMessage == null) return null;

            oldMessage.IsDeleted = true;
            _context.Entry<ContactMessage>(oldMessage).State = EntityState.Modified;
            return oldMessage;
        }

        public async Task<List<PromotionType>> GetPromotionTypesAsync()
        {
            return await _context.PromotionTypes.ToListAsync();
        }

        public async Task<PromotionType> GetPromotionTypeByIdAsync(long id)
        {
            return await _context.PromotionTypes.FirstOrDefaultAsync( p => p.Id == id);
        }

        public async Task<List<PromotionType>> GetPromotionTypesByAsync(Expression<Func<PromotionType, bool>> predicate)
        {
            return await _context.PromotionTypes.Where(predicate).ToListAsync();
        }

        public async Task<PromotionType> InsertPromotionTypeAsync(PromotionType promotionType)
        {
            promotionType.CreationDate = DateTime.Now;
            var insertResult = await _context.PromotionTypes.AddAsync(promotionType);
            return insertResult.Entity;
        }

        public async Task<PromotionType> UpdatePromotionTypeAsync(PromotionType promotionType)
        {
            var oldPromotionType = await _context.PromotionTypes.FirstOrDefaultAsync(p => p.Id == promotionType.Id);
            if (oldPromotionType == null) return null;

            oldPromotionType.Type = promotionType.Type;
            oldPromotionType.ArabicType = promotionType.ArabicType;
            oldPromotionType.ModifiedBy = promotionType.ModifiedBy;
            oldPromotionType.ModificationDate = DateTime.Now;

            _context.Entry<PromotionType>(oldPromotionType).State = EntityState.Modified;
            return oldPromotionType;

        }

        public async Task<PromotionType> DeletePromotionTypeAsync(long id)
        {
            var oldPromotionType = await _context.PromotionTypes.FirstOrDefaultAsync(p => p.Id == id);
            if (oldPromotionType == null) return null;

            _context.PromotionTypes.Remove(oldPromotionType);
            return oldPromotionType;

        }

        public async Task<List<Promotion>> GetPromotionsAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion> GetPromotionByIdAsync(long id)
        {
            return await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Promotion>> GetPromotionsPaginationAsync(int skip, int take)
        {
            return await _context.Promotions.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<Promotion>> GetPromotionsByAsync(Expression<Func<Promotion, bool>> predicate)
        {
            return await _context.Promotions.Where(predicate).ToListAsync();
        }

        public async Task<Promotion> InsertPromotionAsync(Promotion promotion)
        {
            promotion.CreationDate = DateTime.Now;
            var insertResult = await _context.Promotions.AddAsync(promotion);
            return insertResult.Entity;
        }

        public async Task<Promotion> UpdatePromotionAsync(Promotion promotion)
        {
            var oldPromotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == promotion.Id);
            if (oldPromotion == null) return null;

            oldPromotion.TypeId = promotion.TypeId;
            oldPromotion.Name = promotion.Name;
            oldPromotion.Value = promotion.Value;
            oldPromotion.Image = promotion.Image;
            oldPromotion.Descriptions = promotion.Descriptions;
            oldPromotion.IsDeleted = promotion.IsDeleted;
            oldPromotion.ExpireDate = promotion.ExpireDate;
            oldPromotion.ModifiedBy = promotion.ModifiedBy;
            oldPromotion.ModificationDate = DateTime.Now;

            _context.Entry<Promotion>(oldPromotion).State = EntityState.Modified;
            return oldPromotion;
        }

        public async Task<Promotion> DeletePromotionAsync(long id)
        {
            var oldPromotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
            if (oldPromotion == null) return null;

            _context.Promotions.Remove(oldPromotion);
            return oldPromotion;
        }

        public async Task<List<AndroidVersion>> GetAndroidVersionsAsync()
        {
            return await _context.AndroidVersions.ToListAsync();
        }

        public async Task<AndroidVersion> GetAndroidVersionByIdAsync(long id)
        {
            return await _context.AndroidVersions.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AndroidVersion>> GetAndroidVersionsPaginationAsync(int skip, int take)
        {
            return await _context.AndroidVersions.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<AndroidVersion> GetCurrentAndroidVersionAsync() 
        {
            return await _context.AndroidVersions.FirstOrDefaultAsync(a => a.IsCurrent == true);
        }

        public async Task<List<AndroidVersion>> GetAndroidVersionsByAsync(Expression<Func<AndroidVersion, bool>> predicate)
        {
            return await _context.AndroidVersions.Where(predicate).ToListAsync();
        }

        public async Task<AndroidVersion> InsertAndroidVersionAsync(AndroidVersion androidVersion)
        {
            if ((bool)androidVersion?.IsCurrent) {
                var oldVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.IsCurrent == true);
                if (oldVersion != null)
                {
                    oldVersion.ModificationDate = DateTime.Now;
                    oldVersion.ModifiedBy = androidVersion.ModifiedBy;
                    oldVersion.IsCurrent = false;
                    _context.Entry<AndroidVersion>(oldVersion).State = EntityState.Modified;
                }
                androidVersion.IsCurrent = true;
            }

            

            androidVersion.ModifiedBy = 0;
            androidVersion.CreationDate = DateTime.Now;
            var insertResult = await _context.AndroidVersions.AddAsync(androidVersion);
            return insertResult.Entity;
        }

        public async Task<AndroidVersion> UpdateAndroidVersionAsync(AndroidVersion androidVersion)
        {

            var oldVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.Id == androidVersion.Id);
            if (oldVersion == null) return null;

            if ((bool)androidVersion?.IsCurrent)
            {
                var oldCurrentVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.IsCurrent == true);
                if (oldCurrentVersion != null)
                {
                    oldCurrentVersion.ModificationDate = DateTime.Now;
                    oldCurrentVersion.ModifiedBy = androidVersion.ModifiedBy;
                    oldCurrentVersion.IsCurrent = false;
                    _context.Entry<AndroidVersion>(oldCurrentVersion).State = EntityState.Modified;
                }
                
            }


            oldVersion.Name = androidVersion.Name;
            oldVersion.Version = androidVersion.Version;
            oldVersion.FileName = androidVersion.FileName;
            oldVersion.FileExtension = androidVersion.FileExtension;
            oldVersion.FilePath = androidVersion.FilePath;
            oldVersion.Description = androidVersion.Description;
            oldVersion.ModificationDate = DateTime.Now;
            oldVersion.ModifiedBy = androidVersion.ModifiedBy;
            oldVersion.IsCurrent = androidVersion.IsCurrent;
            _context.Entry<AndroidVersion>(oldVersion).State = EntityState.Modified;
            return oldVersion;
        }

        public async Task<AndroidVersion> DeleteAndroidVersionAsync(long id)
        {
            var oldVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.Id == id);
            if (oldVersion == null) return null;

            _context.AndroidVersions.Remove(oldVersion);
            return oldVersion;
        }
    }
}
