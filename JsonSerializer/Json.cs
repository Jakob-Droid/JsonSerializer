using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace JsonSerializer
{
    public static class Json
    {


        public static string Serialize(this object obj)
        {
            StringBuilder builder = new StringBuilder("{");
            Type type = obj.GetType();
            var props = type.GetProperties();
            var lastElement = props.Last();
            foreach (var prop in props)
            {
                builder.Append(HelperBuilder(obj, prop));
                if (lastElement != prop)
                {
                    builder.Append(' ');
                }
            }

            builder.Append('}');

            return builder.ToString();
        }

        private static string HelperBuilder(object obj, PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(string))
            {
                return '"' + prop.Name + "\": " + "\"" + prop.GetValue(obj) + "\"";
            }
            else if (prop.PropertyType == typeof(int))
            {
                return '"' + prop.Name + "\": " + prop.GetValue(obj);
            }
            else if(typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
            {
                StringBuilder builder = new StringBuilder("[");
                if ((IEnumerable)prop.GetValue(obj) != null)
                {
                    foreach (var item in (IEnumerable)prop.GetValue(obj))
                    {
                        builder.Append($"\"{item}\"");
                    }
                }

                builder.Append(']');


                return builder.ToString();
            }

            return "Error";
        }

        //Example of an incoming object:
        //{
        //  Wheels: 4,
        //  Name: "Volvo",
        //}
        public static T Deserialize<T>(this string jsonString) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            T obj = new T();

            jsonString = jsonString.Replace("{", "").Replace("}", "").Replace("\"", "");

            var jsonKeyValue = jsonString.Split(",");

            foreach (var keyValue in jsonKeyValue)
            {
                string[] arr = keyValue.Split(":");

                PropertyInfo prop = props.First(p => p.Name == arr[0]);
                PropertyInfo property = type.GetProperty(arr[0]);
                if (prop.CanWrite)
                {
                    if (prop.PropertyType == typeof(int))
                    {
                        property?.SetValue(obj, Convert.ToInt32(arr[1]));
                    }

                    else
                    {
                        property?.SetValue(obj, arr[1]);
                    }
                }
            }

            return obj;

        }
    }
}
