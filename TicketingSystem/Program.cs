using System.Text.Json;

namespace TicketingSystem;

    public class Program
    {
        static void Main(string[] args)
        {

        string json = File.ReadAllText("data.json");
        var database = JsonSerializer.Deserialize<Database>(json);
       // var database = new Database();

        var mainApp = new MainApp(database);
        while(true)
        {
            mainApp.Run();
        }
            
        }
    }
