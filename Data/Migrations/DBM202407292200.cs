using FluentMigrator;
using Microsoft.EntityFrameworkCore;

namespace TimeKeeperApp.Data.Migrations
{
    [Migration(202407292200)]
    public class DBM202407292200 : ForwardOnlyMigration
    {
        public override void Up()
        {
            Delete.Table("timerecord");
            Create.Table("timerecord")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable()
                .WithColumn("record_date").AsDateTime().NotNullable()
                .WithColumn("starttime_at").AsDateTime().NotNullable()
                .WithColumn("endtime_at").AsDateTime().NotNullable()
                .WithColumn("breaktime").AsDouble().NotNullable().WithDefaultValue(0.0)
                .WithColumn("note").AsString().NotNullable().WithDefaultValue("");
        }
    }
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