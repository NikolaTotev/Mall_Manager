using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core
{

    public class ProgramManager
    {
        private static ProgramManager instance;

        public ProgramManager()
        {

        }

        public static ProgramManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ProgramManager();
            }
            return instance;
        }
    }
}