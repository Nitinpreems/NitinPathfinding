using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BaggagePathFinding.Common;
using BaggagePathFinding.ApplicationRunner;

namespace BaggagePathFinding.ConveyorSystem
{
    //==================================Information=============================
        // 1) All functions required for loading data into MasterEntryPoint list 
    //==================================Information=============================

    public class EntryPointGraphBuilder
    {
        public static void LoadConveyorPath()
        {
            XMLParserCommon.ParseXMLFile(ApplicationSection.ConveyorSystem, AppCommonRepository.ConstConveyorXMLDataPath, AppCommonRepository.ConstConveyorXMLDataPattern);
        }

        //Used by other sections to get existing EntryPoint from the masterlist
        public static EntryPointEntity GetExistingEntryPointByName(string pEntryPointName)
        {
            if (AppRunner.EntryPointsMasterGraph.Count > 0)
            {
                var EntryPointEntity = new EntryPointEntity(pEntryPointName);
                if (AppRunner.EntryPointsMasterGraph.Any(p => p.EntryPoint == pEntryPointName))
                {
                    return AppRunner.EntryPointsMasterGraph.FirstOrDefault(p => p.EntryPoint == pEntryPointName);
                }
            }
            return null;
        }

        public static EntryPointEntity GetEntryPointByName(string pEntryPointName)
        {
            var EntryPointEntity = new EntryPointEntity(pEntryPointName);
            if (AppRunner.EntryPointsMasterGraph.Any(p => p.EntryPoint == pEntryPointName))
            {
                return AppRunner.EntryPointsMasterGraph.FirstOrDefault(p => p.EntryPoint == pEntryPointName);
            }
            else
            {
                AppRunner.EntryPointsMasterGraph.Add(EntryPointEntity);
                return EntryPointEntity;
            }
        }
       
        public static void loadNode(string pEntryPoint, string pExitPoint, int pPathTime)
        {
            EntryPointEntity entryPoint = GetEntryPointByName(pEntryPoint);
            EntryPointEntity exitPoint = GetEntryPointByName(pExitPoint);

            entryPoint.AdjecentEntryPoints.Add(exitPoint, pPathTime);
            exitPoint.AdjecentEntryPoints.Add(entryPoint, pPathTime);
        }
    }
}
