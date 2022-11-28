using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem;

public class MainApp
{
    public MainApp(Database database)
    {
        _database = database;
    }

    public User CurentLoggedInUser { get; set; }

    private readonly Database _database;
    public void Run()
    {
        if (CurentLoggedInUser == null)
        {
            Console.WriteLine("[1] - login");
            Console.WriteLine("[2] - create user");
            var inputs = Console.ReadLine().Split(" ");
            var output = LoginOrCreateUser(inputs);
            Console.WriteLine(output);
        }
        else
        {
            if(CurentLoggedInUser.IsAdmin())
            {
                Console.WriteLine("[0] - logout");
                Console.WriteLine("[1] - create event");
                Console.WriteLine("[2] - delete event");
                var inputs = Console.ReadLine().Split(" ");
                var output = ExecuteAdminCommands(inputs);
                Console.WriteLine(output);
            }
            else 
            {
                Console.WriteLine("[0] - logout");
                Console.WriteLine("[1] - get all events");
                Console.WriteLine("[2] - view specific event");
                Console.WriteLine("[3] - buy ticket");
                var inputs = Console.ReadLine().Split(" ");
                var output = ExecuteUserCommands(inputs);
                Console.WriteLine(output);
            }
        }
    }
    public string ExecuteAdminCommands(string[] inputs)
    {
        if (inputs[0] == "0")
        {
            CurentLoggedInUser = null;
            return "Logged out";
        }

        if (inputs[0] == "1")
        {

            var newEvent = new Event()
            {
                Price = decimal.Parse(inputs[1]),
                Size = int.Parse(inputs[2]),
                Name = inputs[3],
            };
            _database.AddEvent(newEvent);
            return "Event Created";
        }

        if (inputs[0] == "2")
        {
            var EventName = inputs[1];
            var EventToDelete = _database.Events.FirstOrDefault(x=> x.Name == EventName);
            _database.RemoveEvent(EventToDelete);
            return "Event deleted";
        }
        return "Wrong input";
    }
    public string LoginOrCreateUser(string[] inputs)
    {
        var userName = inputs[1];
        var currentUser = _database.Users.FirstOrDefault(x => x.Name == userName);
        if (inputs[0] == "1")
        {
            if (currentUser == null)
            {
                return "User id wrong";
            }
            CurentLoggedInUser = currentUser;

            return "Login succsess";
        }

        else if (inputs[0] == "2")
        {
            if (currentUser != null)
            {
                return "Username Taken";
            }
           
            var newUser = new User()
            {
                Name = userName,
            };
            _database.AddUser(newUser);
            return "User created";
        }
        //else
        //{
        //    return "Wrong input";
        //};
       return "Wrong input";
     }
    public string ExecuteUserCommands(string[] inputs)
    {
        if (inputs[0] == "0")
        {
            CurentLoggedInUser = null;
            return "Logged out";
        }
        if (inputs[0] == "1")
        {
            var eventList = "";
            foreach(var @event in _database.Events)
            {
                eventList += @event.Name + " ";
            }

            return eventList;
        }
        if (inputs[0] == "2")
        {
            var eventName = inputs[1];
            var currentEvent = _database.Events.FirstOrDefault(x => x.Name == eventName);
            return $"Kaina - {currentEvent.Price}, Dysis - {currentEvent.Size}, Vardas - {currentEvent.Name}";

            //viev specific
        }
        if (inputs[0] == "3")
        {
            //buy
            var eventName = inputs[1];
            var currentEvent = _database.Events.FirstOrDefault(x => x.Name == eventName);
            var newTicket = new BuyTicket()
            {
                Event = currentEvent,
                PuchaseDate = DateTime.Now,
            };
            _database.AddTicket(newTicket);

            return "Ticket bought";
        }
        return "Wrong input";
    }
    
}
