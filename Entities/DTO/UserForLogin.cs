using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
    public class UserForLogin : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
