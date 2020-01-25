﻿using System;
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

        public static void OnAttemptToCreateDuplicate(string category)
        {
            MessageBox.Show(string.Format(" Category {0} already exists.", category));
            //Change Error Code
        }

        public static void OnNullParamsToFunction(string function)
        {
            MessageBox.Show(string.Format("Null Params: Function {0}.", function));
        }
    }
}
