using System.IO;
using System.Text.Json;
namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class FileStorage
    {
        private readonly string path;

        public FileStorage(string path = "company.json")
        {
            this.path = path;
        }

        public CompanyStorage Load()
        {
            if (!File.Exists(path))
            {
                return new CompanyStorage();
            }

            var json = File.ReadAllText(path);

            var storage = JsonSerializer.Deserialize<CompanyStorage>(json);

            return storage;
        }

        public void Save(CompanyStorage storage)
        {
            var json = JsonSerializer.Serialize(storage);
            File.WriteAllText(path, json);
        }
    }
}
