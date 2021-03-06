﻿using System.Collections.Generic;
using ElectronicVotingValidator.Server;

namespace Networking.Commands
{
    public class DeclineVoteCommand : ICommand
    {
        public string Type { get; }

        public DeclineVoteCommand()
        {
            Type = "decline_vote";
        }
        public DeclineVoteCommand(Dictionary<string, object> info)
        {
            Type = "decline_vote";
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            return result;
        }

        public void Execute(ValidatorContext context, string id)
        {

        }
    }
}