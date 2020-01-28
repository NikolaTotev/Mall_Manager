using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class FileUtility
    {
        [Test]
        public void CreateActivityConfigFile()
        {
            string savePath = SerializationManager.ActivityConfigSaveBase;
            ActivityConfig newConfig = new ActivityConfig();
            List<string> newCategories = new List<string>(){"Light Maintenance","Heavy Maintenance", "Cleaning", "Inspection"};
            List<Activity> newTemplates = new List<Activity>();
            
            Activity newTemplate = new Activity(Guid.Empty,Guid.Empty, isTemplate:true, category:"Cleaning", status:ActivityStatus.Scheduled,startDate:new DateTime(2020,12,12) );

            newTemplates.Add(newTemplate);
            newConfig.Categories = newCategories;
            newConfig.Templates = newTemplates;
            SerializationManager.SaveActivityConfigFile(newConfig, "Err Suppression");
            //int result = 1;
            //Assert.AreEqual(0, result);
        }

        [Test]
        public void GenInitialDirectories()
        {
            SerializationManager.CheckForDirectories();
        }

        [Test]
        public void GenTestMalls()
        {
            
            ProgramManager currentManager = ProgramManager.GetInstance();
            Mall testMall = new Mall(Guid.NewGuid(), "Test Mall 1", "First test mall");
            MallManager.GetInstance().AddMall(testMall.Id, testMall.Name, testMall.Description);

        }
    }
}
