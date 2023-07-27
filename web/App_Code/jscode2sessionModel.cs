using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// jscode2sessionModel 的摘要说明
/// </summary>
public class jscode2sessionModel
{
    public jscode2sessionModel()
    {     
    }

    public string openid { get; set; }
    public string session_key { get; set; }
    public string unionid { get; set; }
    public int errcode { get; set; }
    public string errmsg { get; set; }
}