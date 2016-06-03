using RulesEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.CoreCommon
{
    public class ErrorContains
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
    public class CommonValidate
    {
        public List<ErrorContains> Errors { get; set; }
        public CommonValidate()
        {
            this.Errors = new List<ErrorContains>();
        }
        public Tuple<bool, string> IsValid()
        {
            return Tuple.Create(!Errors.Any(), String.Join("|", Errors.Select(e => e.Error)));
        }
        public CommonValidate isNullOrEmpty(string str, string strName = "")
        {
            if (string.IsNullOrEmpty(str))
                this.Errors.Add(new ErrorContains() { IsValid = false, Error = string.Format("{0}:数据不能为空", strName) });
            return this;
        }
        public CommonValidate LengthBetween(string str, int minlen, int maxlen, string strName = "")
        {
            if (str.Length < minlen || str.Length > maxlen)
                this.Errors.Add(new ErrorContains() { IsValid = false, Error = string.Format("{0}:长度不符合{1}-{2}标准", strName, minlen, maxlen) });
            return this;
        }
    }
    public class test
    {
        public string TestValidate()
        {
            Tuple<bool,string> tuple = new CommonValidate()
                .LengthBetween("Hello" ?? "", 100, 200)
                .isNullOrEmpty("Hello" ?? "").IsValid();
            //不成立的时候返回信息
            if (!tuple.Item1)
            {
                //返回错误信息中的第一条
                var aa =  tuple.Item2.Split('|').FirstOrDefault();
            }
            return "";
        }
    }

    public class MyRulesEngine
    {
        //放回校验的结果类型为Tuple<bool, string[]>
        public Tuple<bool, string[]> RulesEngine<T>(Engine engine, T model)
        {
            var isvalid = engine.Validate(model);
            var report = new ValidationReport(engine);
            var result = report.Validate(model);
            var errors = report.GetErrorMessages(model);
            return Tuple.Create(result, errors);
        }
        //判断校验是否通过
        public bool IsValid<T>(Engine engine, T model)
        {
            return RulesEngine<T>(engine, model).Item1;
        }
        //返回校验失败记录的第一条
        public string ErrorFirst<T>(Engine engine, T model)
        {
            if (!IsValid<T>(engine, model))
            {
                return ErrorList(engine, model).FirstOrDefault().ToString();
            }
            else return "";
        }
        //返回校验失败的集合
        public string[] ErrorList<T>(Engine engine, T model)
        {
            if (!IsValid<T>(engine, model))
            {
                return RulesEngine<T>(engine, model).Item2;
            }
            else return null;
        }
    }
}
