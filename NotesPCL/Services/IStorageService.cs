using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesPCL.Services
{
    public interface IStorageService
    {
        void Save<T(String key, T value);

        T Load<T>(String key);
    }
}
