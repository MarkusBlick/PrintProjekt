using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.DirectoryServices;

namespace PrintumProjektverwaltung.Models
{
    public class printumUser
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool isMapped { get; set; }

        public void  getUserFromAD(string userName)
        {
            printumUser deruser = GetADUsers()
                            .Where(x => x.UserName == userName)
                            .First();
            this.Email = deruser.Email;
            this.UserName = deruser.UserName;
            this.DisplayName = deruser.DisplayName;
            this.isMapped = deruser.isMapped;
        }

        public List<printumUser> GetADUsers()
        {
            try
            {
                List<printumUser> lstADUsers = new List<printumUser>();
                string DomainPath = "LDAP://DC=printum,DC=de";
                DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("usergroup");
                search.PropertiesToLoad.Add("displayname");//first name
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") &&
                                 result.Properties.Contains("mail") &&
                            result.Properties.Contains("displayname"))
                        {
                            printumUser objSurveyUsers = new printumUser();
                            objSurveyUsers.Email = (String)result.Properties["mail"][0] +
                              "^" + (String)result.Properties["displayname"][0];
                            objSurveyUsers.UserName = (String)result.Properties["samaccountname"][0];
                            objSurveyUsers.DisplayName = (String)result.Properties["displayname"][0];
                            lstADUsers.Add(objSurveyUsers);
                        }
                    }
                }
                return lstADUsers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
