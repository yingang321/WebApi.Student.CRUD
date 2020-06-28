using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebApi.Student.CRUD
{
    public class Dbcontext
    {
        public SqlSugarClient db { get; set; }
        public Dbcontext()
        {
            Init();
        }
        private void Init()
        {
            db = new SqlSugarClient(
                 new ConnectionConfig()
                 {
                    ConnectionString = "Server=rm-bp1p6h45ao4wm91940o.mysql.rds.aliyuncs.com;User Id=jusr8pkef6hp;Password=1qaz2wsx3edC;Database=sqlsugartest3;Convert Zero Datetime=True",
                    DbType = DbType.MySql,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                 });
        }

        internal object Insertable(object insertObj)
        {
            throw new NotImplementedException();
        }
    }
}
