using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace BPDotNet.Data.Persistence.Configurations
{
    internal class SequenceValueGenerator : ValueGenerator<int>
    {
        private readonly string _schema;
        private readonly string _sequenceName;

        public SequenceValueGenerator(string schema, string sequenceName)
        {
            _schema = schema;
            _sequenceName = sequenceName;
        }

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
        {
            using var command = entry.Context.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"SELECT {_schema}.{_sequenceName}.NEXTVAL FROM DUAL";
            entry.Context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }
    }
}