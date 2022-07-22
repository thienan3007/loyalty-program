using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Services
{
    public class EventSourceServiceImpl : EventSourceService
    {
        private readonly DatabaseContext _databaseContext;
        private ISortHelper<EventSource> _sortHelper;
        public EventSourceServiceImpl(DatabaseContext databaseContext, ISortHelper<EventSource> sortHelper)
        {
            _databaseContext = databaseContext;
            _sortHelper = sortHelper;
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

        public PagedList<EventSource> GetEventSources(PagingParameters pagingParameters)
        {
            var filterString = pagingParameters.FilterString;
            IQueryable<EventSource> eventSources;
            if (filterString != null)
            {
                eventSources =_databaseContext.EventSources.Where(b => b.Status == 1 && b.Name.Contains(filterString));
            }
            else
            {
                eventSources =_databaseContext.EventSources.Where(b => b.Status == 1);
            }

            var sortedEventSources = _sortHelper.ApplySort(eventSources, pagingParameters.OrderBy);
            if (eventSources != null)
            {
                return PagedList<EventSource>.ToPagedList(sortedEventSources, pagingParameters.PageNumber, pagingParameters.PageSize);
            }
            return null;
        }

        public bool UpdateEventSource(EventSource eventSource, int id)
        {
           if (eventSource != null)
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
            }

            return false;
        }
    }
}
