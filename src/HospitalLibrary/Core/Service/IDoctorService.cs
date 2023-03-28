﻿using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
        Doctor GetById(int id);
    }
}
