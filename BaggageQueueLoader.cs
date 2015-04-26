using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BaggagePathFinding.Common;

namespace BaggagePathFinding.BagSection
{
    //==================================Information===========================
        // 1) Load bags into master list
    //==================================Information===========================

    public class BaggageQueueLoader
    {
        public static void LoadBaggageList()
        {
            XMLParserCommon.ParseXMLFile(ApplicationSection.Bags,  AppCommonRepository.ConstBaggageXMLDataPath, AppCommonRepository.ConstBaggageXMLDataPattern);
        }
    }
}
