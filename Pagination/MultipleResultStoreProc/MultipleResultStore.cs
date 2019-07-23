using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.MultipleResultStoreProc
{
    public class MultipleResultStore
    {
        private DbContext db;
        private DbCommand dbcom;
        private DbDataReader reader;

        private int round = 1;
        public MultipleResultStore(DbContext db_)
        {
            db = db_;
            db.Database.Initialize(force: false);
            dbcom = db.Database.Connection.CreateCommand();

        }
        public void Execute(string ProcedureName, params SqlParameter[] param)
        {
            db.Database.Connection.Open();
            dbcom.CommandText = ProcedureName;
            foreach (var para in param)
            {
                dbcom.Parameters.Add(para);
            }
            reader = dbcom.ExecuteReader();
        }
        public IEnumerable<T> GetData<T>(string EntityName = null)
        {
             EntityName = EntityName??typeof(T).Name.ToString();
            if (round == 1)
            {
                List<T> res = ((IObjectContextAdapter)db).ObjectContext.Translate<T>(reader, EntityName, MergeOption.PreserveChanges).ToList();
                round++;
                return res;
            }
            else
            {
                reader.NextResult();
                List<T> res = ((IObjectContextAdapter)db).ObjectContext.Translate<T>(reader, EntityName, MergeOption.PreserveChanges).ToList();
                round++;
                return res;
            }
        }

    }
}
