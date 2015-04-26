using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaggagePathFinding.ConveyorSystem;

namespace BaggagePathFinding.Common
{
    //==================================Information===========================
        // 1) Saperate class for holding result path list & total path time
    //==================================Information===========================

    public class ResultPathEntity
    {
        public string BagNumber { get; set; }
        public List<EntryPointEntity> Path;
        public int TotalPathTime { get; set; }

        public ResultPathEntity(string pBagNumber)
        {
            this.BagNumber = pBagNumber;
            Path = new List<EntryPointEntity>();
            TotalPathTime = 0;
        }
    }
}
