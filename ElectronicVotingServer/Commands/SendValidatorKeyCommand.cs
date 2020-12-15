using System.Collections.Generic;
using System.Text;
using ElectronicVoting.Extensions;
using ElectronicVotingServer.Server;

namespace Networking.Commands
{
    public class SendValidatorKeyCommand : ICommand
    {
        public string Type { get; }

        public SendValidatorKeyCommand()
        {
            Type = "send_validator_key";
        }

        public SendValidatorKeyCommand(Dictionary<string, object> info)
        {
            Type = "send_validator_key";
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            return result;
        }

        public void Execute(ValidatorContext context, string id)
        {
            var key = context.Validator.PublicKey;
            var commandData = new Dictionary<string, object>();
            commandData.Add("type", "set_validator_key");
            commandData.Add("key", key);
            var command = context.MainFactory.CreateInstance<ICommand>(commandData);
            var json = fastJSON.JSON.ToJSON(command.GetInfo());
            context.Server.SendMessage(json, id);
        }
    }
}