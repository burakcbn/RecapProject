using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal:IRepositoryBase<User>
    {
         List<OperationClaim> GetOperationClaims(User user);
        
    }
}
