using System.ComponentModel.DataAnnotations;
using NPoco;


namespace TimeKeeperApp.Models;

[TableName("timerecord")]
[PrimaryKey("id", AutoIncrement = false)]
public class TimeRecord
{

    [Required]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Required]
    [Column("record_date")]
    public DateTime RecordDate { get; set; }

    [Required]
    [Column("starttime_at")]

    public DateTime StartTime { get; set; }

    [Required]
    [Column("endtime_at")]

    public DateTime EndTime { get; set; }


    [Column("breaktime")]
    public double BreakTime { get; set; } = 0;


    [Column("note")]
    public string Note { get; set; } = "";

    [Column("is_valid")]
    public bool IsValid { get; set; } = false;

    [Ignore]
    public double WorkTime { get; set; }


}

// CREATE TABLE "timerecord" (
// 	"id"	TEXT NOT NULL,
// 	"created_at"	TEXT NOT NULL,
// 	"updated_at"	TEXT,
// 	"starttime_at"	TEXT NOT NULL,
// 	"endtime_at"	TEXT NOT NULL,
// 	"breaktime"	NUMERIC NOT NULL DEFAULT 0,
// 	"note"	TEXT NOT NULL DEFAULT "",
// 	PRIMARY KEY("id")
// );