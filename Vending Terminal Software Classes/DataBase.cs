using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Entity;

namespace Vending_Terminal_Software_Classes
{
    public class DataBase
    {
        // Google Drive with .bak database: https://drive.google.com/open?id=1i25vSDjtotmf2rps6uBsyl4nesLgCOAT

        public string TerminalLocation;
        public CurrentState CurrentState;
        public List<CurrentProduct> CurrentProducts = new List<CurrentProduct>();

        public DataBase()
        {

        }

        public List<CurrentProduct> GetData()
        {
            using (var Context = new Context())
            {
                return (from P in Context.CurrentStates
                    .Include(cs => cs.Banknotes_n_Coins)
                    .Include(cs => cs.Product)
                    .First(n => n.Location == TerminalLocation).Product
                        join I in Context.Info on P.Code equals I.Code
                        select new CurrentProduct { Code = P.Code, Name = I.Name, Amount = P.Amount, Price = I.SellingPrice }
                    ).ToList();
            }
        }

        public void BasicState()
        {
            using (var Context = new Context())
            {
                Context.Info.AddRange(new List<Info>
                {
                    new Info { Code = "1000", Name = "Mars", SellingPrice = 55, BuyingPrice = 45},
                    new Info { Code = "1001", Name = "Snickers", SellingPrice = 45, BuyingPrice = 35},
                    new Info { Code = "1002", Name = "Twix", SellingPrice = 50, BuyingPrice = 30},
                    new Info { Code = "1003", Name = "Iron ore", SellingPrice = 37, BuyingPrice = 13},
                    new Info { Code = "1004", Name = "Nuka-Cola", SellingPrice = 100, BuyingPrice = 70},
                    new Info { Code = "1005", Name = "Lays", SellingPrice = 80, BuyingPrice = 65},
                    new Info { Code = "1006", Name = "Fanta", SellingPrice = 45, BuyingPrice = 35},
                    new Info { Code = "1007", Name = "Sprite", SellingPrice = 45, BuyingPrice = 37}
                });

                Context.CurrentStates.AddRange(new List<CurrentState>
                {
                    new CurrentState { Money = 0, Location="Kirpichnaya 33, 1st floor",

                        Product = new List<Product>
                        {
                            new Product { Code = "1000", Amount = 20},
                            new Product { Code = "1001", Amount = 25},
                            new Product { Code = "1002", Amount = 20},
                            new Product { Code = "1003", Amount = 100},
                            new Product { Code = "1005", Amount = 30}
                        },

                        Banknotes_n_Coins = new List<Banknotes_n_Coins>
                        {
                            new Banknotes_n_Coins { Cost=1000, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=500, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=100, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=50, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=10, Amount=20, CanBeChange=true },
                            new Banknotes_n_Coins { Cost=5, Amount=40, CanBeChange=true },
                            new Banknotes_n_Coins { Cost=2, Amount=100, CanBeChange=true },
                            new Banknotes_n_Coins { Cost=1, Amount=200, CanBeChange=true }
                        }
                    },

                    new CurrentState { Money = 0, Location="Kirpichnaya 33, 8th floor",

                        Product = new List<Product>
                        {
                            new Product { Code = "1004", Amount = 50},
                            new Product { Code = "1006", Amount = 70},
                            new Product { Code = "1007", Amount = 60}
                        },

                        Banknotes_n_Coins = new List<Banknotes_n_Coins>
                        {
                            new Banknotes_n_Coins { Cost=1000, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=500, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=100, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=50, Amount=0, CanBeChange=false },
                            new Banknotes_n_Coins { Cost=10, Amount=20, CanBeChange=true },
                            new Banknotes_n_Coins { Cost=5, Amount=40, CanBeChange=true },
                            new Banknotes_n_Coins { Cost=2, Amount=100, CanBeChange=true },
                            new Banknotes_n_Coins { Cost=1, Amount=200, CanBeChange=true }
                        }}
                });

                Context.SaveChanges();
            }
        }
    }
}

//CurrentState = context.CurrentStates
//    .Include(cs => cs.Banknotes_n_Coins)
//    .Include(cs => cs.Product)
//    .First(n => n.ID == 1);

//CurrentState.Banknotes_n_Coins.Sort(new MoneyComparer());

//CurrentProduct();

//private void CurrentProduct()
//{
//    using (var Context = new Context())
//    {
//        foreach (Product P in CurrentState.Product)
//        {
//            var tmp = Context.Info.First(n => n.Code == P.Code);

//            CurrentProducts.Add(new CurrentProduct { Name = tmp.Name, Code = P.Code, Amount = P.Amount, Price = tmp.SellingPrice });
//        }
//    }
//}

//public void Save()
//{
//    if (Directory.Exists("Data") == false)
//    {
//        Directory.CreateDirectory("Data");
//    }

//    using (StreamWriter sw = new StreamWriter("Data\\CurrentState.json"))

//    using (JsonWriter writer = new JsonTextWriter(sw))
//    {
//        sw.Write(JsonConvert.SerializeObject(CurrentState));
//    }
//}

//public void Update()
//{
//    string contents = File.ReadAllText("Data\\CurrentState.json");
//    CurrentState = JsonConvert.DeserializeObject<CurrentState>(contents);

//    File.Delete("Data\\CurrentState.json");

//    using (var context = new Context())
//    {
//        var tmp = context.CurrentStates
//            .Include(cs => cs.Banknotes_n_Coins)
//            .Include(cs => cs.Product)
//            .First(n => n.ID == 1);

//        tmp = CurrentState;

//        context.SaveChanges();
//    }
//}