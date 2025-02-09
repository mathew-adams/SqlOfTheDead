using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlOfTheDead.Models;

public class ZombieSettings
{
    public string ConnectionString { get; set; } = "";

    public IEnumerable<string> ValidateConnection(string connection)
    {
        if(string.IsNullOrEmpty(connection))
        {
            yield return "A connection string is required";
            yield break;
        }
    }
}
