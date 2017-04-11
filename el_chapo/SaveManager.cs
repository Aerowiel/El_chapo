using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace el_chapo
{

    class SaveManager
    {
        public List<Save> Saves { get; set; }

        public SaveManager()
        {
            Saves = new List<Save>();
            CheckAndCreateSaveFolder();
            //int saveCount = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Length;
        }

        public void CheckAndCreateSaveFolder() {
            Console.WriteLine(GetPath());
            if (!Directory.Exists(GetPath()))
            {
                
                Console.WriteLine("Directory Created");
                Directory.CreateDirectory(GetPath());
            }
        }

        public void Save()
        {
            Save save = new Save()
            {
                DateLastPlayed = DateTime.Now,
                DateCreated = DateTime.Now,
                //mmInstance = (MatchManager) MatchManager.instance 
            };
            string path = GetPath();

            XDocument xmlFile = new XDocument();
            using (var writer = xmlFile.CreateWriter())
            {
                try{
                    var serializer = new XmlSerializer(typeof(Save));
                    serializer.Serialize(writer, save);
                }
                catch(Exception e)
                {
                    //Pour voir quel élément de la classe pose un probléme on check l'inner exception, ça indique le nom de la variable 
                    Console.WriteLine("Inner Exception : " + e.InnerException);
                }
                

                
            }

            //XmlWriter writer = xmlFile.CreateWriter();
            //XmlSerializer serializer = new XmlSerializer(typeof(Save));


            xmlFile.Save(Path.Combine(path, "sauvegardes.xml"));
        }

        public string GetPath()
        {
            return @"..\..\..\saves";
        }

        

        
    }


}
