using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BlazorWasmUI.Helpers.Classes;

namespace BlazorWasmUI.Extensions
{
    public static class ListExtension
    {
        public static DataTable ToDataTable<T>(this IList<T> list) where T : class, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var displayNameAttributes = DataAnnotationsHelper.GetDisplayNameAttributes(properties);
            DataTable dataTable = new DataTable();
            for (int i = 0; i < properties.Length; i++)
            {
                dataTable.Columns.Add(displayNameAttributes[i], Nullable.GetUnderlyingType(properties[i].PropertyType) ?? properties[i].PropertyType);
            }
            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();
                for (int i = 0; i < properties.Length; i++)
                {
                    row[displayNameAttributes[i]] = properties[i].GetValue(item) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
