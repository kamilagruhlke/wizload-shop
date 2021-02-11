using System.Collections.Generic;

namespace Orders.Domain.Utils.Dictionaries
{
    public static class StatusDictionary
    {
        public static string Pending => "PENDING";

        public static string InProgress => "IN_PROGRESS";

        public static string Finished => "FINISHED";

        public static IEnumerable<string> Statuses()
        {
            yield return Pending;
            yield return InProgress;
            yield return Finished;
        }
    }
}
