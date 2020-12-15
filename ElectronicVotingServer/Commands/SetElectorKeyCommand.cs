using System;
using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingServer.Server;

namespace Networking.Commands
{
    public class SetElectorKeyCommand : ICommand
    {
        public string Type { get; }
        public string Id { get; }
        public Dictionary<string, object> Key { get; }

        public SetElectorKeyCommand()
        {
            Type = "set_elector_key";
        }
        public SetElectorKeyCommand(Dictionary<string, object> info)
        {
            Type = "set_elector_key";
            Id = info.GetString("id");
            Key = info.GetDictionary("key");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("id", Id);
            result.Add("key", Key);
            return result;
        }
        public void Execute(ValidatorContext context, string connectionId)
        {
            context.RegisteredUsers.FillUserKey(Id, Key);
            Console.WriteLine($"User {Id} registered");
        }
    }
}