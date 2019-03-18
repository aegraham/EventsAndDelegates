# Events and Delegates

# Events

A event is  mechanism for communication between objects. When something happens in a object it can notify other objects that something has happened. This allows us to have loosely coupled applications.

# Delegate 
Is an agreement/contract between and Publisher and A subscriber
Determines the signature of the event and the event handler method in the subscriber.
1.	Define a delegate
2.	Define an event based on that delegate
3.	Raise the event

Example delegate, which will be used by subscribing classes. To create the name, we say the name of the event and then we append the event handler. The first parameter is an object, which is the source of the event. Then EventArgs, any additional data we might want to send.

In this example we will be creating and event calendar which allows you to create events and different services will subscribe the to calendar.

```sh
public delegate void EvenCalendarEventHandler(object source, EventArgs args); 
```
> In C# they have created the EventHandler which reduces our code. I will cover this at the end of the example.

# Create an event
Need to define an event, notice it is past tense. 
```sh
public event EvenCalendarEventHandler EventCreated;
```
To raise an event, we need a method that is responsible for it. Methods should eb protected virtual and void. Naming convention they should start with the word on and then the name of the event
 ```sh
protected virtual void OnEventCreated(Event @event)
{
    // Checks to make sure that the list of event isn't empty and someone has subscribed to
    if (EventCreated != null)  the event
    {
        EventCreated(this, EventArgs.Empty);
    }
}
```
The above checks to see if anyone is subscribed to the event and then passes through “this” which is the current object and then no eventargs. 

# Subscribers

Create a mail service classes with the event
 ```sh
public class MailService
{
    public void OnEventCreated(object source, EventArgs e)
    {
         Console.WriteLine("Mail Service: Sending an email .... ");
    }
}
```
We then need to subscribe to event. This would general be done in application start up. For exmaple for Umbraco we can subscribe to the Publish and Deleted events for indexing. As this is a simple console app we will do it in the program main file.
 ```sh
static void Main(string[] args)
{
    var event1 =new Event() {Title ="Event 1"};
    var eventCalendar = new EventCalendar(); //publisher
    var mailService = new MailService();// subscriber
            
     // registers the handler for the event. This is a pointer to a method
    eventCalendar.EventCreated += mailService.OnEventCreated; 
    eventCalendar.Create(event1);
}
```
As you can see in the code above we create our publisher which is EventCalendar and we create our subscriber which is MailService. We then state that when an event is created we want to call OnEventCreated in the mail service. When tehn call then event.

When you run the application you get the following
```sh
Create event ....
Mail Service: Sending an email ....
Press any key to continue . . .
```

You can see we have created an event and then the mail service has been called and has sent and email.
What we can do now is hook this up to a text message service

Create a text message service classes with the event
 ```sh
public class MessageService
{
    public void OnEventCreated(object source, EventArgs e)
    {
         Console.WriteLine("Message Services: Sending and text message ... .... " );
    }
}
```
When the need to subscribe to the event.

 ```sh
static void Main(string[] args)
    {
        var event1 =new Event() {Title ="Event 1"};
        var eventCalendar = new EventCalendar(); //publisher
        var mailService = new MailService();// subscriber
        var messageService = new MessageService(); //subscriber
            
        // registers the handler for the event. This is a pointer to a method
        eventCalendar.EventCreated += mailService.OnEventCreated; 
            
        //registers the handler for the event. This is a pointer to a method
        //eventCalendar.EventCreated += messageService.OnEventCreated; 
        eventCalendar.Create(event1);
    }
```
Results

 ```sh
Create event ....
Mail Service: Sending an email .... Event 1
Message Services: Sending and text message ...Event 1
Press any key to continue . . .
```

As you can see we haven't had to change the Event Calendar code at call. That stays the same, but both services are able to there communication when an event is created.

# Custom event args
We may want to send additional information to the subsriber event, when the event is triggered. We can do this by creating custom event args class.

 ```sh
public class EventCalendarEventArgs : EventArgs
{
    public Event Event { get; set; }

}
 ```
We then update our OnEventCreated function to return the Event. We will need to pass this to the OnEventCreated method, from the create event.
 ```sh
 protected virtual void OnEventCreated(Event @event)
{
    // Checks to make sure that the list of event isn't empty and someone has subscribed to the event
    if (EventCreated != null) 
    {
        //EventCreated(this, EventArgs.Empty);
        EventCreated(this,new EventCalendarEventArgs(){Event = @event});
    }
}
 ```
 We will then need to update our services to create the new EventCalendarEventArgs
 
  ```sh
public void OnEventCreated(object source, EventCalendarEventArgs e)
{
     Console.WriteLine("Mail Service: Sending an email .... " + e.Event.Title);
}
```
```sh
public void OnEventCreated(object source, EventCalendarEventArgs calendarEventArgs)
{
    Console.WriteLine("Message Services: Sending and text message ..." + calendarEventArgs.Event.Title);
}
```
We can now pass data to the services with the information used to create the event.

# Event Handler
In C# they have created the event handler delegate. That allows you to reduce your code and also using generics assign the event args.

```sh
    public delegate void EvenCalendarEventHandler(object source, EventCalendarEventArgs args);
    public event EvenCalendarEventHandler EventCreated;
```

Becomes
```sh
 public event EventHandler<EventCalendarEventArgs> EventCreated;
```
