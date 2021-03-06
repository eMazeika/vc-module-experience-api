using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VirtoCommerce.ExperienceApiModule.Core;
using VirtoCommerce.ExperienceApiModule.DigitalCatalog.Index;
using VirtoCommerce.ExperienceApiModule.DigitalCatalog.Requests;
using VirtoCommerce.SearchModule.Core.Model;
using VirtoCommerce.SearchModule.Core.Services;

namespace VirtoCommerce.ExperienceApiModule.DigitalCatalog.Handlers
{
    public class SearchProductRequestHandler : IRequestHandler<SearchProductRequest, SearchProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISearchProvider _searchProvider;
        private readonly ISearchPhraseParser _searchPhraseParser;
        public SearchProductRequestHandler(ISearchProvider searchProvider, ISearchPhraseParser searchPhraseParser, IMapper mapper)
        {
            _searchProvider = searchProvider;
            _searchPhraseParser = searchPhraseParser;
            _mapper = mapper;
        }

        public virtual async Task<SearchProductResponse> Handle(SearchProductRequest request, CancellationToken cancellationToken)
        {
            var result = new SearchProductResponse();
            var searchRequest = new SearchRequestBuilder(_searchPhraseParser)
                                            .WithFuzzy(request.Fuzzy)
                                            .ParseFilters(request.Filter)
                                            .ParseFacets(request.Facet)
                                            .WithSearchPhrase(request.Query)
                                            .WithPaging(request.Skip, request.Take)
                                            .AddSorting(request.Sort)
                                            //TODO: Remove hardcoded field name  __object from here
                                            .WithIncludeFields(request.IncludeFields.Concat(new[] { "id" }).Select(x => "__object." + x).ToArray())
                                            .WithIncludeFields(request.IncludeFields.Where(x=>x.StartsWith("prices.")).Concat(new[] { "id" }).Select(x => "__prices." + x.TrimStart("prices.")).ToArray())
                                            .Build();
                                           
            var searchResult = await _searchProvider.SearchAsync(KnownDocumentTypes.Product, searchRequest);
            result.Results = searchResult.Documents?.Select(x => _mapper.Map<ExpProduct>(x)).ToList();
            result.Facets = searchRequest.Aggregations?.Select(x => _mapper.Map<FacetResult>(x, opts => opts.Items["aggregations"] = searchResult.Aggregations)).ToList();

            result.TotalCount = (int)searchResult.TotalCount;
            return result;
        }
    }
}
