using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public static class ExceptionManager
    {
        public static void OnFileNotFound(string fileName)
        {
            MessageBox.Show(string.Format("Error Code: FNF - Failed to load data. File {0} is moved or deleted. By clicking ok you will create an empty file.", fileName));
        }

        public static void OnAttemptToCreateDuplicateActivityCategory(string category)
        {
            MessageBox.Show(string.Format("Error Code: FNF - Failed to load data. Category {0} already exists.", category));
            //Change Error Code
        }
    }
}
