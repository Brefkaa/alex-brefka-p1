using Domain;
using Persistence;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PackersScheduleController : ControllerBase
{
    private static readonly string[] Opponents = new[]
    {
        "vs PHI", "vs IND", "@ TEN", "vs MIN", "@ LAR", "vs ARI", "vs HOU", "@JAX", "vs DET", "BYE WEEK", "@ CHI", "vs SF", "vs MIA", "@ DET", "@ SEA", "vs NO", "@ MIN", "vs CHI"
    };

    private static readonly string[] Dates = new[]
    {
        "9/6", "9/15", "9/22", "9/29", "10/6", "10/13", "10/20", "10/27", "11/3", "11/10", "11/17", "11/24", "11/28", "12/5", "12/15", "12/23", "12/29", "1/5"
    };

    private static readonly string[] Times = new[]
    {
        "7:15 pm", "12:00 pm", "12:00 pm", "12:00 pm", "3:25 pm", "12:00 pm", "12:00 pm", "12:00 pm", "3:25 pm", "NA", "12:00 pm", "3:25 pm", "7:20 pm", "7:15 pm", "7:20 pm", "7:15 pm", "12:00 pm", "TBD"
    };

    private readonly ILogger<PackersScheduleController> _logger;

    private readonly DataContext _context;

    public PackersScheduleController(ILogger<PackersScheduleController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetPackersSchedule")]
    public IEnumerable<PackersSchedule> Get()
    {
        return Enumerable.Range(1, 18).Select(index => new PackersSchedule
        {
            Week = "Week " + index,
            Opponent = Opponents[index - 1],
            Date = Dates[index - 1],
            Time = Times[index - 1]
        })
        .ToArray();
    }
    [HttpPost]
    public ActionResult<PackersSchedule> Create()
    {
        Console.WriteLine($"Database path: {_context.DbPath}");
        Console.WriteLine("Insert a new PackerSchedule");

        var schedule = new PackersSchedule()
        {
            Week = "Week 19",
            Opponent = "vs Packers",
            Date = "1/12",
            Time = "12:00 pm"
        };

        _context.PackersSchedules.Add(schedule);
        var success = _context.SaveChanges() > 0;

        if (success)
        {
            return schedule;
        }

        throw new Exception("Error creating PackersSchedule");
    }
}