﻿using Ans.Net8.Common;
using System.Globalization;

namespace Ans.Net8.Psql
{

	public static class SuppSql
    {

        /*
		 * string GetValue(int value);
		 * string GetValue(int? value);
		 * string GetValue(long value);
		 * string GetValue(long? value);
		 * string GetValue(float value);
		 * string GetValue(float? value);
		 * string GetValue(double value);
		 * string GetValue(double? value);
		 * string GetValue(decimal value);
		 * string GetValue(decimal? value);
		 * string GetValue(DateTime value);
		 * string GetValue(DateTime? value);
		 * string GetValue(bool value);
		 * string GetValue(string value);
		 * 
		 * string GetValueAsIntOrNULL(object value);
		 * string GetValueAsIntOr0(object value);
		 * string GetValueAsLongOrNULL(object value);
		 * string GetValueAsLongOr0(object value);
		 * string GetValueAsFloatOrNULL(object value);
		 * string GetValueAsFloatOr0(object value);
		 * string GetValueAsDoubleOrNULL(object value);
		 * string GetValueAsDoubleOr0(object value);
		 * string GetValueAsDecimalOrNULL(object value);
		 * string GetValueAsDecimalOr0(object value);
		 * string GetValueAsDateTimeOrNULL(object value);
		 * string GetValueAsBool(object value);
		 * string GetValueAsString(object value);
		 */


        public static string GetValue(
            int value)
        {
            return value.ToString();
        }


        public static string GetValue(
            int? value)
        {
            if (value == null)
                return "NULL";
            return GetValue(value.Value);
        }


        public static string GetValue(
            long value)
        {
            return value.ToString();
        }


        public static string GetValue(
            long? value)
        {
            if (value == null)
                return "NULL";
            return GetValue(value.Value);
        }


        public static string GetValue(
            float value)
        {
            return value.ToString(
                CultureInfo.InvariantCulture);
        }


        public static string GetValue(
            float? value)
        {
            if (value == null)
                return "NULL";
            return GetValue(value.Value);
        }


        public static string GetValue(
            double value)
        {
            return value.ToString(
                CultureInfo.InvariantCulture);
        }


        public static string GetValue(
            double? value)
        {
            if (value == null)
                return "NULL";
            return GetValue(value.Value);
        }


        public static string GetValue(
            decimal value)
        {
            return value.ToString(
                CultureInfo.InvariantCulture);
        }


        public static string GetValue(
            decimal? value)
        {
            if (value == null)
                return "NULL";
            return GetValue(value.Value);
        }


        public static string GetValue(
            DateTime value)
        {
            string s1 = value.ToString("yyyy-MM-dd HH:mm:ss.000");
            return $"'{s1}'";
            //return $"CAST(N'{s1}' AS DateTime)";
        }


        public static string GetValue(
            DateTime? value)
        {
            if (value == null)
                return "NULL";
            return GetValue(value.Value);
        }


        public static string GetValue(
            bool value)
        {
            if (value)
                return "'t'";
            return "'f'";
        }


        public static string GetValue(
            string value)
        {
            if (string.IsNullOrEmpty(value))
                return "NULL";
            string s1 = value.Replace("'", "''");
            return $"N'{s1}'";
        }





        public static string GetValueAsIntOrNULL(
            object value)
        {
            return GetValue(
                value?.ToString().ToInt());
        }


        public static string GetValueAsIntOr0(
            object value)
        {
            return GetValue(
                value?.ToString().ToInt(0));
        }


        public static string GetValueAsLongOrNULL(
            object value)
        {
            return GetValue(
                value?.ToString().ToLong());
        }


        public static string GetValueAsLongOr0(
            object value)
        {
            return GetValue(
                value?.ToString().ToLong(0));
        }


        public static string GetValueAsFloatOrNULL(
            object value)
        {
            return GetValue(
                value?.ToString().ToFloat());
        }


        public static string GetValueAsFloatOr0(
            object value)
        {
            return GetValue(
                value?.ToString().ToFloat(0));
        }


        public static string GetValueAsDoubleOrNULL(
            object value)
        {
            return GetValue(
                value?.ToString().ToDouble());
        }


        public static string GetValueAsDoubleOr0(
            object value)
        {
            return GetValue(
                value?.ToString().ToDouble(0));
        }


        public static string GetValueAsDecimalOrNULL(
            object value)
        {
            return GetValue(
                value?.ToString().ToDecimal());
        }


        public static string GetValueAsDecimalOr0(
            object value)
        {
            return GetValue(value.ToString().ToDecimal(0));
        }


        public static string GetValueAsDateTimeOrNULL(
            object value)
        {
            if (value == null)
                return "NULL";
            return GetValue(value.ToString().ToDateTime());
        }


        public static string GetValueAsBool(
            object value)
        {
            return GetValue((bool)value);
        }


        public static string GetValueAsString(
            object value)
        {
            return GetValue(value.ToString());
        }

    }

}