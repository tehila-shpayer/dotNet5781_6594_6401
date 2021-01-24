using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;

namespace PL
{
    public static class Tools
    {
        /// <summary>
        /// extension method for
        /// RTTI for ToString
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="t">"this" type</param>
        /// <returns></returns>
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                if (item.Name != "IsActive")
                    str += "\n" + item.Name + ": " + item.GetValue(t, null);
            return str;
        }

        /// <summary>
        /// פונקציה להצפנת סיסמאות
        /// הפונקציה מערבלת מספר כלשהו עם הסיסמא האמיתי
        /// </summary>
        /// <param name="passwordWithSalt">מחרוזת המספר והסיסמא מחוברים</param>
        /// <returns>הסיסמא המוצפנת</returns>
        public static string hashPassword(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }
    }
}
