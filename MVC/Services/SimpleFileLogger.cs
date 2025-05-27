namespace MVC.Services
{
    public class SimpleFileLogger //:IService
    {
        public void Log(string message)
        {
            try
            {
                // Obaluje using...
                File.AppendAllText("log.txt", $"{DateTime.Now.ToString("yyyy-MM-dd")}: {message}\n");  
               
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log to console or another file)
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
