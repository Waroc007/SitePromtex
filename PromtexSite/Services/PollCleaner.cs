using Microsoft.AspNetCore.Mvc;
using PromtexSite.Controllers;
using PromtexSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Services
{
    public class PollCleaner
    {
        public async static void CleanOldPoll(object obj)
        {
            Dictionary<Guid, Poll> poll = obj as Dictionary<Guid, Poll>;
            if (poll is Dictionary<Guid, Poll>)
            {
                var Time = DateTime.Now.AddMinutes(-30);
                var keyDelete = new List<Guid>();
                foreach (var p in poll)
                {
                    if (p.Value.Sent)
                    {
                        keyDelete.Add(p.Key);
                    }
                    else if(p.Value.TimeCreate < Time)
                    {
                        await p.Value.Send();
                        keyDelete.Add(p.Key);
                    }
                }
                if (keyDelete.Count > 0) {
                    foreach (var k in keyDelete)
                    {
                        poll.Remove(k);
                    }
                }
            }
        }
        
    }
}
