using ElectronicVoting.Cryptography;
using ElectronicVoting.Validators;
using Factory;

namespace ElectronicVotingValidator.Server
{
    public class ValidatorContext
    {
        public ICryptographyProvider CryptographyProvider { get; }
        public Validator Validator { get; }
        public ServerModel Server { get; }
        public MainFactory MainFactory { get; }
        public RegisteredUsers RegisteredUsers { get; }

        public ValidatorContext(ServerModel server)
        {
            CryptographyProvider = new RSACryptography();
            Validator = new Validator(CryptographyProvider);
            Validator.CreateKeys();
            
            Server = server;

            MainFactory = new MainFactory();
            MainFactory.RegisterTypes();
            
            RegisteredUsers = new RegisteredUsers();
            RegisteredUsers.RegisterUsers();
        }
    }
}