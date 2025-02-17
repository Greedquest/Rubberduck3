﻿using MediatR;
using Newtonsoft.Json.Linq;
using OmniSharp.Extensions.JsonRpc.Server;
using Rubberduck.RPC.Platform.Model.Database.Responses;

namespace Rubberduck.Server.Database.RPC.Disconnect
{
    public class DisconnectRequest : Request, IRequest<SuccessResult>
    {
        public DisconnectRequest(object id, JToken @params) 
            : base(id, JsonRpcMethods.Connect, @params)
        {
        }
    }
}
