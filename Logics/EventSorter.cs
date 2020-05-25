using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Logics
{
    public static class EventSorter
    {
        public static IEnumerable<Event> OrderByName(IEnumerable<Event> events, OrderRequest orderRequest)
        {
            return orderRequest.Ascending ?
                events.OrderBy(e => e.name) :
                events.OrderByDescending(e => e.name);
        }

        public static IEnumerable<Event> OrderByStartDate(IEnumerable<Event> events, OrderRequest orderRequest)
        {
            return orderRequest.Ascending ? 
                OrderByStartDateAscending(events.ToList()) :
                OrderByStartDateDescending(events.ToList());
        }

        private static IEnumerable<Event> OrderByStartDateAscending(List<Event> events)
        {
            ICollection<Event> exclusions = new List<Event>();

            // filter out all events without availible future hostings and get the first known upcomming eventdate for the unfiltered events
            for (int i = 0; i < events.Count(); i++)
            {
                Event @event = events[i];

                // get upcomming events
                @event.datePlannings = @event.datePlannings.Where(dp => dp.start > DateTime.Now).OrderBy(dp => dp.start).ToList();

                // check if a future event date is known
                if (@event.datePlannings.Count() == 0)
                {
                    exclusions.Add(@event);
                    events.RemoveAt(i);
                    i--;
                    continue;
                }

                events[i] = @event;
            }

            // orden the events by ascending the event start date 
            events = events.OrderBy(e => e.datePlannings.First().start).ToList();

            // add the filtered events without known future event date to the end of the list
            foreach (Event @event in exclusions)
            {
                events.Add(@event);
            }

            return events;
        }

        private static IEnumerable<Event> OrderByStartDateDescending(List<Event> events)
        {
            ICollection<Event> exclusions = new List<Event>();

            // filter out all events without availible future hostings and get the first known upcomming eventdate for the unfiltered events
            for (int i = 0; i < events.Count(); i++)
            {
                Event @event = events[i];

                // get upcomming events
                @event.datePlannings = @event.datePlannings.Where(dp => dp.start > DateTime.Now).OrderBy(dp => dp.start).ToList();

                // check if a future event date is known
                if (@event.datePlannings.Count() == 0)
                {
                    exclusions.Add(@event);
                    events.RemoveAt(i);
                    i--;
                    continue;
                }

                events[i] = @event;
            }

            // orden the events by descending the event start date 
            events = events.OrderByDescending(e => e.datePlannings.First().start).ToList();

            // add the filtered events without known future event date to the end of the list
            foreach (Event @event in exclusions)
            {
                events.Add(@event);
            }

            return events;
        }

        /*
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, OrderRequest orderRequest)
        {
            // Creating a parameter for the expression tree with the name x, so we can do this for example: x.name
            var param = Expression.Parameter(typeof(T), "x");

            // create an instance of the requested property (column)
            var prop = Expression.Property(param, orderRequest.FieldName);

            // create a lambda expresion, so we get something like this: x.name
            var exp = Expression.Lambda(prop, param);

            // define if you want to order by ascending or descending
            string method = orderRequest.Ascending ? "OrderBy" : "OrderByDescending";

            // create a type array with the data requeired to call the expression
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };

            // created the expression to execute, like this: object.OrderBy(x => x.name).AsNoTracking();
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);

            // execute the created expression and return its data
            return q.Provider.CreateQuery<T>(mce);
        }
        */

    }
}
