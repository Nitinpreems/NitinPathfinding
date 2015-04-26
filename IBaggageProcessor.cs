using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaggagePathFinding.BagSection
{
    public interface IBaggageProcessor 
    {
        void BaggageProcessor(bool pThreadingEnabled);
        void ProcessBagsWithThread(Object pCurrentBag);
        void ProcessBagsWithoutThread();

    }
}
