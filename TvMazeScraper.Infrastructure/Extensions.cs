using System;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Infrastructure
{
    public static class Extensions
    {
        public static string ToCompleteString(this Status status)
        {
            return Enum.GetName(typeof(Status), status);
        }
    }
}
