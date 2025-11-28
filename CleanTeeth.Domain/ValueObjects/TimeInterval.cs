using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects
{
    public class TimeInterval
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public TimeInterval(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                throw new BusinessRuleException("The start time must be earlier than the end time.");
            }

            Start = start;
            End = end;
        }
    }
}
