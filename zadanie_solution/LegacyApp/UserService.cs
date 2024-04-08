using System;

namespace LegacyApp
{
    public class UserService: UserData
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!CheckUserData(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }
            
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);
            SetCreditLimit(client, user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName,
                HasCreditLimit = client.Type != "VeryImportantClient"
            };

            return user;
        }

        private void SetCreditLimit(Client client, User user)
        {
            if (client.Type == "VeryImportantClient") return;
            using var userCreditService = new UserCreditService();
            var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            if (client.Type == "ImportantClient")
            {
                creditLimit *= 2;
                        
            }
            user.CreditLimit = creditLimit;
        }
    }
}
