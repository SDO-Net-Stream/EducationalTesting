using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.EntityFramework;
using EduTesting.Model;


namespace EduTesting.SyncADUsers
{
	internal class Program
	{
		const string DefaultDomainPath = "LDAP://OU=_ELEKS_,DC=eleks-software,DC=local";
		const string DefaultUsersFilter = "(&(objectClass=user)(objectCategory=person)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";

		const string AccountNameProperty = "samaccountname";
		const string MailProperty = "mail";
		const string FirstNameProperty = "givenname";
		const string LastNameProperty = "sn";

		private static bool CheckIfPropertyAvailable(SearchResult searchResult, string propertyName)
		{
			bool anyfault = false;
			
			if (!searchResult.Properties.Contains(propertyName) || searchResult.Properties[propertyName].Count == 0)
			{
				Console.WriteLine("Warning: Property {0} absent, path: {1}", propertyName, searchResult.Path);
				anyfault = true;
			}

			return !anyfault;
		}

		private static User CreateUser(SearchResult searchResult)
		{
			User newUser = null;

			if (CheckIfPropertyAvailable(searchResult, AccountNameProperty)
				&& CheckIfPropertyAvailable(searchResult, MailProperty)
				&& CheckIfPropertyAvailable(searchResult, FirstNameProperty)
				&& CheckIfPropertyAvailable(searchResult, LastNameProperty))
			{
				newUser = new User()
				{
					UserDomainName = (String)searchResult.Properties[AccountNameProperty][0],
					UserEmail = (String)searchResult.Properties[MailProperty][0],
					UserFirstName = (String)searchResult.Properties[FirstNameProperty][0],
					UserLastName = (String)searchResult.Properties[LastNameProperty][0],
					UserActivated = false,
					UserDeleted = false
				};
			}

			return newUser;
		}

		private static Dictionary<string, User> GetActiveDirectoryUsers()
		{
			var ADUsers = new Dictionary<string, User>();

			var dirSearcher = new DirectorySearcher(new DirectoryEntry(DefaultDomainPath))
			{
				Filter = DefaultUsersFilter
			};

			foreach (SearchResult searchItem in dirSearcher.FindAll())
			{
				var newUser = CreateUser(searchItem);

				if (newUser != null)
				{
					ADUsers.Add(newUser.UserDomainName, newUser);
				}
			}

			return ADUsers;
		}

		private static void Main(string[] args)
		{
            using (var dbContext = new EduTestingDbContext())
			{
				dbContext.Users.Load();
				var ADUsers = GetActiveDirectoryUsers();

				int removedCounter = 0;
				foreach (var user in dbContext.Users)
				{
					if (!user.UserDeleted && user.UserDomainName != null && !ADUsers.ContainsKey(user.UserDomainName))
					{
						++removedCounter;
						user.UserDeleted = true;
						Console.WriteLine("User removed: {0} ({1})", user.UserDomainName, user.UserEmail);
					}
				}

				int addedCounter = 0;
				foreach (var user in ADUsers)
				{
					if (!dbContext.Users.Any(u => u.UserDomainName == user.Key))
					{
						++addedCounter;
						dbContext.Users.Add(user.Value);
						Console.WriteLine("User added: {0} ({1})", user.Value.UserDomainName, user.Value.UserEmail);
					}
				}
				
				dbContext.SaveChanges();
				Console.WriteLine("Synchronization finished... Totally added: {0}, removed {1}", addedCounter, removedCounter);
			}

			Console.ReadLine();
		}
	}
}
