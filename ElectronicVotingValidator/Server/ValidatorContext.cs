using System;
using System.Diagnostics;
using ElectronicVoting.Cryptography;
using ElectronicVoting.Electors;
using ElectronicVoting.Extensions;
using ElectronicVoting.Validators;
using Factory;

namespace ElectronicVotingValidator.Server
{
    public class ValidatorContext
    {
        public ICryptographyProvider CryptographyProvider { get; }
        public Validator Validator { get; private set; }
        public ServerModel Server { get; }
        public MainFactory MainFactory { get; }
        public RegisteredUsers RegisteredUsers { get; }

        public ValidatorContext(ServerModel server)
        {
            CryptographyProvider = new RSACryptography();

            CreateValidator();
            
            Server = server;

            MainFactory = new MainFactory();
            MainFactory.RegisterTypes();
            
            RegisteredUsers = new RegisteredUsers();
            RegisteredUsers.RegisterUsers();
        }

        private void CreateValidator()
        {
            var checkCrypto = false;
            var i = 0;
            while (!checkCrypto)
            {
                Validator = new Validator(CryptographyProvider);
                Validator.CreateKeys();
                var validatorKey = Validator.PublicKey.GetChangeableCopy();
                var elector = new Elector(CryptographyProvider, validatorKey);
                elector.CreateNewKeys();
                var blindedSigned = elector.CreateBlindedSignedMessage(0);
                var blinded = elector.CreateBlindedMessage(0);
                checkCrypto = CryptographyProvider.VerifyData(elector.PublicSignKey.GetChangeableCopy(), blinded, blindedSigned);
                i++;
            }

        }
    }
}