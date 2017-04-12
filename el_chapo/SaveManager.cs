using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

namespace el_chapo
{

    class SaveManager
    {
        public List<String> Saves { get; set; }

        public SaveManager()
        {
            Saves = new List<String>();
            CheckAndCreateSaveFolder();
            string[] saves = Directory.GetFiles(GetPath());

            foreach (string saveName in saves)
            {
                Saves.Add(saveName);
            }
        }

        public void CheckAndCreateSaveFolder() {
            Console.WriteLine(GetPath());
            if (!Directory.Exists(GetPath()))
            {
                
                Console.WriteLine("Directory Created");
                Directory.CreateDirectory(GetPath());
            }
        }

        public void Save(string name)
        {
            Save save = new Save()
            {
                DateLastPlayed = DateTime.Now,
                DateCreated = DateTime.Now,
                //mmInstance = (MatchManager) MatchManager.instance 
            };
            string path = GetPath();

            XDocument xmlFile = new XDocument();

            //Shortcut pour initialiser une variable local qu'on utilisera directement par la suite ! Trés utile !
            using (XmlWriter writer = xmlFile.CreateWriter())
            {
                try{
                    XmlSerializer serializer = new XmlSerializer(typeof(Save));
                    serializer.Serialize(writer, save);
                }
                catch(Exception e)
                {
                    //Pour voir quel élément de la classe pose un probléme on check l'inner exception, ça indique le nom de la variable 
                    Console.WriteLine("Inner Exception : " + e.InnerException);
                }
                

                
            }
            //Puis on save dans ..\..\..\saves\
            xmlFile.Save(Path.Combine(path, $"{name}.xml"));
        }

        public void LoadAndUpdateObjects(string nameOfSave)
        {
            Save save;
            XmlSerializer serializer = new XmlSerializer(typeof(Save));
            string path = Path.Combine(nameOfSave);
            using (FileStream fileStream = File.Open(path, FileMode.Open))
            {
                save = (Save)serializer.Deserialize(fileStream);
                Console.WriteLine("Deserializing file stream");
                ExperimentalProgressBar progressBar = new ExperimentalProgressBar(ConsoleColor.Blue, 25);
                progressBar.DisplayProgressBar();
                
            }
            //MatchManager
            MatchManager.instance.Catcheurs = save.CatcheurList;
            MatchManager.instance.Season = save.Season;
            MatchManager.instance.MatchThisSeason = save.MatchThisSeason;

            //MoneyManager
            MoneyManager.instance.Money = save.Money;

            //HistoryManager
            HistoryManager.instance.HistoryCatcheur = save.HistoryList;
            Console.WriteLine($"\n *La sauvegarde \"{nameOfSave}\" s'est chargée avec succès !");
            Thread.Sleep(2000);
            Console.Clear(); 
        }

        public string GetPath()
        {
            return @"..\..\..\saves";
        }

        

        
    }


}
