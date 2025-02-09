using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOfTheDead.Models;

public static class ZombieTypes
{
    public static List<ZombieType> GetTypes()
    {
        return new List<ZombieType>()
        {
            new ZombieType() { Name = "Decimal", Type=typeof(decimal), SqlType= SqlDbType.Decimal, DefaultValue = "", DefaultLength = "13, 4"},
            new ZombieType() { Name = "String", Type=typeof(string), SqlType= SqlDbType.NVarChar, DefaultValue = "", DefaultLength = "50"},
            new ZombieType() { Name = "Date", Type=typeof(DateTime), SqlType= SqlDbType.DateTime2, DefaultValue = "'9999-12-31'", DefaultLength = "7"},
            new ZombieType() { Name = "Integer", Type=typeof(int), SqlType= SqlDbType.Int, DefaultValue = "", DefaultLength = ""},
            new ZombieType() { Name = "Short", Type=typeof(short), SqlType= SqlDbType.SmallInt, DefaultValue = "", DefaultLength = ""},
            new ZombieType() { Name = "Long", Type=typeof(long), SqlType= SqlDbType.BigInt, DefaultValue = "", DefaultLength = ""},
            new ZombieType() { Name = "Byte", Type=typeof(byte), SqlType= SqlDbType.TinyInt, DefaultValue = "", DefaultLength = ""},
            new ZombieType() { Name = "Boolean", Type=typeof(bool), SqlType= SqlDbType.Bit, DefaultValue = "", DefaultLength = ""},
            new ZombieType() { Name = "Guid", Type=typeof(Guid), SqlType= SqlDbType.UniqueIdentifier, DefaultValue = "", DefaultLength = ""}
        };
    }

    public static IEnumerable<ZombieField> GetFieldTemplate(Guid tableId)
    {
        yield return new ZombieField() { Id = Guid.CreateVersion7(), TableId = tableId, Name = "Id", Type = "Integer", IsIdentity = true, Order = 0 };
        yield return new ZombieField() { Id = Guid.CreateVersion7(), TableId = tableId, Name = "Secondary_Id", Type = "Integer", Order = 1 };
        yield return new ZombieField() { Id = Guid.CreateVersion7(), TableId = tableId, Name = "Name", Type = "String", Length = "10", Order = 2 };
        yield return new ZombieField() { Id = Guid.CreateVersion7(), TableId = tableId, Name = "Created", Type = "Date", Length = "7", DefaultValue = "'9999-12-31'", Order = 3 };
        yield return new ZombieField() { Id = Guid.CreateVersion7(), TableId = tableId, Name = "Active", Type = "Boolean", DefaultValue = "1", Order = 4 };
    }

    public static SqlDbType GetSqlDbType(string name)
    {
        if(string.IsNullOrEmpty(name)) 
            throw new ArgumentException("Datatype is required to resolved to a SqlDb type.");

        var dbType = GetTypes().Where(f => f.Name == name).FirstOrDefault();
        return dbType!.SqlType;
    }

    public static bool HasLength(string name)
    {
        if(string.IsNullOrEmpty(name)) return false;
        var type = GetTypes().Where(f => f.Name == name).First();
        return !string.IsNullOrEmpty(type.DefaultLength);
    }

    public static bool CanBeIdentity(string name)
    {
        return name switch
        {
            "Integer" => true,
            "Short" => true,
            "Long" => true,
            "Byte" => true,
            _ => false
        };
    }
    public static string GetScript(ZombieTable table)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("create table [").Append(table.Name).Append(']').Append(Environment.NewLine);
        sb.Append('(');

        bool hasLength = false;
        foreach (var field in table.Fields)
        {
            SqlDbType sqlType = ZombieTypes.GetSqlDbType(field.Type);
            sb.Append(Environment.NewLine);
            sb.Append('\t').Append('[').Append(field.Name).Append("] ");
            sb.Append(sqlType.ToString().ToLower());
            hasLength = ZombieTypes.HasLength(field.Type);
            if (hasLength) sb.Append('(').Append(field.Length).Append(')');
            if (field.IsIdentity) sb.Append(" identity (1,1)");
            else if (field.DefaultValue is string dv && !string.IsNullOrEmpty(dv)) sb.Append(" default ").Append(dv);
            else if (field.AllowNulls) sb.Append(" null");
            else if (!field.AllowNulls) sb.Append(" not null");

            sb.Append(',');
        }

        foreach (var index in table.Indexes)
        {
            sb.Append(Environment.NewLine).Append('\t');
            if (index.Primary)
            {
                sb.Append("constraint ").Append(index.Name);
                sb.Append(" primary key ");
                if (index.Clustered) sb.Append("clustered ");
                if (index.NonClustered) sb.Append("nonclustered ");
            }
            else if (index.Unique)
            {
                sb.Append("constraint ").Append(index.Name);
                sb.Append(" unique ");
                if (index.Clustered) sb.Append("clustered ");
                if (index.NonClustered) sb.Append("nonclustered ");
            }
            else //Nonclustered
            {
                sb.Append("index ").Append(index.Name); ;
                sb.Append(" nonclustered ");
            }
            sb.Append('(');
            foreach (var field in index.Fields!)
            {
                sb.Append('[').Append(field.Name).Append(']').Append(',').Append(' ');
            }
            sb.Length += -2;
            sb.Append(')').Append(',');
        }

        sb.Append(Environment.NewLine).Append(')').Append(';').Append(Environment.NewLine);

        return sb.ToString();
    }
}

public class ZombieType
{
    public required Type Type { get; set; } 
    public SqlDbType SqlType { get; set; }
    public string Name { get; set; } = "";
    public string DefaultLength { get; set; } = "";
    public string DefaultValue { get; set; } = "";
}