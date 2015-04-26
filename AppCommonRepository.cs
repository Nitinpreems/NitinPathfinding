using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaggagePathFinding.Common
{
    //==================================Information===========================
        // 1) Consist Common fields shared across application
    //==================================Information===========================

    public class AppCommonRepository
    {
        string CurrentDirectory = Directory.GetCurrentDirectory();
        public const string ConstConveyorXMLDataPath = @"~\..\..\..\RefData\ConveyorPaths.xml";
        public const string ConstFlightDepartureXMLDataPath = @"~\..\..\..\RefData\FlightList.xml";
        public const string ConstBaggageXMLDataPath = @"~\..\..\..\RefData\BaggageList.xml";
        
        public const string ConstConveyorXMLDataPattern = "/ConveyorPaths/Path";
        public const string ConstFlightDepartureXMLDataPattern = "/FlightSchedule/Flight";
        public const string ConstBaggageXMLDataPattern = "/BagList/Bag";
    }

    public enum ApplicationSection
    {
        ConveyorSystem,
        Departure,
        Bags
    }
}
