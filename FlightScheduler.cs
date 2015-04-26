using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BaggagePathFinding.Common;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.ApplicationRunner;

namespace BaggagePathFinding.DepartureSection
{
    //==================================Information===========================
        // 1) Loading the data into master list
    //==================================Information===========================

    public class FlightScheduler
    {
        public static void LoadDepartureList()
        {
            XMLParserCommon.ParseXMLFile(ApplicationSection.Departure, AppCommonRepository.ConstFlightDepartureXMLDataPath, AppCommonRepository.ConstFlightDepartureXMLDataPattern);
        }

        public static FlightEntity GetFlightDetailByFlightId(string pFlightId)
        {
            var flightEntity = new FlightEntity();
            flightEntity.Flight_Id = pFlightId;

            if (pFlightId.ToUpper() == "ARRIVAL")
            {
                flightEntity.FlightEntryPoint = EntryPointGraphBuilder.GetExistingEntryPointByName("BaggageClaim"); 
            }
            else if(AppRunner.FlightScheduleMasterList.Any(p => p.Flight_Id == pFlightId))
            {
                return AppRunner.FlightScheduleMasterList.FirstOrDefault(p => p.Flight_Id == pFlightId);
            }
            return flightEntity;
        }
    }
}
