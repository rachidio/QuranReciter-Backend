using Microsoft.AspNetCore.Http;

namespace HolyQuran.Core.Constants
{
    public static class Constant
    {

        public const string Email = "Email";
        public const string Image = "Image";
        public const string Images = "Images";

        public const string ImageUrl = "ImageUrl";
        public const int NumberOne = 1;
        public const int NumberZero = 0;
        public const int NumberTwilve = 12;
        public const int NumberTwinty = 20;
        public const int NumberFourHundred = 400;
        public const float twintyfivePrecentage = (float)0.25;

    

    public static class RolesFilter
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string SuperAdminAndAdmin = "SuperAdmin,Admin";
        public const string UserAndAdvisor = "User,Adviser";
        public const string User = "User";
        public const string Adviser = "Adviser";


    }
    public static class Area
    {
        public const string ControlPanel = "ControlPanel";
        public const string ProfileAccount = "ProfileAccount";

    }
    public static class FilterFileExtentions
    {
        public const string png = ".png";
        public const string jpg = ".jpg";
        public const string svg = ".svg";
        public const string jpeg = ".jpeg";
        public const string pdf = ".pdf";
        public const string docx = ".docx";
        // 1mb = 1048576
        public const int _maxAllowedPosterSize = 1048576;

    }

    public static class DefaultImage
    {
        public const string ImagefileName = "default.jpeg";
        public const string FolderFileMessage = "FileMessage";
        public const string Images = "Images";
        public static IFormFile File = null;
    }
    public static class EmailTemplate
    {


    }
    public static class Response
    {
        public const string Email = "Email";
        public const string Roles = "Roles";
        public const string UserName = "UserName";
        public const string RolesSelect = "الرجاء اختار صلاحية واحدة على الأقل";
        public const string EmailIsExist = "البريد الالكتروني موجود بالفعل.";
        public const string UserIsExist = "المستخدم موجود بالفعل.";
        public const string UserNameIsExist = "اسم المستخدم موجود بالفعل.";
        public const string MaxLimit1MB = "الحد الأقصى للملف هو 1MB ?.";
        public const string FileDenied = " .pdf and .docx فقط مسموح !?.";
        public const string ImageDenied = " .jpg , .svg and .png فقط مسموح !?.";
        public const string Success = "لقد تم الاضافة بنجاح.";
        public const string Error = "يوجد حطأ بالبيانات.";
        public const string ErrorExistNotActiveWithDrawRequest = "لقد قمت بطلب سحب المبلغ من قبل وجاري التدقيق والسحب من طرف مدراء المنصة ...";
        public const string SuccessWithDrawRequest = " لقد قمت بطلب سحب المبلغ   وجاري التدقيق والسحب من طرف مدراء المنصة وسيتم التواصل والرد عبر البريد الالكتروني خلال 3 أيام  ...";

        public const string ErrorDateNow = "تاريخ الجلسة يجب أن يكون خلال اليوم او لاحقا بعد التوقيت الحالي";
        public const string ErrorDate = "تاريخ بداية الجلسة يجب ان تكون أقل من نهاية الجلسة.";
        public const string ErrorDenied = "يوجد جلسة محجوزة بالتاريخ والوقت المدخل.";
        public const string AlertSucsess = "نشكرك على تواصلك معنا سنرد قريبا على استفساراتك.";
        public const string LoginSucsess = " تم تسجيل الدخول الى الحساب.";
        public const string LogoutSucsess = " تم تسجيل الخروج من الحساب.";
        public const string EditSucsess = " تم التعديل على بيانات المستخدم ";
        public const string Payment = " تم الحجز بنجاح يرجى تاكيد الحجز عبر الدفع  ";
        public const string BalnceEquelZero = " عذراً رصيدك أقل من أو يساوي 0   ";

    }
    public static class Links
    {

    }
    public static class Actions
    {
        public const string Index = "Index";
        public const string Edit = "Edit";
        public const string Create = "Create";
    }

    }
}
