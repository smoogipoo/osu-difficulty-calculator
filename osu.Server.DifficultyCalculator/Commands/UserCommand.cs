// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dapper;
using McMaster.Extensions.CommandLineUtils;

namespace osu.Server.DifficultyCalculator.Commands
{
    [Command("user", Description = "Computes the difficulty of all beatmaps played by a user.")]
    public class UserCommand : CalculatorCommand
    {
        [Required]
        [Argument(0, "user id", Description = "The user id of the player to compute the beatmap difficulties of.")]
        public int UserId { get; set; }

        protected override IEnumerable<int> GetBeatmaps()
        {
            using (var conn = Database.GetSlaveConnection())
            {
                if (conn == null)
                    return Enumerable.Empty<int>();

                return conn.Query<int>($"SELECT DISTINCT(`beatmap_id`) FROM `osu_scores_taiko_high` WHERE `user_id` = {UserId}");
            }
        }
    }
}
