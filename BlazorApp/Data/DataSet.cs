using BlazorApp.Model;
using System.Text.Json;

namespace BlazorApp.Data
{
    public class DataSet
    {
        private  List<Person> _data = null;
        public  List<Person> GetData()
        {
            if (_data == null)
            {
                var path = "C:\\Tmp\\data2024.json"; // Stažený vygenerovaný JSON soubor s fake daty
                var jsonString = File.ReadAllText(path);
                _data = JsonSerializer.Deserialize<List<Person>>(jsonString) ?? new List<Person>();
            }

            return _data;
        }
    }
}
