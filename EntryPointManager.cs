using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaggagePathFinding.ConveyorSystem;
using BaggagePathFinding.DepartureSection;
using BaggagePathFinding.ApplicationRunner;
using BaggagePathFinding.BagSection;

namespace BaggagePathFinding.EntityOperation
{
    //==================================Information===========================
        // 1) Includes classes for all future runtime Add/Delete/Update operation
    //==================================Information===========================

    public class EntryPointManager : IEntityManager<EntryPointEntity>            
    {
        public void Add(EntryPointEntity pEntryPointEntity)
        {
            AppRunner.EntryPointsMasterGraph.Add(pEntryPointEntity);
        }
        public void Remove(EntryPointEntity pEntryPointEntity)
        {
            AppRunner.EntryPointsMasterGraph.Remove(pEntryPointEntity);
        }
    }

    public class FlightManager : IEntityManager<FlightEntity> 
    {
        public void Add(FlightEntity pFlightEntity)
        {
            AppRunner.FlightScheduleMasterList.Add(pFlightEntity);
        }
        public void Remove(FlightEntity pFlightEntity)
        {
            AppRunner.FlightScheduleMasterList.Remove(pFlightEntity);
        }
    }

    public class BaggageManager : IEntityManager<BaggageEntity>
    {
        public void Add(BaggageEntity pBaggageEntity)
        {
            AppRunner.BagMasterQueue.Enqueue(pBaggageEntity);
        }
        public void Remove(BaggageEntity pBaggageEntity)
        {
            AppRunner.BagMasterQueue.Dequeue();
        }
    }
}
