namespace EventsAndDelegates
{
    using EventsAndDelegates.Events;
    using EventsAndDelegates.Services;

    class Program
    {
        static void Main(string[] args)
        {
            var event1 =new Event() {Title ="Event 1"};
            var eventCalendar = new EventCalendar(); //publisher
            var mailService = new MailService();// subscriber
            var messageService = new MessageService(); //subscriber

            eventCalendar.EventCreated += mailService.OnEventCreated; //registers the handler for the event. This is a pointer to a method
            eventCalendar.EventCreated += messageService.OnEventCreated; //registers the handler for the event. This is a pointer to a method
            eventCalendar.Create(event1);
        }
    }
}
