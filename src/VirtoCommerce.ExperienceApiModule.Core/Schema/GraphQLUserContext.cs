using System.Collections.Generic;
using System.Security.Claims;
using GraphQL.Authorization;

namespace VirtoCommerce.ExperienceApiModule.Core.Schema
{
    public class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}
