using CSIFLEX.PartAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace CSIFLEX.PartAnalyzer.Views.Converters
{
    public class EntityDescription: PropertyGroupDescription
    {
        public EntityDescription(string groupName): base(groupName)
        {
        }

        public override bool NamesMatch(object groupName, object itemName)
        {
            if(groupName is GeniusDataViewModel itemVm)
            {
                return itemVm.GroupEquals(itemName);
            }
            return false;
        }
    }
}
