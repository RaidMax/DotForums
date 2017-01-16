using System;
using System.ComponentModel;
using System.Reflection;

namespace DotForums.Controllers
{
    public class ForumError
    {
        public enum ErrorCodes
        {
            [Description("No Error")]
            NO_ERROR,
            [Description("Permission denied")]
            GLOBAL_PERMISSIONDENIED,
            [Description("User exists")]
            USER_DUPLICATE,
            [Description("User not found")]
            USER_NOTFOUND,
            [Description("Invalid credentials")]
            USER_BADCREDENTIALS,
            [Description("Empty credentials")]
            USER_EMPTYCREDENTIALS,
            [Description("User not authorized")]
            USER_NOTAUTHORIZED,
            USER_PASSWORDTOOLONG,
            USER_USERNAMETOOLONG,
            USER_BADPROFILEDATA,
            USER_MISMATCHEDPASSWORD,
            RANK_INVALID,
            SESSION_INVALID,
            THREAD_BADDATA,
            THREAD_EMPTYDATA,
            THREAD_CONTENTTOOLONG,
            THREAD_TITLETOOLONG,
            [Description("Thread not found")]
            THREAD_NOTFOUND,
            REPLY_SAVEFAILED,
            CATEGORY_NOTFOUND,
            CATEGORY_EMPTY,
        }

        public ErrorCodes Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
                Message = Helpers.DescriptionAttr(_code);
            }
        }

        private ErrorCodes _code;
        public string Message { get; set; }
    }

    static class Helpers
    {

        /* from http://stackoverflow.com/questions/2650080/how-to-get-c-sharp-enum-description-from-value */
        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }


}
