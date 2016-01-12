namespace Expedia.Entities
{
	/// <summary>
	/// Error codes returned by the API
	/// </summary>
	public enum ErrorCodes
	{	InvalidEmailAddress = 0,
		EmptyRequiredFields,
		InvalidSignInCredentials,
		InvalidExpediaAccountOrEmail,
		CheckInDateBeforeToday,
		CheckOutDateMustBeLaterThanCheckIn,
		InvalidLocationValue,
		TechnicalDifficulty,
		InvalidExpediaOrUsername,
		InvalidLastName,
		InvalidFirstName,
		InvalidExpediaNumber,
		CountryNotRecognized,
		RegionNotRecognized,
		ChangesCancelled,
	    InvalidCity
	}
}
