using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public interface EventSourceService
    {
        public List<EventSource> GetEventSources();
        public EventSource GetEventSource(int id);
        public bool AddEventSource(EventSource eventSource);
        public bool UpdateEventSource(EventSource eventSource, int id);
        public bool DeleteEventSource(int id);
        public int GetCount();
    }
}
