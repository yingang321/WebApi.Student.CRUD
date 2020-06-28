using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Student.CRUD.Model
{
    /// <summary>
    ///课程管理 查看课程
    /// </summary>
    /// <returns></returns>
    public class GetCourseRequest
    {
        //课程Id
        [Required]
        public long CourseId { get; set; }
        //课程名称
        [Required]
        public string CourseName { get; set; }
        //授课老师
        [Required]
        public string CourseTeacher { get; set; }
        //老师性别
        [Required]
        public string TeacherSex { get; set; }
        //课程简介
        [Required]
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
    /// <summary>
    /// 添加
    /// </summary>
    public class GetAddRequest
    {
        //课程名称
        [Required]
        public string CourseName { get; set; }
        //授课老师
        [Required]
        public string CourseTeacher { get; set; }
        //老师性别
        [Required]
        public string TeacherSex { get; set; }
        //课程简介
        public string CourseDetail { get; set; }

    }
    /// <summary>
    /// 详情
    /// </summary>
    public class DetailRequest
    {
        //课程Id
        [Required]
        public long CourseId { get; set; }
        //课程名称
        public string CourseName { get; set; }
        //课程简介
        public string CourseDetail { get; set; }
        public long? StudentNo { get; set; }
        //学生姓名
        public string Name { get; set; }
        //提交时间
        public DateTime CreateTime { get; set; }
    }
    /// <summary>
    /// 删除
    /// </summary>
    public class GetDeleteRequest
    {
        //课程Id
        [Required]
        public long  CourseId { get; set; }

    }
    /// <summary>
    /// 选课
    /// </summary>
    public class GetChoiceRequest
    {
        //课程Id
        [Required]
        public long CourseId { get; set; }
        //学生
        public long? StudentNo { get; set; }
        
    }
    /// <summary>
    /// 编辑
    /// </summary>
    public class GetEditRequest
    {
        //课程Id
        [Required]
        public long CourseId { get; set; }
        //更改时间
        public DateTime UpdateTime { get; set; }
        [Required]
        public string TeacherSex { get; set; }
        //课程简介
        [Required]
        public string CourseDetail { get; set; }
        //课程名称
        [Required]
        public string CourseName { get; set; }
        //授课老师
        [Required]
        public string CourseTeacher { get; set; }
        //老师性别
    }
    /// <summary>
    /// 详情删除
    /// </summary>
    public class DetailDeleteRequest
    {
        [Required]
        public long Id { get; set; }
        //删除条件
        public long IsDelete { get; set; }
    }
}
