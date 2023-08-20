using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammesSecu.Services
{
    public class ConnectivityServices
    {
        public async Task<bool> Test()
        {
           
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("", "Aucune connexion internet", "Ok");
                return false;
            }
            return true;
        }
    }
}
