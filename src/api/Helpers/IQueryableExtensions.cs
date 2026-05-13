using System.Linq.Dynamic.Core;

namespace TournamentManagementSystem.Helpers
{
    public static class IQueryableExtensions
    {
        //orderBy=firstName,-lastName
        public static IQueryable<T> ApplyOrdering<T>
            (this IQueryable<T> source,
            string orderBy,
            params string[] allowedFields)
        {
            if (string.IsNullOrWhiteSpace(orderBy) || allowedFields.Length == 0)
                return source;
     
            //"FirstName"   => "FirstName",
            //"LastName"    => "LastName",
            //"DateOfBirth" => "DateOfBirth",
            //"Position"    => "Position"
            var map = allowedFields.ToDictionary(f => f, f => f, StringComparer.OrdinalIgnoreCase);


            var pieces = orderBy
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(singleOrder =>
                {
                    var desc = singleOrder.StartsWith("-");
                    var name = desc ? singleOrder.Substring(1) : singleOrder;

                    if (!map.TryGetValue(name, out var actual))
                        throw new ArgumentException($"Cannot sort by {name}");

                    return $"{actual} {(desc ? "descending" : "ascending")}";
                });

            var final = string.Join(",", pieces);
            return source.OrderBy(final);
        }
        

        //var finalSort = string.Empty;
        //var sorters = orderBy.Split (',');
        //foreach (var item in sorters)
        //{
        //    if (finalSort != string.Empty)
        //        finalSort += ",";
        //    finalSort += SortOneField(item);
        //}
        //return source.OrderBy(finalSort);


        //private static bool IsPlayerSortField(string orderBy)
        //{
        //    string[] allowedFields = { "FirstName", "LastName", "DateOfBirth", "Position" };
        //    return allowedFields.Contains(orderBy, StringComparer.OrdinalIgnoreCase);
        //}

        //public static string SortOneField(string item)
        //{
        //    var isDescending = item.StartsWith("-");
        //    var field = isDescending ? item.Substring(1) : item;

        //    if(!IsPlayerSortField(field))
        //        throw new Exception($"Invalid ordering parameter: {field}");

        //    return $"{field} {(isDescending ? "descending" : "ascending")}";
        //}

    }
}
