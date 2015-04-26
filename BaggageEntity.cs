using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.DepartureSection;
using BaggagePathFinding.EntityOperation;

namespace BaggagePathFinding.BagSection
{
    //==================================Information===========================
        // 1) Entity Holding all information of bag
    //==================================Information===========================

    public class BaggageEntity : IBaseEntity
    {
        public string BagNumber { get; set; }
        public EntryPointEntity BagEntryPoint { get; set; }
        public FlightEntity FlightId { get; set; }
    }
}
