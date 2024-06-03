using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.Constants
{
    public class MessageResults
    {
 
        public const string DeleteSuccessResultAr = "تم حذف البيانات بنجاح";
        public const string DeleteSuccessResultEn = "Delete Data Successfully";
       

        public static string GetSuccessResult()
        {
            return "s: تم جلب البيانات بنجاح";
		}
		public static string LogoutSuccessResult()
		{
			return "s: تم تسجيل الخروج بنجاح";
		}
		public static string LoginSuccessResult()
		{
			return "s: تم تسجيل الدخول بنجاح";
		}
        public static string RegisterSuccessResult()
        {
            return "s: تم  انشاء حساب بنجاح ";
        }
        public static string ErrorResult()
		{
			return "d: يوجد خطأ بالبيانات  المدخلة";
        }
        public static string NotFoundResult()
        {
            return "d: لا يوجد بيانات لحتى الان ";
        }
        public static string ChangePasswordResult()
        {
            return "s:  تم تغيير كلمة المرور بنجاح";
        }
        public static string SendPasswordResult()
        {
            return "s:  تم ارسال كلمة المرور  بنجاح";
        }
        public static string ErrorDateNow()
        {
            return "d: تاريخ  يجب أن يكون خلال اليوم او لاحقا بعد التوقيت الحالي";
    }
    public static string InvalidUserNameOrEmailResult()
		{
			return "d: البريد الالكتروني  أو اسم المستخدم غير صحيح";
		}
        public static string InvalidExtentionImageResult()
        {
            return "d: امتداد الملف أو الصورة غير مسموح";
        }
        public static string InvalidAllowedImageSizeResult()
        {
            return "d: حجم الملف أو الصورة أكبر من المسموح بها";
        }
        public static string UserIsLoginResult()
		{
			return "d: عذرا يجب الدخول من حساب واحد فقط يرجى اغلاق الحساب الاخر ثم الدخول مرة أخرى";
        }
        public static string AddSuccessResultString()
        {
            return "s: تمت اضافة العنصر بنجاح";
        }
        public static object AddSuccessResult()
        {
            return new { status = 1, msg = "s: تمت اضافة العنصر بنجاح", close = 1 };
        }
        public static object UpdateStatusResult()
        {
            return new { status = 1, msg = "s: تمت تحديث الحالة بنجاح", close = 1 };
        }
        public static object EditSuccessResult()
        {
            return new { status = 1, msg = "s: تم تحديث بيانات العنصر بنجاح ", close = 1 };
        }

        public static object DeleteSuccessResult()
        {
            return new { status = 1, msg = "s: تم حذف العنصر بنجاح", close = 1 };
        }
        public static object ChangeActiveSuccessResult()
        {
            return new { status = 1, msg = "s: تم تغيير التفعيل بنجاح", close = 1 };
        }
        public static object ChangeStatusSuccessResult()
        {
            return new { status = 1, msg = "s: تم تغيير الحالة بنجاح", close = 1 };
        }
    }
}
