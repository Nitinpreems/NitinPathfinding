using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.ApplicationRunner;
using BaggagePathFinding.Common;

namespace BaggagePathFinding.BagSection
{
    //==================================Information===========================
        // 1) Picks first bag from MasterBag list
        // 2) Process that bag using any of the 2 Approach depending on the flag set in AppRunner
                // a) Normal processing without MultiThreading : Order of Output bag list is maintained
                // b) bag processing using ThreadPool MultiThreading : Order of Output bag list is not maintained 
        // 3) Process that bag using any of the 2 Approach
        // 4) Consists of Algorithm for finding shortest distance between two entry points
    //==================================Information===========================

    public class BaggageQueueProcessor : IBaggageProcessor
    {
        private static BaggageQueueProcessor lnstBagProcessor;
        private static Object singletonObj = new Object();
        private BaggageQueueProcessor() { }

        public static BaggageQueueProcessor GetInstance()
        {
            if (lnstBagProcessor == null)
            {
                lock (singletonObj)
                {
                    if (lnstBagProcessor == null)
                    {
                        lnstBagProcessor = new BaggageQueueProcessor();
                    }
                }
            }
            return lnstBagProcessor;
        }

        public void BaggageProcessor(bool pThreadingEnabled)
        {
            if (pThreadingEnabled)
            {
                BaggageEntity currentBag;
                while (TryDequeue(out currentBag))
                {
                    //Output Order is Not maintained
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessBagsWithThread), currentBag);
                }
            }
            else
            {
                ProcessBagsWithoutThread();
            }
        }

        private bool TryDequeue(out BaggageEntity value)
        {
            lock (singletonObj)
            {
                if (AppRunner.BagMasterQueue.Count > 0)
                {
                    value = AppRunner.BagMasterQueue.Dequeue();
                    return true;
                }
                else
                {
                    value = default(BaggageEntity);
                    return false;
                }
            }
        }

        public void ProcessBagsWithThread(Object pCurrentBag)
        {
            var currentBag = ((BaggageEntity)pCurrentBag);
            var bagProcessorAlgo = new ShortestDistanceAlgorithm();
            try
            {
                var sourceEntryPoint = currentBag.BagEntryPoint;
                EntryPointEntity destEntryPoint = null;
                if (currentBag.FlightId.FlightEntryPoint != null)
                    destEntryPoint = currentBag.FlightId.FlightEntryPoint;
                var CurrentPath = new ResultPathEntity(currentBag.BagNumber);
                CurrentPath.Path.Add(currentBag.BagEntryPoint);
                var ShortestPath = new ResultPathEntity(currentBag.BagNumber);
                ShortestPath.Path.Add(currentBag.BagEntryPoint);

                //Algorithm to calculate Shortest distance
                bagProcessorAlgo.FindShortestDistanceAlgorithm(sourceEntryPoint, destEntryPoint, CurrentPath, ShortestPath);

                DisplayResult(ShortestPath);
            }
            catch (Exception ex)
            {
                // To Do : Log the message in Logger
                // Display error for current bag & process next bag
                Console.WriteLine("Exception In Processing of Bag: {0}, Exception : {1}", currentBag.BagNumber, ex.Message);
            }
        }

        public void ProcessBagsWithoutThread()
        {
            BaggageEntity currentBag;
            while (TryDequeue(out currentBag))
            {
                var bagProcessorAlgo = new ShortestDistanceAlgorithm();
                try
                {
                    var sourceEntryPoint = currentBag.BagEntryPoint;
                    EntryPointEntity destEntryPoint = null;
                    if (currentBag.FlightId.FlightEntryPoint != null)
                        destEntryPoint = currentBag.FlightId.FlightEntryPoint;
                    var CurrentPath = new ResultPathEntity(currentBag.BagNumber);
                    CurrentPath.Path.Add(currentBag.BagEntryPoint);
                    var ShortestPath = new ResultPathEntity(currentBag.BagNumber);
                    ShortestPath.Path.Add(currentBag.BagEntryPoint);

                    //Algorithm to calculate Shortest distance
                    bagProcessorAlgo.FindShortestDistanceAlgorithm(sourceEntryPoint, destEntryPoint, CurrentPath, ShortestPath);

                    DisplayResult(ShortestPath);
                }
                catch (Exception ex)
                {
                    // To Do : Log the message in Logger
                    // Display error for current bag & process next bag
                    Console.WriteLine("Exception In Processing of Bag: {0}, Exception : {1}", currentBag.BagNumber, ex.Message);
                }
            }
        }

        private static void DisplayResult(ResultPathEntity pShortestPath)
        {
            StringBuilder outputPath = new StringBuilder();
            outputPath.AppendFormat(pShortestPath.BagNumber.ToString() + " : ");
            foreach (var item in pShortestPath.Path)
            {
                outputPath.AppendFormat(item.EntryPoint + ",");
            }
            outputPath.Remove(outputPath.Length - 1, 1);
            outputPath.AppendFormat(" : " + pShortestPath.TotalPathTime);
            Console.WriteLine(outputPath);
        }

        
    }
}
