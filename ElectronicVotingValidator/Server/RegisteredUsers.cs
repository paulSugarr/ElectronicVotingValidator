using System.Collections.Generic;

namespace ElectronicVotingValidator.Server
{
    public class RegisteredUsers
    {
        private Dictionary<string, Dictionary<string, object>> _users;

        public void RegisterUsers()
        {
            _users = new Dictionary<string, Dictionary<string, object>>();
            
            _users.Add("paul", null);
        }

        public void FillUserKey(string userId, Dictionary<string, object> key)
        {
            _users[userId] = key;
        }

        public Dictionary<string, object> GetSignKey(string id)
        {
            return new Dictionary<string, object>(_users[id]);
        }
    }
}