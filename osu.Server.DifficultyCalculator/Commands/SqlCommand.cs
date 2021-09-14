// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using Dapper;
using McMaster.Extensions.CommandLineUtils;

namespace osu.Server.DifficultyCalculator.Commands
{
    [Command(Name = "sql", Description = "Calculates the difficulty of beatmaps queried via a custom query.")]
    public class SqlCommand : CalculatorCommand
    {
        [Argument(0, "sql", Description = "The SQL query.")]
        public string Sql { get; set; }

        protected override IEnumerable<int> GetBeatmaps()
        {
            using (var conn = Database.GetSlaveConnection())
            {
                if (conn == null)
                    return Enumerable.Empty<int>();

                return conn.Query<int>(Sql);
            }
        }
    }
}
