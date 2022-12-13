using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalrDemo.EFModels;
using SignalrDemo.HubModels;
using System;
using System.Collections.Generic;
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

        //2Tutorial
        [HttpGet("getGroupMessage")]
        public IEnumerable<MessageDetail> getGroupMessage(int grpId)
        {
         
            var cards = ctx.MessageDetails.Where(x=>x.GroupId==grpId).ToList();
            return cards;
        }
       
    }
}
