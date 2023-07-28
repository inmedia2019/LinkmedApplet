<%@ WebHandler Language="C#" Class="linyimaiapi" %>

using System;
using System.Web;
using System.Configuration;
using LinYIMai_Model;
using System.Collections;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Data;

public class linyimaiapi : IHttpHandler
{

    protected HttpContext httpcontext;
    protected string Method;
    public void ProcessRequest(HttpContext context)
    {
        httpcontext = context;
        context.Response.ContentType = "text/plain";
        string Method = Request("Method").ToLower();
        switch (Method)
        {
            case "authorize":
                authorize();
                break;
            case "savemark":
                savemark();
                break;
            case "markcheck":
                markcheck();
                break;
            case "broadcast":
                broadcast();
                break;
            case "userlog":
                userlog();
                break;
            case "expertinfo":
                getExpertinfo();
                break;
            case "expertinfodetail":
                getExpertinfodetail();
                break;
            case "isenableinfo":
                isEnableInfo();
                break;
            default:
                Response("参数错误!");
                break;
        }
    }

    public void getExpertinfodetail()
    {
        string url = "https://linkmed.jnjadc.com/linkmed2020/expertinfo";
        string sqlWhere = "";
        string uid = Request("uid");
        if (Convert.ToInt32(uid) > 0)
            url += "?uid=" + uid;


        Hashtable header = new Hashtable();
        header["linkmed-token"] = "6CxkakZH";
        string reult = HttpHepler.Http(url, "POST", "application/x-www-form-urlencoded", header);
        Dictionary<string, ExpertInfo> dic = Levox.Common.JSONHelper.FromJson<Dictionary<string, ExpertInfo>>(reult);

        int i = 0;
        using (var db = new LinYIMai_Model.LinYiManEntities())
        {
            foreach (object item in dic.Keys)
            {
                i++;
                var mt = dic[item.ToString()];
                Type t = dic[item.ToString()].GetType();
                if (t.GetProperty("name").GetValue(mt) == null)
                {
                    continue;
                }
                string name = t.GetProperty("name").GetValue(mt).ToString();
                string hospital= t.GetProperty("hospital").GetValue(mt).ToString();

                DataTable dt = slf_DbHelper.ExecuteDataTable(CommandType.Text, "select * from yslist_UI where name=N'" + name + "' and yiyuan=N'" + hospital + "'", null);
                string headImg = "http://w.linkmed.cn/wx/imgs/default.png";
               // int num = Convert.ToInt32(db.Database.SqlQuery<int>("select count(1) from UserLog where doctorId=" + uid + " and OperationType=1").First());
                int clickNum = db.UserLog.Where(p => p.doctorId == Convert.ToInt32(uid) && p.OperationType == 1).Count();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["headImgUrl"].ToString() != "")
                        headImg = dt.Rows[0]["headImgUrl"].ToString();
                }

                t.GetProperty("headImg").SetValue(mt, headImg);
                t.GetProperty("clickNum").SetValue(mt, clickNum);
            }
        }

        if (i == 1)
        {
            Response(new Levox.Common.BoolMessage(false, reult).ToJson());
        }
        else
        {
            Response(new Levox.Common.BoolMessage(true, Levox.Common.JSONHelper.ToJson(dic)).ToJson());
        }
    }

    public void isEnableInfo()
    {
        Response(""); //https://apps.atline.cn/linyima/img.png
    }

    private void getExpertinfo()
    {
        string url = "https://linkmed.jnjadc.com/linkmed2020/expertinfo?";

        string ishot = Request("ishot");
        string page = Request("page");

        if (Convert.ToInt32(ishot) > 0)
            url += "ishot=" + ishot + "&";

        if (!string.IsNullOrEmpty(page))
        {
            url += "page=" + page + "&";
        }



        Hashtable header = new Hashtable();
        header["linkmed-token"] = "6CxkakZH";
        string reult = HttpHepler.Http(url, "POST", "application/x-www-form-urlencoded", header);
        Dictionary<string, ExpertInfo> dic = Levox.Common.JSONHelper.FromJson<Dictionary<string, ExpertInfo>>(reult);

        int i = 0;
        using (var db = new LinYIMai_Model.LinYiManEntities())
        {
            foreach (object item in dic.Keys)
            {
                i++;
                var mt = dic[item.ToString()];
                Type t = dic[item.ToString()].GetType();
                if (t.GetProperty("name").GetValue(mt) == null)
                {
                    continue;
                }
                string name = t.GetProperty("name").GetValue(mt).ToString();
                string hospital= t.GetProperty("hospital").GetValue(mt).ToString();

                DataTable dt = slf_DbHelper.ExecuteDataTable(CommandType.Text, "select * from yslist_UI where name=N'" + name + "' and yiyuan=N'" + hospital + "'", null);
                string headImg = "http://w.linkmed.cn/wx/imgs/default.png";

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["headImgUrl"].ToString() != "")
                        headImg = dt.Rows[0]["headImgUrl"].ToString();
                }

                t.GetProperty("headImg").SetValue(mt, headImg);

            }
        }

        if (i == 1)
        {
            Response(new Levox.Common.BoolMessage(false, reult).ToJson());
        }
        else
        {
            Response(new Levox.Common.BoolMessage(true, Levox.Common.JSONHelper.ToJson(dic)).ToJson());
        }

    }

    private void userlog()
    {
        try
        {
            string OpenId = Request("OpenId");
            string ClassType = Request("ClassType");
            int ClassId = Convert.ToInt32(Request("ClassId"));
            string ClassTitle = Request("ClassTitle");
            string ClassAuthor = Request("ClassAuthor");
            int OperationType = Convert.ToInt32(Request("OperationType"));
            int VideoPlayLong = Convert.ToInt32(Request("VideoPlayLong"));
            using (var db = new LinYIMai_Model.LinYiManEntities())
            {
                var model = new UserLog();
                model.OpenId = OpenId;
                model.ClassType = ClassType;
                model.ClassId = ClassId;
                model.ClassTitle = ClassTitle;
                model.ClassAuthor = ClassAuthor;
                model.OperationType = OperationType;
                model.VideoPlayLong = VideoPlayLong;
                db.UserLog.Add(model);
                db.SaveChanges();
            }
            Response(new Levox.Common.BoolMessage(true, "执行成功!").ToJson());
        }
        catch (Exception ex)
        {
            Response(new Levox.Common.BoolMessage(false, ex.ToString()).ToJson());
        }
    }

    private void authorize()
    {
        string key = Request("js_code");
        string url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", ConfigurationManager.AppSettings["AppId"], ConfigurationManager.AppSettings["secret"], key);
        string result = HttpHepler.Http(url);
        jscode2sessionModel model = Levox.Common.JSONHelper.FromJson<jscode2sessionModel>(result);
        Levox.Common.TxtLog.Write("authorize", "authorize", result);
        if (model.errcode == 0)
        {
            Response(new Levox.Common.BoolMessage(true, model.openid).ToJson());
        }
        else {
            Response(new Levox.Common.BoolMessage(false, "系统异常请稍后再试！").ToJson());
        }
    }

    private void markcheck()
    {
        try
        {
            string openid = Request("openid");
            int classid = Convert.ToInt32(Request("classid"));
            using (var db = new LinYIMai_Model.LinYiManEntities())
            {
                var model = db.MarkClass.Where(p => p.OpenID == openid && p.ClassID == classid).FirstOrDefault();
                int count = db.MarkClass.Where(p => p.ClassID == classid).Count(); ;
                if (model == null)
                {
                    Response(new Levox.Common.BoolMessage(false, "未预约!", count.ToString()).ToJson());
                }
                else {
                    Response(new Levox.Common.BoolMessage(true, "已预约!", count.ToString()).ToJson());
                }
            }
        }
        catch (Exception ex)
        {
            Response(new Levox.Common.BoolMessage(false, "系统异常请稍后再试！").ToJson());
        }
    }

    private void savemark()
    {
        try
        {
            string openid = Request("openid");
            int classid = Convert.ToInt32(Request("classid"));
            using (var db = new LinYIMai_Model.LinYiManEntities())
            {
                var model = new LinYIMai_Model.MarkClass();
                model.OpenID = openid;
                model.ClassID = classid;
                model.CreateTime = DateTime.Now;
                db.MarkClass.Add(model);
                db.SaveChanges();
            }
            Response(new Levox.Common.BoolMessage(true, "执行成功!").ToJson());
        }
        catch (Exception ex)
        {
            Response(new Levox.Common.BoolMessage(false, "系统异常请稍后再试！").ToJson());
        }
    }

    public void broadcast()
    {
        string category = Request("category");
        string openid = Request("openid");
        string tags = Request("tags");
        string ishot = Request("ishot");
        string date = Request("date");
        string url = "https://linkmed.jnjadc.com/linkmed2020/broadcast?";
        if (!string.IsNullOrEmpty(category))
        {
            url += "category=" + category + "&";
        }
        if (!string.IsNullOrEmpty(openid))
        {
            url += "openid=" + openid + "&";
        }
        if (!string.IsNullOrEmpty(tags))
        {
            url += "tags=" + tags + "&";
        }
        if (!string.IsNullOrEmpty(ishot))
        {
            url += "ishot=" + ishot + "&";
        }
        if (!string.IsNullOrEmpty(date))
        {
            url += "date=" + date + "&";
        }
        Hashtable header = new Hashtable();
        header["linkmed-token"] = "6CxkakZH";
        string reult = HttpHepler.Http(url, "POST", "application/x-www-form-urlencoded", header);
        Dictionary<string, classInfo> dic = Levox.Common.JSONHelper.FromJson<Dictionary<string, classInfo>>(reult);
        string str = "";
        int i = 0;
        using (var db = new LinYIMai_Model.LinYiManEntities())
        {
            foreach (object item in dic.Keys)
            {
                i++;
                var mt = dic[item.ToString()];
                Type t = dic[item.ToString()].GetType();
                if (t.GetProperty("id").GetValue(mt) == null)
                {
                    continue;
                }
                string cId = t.GetProperty("id").GetValue(mt).ToString();
                int classId = Convert.ToInt32(cId);
                var model = db.MarkClass.Where(p => p.OpenID == openid && p.ClassID == classId).FirstOrDefault();
                if (model == null)
                {
                    t.GetProperty("IsMark").SetValue(mt, false);
                }
                else {
                    t.GetProperty("IsMark").SetValue(mt, true);
                }

            }
        }
        if (i == 1)
        {
            Response(new Levox.Common.BoolMessage(false, "/Date(" + ConvertDateTimeToInt(DateTime.Now).ToString() + ")/").ToJson());
        }
        else {
            Response(new Levox.Common.BoolMessage(true, Levox.Common.JSONHelper.ToJson(dic)).ToJson());
        }

    }

    public static long ConvertDateTimeToInt(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
        return t;
    }

    private void Response(string OutValue)
    {
        httpcontext.Response.Write(OutValue);
    }

    private string Request(string Key)
    {
        if (httpcontext.Request[Key] != null)
        {
            string str = httpcontext.Server.UrlDecode(httpcontext.Request[Key].ToString());
            return System.Text.RegularExpressions.Regex.Unescape(str);
        }
        else
        {
            return "";
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}