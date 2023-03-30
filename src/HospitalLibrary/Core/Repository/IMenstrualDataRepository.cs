using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface IMenstrualDataRepository
    {
        IEnumerable<MenstrualData> GetAll();
        MenstrualData GetById(int id);
        void Create(MenstrualData data);
        void Update(MenstrualData data);
        void Delete(MenstrualData data);
    }
}
