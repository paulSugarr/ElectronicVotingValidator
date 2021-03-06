﻿using System;
using System.Collections.Generic;
using System.Text;
using ElectronicVoting.Extensions;
using ElectronicVotingValidator.Server;

namespace Networking.Commands
{
    public class SendValidatorBlindSignCommand : ICommand
    {
        public string Type { get; }
        public byte[] Blinded { get; }
        public byte[] BlindedSigned { get; }
        public string Id { get; }
        
        public SendValidatorBlindSignCommand(byte[] blinded, byte[] blindedSigned, string id)
        {
            Type = "elector_blind_sign";
            Blinded = blinded;
            BlindedSigned = blindedSigned;
            Id = id;
        }
        public SendValidatorBlindSignCommand(Dictionary<string, object> info)
        {
            Type = "elector_blind_sign";
            Blinded = System.Numerics.BigInteger.Parse(info.GetString("blinded")).ToByteArray();
            BlindedSigned = System.Numerics.BigInteger.Parse(info.GetString("blinded_signed")).ToByteArray();
            Id = info.GetString("id");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("blinded", new System.Numerics.BigInteger(Blinded).ToString());
            result.Add("blinded_signed", new System.Numerics.BigInteger(BlindedSigned).ToString());
            result.Add("id", Id);
            return result;
        }

        public void Execute(ValidatorContext context, string id)
        {
            var validator = context.Validator;
            if (!context.RegisteredUsers.CanVote(Id))
            {
                var command = new DeclineVoteCommand();
                context.Server.SendCommand(command, id);
            }
            else if (validator.VerifyBulletin(BlindedSigned, Blinded, context.RegisteredUsers.GetSignKey(Id)))
            {
                var signedByValidator = validator.SignBulletin(Blinded);
                Console.WriteLine("bulletin validated");
                context.RegisteredUsers.SetVoted(Id);
                var command = new SendElectorSignedCommand(signedByValidator);
                context.Server.SendCommand(command, id);
            }
        }
    }
}