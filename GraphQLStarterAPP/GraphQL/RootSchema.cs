using System;
using GraphQL;
using GraphQL.Types;

namespace GraphQLStarterAPP.GraphQL
{
        public class RootSchema : Schema
        {
            public RootSchema(IDependencyResolver resolver) : base(resolver)
            {
                Query = resolver.Resolve<Query>();
            }
        }
}
