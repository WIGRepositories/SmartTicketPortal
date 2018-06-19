using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTicketPortal.Models
{
    public class WebsiteUserInfo
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string UserName { set; get; }

        public string Password { set; get; }

        public string EmailAddress { set; get; }

        public string Mobile { set; get; }
        public string Country { set; get; }
        public string logininfo { set; get; }
    }
    public class login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int result { get; set; }
    }
    public class UserLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LoginInfo { get; set; }
        public string Passkey { get; set; }
        public string Salt { get; set; }
        public string Active { get; set; }
        public int NoofAttempts { get; set; }


    }
    public class reset
    {

        public string UserName { set; get; }
        public string OldPassword { set; get; }
        public string NewPassword { set; get; }
        public string ReenterNewPassword { set; get; }

    }
    public class BookingDetails
    {
        public int Id { get; set; }
        public string TicketNo { get; set; }
        public string TransId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string AltMobileNo { get; set; }
        public string Address { get; set; }
        public DateTime? JourneyDate { get; set; }
        public DateTime? JourneyTime { get; set; }
        public decimal perunitprice { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string Src { get; set; }
        public string Dest { get; set; }
        public int SrcId { get; set; }
        public int DestId { get; set; }
        public int RouteId { get; set; }
        public int VehicleId { get; set; }
        public int NoOfSeats { get; set; }
        public string Seats { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string BookedBy { get; set; }
        public int BookedById { get; set; }
        public decimal Amount { get; set; }
        public string BookingType { get; set; }
        public int BookingTypeId { get; set; }
        public string JourneyType { get; set; }
        public int JourneyTypeId { get; set; }
        public int UserId { get; set; }
        public string insupddelflag { get; set; }

        public IEnumerable<BookedSeats> BookedSeats { get; set; }

    }
    public class BookedSeats
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string TicketNo { get; set; }
        public string SeatNo { get; set; }
        public int SeatId { get; set; }
        public int VehicleId { get; set; }
        public int Row { get; set; }

        public int Col { get; set; }

        public DateTime JourneyDate { get; set; }
        public DateTime JourneyTime { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string IdProof { get; set; }
        public string PassengerType { get; set; }
        public int PrimaryPassenger { get; set; }

        public string insupddelflag { get; set; }
    }
    public class BookedTicketDetails
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string TicketNo { get; set; }
        public string TransId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string TicketContent { get; set; }
        public string insupddelflag { get; set; }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String EmailAddress { get; set; }
        public String Mobile { get; set; }
        public String Country { get; set; }
        public int CountryId { get; set; }
        public String AltMobileNo { get; set; }
        public int Gender { get; set; }
        public int UserTypeId { get; set; }
        public int UserId { get; set; }
        public int Active { get; set; }
        public string InsUpdDelFlag { get; set; }
        public String EVerificationCode { get; set; }
        public DateTime EVerifiedOn { get; set; }
        public int IsEmailVerified { get; set; }
        public String MVerificationCode { get; set; }
        public DateTime MVerifiedOn { get; set; }
        public int IsMobileVerified { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ENoOfAttempts { get; set; }
        public int MNoOfAttempts { get; set; }

       
    }
    public class Country
    {
        //Id, Name, Latitude, Longitude,ISOCode, HasOperations
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISOCode { get; set; }
        public string HasOperations { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
    public class LicensePage
    {
        public int Id { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseType { get; set; }
        public string fleetownercode { get; set; }
        public Decimal Unitprice { get; set; }
        public int FeatureName { get; set; }
        public int FeatureLabel { get; set; }
        public int FeatureValue { get; set; }


        public string insupdflag { get; set; }

        public int LicenseCatId { get; set; }

    }

    public class FleetOwnerRequest
    {
        //user details

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string AltPhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int Gender { set; get; }

        public string userPhoto { get; set; }

        //Company details
        public string CompanyName { get; set; }
        public string CmpEmailAddress { get; set; }
        public string CmpTitle { get; set; }
        public string CmpCaption { get; set; }
        public string FleetSize { set; get; }
        public int StaffSize { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string CmpFax { get; set; }
        public string CmpAddress { get; set; }
        public string CmpAltAddress { get; set; }
        public string state { get; set; }
        public string ZipCode { get; set; }
        public string CmpPhoneNo { set; get; }
        public string CmpAltPhoneNo { set; get; }
        public string CurrentSystemInUse { set; get; }
        public string howdidyouhearaboutus { get; set; }
        public string SendNewProductsEmails { set; get; }
        public int Agreetotermsandconditions { get; set; }


        public string CmpLogo { get; set; }

        public string insupdflag { get; set; }
    }
    public class UserLicenseDetails
    {
        public List<ULLicense> checkSchedule { get; set; }
        public int Id { set; get; }
        public int UserId { set; get; }
        public int FOId { set; get; }
        public string FOCode { set; get; }
        public int LicenseTypeId { set; get; }
        public DateTime? StartDate { set; get; }
        public DateTime? ExpiryOn { set; get; }
        public int GracePeriod { set; get; }
        public DateTime? ActualExpiry { set; get; }
        public DateTime? LastUpdatedOn { set; get; }
        public int Active { set; get; }
        public int RenewFreqTypeId { set; get; }
        public int StatusId { set; get; }

        public string insupddelflag { set; get; }

    }
    public class ULLicense
    {
        public int Id { set; get; }
        public int ULId { set; get; }
        public string TransId { set; get; }
        public DateTime? CreatedOn { set; get; }
        public decimal Amount { set; get; }
        public decimal UnitPrice { set; get; }
        public decimal Units { set; get; }
        public int StatusId { set; get; }
        public int LicensePymtTransId { set; get; }
        public int IsRenewal { set; get; }
        public string insupddelflag { set; get; }
    }
    public class ULConfirmDetails
    {
        public int Id { set; get; }
        public int ULId { set; get; }
        public int ULPymtId { set; get; }
        public int foId { set; get; }
        public int userId { set; get; }
        public string TransId { set; get; }
        public string GatewayTransId { set; get; }
        public int itemId { set; get; }
        public string address { set; get; }
        public decimal Amount { set; get; }
        public decimal Units { set; get; }
        public decimal POSUnits { set; get; }
        public int IsRenewal { set; get; }
        public string insupddelflag { set; get; }
    }
    public class UserLocation
    {
        public string flag { get; set; }

        public int BNo { get; set; }
        public string BookingType { get; set; }

        public string ReqVehicle { get; set; }
        public string Customername { get; set; }
        public string CusID { get; set; }
        public string PhoneNo { get; set; }
        public string AltPhoneNo { get; set; }
        public string CAddress { get; set; }
        public string PickupAddress { get; set; }
        public string LandMark { get; set; }
        public string Package { get; set; }
        public string PickupPalce { get; set; }
        public string DropPalce { get; set; }
        public string ReqType { get; set; }
        public int ExtraCharge { get; set; }
        public int NoofVehicle { get; set; }
        public string ExecutiveName { get; set; }
        public int VID { get; set; }
        public string BookingStatus { get; set; }
        public string CustomerSMS { get; set; }
        public string CancelReason { get; set; }
        public decimal CBNo { get; set; }
        public string ModifiedBy { get; set; }
        public string CancelBy { get; set; }
        public string ReconfirmedBy { get; set; }
        public string AssignedBy { get; set; }

        public float lat { get; set; }
        public float lng { get; set; }

        public object Mobileotp { get; set; }
    }
    public class HourBase
    {
        public string insupddelflag { get; set; }
        public int Id { get; set; }
        public int VehicleModelId { get; set; }
        //public string VehicleModel { get; set; }
        public string Hours { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public decimal Price { get; set; }

    }

    public class LicensePayments
    {
        public DateTime expiryOn { get; set; }

        public int Id { get; set; }

        public string licenseFor { get; set; }

        public int licenseId { get; set; }

        public string licenseType { get; set; }

        public DateTime paidon { get; set; }

        public DateTime renewedon { get; set; }

        public string transId { get; set; }

    }
}


namespace Paysmart.Models
{


    public class UserAccount
    {

        public string flag { get; set; }
        public int id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public string Mobilenumber { get; set; }
        public string Password { get; set; }
        public String EVerificationCode { get; set; }
        public DateTime EVerifiedOn { get; set; }
        public int IsEmailVerified { get; set; }
        public String MVerificationCode { get; set; }
        public string Passwordotp { get; set; }
        public DateTime MVerifiedOn { get; set; }
        public int IsMobileVerified { get; set; }

        public DateTime CreatedOn { get; set; }
        public int ENoOfAttempts { get; set; }
        public int MNoOfAttempts { get; set; }
        public string Firstname { get; set; }
        public string lastname { get; set; }
        public int AuthTypeId { get; set; }
        public string AltPhonenumber { get; set; }
        public string Altemail { get; set; }
        public string AccountNo { get; set; }
        public string NewPassword { get; set; }



        public object Emailotp { get; set; }

        public object Mobileotp { get; set; }
    }

    public class UserLogin
    {
        public string Mobilenumber { get; set; }
        public string Password { get; set; }

    }

    public class Tickets
    {

        public int id { get; set; }
        public int Userid { get; set; }
        public string EmailId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Onbehalfofph { get; set; }
        public string PhoneNumber { get; set; }
        public string TicketNo { get; set; }
        public string Catid { get; set; }

        public string Description { get; set; }
        public int Emailsent { get; set; }
        public int Smssent { get; set; }
        public string TicketTypeId { get; set; }
        public int StatusId { get; set; }
        public string flag { get; set; }
    }

    public class faqs
    {

        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Catid { get; set; }
        public DateTime CreatedOn { get; set; }

        public int Createdby { get; set; }
        public string flag { get; set; }
    }

    public class UserInfo
    {

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string Emailid { get; set; }
        public string Phonenumber { get; set; }
        public string Mobileotp { get; set; }
        public string Emailotp { get; set; }
        public string Pwdotp { get; set; }
        public string Password { get; set; }
        public int statusId { get; set; }
        public string AccountNo { get; set; }
        public int AuthTypeId { get; set; }

        public string AltPhonenumber { get; set; }
        public string Altemail { get; set; }

        public string flag { get; set; }
    }

    public class Users
    {
        public int Id { get; set; }

        public int userid { get; set; }
        public string email { get; set; }
        public string Address { get; set; }
        public int Accountid { get; set; }
        public int preferenceTypeId { get; set; }
        public int preferenceId { get; set; }
        public string flag { get; set; }
    }
    public class Usercards
    {


        public int Id { get; set; }

        public int AccId { get; set; }
        public string Cardno { get; set; }
        public string ccv { get; set; }
        public int Expyear { get; set; }
        public int expmonth { get; set; }
        public int Statusid { get; set; }
        public string isDefault { get; set; }

        public string flag { get; set; }
    }

    public class UserDownloads
    {
        public int Id { get; set; }
        public string Android { get; set; }
        public string Win { get; set; }
        public string Ios { get; set; }
        public string Websitedown { get; set; }
        public string Iosdown { get; set; }
        public string Wincount { get; set; }
        public string flag { get; set; }

    }

    public class payments
    {
        public string Transaction_Number { get; set; }
        public int Amount { get; set; }
        public int Paymentmode { get; set; }
        public DateTime dateandtime { get; set; }
        public int Pnr_Id { get; set; }
        public string Pnr_No { get; set; }
        public string Gateway_transId { get; set; }
        public int TransactionStatus { get; set; }

    }

    public class passenger
    {
        public string Fname { get; set; }

        public string Lname { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string datetime { get; set; }
        public int Pnr_Id { get; set; }
        public string Pnr_No { get; set; }
        public int Identityproof { get; set; }
    }
    public class Seats
    {
        public int Src_Id { get; set; }
        public int Des_Id { get; set; }
    }
    public class ResetPwd 
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
    public class DriverAccount
    {

        public string flag { get; set; }
        public int id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public string Mobilenumber { get; set; }
        public string Password { get; set; }
        public String EVerificationCode { get; set; }
        public DateTime EVerifiedOn { get; set; }
        public int IsEmailVerified { get; set; }
        public String MVerificationCode { get; set; }
        public string Passwordotp { get; set; }
        public DateTime MVerifiedOn { get; set; }
        public int IsMobileVerified { get; set; }

        public DateTime CreatedOn { get; set; }
        public int ENoOfAttempts { get; set; }
        public int MNoOfAttempts { get; set; }
        public string Firstname { get; set; }
        public string lastname { get; set; }
        public int AuthTypeId { get; set; }
        public string AltPhonenumber { get; set; }
        public string Altemail { get; set; }
        public string AccountNo { get; set; }
        public string NewPassword { get; set; }
        public object Mobileotp { get; set; }

        public object Emailotp { get; set; }
    }
    public class VehicleBooking
    {
        public string flag { get; set; }
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int BNo { get; set; }
        public string BookingType { get; set; }

        public string ReqVehicle { get; set; }
        public string Customername { get; set; }
        public string CusID { get; set; }
        public string PhoneNo { get; set; }
        public string AltPhoneNo { get; set; }
        public string CAddress { get; set; }
        public string PickupAddress { get; set; }
        public string LandMark { get; set; }
        public string Package { get; set; }
        public string PickupPalce { get; set; }
        public string DropPalce { get; set; }
        public string ReqType { get; set; }
        public int ExtraCharge { get; set; }
        public int NoofVehicles { get; set; }
        public string ExecutiveName { get; set; }
        public int VID { get; set; }
        public string BookingStatus { get; set; }
        public string CustomerSMS { get; set; }
        public string CancelReason { get; set; }
        public decimal CBNo { get; set; }
        public string ModifiedBy { get; set; }
        public string CancelBy { get; set; }
        public string ReconfirmedBy { get; set; }
        public string AssignedBy { get; set; }

        public float lat { get; set; }
        public float lng { get; set; }
        public string Mobileotp { get; set; }

        public int VehicleGroupId { get; set; }
        public int VehicleTypeId { get; set; }
        public float Rating { get; set; }
        public string RatedBy { get; set; }
        public string Comments { get; set; }
        public string PMobNo { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int SrcId { get; set; }
        public int DestId { get; set; }
        public string Src { get; set; }
        public string Dest { get; set; }
        public int packageId { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? ETA { get; set; }
        public string BookingId { get; set; }
        public DateTime? BookedDate { get; set; }
        public DateTime? BookedTime { get; set; }
        public DateTime? DepartueDate { get; set; }
        public DateTime? DepartureTime { get; set; }
        public float SrcLatitude { get; set; }
        public float SrcLongitude { get; set; }
        public float DestLatitude { get; set; }
        public float DestLongitude { get; set; }
        public int VechId { get; set; }
        public decimal Pricing { get; set; }
        public int DriverId { get; set; }
        public string DriverPhoneNo { get; set; }
        public string CustomerPhoneNo { get; set; }
        public int CustomerId { get; set; }
        public int NoofSeats { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? ClosingTime { get; set; }
        public DateTime? CancelledOn { get; set; }
        public int cancellationType { get; set; }
        public string CancelledBy { get; set; }
        public string BookingChannel { get; set; }
        public string Reasons { get; set; }
        public String BVerificationCode { get; set; }
        public string OTPVerification { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string BookingOTP { get; set; }
        public int UpdatedBy { get; set; }
        public int UpdatedUserId { get; set; }
        public float DriverRating { get; set; }
        public int DriverRated { get; set; }
        public string DriverComments { get; set; }
        public string distance { get; set; }
    }
     public class Complaints
    {
         public string flag { get; set; }
        public string PhoneNumber { get; set; }

        public string EmailId { get; set; }
         public string Description { get; set; }
         public string TicketNo { get; set; }
         public string Name { get; set; }
         public string Category { get; set; }
         public string Subject { get; set; }
        

    }
  public class contact
  {
      public string name { get; set; }
      public string email { get; set; }
      public string category { get; set; }
      public string subject { get; set; }
      public string message { get; set; }
  }

     public class hirevehicle
  {
      public string Source { get; set; }
      public string destination { get; set; }
      public string source { get; set; }
      public string flag { get; set; }
      public int Id { get; set; }
      public string Username { get; set; }
      public string Firstname { get; set; }
      public string lastname { get; set; }
      public string Email { get; set; }
      public string Mobilenumber { get; set; }
      
      public string Holdername { get; set; }
      public string Cardnumber { get; set; }
      
      public string ExpMonth { get; set; }
      public string ExpYear { get; set; }
  }

     public class getalyft
     {
         public int Mobilenumber { get; set; }
        
     }

    public class POBooking
    {
        public string flag { get; set; }
        public int Id { get; set; }
        public string Src { get; set; }
        public string Dest { get; set; }
        public decimal SrcLat { get; set; }
        public decimal SrcLong { get; set; }
        public decimal DestLat { get; set; }
        public decimal DestLong { get; set; }
        public string BookingType { get; set; }
        public decimal Pricing { get; set; }
        public int PackageId { get; set; }
        public string MobileNumber { get; set; }
        public string Mobileotp { get; set; }

        public decimal Distance { get; set; }
        public int VehicleGroupId { get; set; }
        public int vehicleTypeId { get; set; }
        public int PaymentTypeId { get; set; }
      

    }


    public class ConfigData
    {
        public int includeStatus { get; set; }
        public int includeCategories { get; set; }
        public int includeLicenseCategories { get; set; }
        public int includeVehicleGroup { get; set; }
        public int includeGender { get; set; }
        public int includeFrequency { get; set; }
        public int includePricingType { get; set; }
        public int includeTransactionType { get; set; }
        public int includeApplicability { get; set; }
        public int includeFeeCategory { get; set; }
        public int includeTransChargeType { get; set; }
        public int includeVehicleType { get; set; }
        public int includeVehicleModel { get; set; }
        public int includeVehicleMake { get; set; }
        public int includeDocumentType { get; set; }
        public int includePaymentType { get; set; }
        public int includeMiscellaneousTypes { get; set; }
        public int includeCardCategories { get; set; }
        public int includeCardTypes { get; set; }
        public int includeVehicleLayoutType { get; set; }
        public int includeLicenseFeatures { get; set; }
        public int includeCardModels { get; set; }
        public int includeCards { get; set; }
        public int includeTransactions { get; set; }
        public int includeCountry { get; set; }
        public int includeActiveCountry { get; set; }
        public int includeFleetOwner { get; set; }
        public int includeUserType { get; set; }
        public int includeAuthType { get; set; }
        public int includeState { get; set; }

        public int includePackageNames { get; set; }

        public int includePackageTypeName { get; set; }
    }




}

