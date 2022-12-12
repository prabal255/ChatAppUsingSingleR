using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalrDemo.EFModels;
using SignalrDemo.HubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrDemo.HubConfig
{
    //4TutorialGuid
    public partial class MyHub
    {
        public async Task<string> GroupName(Group grp, string people)
        {
            string grpName = grp.GroupName;

            var group = ctx.Groups.FirstOrDefault(x => x.GroupName == grpName);
            if (group == null)
            {
                await ctx.Groups.AddAsync(grp);
                await ctx.SaveChangesAsync();
                UserGroups(grpName, people);
                return "Group Created";
            }
            else
            {
                return "Please Enter Different Name";
            }

        }
        public void UserGroups(string grpName, string people)
        {

            UserGroup userGroup = new UserGroup();
            String[] spearator = { "[", ",","]","'","'\'" };
            String[] strlist = people.Split(spearator,StringSplitOptions.RemoveEmptyEntries);

            var grp = ctx.Groups.FirstOrDefault(x => x.GroupName == grpName);
            userGroup.GroupId = grp.GroupId;
            List<string> a = JsonConvert.DeserializeObject<List<string>>(people);
           
            Console.WriteLine("This is Array" + a.Count);

               
            foreach (var item in a)
            {
                Console.WriteLine("This is userID " +item);
                var user = ctx.Person.FirstOrDefault(x => x.Name == item);
                userGroup.UseId = user.Id;
                ctx.UserGroups.Add(userGroup);
                ctx.SaveChanges();
               userGroup = new UserGroup();
                userGroup.GroupId = grp.GroupId;

            }
            //catch (Exception ex) {  }
        }

    }
}

