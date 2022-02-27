using System.ComponentModel.DataAnnotations;

namespace TreePorts.DTO.Records;

public record class NearCaptainUser(string Id,string FirstName,string LastName,string Lat, string Long);

public record class OrderFilterResponse(
	long? OrderId,
	long? OrderCurrentStatus,
	string? AgentName,
	long? ProductTypeId,
	long? PaymentTypeId,
	string? CustomerName,
	string? CustomerAddress,
	string? CustomerPhone,
	decimal? DeliveryAmount,
	string? CaptainName,
	DateTime? OrderCreationDate,
	string? DurationToCurrentStatus,
	DateTime? OrderStatusHistoryCreationDate,
	string? DurationToStatusHistory
);

public record class AdminUserResponse ( AdminUserAccount? UserAccount, AdminUser? User );


public record class AdminUserDto(
	string? FirstName,
	string? LastName,
	string? NationalNumber,
	long? CountryId,
	long? CityId,
	string? Address,
	string? Gender,
	string? Mobile,
	DateTime? BirthDate,
	DateTime? ResidenceExpireDate,
	string? PersonalImage,
	long? ResidenceCountryId,
	long? ResidenceCityId,
	long? AdminTypeId,
	long? StatusTypeId,
	string? Email
);



public record class SupportUserDto(
	string? FirstName,
	string? LastName,
	string? NationalNumber,
	long? CountryId,
	long? CityId,
	string? Address,
	string? Gender,
	string? Mobile,
	DateTime? BirthDate,
	DateTime? ResidenceExpireDate,
	string? PersonalImage,
	long? ResidenceCountryId,
	long? ResidenceCityId,
	long? AdminTypeId,
	long? StatusTypeId,
	string? Email
);



public record class SupportUserResponse(SupportUserAccount? UserAccount, SupportUser? User);


public record class CaptainUserDto(
	string? NationalNumber,
	long? CountryId,
	long? CityId,
	string? Mobile,
	long? ResidenceCountryId,
	long? ResidenceCityId,
	string? RecidenceImage,
	string? FirstName,
	string? LastName,
	DateTime? BirthDate,
	DateTime? NationalNumberExpireDate,
	string? StcPay,
	string? Gender,
	string? PersonalImage,
	string? NbsherNationalNumberImage,
	string? NationalNumberFrontImage,
	string? VehicleRegistrationImage,
	string? DrivingLicenseImage,
	string? VehiclePlateNumber
);


public record class AgentDto(
	string? Fullname,
	string? Email,
	long? CountryId,
	long? CityId,
	string? Address,
	string? Mobile,
	long? AgentTypeId,
	bool? IsBranch,
	string? LocationLat,
	string? LocationLong,
	string? Image,
	string? CommercialRegistrationNumber,
	string? Website
);


public record class LoginUserDto
(
	[Required]
	string Email ,
	[Required]
	[StringLength(6, MinimumLength = 6, ErrorMessage = "You must enter password 6 digites")]
	string Password
);



public record class LoginCaptainUserDto
(
	[Required]
	string Mobile,
	[Required]
	[StringLength(6, ErrorMessage = "You must enter password 6 digites")]
	string Password
);

public record class CaptainUserAccountVehicle(
	CaptainUserAccount CaptainUserAccount,
	IEnumerable<CaptainUserVehicleBox> CaptainUserVehicleBoxs
);
public record class CaptainUserVehicleBox( CaptainUserVehicle CaptainUserVehicle, IEnumerable<CaptainUserBox> CaptainUserBoxs );
public record class CaptainUserVechicleResponse(
	CaptainUser CaptainUser,
	IEnumerable<CaptainUserAccountVehicle> CaptainUserAccountsVehicles
//CaptainUserVehicleBox[] CaptainUserVehicle
);


public record class CaptainUserResponse(
	CaptainUser CaptainUser,
	CaptainUserAccount CaptainUserAccount
);



public record class BonusCheckDto(
	string captainUserAccountId,
	DateTime date
);



public record class OrderRequest
(
	string CaptainUserAccountId,
	long OrderId
);


public record class StatusAction
(
	string UserType,
	string UserAccountId,
	long StatusTypeId,
	long ModifiedBy
);


public record class UserHubToken
(
	 string UserType,
	 string UserAccountId,
	 string Token
);


public record class FBNotify
(
	 string UserAccountId,
	 string Token,
	 string Topic,
	 List<string> Tokens,
	 List<string> UserAccountIds,
	 string Title,
	 string Message
);