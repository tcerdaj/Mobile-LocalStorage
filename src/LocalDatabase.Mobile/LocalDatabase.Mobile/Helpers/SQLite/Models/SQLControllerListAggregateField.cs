using System.Diagnostics.Contracts;

namespace LocalDatabase.Mobile.Helpers.SQLite.Models
{
    public class SQLControllerListAggregateField
    {
        public enum AggregateEnum
        {
            Avg,
            Count,
            Max,
            Min,
            Sum
        }

        public string FieldName { get; set; }
        public  AggregateEnum AggregateType { get; set; }
    }
}