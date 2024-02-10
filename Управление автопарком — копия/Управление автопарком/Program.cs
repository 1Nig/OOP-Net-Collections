using Bogus.DataSets;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using DocumentFormat.OpenXml.Office2013.Word;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Bibliography;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml;
using System.Data.Common;
using System.Runtime.ConstrainedExecution;
using Bogus;
using Bogus.Bson;
using DocumentFormat.OpenXml.EMMA;
using System.Reflection;
using DocumentFormat.OpenXml.Office2019.Excel.ThreadedComments;
using DocumentFormat.OpenXml.Office2019.Excel.RichData2;
using System.Security.Claims;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System.IO.Packaging;
using System.Runtime.InteropServices;

namespace Управление_автопарком
{
    class Program
    {

        static void Main()
        {
            bool endApp = false;
            Console.WriteLine("Добро пожаловать в программу управления автопарком");
            Console.WriteLine("------------------------\n");


            Console.WriteLine("Здесь Вы узнаете всю информацию об имеющихся легковых, грузовых автомобилях, автобусах и скутерах ");

            Car Lada = new Car("Lada", 175)
            {
                движок = { Мощность = 113, Объем = 1.3, Тип = "бензиновый", СерийныйНомер = "144213" },
                трансмиссия = { Тип = "механическая", КоличествоПередач = 5, Производитель = "АВТОВАЗ" },
                шасси = { КоличествоКолес = 4, Номер = "1151GHT511J", ДопустимаяНагрузка = 1730 }
            };


            Car Renault = new Car("Renault", 260)
            {
                движок = { Мощность = 90, Объем = 1.6, Тип = "бензиновый", СерийныйНомер = "1596485" },
                трансмиссия = { Тип = "МКПП", КоличествоПередач = 5, Производитель = "Франция" },
                шасси = { КоличествоКолес = 4, Номер = "54834HUj", ДопустимаяНагрузка = 560 }
            };


            Lada.Print();

            Console.WriteLine("------------------------\n");
            Renault.Print();
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Это вся информация по легковым автомобилям");
            List<TC> jf = new List<TC>() { Lada, Renault };

            Truck Shacman = new Truck("Тягач Shacman SX42584V324", "седельный тягач", 700)
            {
                движок = { Мощность = 430, Объем = 11.6, Тип = "с турбонаддувом и промежуточным охлаждением воздуха", СерийныйНомер = "H130604805" },
                трансмиссия = { Тип = "механическая, синхронизированная", КоличествоПередач = 12, Производитель = "Китай" },
                шасси = { КоличествоКолес = 6, Номер = "463258H765IU", ДопустимаяНагрузка = 8000 }
            };
            Shacman.Print();
            jf.Add(Shacman);
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Это вся информация по грузовым автомобилям автопарка");

            Bus School = new Bus("Школьный", 30)
            {
                движок = { Мощность = 149, Объем = 4.43, Тип = "дизельный", СерийныйНомер = "ЯМЗ-53423" },
                трансмиссия = { Тип = "механическая", КоличествоПередач = 20, Производитель = "МКПП: Fastgear" },
                шасси = { КоличествоКолес = 4, Номер = "5121LOJ1654", ДопустимаяНагрузка = 4000 }
            };
            School.Print();
            jf.Add(School);
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Это вся информация по автобусам автопарка");


            Scooter Clever = new Scooter("Clever", 90, 120)
            {

                движок = { Мощность = 6, Объем = 0.060, Тип = "4-х такт", СерийныйНомер = "56121TRE56165" },
                трансмиссия = { Тип = "механическая", КоличествоПередач = 3, Производитель = "Тайвань" },
                шасси = { КоличествоКолес = 2, Номер = "65054HFR1651", ДопустимаяНагрузка = 150 }
            }
            ;
            Clever.Print();
            jf.Add(Clever);
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Это вся информация по скутерам автопарка");




            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TC>));

            using (FileStream fs = new FileStream("TC.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, jf);
            }
            Console.WriteLine("Objects has been serialized");

            List<TC> trans = jf.FindAll(TC => TC.движок.Объем >= 1.5);
            XmlSerializer xmlSeri = new XmlSerializer(typeof(List<TC>));

            using (FileStream fs = new FileStream("TC2.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, trans);
            }
            Console.WriteLine("Objects has been serialized");

            var tran = jf.OrderBy(TC => TC.трансмиссия.Тип).ToList();
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<TC>));

            using (FileStream fs = new FileStream("TC3.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, tran);
            }
            Console.WriteLine("Objects has been serialized");


            Console.WriteLine(); // для разделения между группами




            XDocument doc = XDocument.Load("TC.xml");

            XElement root = doc.Root;



            var q = from node in doc.Descendants("движок")
                    let Мощность = node.Attribute("Мощность")
                    let Тип = node.Attribute("Тип")
                    let СерийныйНомер = node.Attribute("СерийныйНомер")
                    select new { Тип = (Тип != null) ? Тип.Value : "", Мощность = (Мощность != null) ? Мощность.Value : "", СерийныйНомер = (СерийныйНомер != null) ? СерийныйНомер.Value : "", };

            foreach (var движок in q)
            {
                Console.WriteLine("Тип={0}, Мощность={1},СерийныйНомер = {2}", движок.Тип, движок.Мощность, движок.СерийныйНомер);

            }
        
            XmlTextWriter textWritter = new XmlTextWriter("TC5.xml", Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("head");
            textWritter.WriteEndElement();
            textWritter.Close();
            XmlDocument document = new XmlDocument();
            document.Load("TC5.xml");
            XmlNode TS = document.CreateElement("TC");
            document.DocumentElement.AppendChild(TS);
            XmlNode Buss = document.CreateElement("Bus");
            document.DocumentElement.AppendChild(Buss);

            XmlNode двигатель = document.CreateElement("движок");
            document.DocumentElement.AppendChild(двигатель);
            XmlNode тип = document.CreateElement("Тип");
            тип.InnerText = "дизельный";
            двигатель.AppendChild(тип);
            XmlNode Серийный_номер = document.CreateElement("СерийныйНомер");
            Серийный_номер.InnerText = "ЯМЗ-53423";
            двигатель.AppendChild(Серийный_номер);
            XmlNode мощность = document.CreateElement("Мощность");
            мощность.InnerText = "149";
            двигатель.AppendChild(мощность);

            XmlNode truck = document.CreateElement("Truck");
            document.DocumentElement.AppendChild(truck);

            XmlNode Двигатель = document.CreateElement("движок");
            document.DocumentElement.AppendChild(Двигатель);
            XmlNode ТИП = document.CreateElement("Тип");
            ТИП.InnerText = "с турбонаддувом и промежуточным охлаждением воздуха";
            Двигатель.AppendChild(ТИП);
            XmlNode Серийный_Номер = document.CreateElement("СерийныйНомер");
            Серийный_Номер.InnerText = "H130604805";
            Двигатель.AppendChild(Серийный_Номер);
            XmlNode МОЩНОСТЬ = document.CreateElement("Мощность");
            МОЩНОСТЬ.InnerText = "430";
            Двигатель.AppendChild(МОЩНОСТЬ);

            document.Save("TC5.xml");

            Console.WriteLine("\n");

                return;


            }
        }
    }






        
    

    

