﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.IBLL;

namespace Test.BLLFactory
{
    public class BLLSession : IBLLSession
    {
        public IUserInfoBll UserBll => AbstractFactory.CreateUserBll();
    }
}
