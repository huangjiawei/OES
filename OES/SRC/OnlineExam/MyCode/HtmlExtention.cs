using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.WebPages.Html;
using System.Web.Mvc;

namespace OnlineExam
{
    public static class HtmlHelpers
    {

        public static string Truncate(this HtmlHelper helper, string s, int maxLength)
        {
            if (s == null) return "";
            return s.Length < maxLength ? s : s.Substring(0, maxLength) + "...";
        }
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string alt)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", helper.Encode(src));
            tb.Attributes.Add("alt", helper.Encode(alt));
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }
        //public static MvcHtmlString DDLExamType(this HtmlHelper helper, string src, string alt)
        //{
        //    TagBuilder top = new TagBuilder("select");
        //    TagBuilder tb1 = new TagBuilder("option");
        //    tb1.Attributes.Add("value", helper.Encode(ExamType.期中考试));
        //    tb1.InnerHtml = ExamType.期中考试.ToString();
        //    TagBuilder tb2 = new TagBuilder("option");
        //    tb2.Attributes.Add("value", helper.Encode(ExamType.期中考试));
        //    tb2.InnerHtml = ExamType.期中考试.ToString();
        //    TagBuilder tb3 = new TagBuilder("option");
        //    tb3.Attributes.Add("value", helper.Encode(ExamType.期中考试));
        //    tb3.InnerHtml = ExamType.期中考试.ToString();
        //    TagBuilder tb4 = new TagBuilder("option");
        //    tb4.Attributes.Add("value", helper.Encode(ExamType.期中考试));
        //    tb4.InnerHtml = ExamType.期中考试.ToString();
        //    TagBuilder tb5 = new TagBuilder("option");
        //    tb5.Attributes.Add("value", helper.Encode(ExamType.期中考试));
        //    tb5.InnerHtml = ExamType.期中考试.ToString();

            
        //    top.InnerHtml = tb1.ToString(TagRenderMode.SelfClosing) + tb1.ToString(TagRenderMode.SelfClosing);
        //    return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        //}
        readonly static string imgCheck = "/Content/Image/right.png";
        readonly static string imgUnCheck = "/Content/Image/wrong.png";
        public static MvcHtmlString ImageCheckBox(this HtmlHelper helper, bool check, object htmlAttribute, int width = 50)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", helper.Encode(check ? imgCheck : imgUnCheck));

            var dict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute);
            foreach (var attr in dict)
            {
                tb.Attributes.Add(attr.Key, attr.Value as string);
            }
            if (tb.Attributes.ContainsKey("style"))
                tb.Attributes["style"] = tb.Attributes["style"] + "/r/n width: " + width + "px";
            else tb.Attributes.Add("style", " width:" + width + "px");
            if (tb.Attributes.Where(m => m.Key == "class").Count() <= 0) tb.Attributes.Add("class", "");
            tb.Attributes["class"] = tb.Attributes["class"] + " image-check-box";
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }
        //public static string Image(this HtmlHelper helper, string src, IDictionary<string, object> htmlAttribute, object )
        //{
        //    TagBuilder tb = new TagBuilder("img");
        //    tb.Attributes.Add("src", helper.Encode(src));
        //    Type type = htmlAttribute.GetType();
        //    HtmlHelper.AnonymousObjectToHtmlAttributes()
        //    foreach (var attr in htmlAttribute)
        //    {

        //    }
        //    return tb.ToString(TagRenderMode.SelfClosing);
        //}

        public static MvcHtmlString Image(this HtmlHelper helper, string src, object htmlAttribute)
        {
            TagBuilder tb = new TagBuilder("img");
            //src = UrlHelper.GenerateContentUrl(src, HttpContext.Current);
            tb.Attributes.Add("src", helper.Encode(src));
            //Type type = htmlAttribute.GetType();
            var dict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute);
            foreach (var attr in dict)
            {
                tb.Attributes.Add(attr.Key, attr.Value as string);
            }
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// 用与生成评分
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MvcHtmlString DivStarForRaterater(this HtmlHelper helper, int value, long rateId)
        {
            TagBuilder tb = new TagBuilder("div");
            tb.Attributes.Add("data-rating", value.ToString());
            tb.Attributes.Add("data-id", rateId.ToString());
            tb.Attributes.Add("class", "ratebox");
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }


        /// <summary>
        /// 对问题编号进行编码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="qtype"></param>
        /// <returns></returns>
        public static MvcHtmlString EncodeQuestionID(this HtmlHelper helper, long id, QuestionType qtype)
        {
            return MvcHtmlString.Create(Utitlity.EncodeQuestionID(id, qtype));
        }
    }
}