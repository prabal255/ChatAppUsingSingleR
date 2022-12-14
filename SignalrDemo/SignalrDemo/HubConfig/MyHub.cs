using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

//2Tutorial
using SignalrDemo.EFModels;
using SignalrDemo.HubModels;

namespace SignalrDemo.HubConfig
{
    public partial class MyHub : Hub
    {
        //2Tutorial
        private readonly ChatAppContext ctx;
        
        public MyHub(ChatAppContext context)
        {
            ctx = context;
        }

        //4Tutorial
        public override Task OnDisconnectedAsync(Exception exception)
        {
            int currUserId = ctx.Connections.Where(c => c.SignalrId == Context.ConnectionId).Select(c => c.PersonId).SingleOrDefault();
            ctx.Connections.RemoveRange(ctx.Connections.Where(p => p.PersonId == currUserId).ToList());
            ctx.SaveChanges();
            Clients.Others.SendAsync("userOff", currUserId);
            return base.OnDisconnectedAsync(exception);
        }


        //2Tutorial
        public async Task authMe(PersonInfo personInfo)
        {
            string currSignalrID = Context.ConnectionId;
            Person tempPerson = ctx.Person.Where(p => p.Username == personInfo.userName && p.Password == personInfo.password)
                .SingleOrDefault();

            if (tempPerson != null) //if credentials are correct
            {
                Console.WriteLine("\n" + tempPerson.Name + " logged in" + "\nSignalrID: " + currSignalrID);

                Connections currUser = new Connections
                {
                    PersonId = tempPerson.Id,
                    SignalrId = currSignalrID,
                    TimeStamp = DateTime.Now
                };
                //if(ctx.Connections.FirstOrDefault(x=>x.PersonId==tempPerson.Id) == null)
                //{
                await ctx.Connections.AddAsync(currUser);
                await ctx.SaveChangesAsync();

                //}

                User newUser = new User(tempPerson.Id, tempPerson.Name, currSignalrID);
                await Clients.Caller.SendAsync("authMeResponseSuccess", newUser);//4Tutorial
                await Clients.Others.SendAsync("userOn", newUser);//4Tutorial
            }

            else //if credentials are incorrect
            {
                await Clients.Caller.SendAsync("authMeResponseFail");
            }
        }


        //3Tutorial
        public async Task reauthMe(int personId)
        {
            string currSignalrID = Context.ConnectionId;
            Person tempPerson = ctx.Person.Where(p => p.Id == personId)
                .SingleOrDefault();

            if (tempPerson != null) //if credentials are correct
            {
                Console.WriteLine("\n" + tempPerson.Name + " logged in" + "\nSignalrID: " + currSignalrID);

                Connections currUser = new Connections
                {
                    PersonId = tempPerson.Id,
                    SignalrId = currSignalrID,
                    TimeStamp = DateTime.Now
                };
                await ctx.Connections.AddAsync(currUser);
                await ctx.SaveChangesAsync();

                User newUser = new User(tempPerson.Id, tempPerson.Name, currSignalrID);
                await Clients.Caller.SendAsync("reauthMeResponse", newUser);//4Tutorial
                await Clients.Others.SendAsync("userOn", newUser);//4Tutorial
            }
        } //end of reauthMe


        //4Tutorial
        public void logOut(int personId)
        {
            ctx.Connections.RemoveRange(ctx.Connections.Where(p => p.PersonId == personId).ToList());
            ctx.SaveChanges();
            Clients.Caller.SendAsync("logoutResponse");
            Clients.Others.SendAsync("userOff", personId);
        }
    public async Task Register(Person personInfo)
    {
      
        await ctx.Person.AddAsync(personInfo);
        await ctx.SaveChangesAsync();
      }

    
    }
  }

