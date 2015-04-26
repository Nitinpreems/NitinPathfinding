using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.Common;

namespace BaggagePathFinding.BagSection
{
    //==================================Information===========================
        // 1) Algorithm for finding shortest distance between the two given entryPoints
        // 2) fills the ResultPathEntity which has all the data required for output with list of shortest path entry points
    //==================================Information===========================

    public class ShortestDistanceAlgorithm
    {
        //Algorithm for finding shortest distance between two entry points
        public void FindShortestDistanceAlgorithm(EntryPointEntity pSourceEntryPoint, EntryPointEntity pDestEntryPoint, ResultPathEntity pCurrentPath, ResultPathEntity pShortestPath)
        {
            var adjecentEntryPoints = pSourceEntryPoint.AdjecentEntryPoints;
            foreach (var adjecentEntryPoint in adjecentEntryPoints)
            {
                if (pCurrentPath.Path.Contains(adjecentEntryPoint.Key))
                {
                    // avoid cycle
                    continue;
                }
                pCurrentPath.Path.Add(adjecentEntryPoint.Key);
                pCurrentPath.TotalPathTime = pCurrentPath.TotalPathTime + adjecentEntryPoint.Value;
                if (adjecentEntryPoint.Key.Equals(pDestEntryPoint))
                {
                    int currentTravelTime = pCurrentPath.TotalPathTime;
                    int shortestTime = pShortestPath.TotalPathTime;
                    if (shortestTime == 0 || shortestTime > currentTravelTime)
                    {
                        pShortestPath.Path.Clear();
                        pShortestPath.Path.AddRange(pCurrentPath.Path);
                        pShortestPath.TotalPathTime = currentTravelTime;                        
                    }
                    //Console.WriteLine("===To test other found path===");
                    //BaggageQueueProcessor.DisplayResult(pCurrentPath);
                }
                else
                {
                    FindShortestDistanceAlgorithm(adjecentEntryPoint.Key, pDestEntryPoint, pCurrentPath, pShortestPath);
                }
                // remove current node
                pCurrentPath.Path.RemoveAt(pCurrentPath.Path.Count - 1);
                // substract the added time for current node
                pCurrentPath.TotalPathTime = pCurrentPath.TotalPathTime - adjecentEntryPoint.Value;
            }
        }
    }
}
