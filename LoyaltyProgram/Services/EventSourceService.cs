using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public interface EventSourceService
    {
        public PagedList<EventSource> GetEventSources(PagingParameters pagingParameters);
        public EventSource GetEventSource(int id);
        public bool AddEventSource(EventSource eventSource);
        public bool UpdateEventSource(EventSource eventSource, int id);
        public bool DeleteEventSource(int id);
        public int GetCount();
    }
}
