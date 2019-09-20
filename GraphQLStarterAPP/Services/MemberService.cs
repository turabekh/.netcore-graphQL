using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLStarterAPP.Models;

namespace GraphQLStarterAPP.Services
{
    public class MemberService : IMemberService
    {
        private static List<DevTeamMember> _members;

        public MemberService()
        {
            _members = new List<DevTeamMember>()
            {
                new DevTeamMember() {FirstName = "Adarsh", LastName = "Kurudi", FromCountry = "India"},
                new DevTeamMember() {FirstName = "Al", LastName = "Pascale", FromCountry = "USA"},
                new DevTeamMember() {FirstName = "Turaboy", LastName = "Holmirzaev", FromCountry = "Uzbekistan"},
                new DevTeamMember() {FirstName = "Steve", LastName = "Chen", FromCountry = "Taiwan"},
                new DevTeamMember() {FirstName = "Parth", LastName = "Patel", FromCountry = "India"},
                new DevTeamMember() {FirstName = "Limon", LastName = "Karim", FromCountry = "Bangladesh"},
                new DevTeamMember() {FirstName = "Ankit", LastName = "Prajapati", FromCountry = "India"},
            };
        }
        public IEnumerable<DevTeamMember> GetMembers()
        {
            return _members;
        }
        public DevTeamMember GetMemberByName(string name)
        {
            return _members.Where(m => m.FirstName.Trim().ToLower().Equals(name.Trim().ToLower())).FirstOrDefault<DevTeamMember>();
        }
    }

    public interface IMemberService
    {
        IEnumerable<DevTeamMember> GetMembers();
        DevTeamMember GetMemberByName(string name);
    }
}
