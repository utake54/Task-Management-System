using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility.ExceptionHelper
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException()
        {
            throw new RecordNotFoundException();
        }

        public RecordNotFoundException(string message) : base(message)
        {
            throw new RecordNotFoundException(message);
        }
    }
}
