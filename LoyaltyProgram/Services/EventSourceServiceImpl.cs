using LoyaltyProgram.Models;

namespace LoyaltyProgram.Services
{
    public class EventSourceServiceImpl : EventSourceService
    {
        private readonly DatabaseContext _databaseContext;
        public EventSourceServiceImpl(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddEventSource(EventSource eventSource)
        {

            _databaseContext.EventSources.Add(eventSource);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool DeleteEventSource(int id)
        {
            var eventSource = _databaseContext.EventSources.FirstOrDefault(b => b.PartnerId == id);
            if (eventSource != null)
            {
                if (eventSource.Status == 1)
                {
                    eventSource.Status = 0;
                    return _databaseContext.SaveChanges() > 0;
                }
            }
            return false;
        }

        public EventSource GetEventSource(int id)
        {
            var eventSource = _databaseContext.EventSources.FirstOrDefault(b => b.PartnerId == id);
            if (eventSource != null)
            {
                if (eventSource.Status == 1)
                {
                    return eventSource;
                }
            }
            return null;
        }

        public int GetCount()
        {
            return _databaseContext.EventSources.Where(b => b.Status == 1).Count();
        }

        public List<EventSource> GetEventSources()
        {
            return _databaseContext.EventSources.Where(b => b.Status == 1).ToList();
        }

        public bool UpdateEventSource(EventSource eventSource, int id)
        {
            var eventSourceDb = GetEventSource(id);
            if (eventSourceDb != null)
            {
                if (eventSourceDb.Status == 1)
                {
                    if (eventSource.Name != null)
                        eventSourceDb.Name = eventSource.Name;
                    if (eventSource.Status != null)
                        eventSourceDb.Status = eventSource.Status;
                    if (eventSource.Description != null)
                        eventSourceDb.Description = eventSource.Description;

                    return _databaseContext.SaveChanges() > 0;
                }
            }

            return false;
        }
    }
}
