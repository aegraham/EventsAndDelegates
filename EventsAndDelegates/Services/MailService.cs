// --------------------------------------------------------------------------------------------------------------------
// <copyright company="twentysix" file="MailService.cs">
// Copyright (c) twentysix.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace EventsAndDelegates.Services
{
    using System;

    using EventsAndDelegates.Events;

    public class MailService
    {
        public void OnEventCreated(object source, EventCalendarEventArgs e)
        {
            Console.WriteLine("Mail Service: Sending an email .... " + e.Event.Title);
        }
    }
}