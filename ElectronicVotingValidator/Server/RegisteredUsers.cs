using System.Collections.Generic;

namespace ElectronicVotingValidator.Server
{
    public class RegisteredUsers
    {
        private Dictionary<string, Dictionary<string, object>> _users;
        private Dictionary<string, bool> _votedUsers;

        public void RegisterUsers()
        {
            _users = new Dictionary<string, Dictionary<string, object>>();
            _votedUsers = new Dictionary<string, bool>();
            
            _users.Add("paul", null);
        }

        public bool CanRegister(string userId)
        {
            return _users.ContainsKey(userId) && _users[userId] == null;
        }

        public void FillUserKey(string userId, Dictionary<string, object> key)
        {
            _users[userId] = key;
            _votedUsers.Add(userId, false);
        }

        public bool CanVote(string userId)
        {
            return !_votedUsers[userId];
        }
        
        public void SetVoted(string userId)
        {
            _votedUsers[userId] = true;
        }

        public Dictionary<string, object> GetSignKey(string id)
        {
            return new Dictionary<string, object>(_users[id]);
        }
    }
}