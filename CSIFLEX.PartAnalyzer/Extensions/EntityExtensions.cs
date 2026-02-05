using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CSIFLEX.PartAnalyzer.Entities;

namespace CSIFLEX.PartAnalyzer.Extensions
{
    public static class EntityExtensions
    {

        /// <summary>
        /// This provides a function that returns the relational code of its parent entity type.
        /// </summary>
        private static readonly Dictionary<Type, Func<ProductionTasksEntity, string>> ParentIdFuncMap = new Dictionary<Type, Func<ProductionTasksEntity, string>>()
        {
            { typeof(JobEntity), x=> x.JobCode },
            { typeof(WorkOrderEntity), x=> x.WorkOrderCode }
        };

        private static readonly Dictionary<Type, string> ParentMap = new Dictionary<Type, string>()
        {
            {typeof(JobEntity),"JobCode" },
            {typeof(WorkOrderEntity),"WorkOrderCode" }
        };

        public static string ToParams(this IEnumerable<string> ids)
        {
            var paramToSubmit = new StringBuilder("{");
            var idsA = ids.ToArray();
            for (int i = 0; i < idsA.Length; i++)
            {
                paramToSubmit.Append($"\"{idsA[i]}\"");
                if (i == idsA.Length - 1)
                {
                    break;
                }
                else
                {
                    paramToSubmit.Append(',');
                }
            }
            paramToSubmit.Append("}");
            return paramToSubmit.ToString();
        }

        public static string GetIdFromParentType(this ProductionTasksEntity prodEntity, Type t)
        {
            return ParentIdFuncMap[t](prodEntity);
        }

        public static string GetIdFromParentType(this Type t)
        {
            return ParentMap[t];
        }
        
    }
}
