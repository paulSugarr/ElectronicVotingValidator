﻿using System.Collections.Generic;
using ElectronicVotingServer.Server;

namespace Networking.Commands
{
    public interface ICommand
    {
        string Type { get; }
        void Execute(ValidatorContext context, string id);
        Dictionary<string, object> GetInfo();
    }
}