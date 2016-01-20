using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entites;
using Newtonsoft.Json;

namespace Expedia.Entities.User
{
	public enum MobileErrorCode
	{
		INVALID_INPUT, 
		HOTEL_ROOM_UNAVAILABLE, 
		HOTEL_OFFER_UNAVAILABLE, 
		HOTEL_SERVICE_FATAL_FAILURE, 
		PAYMENT_FAILED, 
		BOOKING_SUCCEEDED_WITH_ERRORS, 
		BOOKING_FAILED, 
		STORE_CREDIT_CARD_INFO_FAILED, 
		SSL_REQUIRED, 
		USER_UNAUTHORIZED_FOR_PROFILE, 
		UNKNOWN_ERROR, 
		TRIP_SERVICE_ERROR, 
		HOTEL_PRODUCT_NOT_FOUND, 
		APPLY_COUPON_ERROR, 
		REMOVE_COUPON_ERROR, 
		APPLY_BACKICK_PRICE_ADJUSTMENT_ERROR, 
		APPLY_ADJUSTMENT_UNAUTHORIZED, 
		FLIGHT_SOLD_OUT, 
		FLIGHT_PRODUCT_NOT_FOUND, 
		USER_SERVICE_ERROR, 
		USER_SERVICE_FATAL_FAILURE, 
		USER_SERVICE_TRAVELER_ALREADY_EXISTS, 
		SESSION_TIMEOUT, 
		TRIP_ALREADY_BOOKED, 
		USER_SERVICE_DUPLICATE_EMAIL, 
		USER_SERVICE_CREATE_USER_ERROR, 
		IMAGE_NOT_FOUND, 
		CANNOT_BOOK_WITH_MINOR, 
		USER_CREATION_FAILED_DURING_CHECKOUT, 
		OMS_ERROR, 
		INVALID_INPUT_NOT_OPAQUE_PRODUCT_KEY, 
		PACKAGE_SEARCH_ERROR, 
		CAR_PRODUCT_NOT_AVAILABLE, 
		CAR_SERVICE_ERROR, 
		CAR_SEARCH_ERROR
	}

    public class MobileError
    {
		[JsonProperty("errorCode")]
		public MobileErrorCode ErrorCode { get; set; }

		[JsonProperty("errorInfo")]
		public string ErrorInfo { get; set; }

		[JsonProperty("diagnosticId")]
		public string DiagnosticId { get; set; }

		[JsonProperty("diagnosticFullText")]
		public string DiagnosticFullText { get; set; }

		[JsonProperty("activityId")]
		public string ActivityId { get; set; }

		public override string ToString()
		{
			return "Code={0}, Info={1}, DiagId={2}, Diag={3}".InvariantCultureFormat(ErrorCode, ErrorInfo, DiagnosticId, DiagnosticFullText);
		}
    }
}
