﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;

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
        public static void GeneralSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
