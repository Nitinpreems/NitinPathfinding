using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using BaggagePathFinding.BagSection;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.ApplicationRunner;
using BaggagePathFinding.DepartureSection;

namespace BaggagePathFinding.Common
{
    //==================================Information===========================
        // 1) Common XML parser which can be used by all section
    //==================================Information===========================

    public class XMLParserCommon 
    {
        public static void ParseXMLFile(ApplicationSection pSection, string pXMLPath, string pNodePattern)
        {
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string CurrentDirectory1 = AppDomain.CurrentDomain.BaseDirectory;
           // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConnectionString.txt");
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(pXMLPath);
                XmlNodeList xnList = xml.SelectNodes(pNodePattern);

                switch (pSection)
                {
                    case ApplicationSection.ConveyorSystem:
                        {
                            foreach (XmlNode xn in xnList)
                            {
                                string entryPoint = xn["EntryPoint"].InnerText;
                                string exitPoint = xn["ExitPoint"].InnerText;
                                int pathTime = Convert.ToInt16(xn["PathTime"].InnerText);
                                EntryPointGraphBuilder.loadNode(entryPoint, exitPoint, pathTime);
                            }
                            break;
                        }

                    case ApplicationSection.Departure:
                        {
                            foreach (XmlNode xn in xnList)
                            {
                                var flight = new FlightEntity();
                                flight.Flight_Id = xn["FlightId"].InnerText;
                                flight.FlightEntryPoint = EntryPointGraphBuilder.GetExistingEntryPointByName(xn["FlightEntryPoint"].InnerText);
                                flight.DestinationCity = xn["DestinationCity"].InnerText;
                                flight.FlightTime = xn["FlightTime"].InnerText;
                                AppRunner.FlightScheduleMasterList.Add(flight);
                            }
                            break;
                        }

                    case ApplicationSection.Bags:
                        {
                            foreach (XmlNode xn in xnList)
                            {
                                var bag = new BaggageEntity();
                                bag.BagNumber = xn["BagNumber"].InnerText;
                                bag.BagEntryPoint = EntryPointGraphBuilder.GetExistingEntryPointByName(xn["BagEntryPoint"].InnerText);
                                bag.FlightId = FlightScheduler.GetFlightDetailByFlightId(xn["FlightId"].InnerText);
                                AppRunner.BagMasterQueue.Enqueue(bag);
                            }
                            break;
                        }
                }

            }
            catch (FileNotFoundException ex)
            {
                // To Do : Log the message in Logger
                Console.WriteLine("Exception In XML file Parsing: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                // To Do : Log the message in Logger
                Console.WriteLine("Exception In XML file Parsing: {0}", ex.Message);
            }
        }
    }
}
