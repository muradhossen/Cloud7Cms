using Dapper;
using PaymentGrameenphone.Helper.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class ObjectToDataTableConverter
    {
        public static DataTable ObjectToDataTable<T>(this T data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                // Check if the property has IgnoreAttribute
                if (HasIgnoreAttribute(prop))
                    continue;

                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
            {
                // Check if the property has IgnoreAttribute
                if (HasIgnoreAttribute(prop))
                    continue;

                row[prop.Name] = prop.GetValue(data) ?? DBNull.Value;
            }
            table.Rows.Add(row);
            return table;
        }
        public static DataTable ListToDataTable<T>(this IEnumerable<T> data)
        {
            var properties = typeof(T).GetProperties();

            var dataTable = new DataTable();

            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            foreach (var item in data)
            {
                var values = properties.Select(p => p.GetValue(item)).ToArray();
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        private static bool HasIgnoreAttribute(PropertyDescriptor prop)
        {
            // Check if the property has IgnoreAttribute
            return prop.Attributes.OfType<IgnoreAttribute>().Any();
        }
    }
}
