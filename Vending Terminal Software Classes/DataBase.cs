using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vending_Terminal_Software_Classes
{
    public class DataBase
    {
        public CurrentState CurrentState { get; set; }

        public DataBase()
        {
            try
            {
                string contents = File.ReadAllText("Data\\CurrentState.json");
                CurrentState = JsonConvert.DeserializeObject<CurrentState>(contents);
            }
            catch { BasicState(); }

            Save();
        }

        public void BasicState()
        {
            CurrentState = new CurrentState();

            CurrentState.Money = 0;

            CurrentState.Product = new List<Product>
            {
                new Product { Name = "Mars", Amount = 20, Price = 45 },
                new Product { Name = "Snickers", Amount = 25, Price = 55 },
                new Product { Name = "Twix", Amount = 20, Price = 45 },
                new Product { Name = "Iron ore", Amount = 100, Price = 37 },
                new Product { Name = "Nuka-Cola", Amount = 50, Price = 75 }
            };

            CurrentState.Banknotes_n_Coins = new List<Banknotes_n_Coins>
            {
                new Banknotes_n_Coins { Cost=1000, Amount=0, CanBeChange=false },
                new Banknotes_n_Coins { Cost=500, Amount=0, CanBeChange=false },
                new Banknotes_n_Coins { Cost=100, Amount=0, CanBeChange=false },
                new Banknotes_n_Coins { Cost=50, Amount=0, CanBeChange=false },
                new Banknotes_n_Coins { Cost=10, Amount=20, CanBeChange=true },
                new Banknotes_n_Coins { Cost=5, Amount=40, CanBeChange=true },
                new Banknotes_n_Coins { Cost=2, Amount=100, CanBeChange=true },
                new Banknotes_n_Coins { Cost=1, Amount=200, CanBeChange=true }
            };

            Save();
        }

        public void Save()
        {
            if (Directory.Exists("Data") == false)
            {
                Directory.CreateDirectory("Data");
            }

            using (StreamWriter sw = new StreamWriter("Data\\CurrentState.json"))

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                sw.Write(JsonConvert.SerializeObject(CurrentState));
            }
        }
    }
}