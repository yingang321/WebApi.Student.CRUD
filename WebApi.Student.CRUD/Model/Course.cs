using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Renci.SshNet.Security;
using SqlSugar;

namespace WebApi.Student.CRUD.Model
{
    [SugarTable("Course")]
    
    public class Course
    {
        //设置主键
        [SugarColumn(IsPrimaryKey = true)]
        //课程Id
        public long CourseId { get; set; }
        //课程名称
        public string CourseName { get; set; }
        //授课老师
        public string CourseTeacher { get; set; }
        //老师性别
        public string TeacherSex { get; set; }
        //课程简介
        public string CourseDetail { get; set; }
        //提交时间
        public DateTime CreateTime { get; set; }
        //更改时间
        public DateTime UpdateTime { get; set; }
        //删除时间
        public DateTime DeleteTime { get; set; }
        //删除条件
        public long IsDelete { get; set; }
    }
    

    
}
