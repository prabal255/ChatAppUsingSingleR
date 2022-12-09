using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalrDemo.EFModels;
using SignalrDemo.HubModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrDemo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ChatController : Hub
  {
    private readonly ChatAppContext ctx;

    public ChatController(ChatAppContext context)
    {
      ctx = context;
    }
    [HttpGet]
    public async Task<string> GetTest()
    {
      return "Hello World";
    }

    //2Tutorial
    [HttpPost("authMe")]
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
        await ctx.Connections.AddAsync(currUser);
        await ctx.SaveChangesAsync();

        User newUser = new User(tempPerson.Id, tempPerson.Name, currSignalrID);
        await Clients.Caller.SendAsync("authMeResponseSuccess", newUser);//4Tutorial
        await Clients.Others.SendAsync("userOn", newUser);//4Tutorial
      }

      else //if credentials are incorrect
      {
        await Clients.Caller.SendAsync("authMeResponseFail");
      }
    }


  }
}
