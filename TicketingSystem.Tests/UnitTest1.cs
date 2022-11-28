using System.Runtime.InteropServices;

namespace TicketingSystem.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            var database = new Database();
            database.Users.Add(new User
                {
                Name = "Kestutis" 
            });
           
            var mainApp = new MainApp(database);
            var input = new string[]
            {
                "1",
                "Kestutis",
            };
           
            var result = mainApp.LoginOrCreateUser(input);
            Assert.AreEqual(result, "Login succsess");
            Assert.AreEqual(mainApp.CurentLoggedInUser.Name, "Kestutis");

        }

        [TestMethod]
        public void TestIsAdmin()
        {
            var user = new User() { Name = "admin"};
            var result = user.IsAdmin();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void TestCanAdminCreateEvent()
        {
            var user = new User() 
            { Name = "admin" };
            var database = new Database();

            var mainApp = new MainApp(database);
            var input = new string[]
            {
                "1",
                "1900",
                "2000",
                "Granatos"
            };

            var result = mainApp.ExecuteAdminCommands(input);

            Assert.AreEqual(result, "Event Created");
            Assert.AreEqual(database.Events[0].Name, "Granatos");
            Assert.AreEqual(database.Events[0].Size, 2000);
            Assert.AreEqual(database.Events[0].Price, 1900);
        }

        [TestMethod]
        public void TestCanAdmitDeleteEvents()
        {
            var database = new Database();
            database.Events.Add(new Event 
            { Name = "Granatos" });


            var mainApp = new MainApp(database);
            var input = new string[]
            {
                "2",
                "Granatos"
            };
            var result = mainApp.ExecuteAdminCommands(input);
            Assert.AreEqual(result, "Event deleted");
            Assert.AreEqual(database.Events.Count, 0);
        }

        [TestMethod]
        public void TestUserFun()
        {
            var database = new Database();
            database.Events.Add(new Event
            { Name = "Granatos" });
            database.Events.Add(new Event
            { Name = "Pupos" });


            var mainApp = new MainApp(database);
            var input = new string[]
            {
                "1",
                
            };
            var result = mainApp.ExecuteUserCommands(input);
            Assert.AreEqual(result, "Granatos Pupos ");
           
        }

        [TestMethod]
        public void TestUserFunBuyTicket()
        {
            var database = new Database();
            database.AddEvent(new Event
            { Name = "Granatos" });
            database.AddEvent(new Event
            { Name = "Pupos" });
            database.AddEvent(new Event
            { Name = "Magas" });


            var mainApp = new MainApp(database);
            var input = new string[]
            {
                "3",
                "Magas"

            };
            var result = mainApp.ExecuteUserCommands(input);
            Assert.AreEqual(result, "Ticket bought");
            Assert.AreEqual(database.Tickets[0].Event.Name, "Magas");

        }
    }
}