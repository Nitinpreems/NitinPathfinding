using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaggagePathFinding.EntityOperation
{
    //==================================Information===========================
        // 1) Common Interface containing Add/Edit/Update operations for all sections entities in application
    //==================================Information===========================

    public interface IEntityManager<T>
    {
        void Add(T pEntity);
        void Remove(T pEntity);
    }
}
