using FluentMigrator.Runner.VersionTableInfo;

namespace TimeKeeperApp.Data;

public class VersionTableMetaData : IVersionTableMetaData
{
    public object ApplicationContext { get; set; }

    public bool OwnsSchema => true;

    public string SchemaName => "";

    public string TableName => "version_info";

    public string ColumnName => "version";

    public string DescriptionColumnName => "description";

    public string UniqueIndexName => "idx_version";

    public string AppliedOnColumnName => "applied_on";
}