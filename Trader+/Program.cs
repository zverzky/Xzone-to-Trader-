using Newtonsoft.Json;
using System;
using static Traders.Program;
using System.IO;
using System.Diagnostics;



namespace Traders
{
    class Program
    {


        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Зверь23\\source\\repos\\Trader+\\Trader+\\XZoneTraderStock.json";
            string json = File.ReadAllText(filePath);
            AllXzoneTraders allxzonetraders = JsonConvert.DeserializeObject<AllXzoneTraders>(json);


            TraderPlusPrice traderPlusPrice = new TraderPlusPrice();

            TraderPlusGeneral traderPlusGeneral = new TraderPlusGeneral();

            TraderPlusDs traderPlusDs = new TraderPlusDs(); 


            foreach (var traders in allxzonetraders.Traders)
            {

                string tradername = traders.TraderName;
                int uid = traders.UnicId;
                string role = "Trader";
                var position = traders.PositionsSpawnMain;
                var orention = traders.OrientationSpawnMain;
                string type = traders.TraderType;

                TraderPlusGeneralTraders traderplusGeneraltrader = new TraderPlusGeneralTraders()
                {
                    TraderPlusGeneralTraders_ID = uid,
                    TraderPlusGeneralTraders_classname = traders.TraderType,
                    TraderPlusGeneralTraders_Orientation = orention,
                    TraderPlusGeneralTraders_Position = position,
                    TraderPlusGeneralTraders_role = role,
                    TraderPlusGeneralTraders_tradername = tradername,
                    TraderPlus_Tradercategory_Clothes = traders.TraderClothing

                };
                traderPlusGeneral.traderPlusGeneralTraders.Add(traderplusGeneraltrader);

                TraderPlusDsIDs traderPlusDsIDs = new TraderPlusDsIDs
                {
                    TraderPlusDsIDsId = uid - 1
                };


                foreach (var traderstocks in traders.TraderStocks)
                {
                    var traderstocksname = traderstocks.TraderCaregory + " " + tradername;

                    TraderPlus_Tradercategory traderCategory = new TraderPlus_Tradercategory
                    {
                        TraderPlus_Tradercategory_CategoryName = traderstocksname,
                        TraderPlus_Tradercategory_Products = new string[traderstocks.Products.Count]
                    };

                    traderPlusDsIDs.TraderPlusDsIDsCategories.Add(traderstocksname);


                    for (int i = 0; i < traderstocks.Products.Count; i++)
                    {
                        var product = traderstocks.Products[i];
                        traderCategory.TraderPlus_Tradercategory_Products[i] = $"{product.Classname},1,-1,1,{product.PurchasePrice},{product.SellingPrice}";
                    }

                    traderPlusPrice.TraderPlus_TraderCategories.Add(traderCategory);
                }
                traderPlusDs.traderPlusDsIDs.Add(traderPlusDsIDs);
            }

            string newFilePath = "C:\\Users\\Зверь23\\source\\repos\\Trader+\\Trader+\\NewTraderPlusPrice.json";
            string newJson = JsonConvert.SerializeObject(traderPlusPrice, Formatting.Indented);
            File.WriteAllText(newFilePath, newJson);

            string newFilePath2 = "C:\\Users\\Зверь23\\source\\repos\\Trader+\\Trader+\\NewTraderPlusGeneral.json";
            string newJson2 = JsonConvert.SerializeObject(traderPlusGeneral, Formatting.Indented);
            File.WriteAllText(newFilePath2, newJson2);

            string newFilePath3 = "C:\\Users\\Зверь23\\source\\repos\\Trader+\\Trader+\\NewTraderPlusDs.json";
            string newJson3 = JsonConvert.SerializeObject(traderPlusDs, Formatting.Indented);
            File.WriteAllText(newFilePath3, newJson3);


        }
        public class AllXzoneTraders
        {
            public List<XzoneTrader> Traders { get; set; }
        }

        public class XzoneTrader
        {
            public double[] PositionsSpawnMain { get; set; }
            public double[] OrientationSpawnMain { get; set; }
            public int RandomSpawnYesOrNo { get; set; }
            public double[] RandomPos { get; set; }
            public string TraderType { get; set; }
            public string TraderName { get; set; }
            public int UnicId { get; set; }
            public string RejectionPhrase { get; set; }
            public string PathToBackgroundImage { get; set; }
            public string CarPosSpawn { get; set; }
            public string Currency { get; set; }
            public string[] AllowFraction { get; set; }
            public string[] TraderClothing { get; set; }
            public List<TraderStock> TraderStocks { get; set; }
        }

        public class TraderStock
        {
            public string TraderCaregory { get; set; }
            public List<Product> Products { get; set; }
        }

        public class Product
        {
            public string Classname { get; set; }
            public decimal PurchasePrice { get; set; }
            public decimal SellingPrice { get; set; }
            public int Type { get; set; }
            public int QuantityForSale { get; set; }
        }

        }
        public class TraderPlusPrice
        {
            //public Tradercategory[] TraderCategories { get; set; }
            public List<TraderPlus_Tradercategory> TraderPlus_TraderCategories { get; set; } = new List<TraderPlus_Tradercategory>();
        }

        public class TraderPlus_Tradercategory
        {
            public string TraderPlus_Tradercategory_CategoryName { get; set; }
            public string[] TraderPlus_Tradercategory_Products { get; set; }
    }

        public class TraderPlusGeneral 
        {
            public List<TraderPlusGeneralTraders> traderPlusGeneralTraders { get; set; } = new List<TraderPlusGeneralTraders>();
        }
        public class TraderPlusGeneralTraders
        {
               public int TraderPlusGeneralTraders_ID { get; set; }
               public string TraderPlusGeneralTraders_classname { get; set; }
               public string TraderPlusGeneralTraders_tradername { get; set; }

               public string TraderPlusGeneralTraders_role { get; set; }
            
               public double[] TraderPlusGeneralTraders_Position { get; set; }

               public double[] TraderPlusGeneralTraders_Orientation { get; set; }

               public string[] TraderPlus_Tradercategory_Clothes { get; set; }


        }
        public class TraderPlusDs 
        {
        public List<TraderPlusDsIDs> traderPlusDsIDs { get; set; } = new List<TraderPlusDsIDs>();    
        }
        public class TraderPlusDsIDs 
        {
           public int TraderPlusDsIDsId { get; set; }
           public List<string> TraderPlusDsIDsCategories { get; set; } = new List<string>();
           public string[] LicencesRequired { get; set; }
           public string[] CurrenciesAccepted { get; set; }
    }

}

