using GraphQL.Types;
using GraphQLStarterAPP.GraphQL.Types;
using GraphQLStarterAPP.Services;

namespace GraphQLStarterAPP.GraphQL
{
    public class Query : ObjectGraphType
    {
        public Query(IMemberService memberService)
        {
            Field<ListGraphType<DevTeamMemberType>>(
                "members",
                resolve: context => memberService.GetMembers());

            Field<DevTeamMemberType>(
                "member",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
                resolve: context => memberService.GetMemberByName(context.GetArgument<string>("name")));
        }
    }
}
