using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQLStarterAPP.Models;

namespace GraphQLStarterAPP.GraphQL.Types
{
    public class DevTeamMemberType : ObjectGraphType<DevTeamMember>
    {
        public DevTeamMemberType()
        {
            Field(x => x.FirstName, type: typeof(StringGraphType));
            Field(x => x.LastName, type: typeof(StringGraphType));
            Field(x => x.FromCountry, type: typeof(StringGraphType));
        }
    }
}
