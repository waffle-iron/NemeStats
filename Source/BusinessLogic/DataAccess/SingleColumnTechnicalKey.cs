﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataAccess
{
    //TODO make this generic
    public interface SingleColumnTechnicalKey
    {
        int Id { get; set; }
    }
}
