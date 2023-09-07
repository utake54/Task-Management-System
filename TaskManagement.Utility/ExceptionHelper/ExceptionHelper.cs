using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility.ExceptionHelper
{
    public static class ExceptionHelper
    {
        public static void IfNullOrEmpty(this object obj, string msgCode)
        {
            if (obj == null)
            {
               throw new RecordNotFoundException(msgCode);
            }
        }
    }
}
