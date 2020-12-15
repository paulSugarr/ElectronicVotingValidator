using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingServer.Server;

namespace Networking.Commands
{
    public class SendElectorSignedCommand : ICommand
    {
        public string Type { get; }
        public byte[] Signed { get; }

        public SendElectorSignedCommand(byte[] signed)
        {
            Type = "validator_sign";
            Signed = signed;
        }
        public SendElectorSignedCommand(Dictionary<string, object> info)
        {
            Type = "validator_sign";
            Signed = System.Numerics.BigInteger.Parse(info.GetString("sign")).ToByteArray();
        }
        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("sign", new System.Numerics.BigInteger(Signed).ToString());
            return result;
        }

        public void Execute(ValidatorContext context, string id)
        {

        }
    }
}