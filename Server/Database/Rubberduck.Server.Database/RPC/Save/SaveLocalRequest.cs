﻿using Newtonsoft.Json.Linq;
using Rubberduck.RPC.Platform.Model.Database;

namespace Rubberduck.Server.Database.RPC.Save
{
    public class SaveLocalRequest : SaveRequest<Local>
    {
        public SaveLocalRequest(object id, JToken @params) 
            : base(id, JsonRpcMethods.SaveLocal, @params)
        {
        }
    }
}
