// --------------------------------------------------------------------------------------------------------------------
// <copyright company="twentysix" file="VideoEncoder.cs">
// Copyright (c) twentysix.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace EventsAndDelegates.Events
{
    using System;
    using System.Threading;

    public class EventCalendarEventArgs : EventArgs
    {
        public Event Event { get; set; }

    }

    public class EventCalendar
    {
        // Original before adding out own custom EventArgs
        // public delegate void EvenCalendarEventHandler(object source, EventArgs args); 

        // public delegate void EvenCalendarEventHandler(object source, EventCalendarEventArgs args);
        // public event EvenCalendarEventHandler EventCreated;

        // Built into C# os Event Handler so we don't need to declare the delegate
        public event EventHandler<EventCalendarEventArgs> EventCreated;
        
        
        public void Create(Event @event)
        {
            Console.WriteLine("Create event ....");
            Thread.Sleep(3000);
            OnEventCreated(@event);
        }

        protected virtual void OnEventCreated(Event @event)
        {
            if (EventCreated != null) // Checks to make sure that the list of event isn't empty and someone has subscribed to the event
            {
                //EventCreated(this, EventArgs.Empty);
                EventCreated(this,new EventCalendarEventArgs(){Event = @event});
            }
        }
    }

}