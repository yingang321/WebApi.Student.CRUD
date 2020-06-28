using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace WebApi.Student.CRUD.Model
{


    [SugarTable("student_course_relation")]
    public class StudentCourseRelation
    {
        /// <summary>
        /// 课程报名详情
        /// </summary>
       
        public long? Id { get; set; }
        //课程Id
        public long? CourseId { get; set; }
        //学号
        public long? StudentNo { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }
        //更改时间
        public DateTime? UpdateTime { get; set; }
        //删除时间
        public DateTime? DeleteTime { get; set; }
        //删除条件
        public bool IsDelete { get; set; }

    }

}
