using FluentMigrator.Runner.VersionTableInfo;

namespace BPDotNet.Scripts;

[VersionTableMetaData]
public class CustomMetadataTable : DefaultVersionTableMetaData
{
    public override string TableName => "__migrations_metadata";
    public override string ColumnName => "version";
    public override string AppliedOnColumnName => "applied_on";
    public override string DescriptionColumnName => "description";
    public override string UniqueIndexName => "migrations_metadata_uk01";
}