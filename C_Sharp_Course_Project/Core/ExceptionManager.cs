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
            //FNF = File Not Found.
            // MessageBox.Show(string.Format("Error Code: FNF - Failed to load data. File {0} is moved or deleted. By clicking ok you will create an empty file.", fileName));
        }

        public static void OnCreateDuplicateCategory(string category)
        {
            //ACDC = Attempt to Create Duplicate Category.
            // MessageBox.Show(string.Format("Error Code: ACDC - Failed to load data. Category {0} already exists.", category));
        }

        public static void OnNullParamsToFunction(string nameOfFunctionCalled)
        {
            //NULLP = Null Params.
            // MessageBox.Show(string.Format("Warning Code: NULLP - Attempting to {0} with missing input. ", nameOfFunctionCalled));
        }

        public static void OnInvalidDate()
        {
            // MessageBox.Show(string.Format("Warning Code: InvD - Invalid dates entered "));
        }

    }
}
