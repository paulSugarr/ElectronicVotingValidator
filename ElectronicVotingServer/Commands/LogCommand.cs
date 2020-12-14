using System;
using System.Collections.Generic;
using ElectronicVoting.Extensions;

namespace Networking.Commands
{
    public class LogCommand : ICommand
    {
        public string Type { get; }
        public string Message { get; }

        public LogCommand(string message)
        {
            Type = "log";
            Message = message;
        }
        public LogCommand(string type, Dictionary<string, object> info)
        {
            Type = "log";
            Message = info.GetString("message");
        }

        private Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("message", Message);
            return result;
        }
        public void Execute()
        {
            Console.WriteLine(Message);
        }
    }
}