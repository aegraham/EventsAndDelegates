// --------------------------------------------------------------------------------------------------------------------
// <copyright company="twentysix" file="MessageService.cs">
// Copyright (c) twentysix.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace EventsAndDelegates.Services
{
    using System;

    using EventsAndDelegates.Events;

    public class MessageService
    {
        public void OnEventCreated(object source, EventCalendarEventArgs calendarEventArgs)
        {
            Console.WriteLine("Message Services: Sending and text message ..." + calendarEventArgs.Event.Title);
        }
    }
}