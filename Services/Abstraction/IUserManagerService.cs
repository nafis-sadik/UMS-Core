﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface IUserManagerService
    {
        bool AddNewUser(UserInfo userInfo);
    }
}