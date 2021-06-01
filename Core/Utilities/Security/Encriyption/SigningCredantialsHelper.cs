using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encriyption
{
   public  class SigningCredantialsHelper
    {
        public static SigningCredentials CreateSigningCredantialKey(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
      
    }
}
