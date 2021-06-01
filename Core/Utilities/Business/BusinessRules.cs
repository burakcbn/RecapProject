using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            // var list = new List<IResult>();
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                    // list.Add(logic);
                }
            }
            
            return null;
        }
    }
}
