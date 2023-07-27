using System;
using System.Collections.Generic;

public class Broadcast
{
    public List<classInfo> classList { get; set; }
}
public class classInfo
{
    public string id { get; set; }
    /// <summary>
    /// Ethicon Live四川大学华西医院泌尿外科手术网络直播
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime startTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime endTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Thumbnail thumbnail { get; set; }
    /// <summary>
    /// 网络直播
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string content { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string link { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string ishot { get; set; }
    /// <summary>
    /// 泌尿科
    /// </summary>
    public string category { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime requestTime { get; set; }

    public bool IsMark { get; set; }
}

public class Thumbnail
{
    public string src { get; set; }
    public string alt { get; set; }
    public string title { get; set; }
}