using System;

namespace Domain.Services
{
    public class CurrentDateTimeService : IDateTimeService
    {
        public DateTime GetUtc() => DateTime.UtcNow;
    }
}
