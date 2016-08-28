using System.Collections.Generic;
using System.Data.Entity;
using DATASCAN.Model.Floutecs.Catalogs;

namespace DATASCAN.Context
{
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            List<FloutecParamsTypes> paramsTypes = new List<FloutecParamsTypes>
            {
                new FloutecParamsTypes {Code = 0, Param = "Д", Description = "Давление"}
            };

            context.FloutecParamsTypes.AddRange(paramsTypes);

            base.Seed(context);
        }
    }
}
