using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using VirtoCommerce.ExperienceApiModule.DigitalCatalog.Requests;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Exp.ExtensionSamples
{
    public class EvalInventoriesForProductsPipelineBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            var expProducts = new List<ExpProduct2>();
            if (request is LoadProductRequest && response is LoadProductResponse loadProductResponse)
            {
                expProducts = loadProductResponse.Products.OfType<ExpProduct2>().ToList();
            }
            else if(request is SearchProductRequest && response is SearchProductResponse searchProductResponse)
            {
                expProducts = searchProductResponse.Results.OfType<ExpProduct2>().ToList();
            }
            foreach(var expProduct in expProducts)
            {
                if(expProduct.Inventories.IsNullOrEmpty())
                {
                    expProduct.Inventories = new List<Inventory> { new Inventory { FulfillmentCenterId = "dummy", InStockQuantity = 0 } };
                }
            }
        }
    }

}
