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
        readonly string m_TestRoomId = Guid.NewGuid().ToString();
        readonly string m_TestActivityId = Guid.NewGuid().ToString();
        readonly string m_TestMall = "TstMall";

        [Test]
        public void TestForActivitiesSaveFile()
        {
            Assert.IsTrue(File.Exists(SerializationManager.GenActivitySaveName(m_TestMall)), "File not created.");
        }

        [Test]
        public void AddNewActivity()
        {
            string testRoomName = "Test room name";
            string testRoomDescription = "Test room description";
            string testRoomType = "Test type";
            int testFloorNumber = 10;
            int testRoomNumber = 102;

            RoomManager roomManager = RoomManager.GetInstance();
            roomManager.CreateRoom(testRoomName, testRoomDescription, testRoomType, testFloorNumber, testRoomNumber, m_TestRoomId,m_TestMall);

            string testActivityCatgory = "Test Category";
            string testActivityDescription = "Test description";
            ActivityStatus testActivityStatus = ActivityStatus.InProgress;
            DateTime testStartTime = default;
            DateTime testEndTime = default;

            m_CurrentManager.AddActivity(m_TestMall,m_TestActivityId, testActivityCatgory, testActivityDescription, m_TestRoomId, testActivityStatus, testStartTime, testEndTime);

            Assert.IsTrue(m_CurrentManager.Activities.ContainsKey(m_TestActivityId));
            Assert.IsTrue(m_CurrentManager.Activities[m_TestActivityId].Category == testActivityCatgory);
            Assert.IsTrue(m_CurrentManager.Activities[m_TestActivityId].Description == testActivityDescription);
            Assert.IsTrue(m_CurrentManager.Activities[m_TestActivityId].CurActivityStatus == testActivityStatus);
            Assert.IsTrue(m_CurrentManager.Activities[m_TestActivityId].StartTime == testStartTime);
            Assert.IsTrue(m_CurrentManager.Activities[m_TestActivityId].EndTime == testEndTime);
            Assert.IsTrue(m_CurrentManager.Activities[m_TestActivityId].Id == m_TestActivityId);

            Dictionary<string, Activity> testDictionary = SerializationManager.GetActivities(m_TestMall);

            Assert.AreEqual(1, testDictionary.Count);
            Assert.IsTrue(testDictionary.ContainsKey(m_TestActivityId));
            Assert.IsTrue(testDictionary[m_TestActivityId].Category == testActivityCatgory);
            Assert.IsTrue(testDictionary[m_TestActivityId].Description == testActivityDescription);
            Assert.IsTrue(testDictionary[m_TestActivityId].CurActivityStatus == testActivityStatus);
            Assert.IsTrue(testDictionary[m_TestActivityId].StartTime == testStartTime);
            Assert.IsTrue(testDictionary[m_TestActivityId].EndTime == testEndTime);
            Assert.IsTrue(testDictionary[m_TestActivityId].Id == m_TestActivityId);
        }
    }
}
