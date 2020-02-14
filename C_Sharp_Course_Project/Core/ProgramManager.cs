namespace Core
{
    public class ProgramManager
    {
        private static ProgramManager m_Instance;

        private ProgramManager()
        {
            m_Instance = this;
            SerializationManager.CheckForDirectories();
            MallManager.GetInstance();
        }

        /// <summary>
        /// Singleton pattern. Gets the instance of the ProgramManager if there is one.
        /// If one doesn't exist a new one is created.
        /// </summary>
        /// <returns></returns>
        public static ProgramManager GetInstance() {
            if (m_Instance == null)
            {
                m_Instance = new ProgramManager();
            }
            return m_Instance;
        }

        /// <summary>
        /// Completes initialization of the remaining managers after a mall is opened.
        /// </summary>
        public void CompleteInitialization()
        {
            ActivityManager.GetInstance();
            RoomManager.GetInstance();
        }
    }
}