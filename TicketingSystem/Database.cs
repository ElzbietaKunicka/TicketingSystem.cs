using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TicketingSystem
{
    public class Database
    {
        public List<User> Users { get; set; }

        public List<Event> Events { get; set; } 
        public Database() 
        { 
            Users = new List<User> { };
            Events = new List<Event>
            {

            };
            Tickets = new List<BuyTicket>
            { };
        }

        public List<BuyTicket> Tickets { get; set; }

        public void AddUser(User user)
        {
            Users.Add(user);
            Save();
        }
        public void RemoveEvent(Event @event)
        {
            Events.Remove(@event);
            Save();
        }
        public void AddEvent(Event @event)
        {
            Events.Add(@event);
            Save();
        }
        public void AddTicket(BuyTicket ticket)
        {
            Tickets.Add(ticket);
            Save();

        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText("data.json", json);
        }
    }
}
