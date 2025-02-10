using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SqlOfTheDead.Models;

[DebuggerDisplay("Name = {Name}")]
public class ZombieTable
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime Created { get; set; }
    public virtual List<ZombieField> Fields { get; set; } = [];
    public virtual List<ZombieIndex> Indexes { get; set; } = [];
}

[DebuggerDisplay("Name = {Name}, Type = {Type}")]
public class ZombieField
{
    public Guid Id { get; set; }
    [Column("TableId")]
    public Guid TableId { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public string Length { get; set; } = "";
    public string DefaultValue { get; set; } = "";
    public bool AllowNulls { get; set; }
    public bool IsIdentity { get; set; }
    public int Order { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(TableId))]
    public virtual ZombieTable Table { get; set; }  // <-- Navigation property
}

public class ZombieIndex
{
    public Guid Id { get; set; }

    [Column("TableId")]
    public Guid TableId { get; set; }

    public string Name { get; set; } = "";
    public bool Primary { get; set; }
    public bool Unique { get; set; }
    public bool Clustered { get; set; }
    public bool NonClustered { get; set; }
    public int Order { get; set; }
    public virtual List<ZombieIndexField> Fields { get; set; } = [];

    [JsonIgnore]
    [ForeignKey(nameof(TableId))]
    public virtual ZombieTable Table { get; set; }  // <-- Navigation property
}

public class ZombieIndexField
{

    public Guid Id { get; set; }

    [Column("IndexId")]
    public Guid IndexId { get; set; }

    public string Name { get; set; } = "";

    public int Order { get; set; }

    [JsonIgnore]
    [NotMapped]
    public string Category { get; set; } = "";

    [JsonIgnore]
    [ForeignKey(nameof(IndexId))]
    public virtual ZombieIndex Index { get; set; }  // <-- Navigation property
}