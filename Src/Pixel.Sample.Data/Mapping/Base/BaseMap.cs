using FluentNHibernate.Mapping;
using Pixel.Sample.Core.Domain.Base;

namespace Pixel.Sample.Data.Mapping
{
    public abstract class BaseMap<T> : ClassMap<T> where T : BaseEntityIdent<int>
    {
        private const string SequencePrefix = "sqce_table_";
        protected readonly string SequenceName;
        protected const string TablePrefix = "";

        private const string DEFAULT_ID_NAME = "Id";
        private const string DEFAULT_DB_SCHEMA = "dbo";
        protected string DbSchema { get; set; }

        protected string TableName { get; set; }

        protected BaseMap(string dbSchema, string tablePrefix, string tableName, string idColumnName)
        {
            /*Init*/
            TableName = $"{TablePrefix}{tableName}"; ;
            SequenceName = $"{SequencePrefix}{TableName}";

            DbSchema = dbSchema;
            TableName = $"{tablePrefix}{tableName}";

            /* Mapping */
            Table(TableName);

            Schema(DbSchema);

            Id(x => x.Id).Column(idColumnName)
                .GeneratedBy.Sequence(SequenceName)
                .Not.Nullable();
        }

        protected BaseMap(string tableName) : this(DEFAULT_DB_SCHEMA, string.Empty, tableName, DEFAULT_ID_NAME)
        {
        }

    }
}