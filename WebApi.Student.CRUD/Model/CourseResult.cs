using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Reflection;

namespace WebApi.Student.CRUD.Model
{
    /// <summary>
    /// 公共返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetCoueseResult
    {
        //状态码
        public int code { get; set; }
        //消息
        public string msg { get; set; }
        //数据总数
        public int count { get; set; }
        //数据
        public object data { get; set; }
    }
    /// <summary>
    /// 返回模型
    /// </summary>
    public class CourseResult : ControllerBase //where TResult : IActionResult
    {
        /// <summary>
        /// 不带数据成功返回
        /// </summary>
        /// <returns></returns>
        public IActionResult Success()
        {
            GetCoueseResult result = new GetCoueseResult();
            result.code = (int)ResultCode.SUCCESS;
            result.msg = ResultCode.SUCCESS.GetDescription();
            return Ok(result);
        }
        /// <summary>
        /// 带数据成功返回
        /// </summary>
        /// <returns></returns>
        public IActionResult Success<T>(T data)
        {
            GetCoueseResult result = new GetCoueseResult
            {
                code = (int)ResultCode.SUCCESS,
                msg = ResultCode.SUCCESS.GetDescription(),
                data = data
            };
            return Ok(result);
        }
        /// <summary>
        /// 成功带数据集返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult Success<T>(T data, int Count)
        {
            GetCoueseResult result = new GetCoueseResult
            {
                code = 0,
                msg = ResultCode.SUCCESS.GetDescription(),
                count = Count,
                data = data
            };
            return Ok(result);
        }
        /// <summary>
        /// 不带参失败返回
        /// </summary>
        /// <param name="resultCode"></param>
        /// <returns></returns>
        public IActionResult Failure(ResultCode resultCode)
        {
            GetCoueseResult result = new GetCoueseResult
            {
                code = (int)resultCode,
                msg = resultCode.GetDescription()
            };
            return Ok(result);
        }
        /// <summary>
        /// 带数据失败返回类型
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult Failure<T>(ResultCode resultCode, T data)
        {
            GetCoueseResult result = new GetCoueseResult
            {
                code = (int)resultCode,
                msg = resultCode.GetDescription(),
                data = data
            };
            return Ok(result);
        }
    }
    /// <summary>
    /// 错误提示枚举
    /// </summary>
    public enum ResultCode
    {
        [Description("操作成功")]
        SUCCESS = 200,

        /* 参数错误：10001-19999 */
        [Description("参数无效")]
        PARAM_IS_INVALID = 10001,
        [Description("参数为空")]
        PARAM_IS_BLANK = 10002,
        [Description("参数类型错误")]
        PARAM_TYPE_BIND_ERROR = 10003,
        [Description("参数缺失")]
        PARAM_NOT_COMPLETE = 10004,

        /* 系统错误：40001-49999 */
        [Description("系统错误")]
        SYSTEM_INNER_ERROR = 40001,
        [Description("操作失败")]
        DEFEAT = 40002,

        /* 数据错误：50001-599999 */
        [Description("数据未找到")]
        RESULE_DATA_NONE = 50001,
        [Description("数据有误")]
        DATA_IS_WRONG = 50002,
        [Description("数据已存在")]
        DATA_ALREADY_EXISTED = 50003,

        /* 接口错误：60001-69999 */
        [Description("内部系统接口调用异常")]
        INTERFACE_INNER_INVOKE_ERROR = 60001,

        [Description("用户无权限")]
        NO_AUTHORITY = 99998,
        //[Description("session失效")]
        //SESSION_INVALID = 99999,

        /*自定义错误：800-900*/
        [Description("已被删除")]
        ISDELETE_ON = 800,
        [Description("该课程未找到")]
        COURSE_DATA_NONE = 801,
        [Description("课程名为必填项")]
        COURSE_NAME_NONE = 802,
        [Description("教师名为必填项")]
        TEACHER_NAME_NONE = 803,
        [Description("教师性别为必填项")]
        TEACHER_SEX_NONE = 804,
        [Description("该课程已存在")]
        COURSE_NAME_EXIST = 805,
    }

    /// <summary>
    /// 获取枚举描述
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();
            FieldInfo fd = type.GetField(en.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string name = string.Empty;
            foreach (DescriptionAttribute attr in attrs)
            {
                name = attr.Description;
            }
            return name;
        }
    }

}
