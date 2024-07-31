using FluentMigrator;

namespace TimeKeeperApp.Data.Migrations
{
    [Migration(202407292218)]
    public class DBM202407292218 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("is_valid").OnTable("timerecord").AsBoolean().NotNullable().WithDefaultValue(true);
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