using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;
using SqlSugar;

namespace WebApi.Student.CRUD.Model
{
    [SugarTable("tb_student")]
    public class Tb_Student
    {
        //设置主键
        [SugarColumn(IsPrimaryKey = true)]
        public long? Id { get; set; }
        //学号
        public long? StudentNo { get; set; }
        //学生姓名
        public string Name { get; set; }
        //学生性别
        public int? Sex { get; set; }
        //所属宿舍编号
        public long? StudentGroupId { get; set; }
        //所属班级编号
        public long? ClassId { get; set; }
        //手机号
        public string Mobile { get; set; }
        //QQ
        public string QQ { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }
        //删除条件
        public int IsDelete { get; set; }
        //更改时间
        public DateTime UpdateTime { get; set; }
        //修改人
        public long UpdateUser { get; set; }
    }
}