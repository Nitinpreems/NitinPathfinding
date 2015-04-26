using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaggagePathFinding.EntityOperation;

namespace BaggagePathFinding.ConveyorSystem
{
    //==================================Information===========================
        // 1) Entity Holding all information of EntryPoint
    //==================================Information===========================


    public class EntryPointEntity : IBaseEntity
    {
        public string EntryPoint { get; set; }
        public string ExitPoint { get; set; }
        public Dictionary<EntryPointEntity, int> AdjecentEntryPoints { get; set; }   //Key = EntryPointEntity    & Value = PathTime
        public EntryPointEntity(){}
        public EntryPointEntity(string pEntryPoint) 
        {
            this.EntryPoint = pEntryPoint;
            this.AdjecentEntryPoints = new Dictionary<EntryPointEntity, int>();
        }
    }
}
