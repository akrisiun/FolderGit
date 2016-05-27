using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace WbxUsers.Model
{
    public class Segment // : Infrastructure.DbContext
    {
        public static Segment Open(string firmID = null)
        {
            string connStr = null;
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Segment.LastConnKey].ConnectionString;
            var fix = new SqlConnectionStringBuilder(connStr);

            fix.MultipleActiveResultSets = true;
            connStr = fix.ConnectionString;

            return new Segment(connStr, Segment.LastConnKey) { };
        }

        static Segment()
        {
            Instance = Open();
            Instance.AssureOpen();
        }

        public static Segment ReOpen(string firmID = null)
        {
            if (Instance != null)
            {
                Instance.Dispose();
                Instance = null;
            }

            Instance = Open(firmID);
            if (!Instance.AssureOpen())
                return null;
            return Instance;
        }

        public Segment(string connstr, string connKey) : base(connstr, connKey) { }

        public static new Segment Instance { get; protected set; }

        public string Lang { get; set; }
        public int? nUserID { get; set; }
        // public DomainData DomainData { get; set; } 

    }

}
