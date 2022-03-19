using TreePorts.DTO;
using System.Linq.Expressions;

namespace TreePorts.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        private TreePortsDBContext _context;

        public SystemRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<SystemSetting?> DeleteSystemSettingAsync(long id, CancellationToken cancellationToken)
        {
            var settings = await _context.SystemSettings.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
            if (settings == null) return null;

            settings.IsDeleted = true;
            _context.Entry<SystemSetting>(settings).State = EntityState.Modified;
            return settings;
        }

        public async Task<List<SystemSetting>> GetSystemSettingsAsync(CancellationToken cancellationToken)
        {
            return await _context.SystemSettings.ToListAsync(cancellationToken);
        }

        public async Task<List<CaptainUserIgnoredPenalty>?> GetCaptainUserIgnoredPenaltiesByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
        {
            return await _context.CaptainUserIgnoredPenalties.Where(u => u.CaptainUserAccountId == captainUserAccountId).ToListAsync(cancellationToken);
        }

        public async Task<List<SystemSetting>> GetSystemSettingsByAsync(Expression<Func<SystemSetting, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.SystemSettings.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<SystemSetting?> GetSystemSettingByIdAsync(long id, CancellationToken cancellationToken)
        {
             return await _context.SystemSettings.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
        }

        public async Task<SystemSetting?> GetCurrentSystemSettingAsync(CancellationToken cancellationToken)
        {
            return await _context.SystemSettings.FirstOrDefaultAsync(s => s.IsCurrent == true && s.IsDeleted == false,cancellationToken );
        }

        public async Task<CaptainUserIgnoredPenalty?> GetCaptainUserIgnoredPenaltyByCaptainUserAccountIdAsync(string captainUserAccountId, CancellationToken cancellationToken)
        {
            return await _context.CaptainUserIgnoredPenalties.FirstOrDefaultAsync(u => 
            u.CaptainUserAccountId == captainUserAccountId && 
            (u.PenaltyStatusTypeId == (long) PenaltyStatusTypes.New ),cancellationToken);
        }

       /* public async Task<CaptainUserRejectPenalty> GetCaptainUserRejectedPenaltyByCaptainUserAccountIdAsync(long captainUserAccountId)
        {
            return await _context.CaptainUserRejectPenalties.FirstOrDefaultAsync(u => 
            u.UserId == captainUserAccountId &&
            (u.PenaltyStatusTypeId == (long)PenaltyStatusTypes.New ));
        }*/

        public async Task<List<IgnorPerType>> GetIgnorPerTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.IgnorPerTypes.ToListAsync(cancellationToken);
        }

        public async Task<List<PenaltyPerType>> GetPenaltyPerTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.PenaltyPerTypes.ToListAsync(cancellationToken);
        }

        public async Task<List<PenaltyStatusType>> GetPeniltyStatusTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.PenaltyStatusTypes.ToListAsync(cancellationToken);
        }

        /*public async Task<List<RejectPerType>> GetRejectPerTypesAsync()
        {
            return await _context.RejectPerTypes.ToListAsync(cancellationToken);
        }*/

        /*public async Task<List<CaptainUserRejectPenalty>> GetCaptainUserRejectedPenaltiesByCaptainUserAccountIdAsync(string captainUserAccountId)
        {
            return await _context.CaptainUserRejectPenalties.Where(u => u.CaptainUserAccountId == captainUserAccountId).ToListAsync(cancellationToken);
        }*/

        public async Task<SystemSetting> InsertSystemSettingAsync(SystemSetting systemSetting, CancellationToken cancellationToken)
        {
            var oldSystem = await _context.SystemSettings.FirstOrDefaultAsync(s => s.IsCurrent == true ,cancellationToken);
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
            var result = await _context.SystemSettings.AddAsync(systemSetting,cancellationToken);
            return result.Entity;

        }

        public async Task<CaptainUserIgnoredPenalty?> InsertCaptainUserIgnoredPenaltyAsync(string captainUserAccountId, CancellationToken cancellationToken)
        {

            var system = await this.GetCurrentSystemSettingAsync(cancellationToken);
            CaptainUserIgnoredPenalty penalty = new ()
            {
                CaptainUserAccountId = captainUserAccountId,
                SystemSettingId = system?.Id,
                PenaltyStatusTypeId =(long) PenaltyStatusTypes.New,
                CreationDate = DateTime.Now
            };

            var result = await _context.CaptainUserIgnoredPenalties.AddAsync(penalty,cancellationToken);
            return result.Entity;

        }

       /* public async Task<CaptainUserRejectPenalty> InsertCaptainUserRejectedPenaltyAsync(string captainUserAccountId)
        {
            var system = await this.GetCurrentSystemSettingAsync(cancellationToken);
            CaptainUserRejectPenalty penalty = new ()
            {
                CaptainUserAccountId = captainUserAccountId,
                SystemSettingId = system?.Id,
                PenaltyStatusTypeId = (long)PenaltyStatusTypes.New,
                CreationDate = DateTime.Now
            };

            var result = await _context.CaptainUserRejectPenalties.AddAsync(penalty);
            return result.Entity;
        }*/

        public async Task<CaptainUserIgnoredPenalty?> UpdateCaptainUserIgnoredPenaltyAsync(string captainUserAccountId, long penaltyStatusType, CancellationToken cancellationToken)
        {
            var oldPenalty = await _context.CaptainUserIgnoredPenalties.FirstOrDefaultAsync(u => u.CaptainUserAccountId == captainUserAccountId && u.PenaltyStatusTypeId != (long)PenaltyStatusTypes.End , cancellationToken);
            if (oldPenalty == null) return null;
            oldPenalty.PenaltyStatusTypeId = penaltyStatusType;

            _context.Entry<CaptainUserIgnoredPenalty>(oldPenalty).State = EntityState.Modified;
            return oldPenalty;

        }

        /*public async Task<CaptainUserRejectPenalty?> UpdateCaptainUserRejectedPenaltyAsync(string captainUserAccountId, long penaltyStatusType)
        {
            var oldPenalty = await _context.CaptainUserRejectPenalties.FirstOrDefaultAsync(u => u.CaptainUserAccountId == captainUserAccountId && u.PenaltyStatusTypeId != (long)PenaltyStatusTypes.End);
            if (oldPenalty == null) return null;

            oldPenalty.PenaltyStatusTypeId = penaltyStatusType;

            _context.Entry<CaptainUserRejectPenalty>(oldPenalty).State = EntityState.Modified;
            return oldPenalty;
        }*/

        public async Task<List<Shift>> GetShiftsAsync(CancellationToken cancellationToken)
        {
            return await _context.Shifts.ToListAsync(cancellationToken);
        
        }

        public async Task<Shift?> GetShiftByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Shifts.FirstOrDefaultAsync(s => s.Id == id,cancellationToken);
        }

        public async Task<List<Shift>> GetShiftsByAsync(Expression<Func<Shift, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Shifts.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<Shift>> GetShiftsByShiftDateAsync(Shift shift, CancellationToken cancellationToken)
        {
            return await _context.Shifts.Where(s => s.Day == shift.Day &&
           s.Month == shift.Month && s.Year == shift.Year).ToListAsync(cancellationToken);
        }

        public async Task<Shift> InsertShiftAsync(Shift shift, CancellationToken cancellationToken)
        {
            var insertResult = await _context.Shifts.AddAsync(shift,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<Shift?> UpdateShiftAsync(Shift shift, CancellationToken cancellationToken)
        {
            var oldShift = await _context.Shifts.FirstOrDefaultAsync(u => u.Id == shift.Id,cancellationToken);
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

        public async Task<Shift?> DeleteShiftAsync(long id, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
            if (shift == null) return null;

            shift.IsDeleted = true;
            _context.Entry<Shift>(shift).State = EntityState.Modified;
            return shift;
        }

        public async Task<List<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken)
        {
            return await _context.Vehicles.ToListAsync(cancellationToken);
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Vehicles.FirstOrDefaultAsync( v => v.Id == id,cancellationToken);
        }

        public async Task<List<Vehicle>> GetVehiclesByAsync(Expression<Func<Vehicle, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Vehicles.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<BoxType>> GetBoxTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.BoxTypes.ToListAsync(cancellationToken);
        }

        public async Task<BoxType?> GetBoxTypeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.BoxTypes.FirstOrDefaultAsync( b => b.Id == id,cancellationToken);
        }

        public async Task<List<BoxType>> GetBoxTypesByAsync(Expression<Func<BoxType, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.BoxTypes.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<ContactMessage>> GetContactMessagesAsync(CancellationToken cancellationToken)
        {
            return await _context.ContactMessages.ToListAsync(cancellationToken);
        }

        public async Task<ContactMessage?> GetContactMessageByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.ContactMessages.FirstOrDefaultAsync(b => b.Id == id,cancellationToken);
        }

        public async Task<List<ContactMessage>> GetContactMessagesByAsync(Expression<Func<ContactMessage, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.ContactMessages.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<ContactMessage> InsertContactMessageAsync(ContactMessage contactMessage, CancellationToken cancellationToken)
        {
            contactMessage.CreationDate = DateTime.Now;
            contactMessage.Status = (long)MessageStatusTypes.New;
            var insertResult = await _context.ContactMessages.AddAsync(contactMessage,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<ContactMessage?> UpdateContactMessageAsync(ContactMessage contactMessage, CancellationToken cancellationToken)
        {
            var oldMessage = await _context.ContactMessages.FirstOrDefaultAsync(c => c.Id == contactMessage.Id,cancellationToken);
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

        public async Task<ContactMessage?> DeleteContactMessageAsync(long id, CancellationToken cancellationToken)
        {
            var oldMessage = await _context.ContactMessages.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (oldMessage == null) return null;

            oldMessage.IsDeleted = true;
            _context.Entry<ContactMessage>(oldMessage).State = EntityState.Modified;
            return oldMessage;
        }

        public async Task<List<PromotionType>> GetPromotionTypesAsync(CancellationToken cancellationToken)
        {
            return await _context.PromotionTypes.ToListAsync(cancellationToken);
        }

        public async Task<PromotionType?> GetPromotionTypeByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.PromotionTypes.FirstOrDefaultAsync( p => p.Id == id,cancellationToken);
        }

        public async Task<List<PromotionType>> GetPromotionTypesByAsync(Expression<Func<PromotionType, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.PromotionTypes.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<PromotionType> InsertPromotionTypeAsync(PromotionType promotionType, CancellationToken cancellationToken)
        {
            promotionType.CreationDate = DateTime.Now;
            var insertResult = await _context.PromotionTypes.AddAsync(promotionType,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<PromotionType?> UpdatePromotionTypeAsync(PromotionType promotionType, CancellationToken cancellationToken)
        {
            var oldPromotionType = await _context.PromotionTypes.FirstOrDefaultAsync(p => p.Id == promotionType.Id,cancellationToken);
            if (oldPromotionType == null) return null;

            oldPromotionType.Type = promotionType.Type;
            oldPromotionType.ArabicType = promotionType.ArabicType;
            oldPromotionType.ModifiedBy = promotionType.ModifiedBy;
            oldPromotionType.ModificationDate = DateTime.Now;

            _context.Entry<PromotionType>(oldPromotionType).State = EntityState.Modified;
            return oldPromotionType;

        }

        public async Task<PromotionType?> DeletePromotionTypeAsync(long id, CancellationToken cancellationToken)
        {
            var oldPromotionType = await _context.PromotionTypes.FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
            if (oldPromotionType == null) return null;

            _context.PromotionTypes.Remove(oldPromotionType);
            return oldPromotionType;

        }

        public async Task<List<Promotion>> GetPromotionsAsync(CancellationToken cancellationToken)
        {
            return await _context.Promotions.ToListAsync(cancellationToken);
        }

        public async Task<Promotion?> GetPromotionByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
        }

        public async Task<List<Promotion>> GetPromotionsPaginationAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _context.Promotions.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public async Task<List<Promotion>> GetPromotionsByAsync(Expression<Func<Promotion, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Promotions.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<Promotion> InsertPromotionAsync(Promotion promotion, CancellationToken cancellationToken)
        {
            promotion.CreationDate = DateTime.Now;
            var insertResult = await _context.Promotions.AddAsync(promotion,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<Promotion?> UpdatePromotionAsync(Promotion promotion, CancellationToken cancellationToken)
        {
            var oldPromotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == promotion.Id,cancellationToken);
            if (oldPromotion == null) return null;

            oldPromotion.PromotionTypeId = promotion.PromotionTypeId;
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

        public async Task<Promotion?> DeletePromotionAsync(long id, CancellationToken cancellationToken)
        {
            var oldPromotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (oldPromotion == null) return null;

            _context.Promotions.Remove(oldPromotion);
            return oldPromotion;
        }

        public async Task<List<AndroidVersion>> GetAndroidVersionsAsync(CancellationToken cancellationToken)
        {
            return await _context.AndroidVersions.ToListAsync(cancellationToken);
        }

        public async Task<AndroidVersion?> GetAndroidVersionByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.AndroidVersions.FirstOrDefaultAsync(a => a.Id == id,cancellationToken);
        }

        public async Task<List<AndroidVersion>> GetAndroidVersionsPaginationAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _context.AndroidVersions.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public async Task<AndroidVersion?> GetCurrentAndroidVersionAsync(CancellationToken cancellationToken) 
        {
            return await _context.AndroidVersions.FirstOrDefaultAsync(a => a.IsCurrent == true,cancellationToken);
        }

        public async Task<List<AndroidVersion>> GetAndroidVersionsByAsync(Expression<Func<AndroidVersion, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.AndroidVersions.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<AndroidVersion> InsertAndroidVersionAsync(AndroidVersion androidVersion, CancellationToken cancellationToken)
        {
            if (androidVersion?.IsCurrent?? false) {
                var oldVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.IsCurrent == true,cancellationToken);
                if (oldVersion != null)
                {
                    oldVersion.ModificationDate = DateTime.Now;
                    oldVersion.ModifiedBy = androidVersion.ModifiedBy;
                    oldVersion.IsCurrent = false;
                    _context.Entry<AndroidVersion>(oldVersion).State = EntityState.Modified;
                }
                androidVersion.IsCurrent = true;
            }

            

            
            androidVersion.CreationDate = DateTime.Now;
            var insertResult = await _context.AndroidVersions.AddAsync(androidVersion,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<AndroidVersion?> UpdateAndroidVersionAsync(AndroidVersion androidVersion, CancellationToken cancellationToken)
        {

            var oldVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.Id == androidVersion.Id,cancellationToken);
            if (oldVersion == null) return null;

            if (androidVersion?.IsCurrent?? false)
            {
                var oldCurrentVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.IsCurrent == true, cancellationToken);
                if (oldCurrentVersion != null)
                {
                    oldCurrentVersion.ModificationDate = DateTime.Now;
                    oldCurrentVersion.ModifiedBy = androidVersion.ModifiedBy;
                    oldCurrentVersion.IsCurrent = false;
                    _context.Entry<AndroidVersion>(oldCurrentVersion).State = EntityState.Modified;
                }
                
            }


            oldVersion.Name = androidVersion?.Name;
            oldVersion.Version = androidVersion?.Version;
            oldVersion.FileName = androidVersion?.FileName;
            oldVersion.FileExtension = androidVersion?.FileExtension;
            oldVersion.FilePath = androidVersion?.FilePath;
            oldVersion.Description = androidVersion?.Description;
            oldVersion.ModificationDate = DateTime.Now;
            oldVersion.ModifiedBy = androidVersion?.ModifiedBy;
            oldVersion.IsCurrent = androidVersion?.IsCurrent;
            _context.Entry<AndroidVersion>(oldVersion).State = EntityState.Modified;
            return oldVersion;
        }

        public async Task<AndroidVersion?> DeleteAndroidVersionAsync(long id, CancellationToken cancellationToken)
        {
            var oldVersion = await _context.AndroidVersions.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (oldVersion == null) return null;

            _context.AndroidVersions.Remove(oldVersion);
            return oldVersion;
        }
    }
}
