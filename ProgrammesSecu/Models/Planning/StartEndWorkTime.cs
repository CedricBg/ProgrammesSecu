

namespace ProgrammesSecu.Models.Planning;

public class StartEndWorkTime
{
    public int StartId { get; set; }

    public DateTime? ArrivingTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int SiteId { get; set; }

    public int EmployeeId { get; set; }
}
