using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using SqlSugar;
using WebApi.Student.CRUD.Model;


namespace WebApi.Student.CRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class CourseController : CourseResult
    {
        public readonly Dbcontext _dbContxt;
        public CourseController(Dbcontext dbContxt)
        {
            _dbContxt = dbContxt;
        }

        /// <summary>
        ///课程管理 查看课程接口  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetCourse(string CourseName, string CourseTeacher)
        {            
            //连表查询，获取到课程表和课程报名表的信息以及学生表的姓名
            var list = _dbContxt.db.Queryable<Course, StudentCourseRelation, Tb_Student>((c, scr, tb) => new object[] {
               JoinType.Left,c.CourseId==scr.Id,
               JoinType.Left,scr.StudentNo==tb.StudentNo})
               .Select((c, scr, tb) => new
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseTeacher = c.CourseTeacher,
                    TeacherSex = c.TeacherSex == "1" ? "男" : "女",
                    CourseDetail = c.CourseDetail,
                    CreateTime = scr.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                    StudentNo = tb.StudentNo,
                    Isdelete = c.IsDelete,
                    Name = tb.Name
                }).Where(c => c.Isdelete == 0).ToList();

            int count = list.Count;
            //判断是否查询到课程
            if (list == null)
            {
                return Failure(ResultCode.COURSE_DATA_NONE);
            }
            //表单初始化         
            return Success(list,count);
        }

        /// <summary>
        /// 添加接口
        /// </summary>
        [HttpPost]
        public IActionResult GetAdd(GetAddRequest request)
        {
            //判断课程名是否为空
            if (request.CourseName == null)
            {
                return Failure(ResultCode.COURSE_NAME_NONE);
            }
            //判断教师姓名是否为空
            if (request.CourseTeacher == null)
            {
                return Failure(ResultCode.TEACHER_NAME_NONE);
            }
            //判断性别是否为空
            if (request.TeacherSex == null)
            {
                return Failure(ResultCode.TEACHER_SEX_NONE);
            }
            //校验课程是否存在
            bool cn = _dbContxt.db.Queryable<Course>().Where(it => it.CourseName == request.CourseName).Any();
            if (cn)
            {
                return Failure(ResultCode.COURSE_DATA_NONE);
            }
            
            Course course = new Course
            {
                //获取添加时间   
                CreateTime=DateTime.Now,
                IsDelete=0,
                CourseName = request.CourseName,
                CourseDetail = request.CourseDetail,
                CourseTeacher = request.CourseTeacher,
                TeacherSex=request.TeacherSex                
            };
            //执行添加语句
            bool add = _dbContxt.db.Insertable(course).ExecuteCommandIdentityIntoEntity();//添加课程信息 
            return add ? Success() : Failure(ResultCode.DEFEAT);
        }

        /// <summary>
        /// 详情接口
        /// </summary>
        [HttpPost]
        public IActionResult Detail(int courseId)
        {
            //连表查询获取课程信息，学生信息可选报课程的信息
            var list = _dbContxt.db.Queryable<StudentCourseRelation, Tb_Student>((scr, tb) => new object[] {
               JoinType.Left,scr.StudentNo == tb.StudentNo
            }).Select((scr, tb) => new
            {
                CourseId = scr.CourseId,
                CreateTime = scr.CreateTime,
                StudentNo = tb.StudentNo,
                Name = tb.Name
            }).Where(scr => scr.CourseId == courseId).ToList();            
            return Success(list,list.Count);
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        [HttpPost]

        public IActionResult GetDelete(GetDeleteRequest request)
        {
            //查询课程的第一条数据
            var entit = _dbContxt.db.Queryable<Course>().Where(a => a.CourseId == request.CourseId).First();    
            //判断课程是否存在
            if (entit == null)
            {
                return  Failure(ResultCode.COURSE_DATA_NONE); ;
            }
            //判断是否删除
            if (entit.IsDelete == 1)
            {
                return Failure(ResultCode.ISDELETE_ON);
            }
            //获取删除时间
            Course course = new Course()
            {
                UpdateTime = DateTime.Now,
            };
            //执行删除假语句
            int del = _dbContxt.db.Updateable<Course>(new { IsDelete = 1, UpdateTime = DateTime.Now,request.CourseId }).ExecuteCommand();
            //判断是否删除成功
            return del >  0 ? Success(): Failure(ResultCode.DEFEAT) ;
        }

        /// <summary>
        /// 选课接口
        /// </summary>
        [HttpPost]
        public IActionResult GetChoice(GetChoiceRequest request)
        {
            StudentCourseRelation studentCourseRelation = new StudentCourseRelation() 
            {
                //获取创建时间
                CreateTime = DateTime.Now,
                CourseId = request.CourseId,
                StudentNo = request.StudentNo
            };
            bool cor = _dbContxt.db.Queryable<StudentCourseRelation>().Where(it => it.CourseId==request.CourseId).Any();
            if (cor==false)
            {
                return Failure(ResultCode.COURSE_NAME_EXIST);
            }
            //执行提交课程
            var ch = _dbContxt.db.Insertable(studentCourseRelation).ExecuteCommand();
            //判断是否成功
            return ch > 0 ? Success() : Failure(ResultCode.DEFEAT);
        }

        /// <summary>
        /// 编辑接口
        /// </summary>

        [HttpPost]
        public IActionResult GetEdit(GetEditRequest request)
        {
            //查询课程的第一条数据
            var entit = _dbContxt.db.Queryable<Course>().Where(a => a.CourseId == request.CourseId).First();
            return Success(entit);
        }
        [HttpPost]
        public IActionResult EditCourse(GetEditRequest request)
        {
            Course course = new Course
            {
                UpdateTime=DateTime.Now,
                CourseId=request.CourseId,
                CourseName=request.CourseName,
                CourseDetail=request.CourseDetail,
                CourseTeacher=request.CourseTeacher,
                TeacherSex=request.TeacherSex
            };
            try
            {
                int value = _dbContxt.db.Updateable(course).ExecuteCommand();
                if (value == 0)
                {
                    //保存失败
                    throw new Exception("保存数据失败");
                }
                return Success(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
       
        }       
    }
}

