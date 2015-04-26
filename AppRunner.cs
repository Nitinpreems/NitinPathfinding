using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaggagePathFinding.BagSection;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.DepartureSection;

namespace BaggagePathFinding.ApplicationRunner
{
    //==================================Information===========================
        // 1) This class is entry point to the application
        // 2) It has 3 Master lists which are loaded from XML file using static constructor
        // 3) Main method process all the Bags in Queue in FIFO manner
        // 4) Set IsThreadingEnabled = True for multiThreading enabled application
    //==================================Information===========================

    class AppRunner
    {
        public static List<EntryPointEntity> EntryPointsMasterGraph;
        public static List<FlightEntity> FlightScheduleMasterList;
        public static Queue<BaggageEntity> BagMasterQueue;
        private static bool IsThreadingEnabled = false;

        static AppRunner()
        {
            AppInitialiser();
            AppDataLoader();
        }

        static void AppInitialiser()
        {
            EntryPointsMasterGraph = new List<EntryPointEntity>();      
            FlightScheduleMasterList = new List<FlightEntity>();        
            BagMasterQueue = new Queue<BaggageEntity>();                
        }

        static void AppDataLoader()
        {
            EntryPointGraphBuilder.LoadConveyorPath();          // Builds graph from given data of Conveyor System
            FlightScheduler.LoadDepartureList();                // Builds Departure list of flights from given data of Departure System
            BaggageQueueLoader.LoadBaggageList();               // Adds the bags into Queue in FIFO from given data of Bags
        }

        static void Main(string[] args)                         // Process the bags in Queue
        {
            if (EntryPointsMasterGraph.Count > 0 && FlightScheduleMasterList.Count > 0 && BagMasterQueue.Count > 0)
            {
                IBaggageProcessor baggageProcessor = BaggageQueueProcessor.GetInstance();  
                baggageProcessor.BaggageProcessor(IsThreadingEnabled);
            }
            else
            {
                // To Do : Log the message in Logger
                Console.WriteLine("XML Data not parsed correctly..!!!");
            }
            Console.ReadKey();
        }
    }
}
