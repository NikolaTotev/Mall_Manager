using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace UnitTests
{
    [TestFixture]
    class ActivityManagerTester
    {
        private ActivityManager m_CurrentManager = ActivityManager.GetInstance(); 
    }
}
