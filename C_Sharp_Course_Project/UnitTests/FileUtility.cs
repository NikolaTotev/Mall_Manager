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
            RoomManager.GetInstance().CreateRoom("Test room", "Test Desc", "Store", 1, 20, Guid.NewGuid(),MallManager.GetInstance().CurrentMall.Name);
            ActivityManager.GetInstance().AddActivity(MallManager.GetInstance().CurrentMall.Name, Guid.NewGuid(), "Cl",
                "tstDesc", RoomManager.GetInstance().GetRooms().Keys.ToList()[0], ActivityStatus.Scheduled, DateTime.Now,
                new DateTime(2021, 12, 01));
        }

        [Test]
        public void GetTestMalls()
        {
            ProgramManager currentManager = ProgramManager.GetInstance();
            MallManager.GetInstance().ChangeCurrentMall(MallManager.GetInstance().Malls.Keys.ToList()[0]);
            Dictionary<Guid, string> malls = MallManager.GetInstance().GetMalls();
            Assert.IsTrue(malls.Values.ToList()[0] == "Mall Name: Test Mall 1Mall Description: First test mall");
        }

    }
}
