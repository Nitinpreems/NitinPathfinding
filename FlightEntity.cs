using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.EntityOperation;

namespace BaggagePathFinding.DepartureSection
{
    //==================================Information===========================
        // 1) Entity Holding all information of Flight 
    //==================================Information===========================


    public class FlightEntity : IBaseEntity
    {
        public string Flight_Id { get; set; }
        public EntryPointEntity FlightEntryPoint { get; set; }
        public string DestinationCity { get; set; }
        public string FlightTime { get; set; }
    }
}
