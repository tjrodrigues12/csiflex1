using CSIFLEX.PartAnalyzer.Entities;
using CSIFLEX.PartAnalyzer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CSIFLEX.PartAnalyzer.Service
{
    /// <summary>
    /// class attempts to locate parts based on a given job entity
    /// </summary>
    public class CSIFlexPartLocator
    {

        private CSIFlexDbProvider dbProvider;
        private GeniusDataProvider geniusDataProvider;
        public CSIFlexPartLocator(CSIFlexDbProvider dbProvider, GeniusDataProvider geniusDataProvider)
        {
            this.dbProvider = dbProvider;
            this.geniusDataProvider = geniusDataProvider;
        }

        public async Task<IEnumerable<(JobEntity job, ProductionPart)>> GetJobs(CancellationToken ct, SearchOptions searchOptions, params string[] jobs)
        {
            var retVal = await Fetch<JobEntity>(ct, t => t.ProductLink, searchOptions, jobs);
            return retVal;
        }

        private async Task<(T, IEnumerable<MachinePartPerformance>)> GetObjectWithParts<T>(T data, SearchOptions searchOptions, DateTime start, DateTime end, string partId)
        {
            var result = await dbProvider.GetMachinePartPerformance(searchOptions, start, end, partId);
            return (data, result);
        }

        public async Task<IEnumerable<(WorkOrderEntity workOrder, ProductionPart productionPart)>> GetWorkOrders(CancellationToken ct, SearchOptions searchOptions, params string[] workOrderIds)
        {
            var retVal = await Fetch<WorkOrderEntity>(ct, t => t.ProductLink, searchOptions, workOrderIds);
            return retVal;
        }
         

        private async Task<(T, ProductionPart)[]> Fetch<T>(CancellationToken ct, Func<T, string> getPartId, SearchOptions searchOptions, params string[] ids) where T : IProductionEntity
        {
            var collection = await geniusDataProvider.Fetch<T>(ct, ids: ids);
            var collectionIds = collection
                .Select(t => t.Id)
                .ToParams();
            var productionEntities = await geniusDataProvider.FetchWithFilter<ProductionTasksEntity>(ct, filter: $"{typeof(T).GetIdFromParentType()}{collectionIds}");

            //map these production entities to the parent
            var mappedEntities = collection
                .Select(t =>
                {
                    var prodEntites = productionEntities
                        .Where(pEntities =>
                        {
                            return pEntities.GetIdFromParentType(typeof(T)).Equals(t.Id);
                        });
                    return (t, prodEntites);
                });

            var mEntitiesTasks = mappedEntities
               .SelectMany(x =>
               {
                   var retVals = x.prodEntites
                    .Select(async y =>
                    {
                        var result = await dbProvider.GetMachinePartPerformance(searchOptions, y.ProductionStartDate, y.ProductionEndDate, getPartId(x.t));
                        return (x.t, new ProductionPart() { MachinePartPerformance = result.FirstOrDefault(), ProductionTasksEntity = y });
                    });
                   return retVals;
               });

            var result = await Task.WhenAll(mEntitiesTasks);

            return result;
        }

    }
}
