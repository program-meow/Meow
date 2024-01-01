using Meow.Extension;
using Meow.Http;

namespace Meow.Helper;

/// <summary>
/// Web操作
/// </summary>
public static class Web {

    #region HttpContextAccessor  [Http上下文访问器]

    /// <summary>
    /// Http上下文访问器
    /// </summary>
    public static IHttpContextAccessor HttpContextAccessor { get; set; }

    #endregion

    #region HttpContext  [Http上下文]

    /// <summary>
    /// 当前Http上下文
    /// </summary>
    public static HttpContext HttpContext => HttpContextAccessor?.HttpContext;

    #endregion

    #region ServiceProvider  [服务提供器]

    /// <summary>
    /// 当前Http请求服务提供器
    /// </summary>
    public static IServiceProvider ServiceProvider => HttpContext?.RequestServices;

    #endregion

    #region Request  [Http请求]

    /// <summary>
    /// 当前Http请求
    /// </summary>
    public static HttpRequest Request => HttpContext?.Request;

    #endregion

    #region Response  [Http响应]

    /// <summary>
    /// 当前Http响应
    /// </summary>
    public static HttpResponse Response => HttpContext?.Response;

    #endregion

    #region Body  [请求正文]

    /// <summary>
    /// 请求正文
    /// </summary>
    public static byte[] Body {
        get {
            Request.EnableBuffering();
            return File.ReadToBytes( Request.Body );
        }
    }

    #endregion

    #region GetBodyAsync  [获取请求正文]

    /// <summary>
    /// 获取请求正文
    /// </summary>
    public static async Task<byte[]> GetBodyAsync() {
        Request.EnableBuffering();
        return await File.ReadToBytesAsync( Request.Body );
    }

    #endregion

    #region Environment  [主机环境]

    /// <summary>
    /// 主机环境
    /// </summary>
    public static IWebHostEnvironment Environment => ServiceProvider?.GetService<IWebHostEnvironment>();

    #endregion

    #region HttpClient  [Http客户端]

    /// <summary>
    /// Http客户端
    /// </summary>
    public static IHttpClient HttpClient {
        get {
            try {
                return Ioc.Create<IHttpClient>();
            } catch {
                return new HttpClientService().SetHttpClient( new HttpClient() );
            }
        }
    }

    #endregion

    #region GetPhysicalPath  [获取物理路径]

    /// <summary>
    /// 获取物理路径,基路径为IWebHostEnvironment.ContentRootPath
    /// </summary>
    /// <param name="relativePath">相对路径,范例:"test/a.txt" 或 "/test/a.txt"</param>
    public static string GetPhysicalPath( string relativePath ) {
        return Meow.Helper.Program.GetPhysicalPath( relativePath , Environment.ContentRootPath );
    }

    #endregion

    #region GetFiles  [获取客户端文件集合]

    /// <summary>
    /// 获取客户端文件集合
    /// </summary>
    public static List<IFormFile> GetFiles() {
        List<IFormFile> result = new List<IFormFile>();
        IFormFileCollection files = Request.Form.Files;
        if( files.Count == 0 )
            return result;
        result.AddRange( files.Where( file => file?.Length > 0 ) );
        return result;
    }

    #endregion

    #region GetFile  [获取客户端文件]

    /// <summary>
    /// 获取客户端文件
    /// </summary>
    public static IFormFile GetFile() {
        List<IFormFile> files = GetFiles();
        return files.Count == 0 ? null : files[ 0 ];
    }

    #endregion

    #region GetParam  [获取请求参数]

    /// <summary>
    /// 获取请求参数，搜索路径：查询参数->表单参数->请求头
    /// </summary>
    /// <param name="name">参数名</param>
    public static string GetParam( string name ) {
        if( name.IsEmpty() )
            return string.Empty;
        if( Request == null )
            return string.Empty;
        string result = Request.Query[ name ];
        if( result.IsEmpty() == false )
            return result;
        result = Request.Form[ name ];
        if( result.IsEmpty() == false )
            return result;
        return Request.Headers[ name ];
    }

    #endregion

    #region Url  [请求地址]

    /// <summary>
    /// 请求地址
    /// </summary>
    public static string Url => Request?.GetDisplayUrl();

    #endregion

    #region DownloadAsync  [下载]

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="fileName">文件名,包含扩展名</param>
    public static async Task DownloadFileAsync( string filePath , string fileName ) {
        await DownloadFileAsync( filePath , fileName , Encoding.UTF8 );
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="fileName">文件名,包含扩展名</param>
    /// <param name="encoding">字符编码</param>
    public static async Task DownloadFileAsync( string filePath , string fileName , Encoding encoding ) {
        byte[] bytes = File.ReadToBytes( filePath );
        await DownloadAsync( bytes , fileName , encoding );
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="fileName">文件名,包含扩展名</param>
    public static async Task DownloadAsync( SystemStream stream , string fileName ) {
        await DownloadAsync( stream , fileName , Encoding.UTF8 );
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="fileName">文件名,包含扩展名</param>
    /// <param name="encoding">字符编码</param>
    public static async Task DownloadAsync( SystemStream stream , string fileName , Encoding encoding ) {
        byte[] bytes = await Meow.Helper.Stream.ToBytesAsync( stream );
        await DownloadAsync( bytes , fileName , encoding );
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="bytes">字节流</param>
    /// <param name="fileName">文件名,包含扩展名</param>
    public static async Task DownloadAsync( byte[] bytes , string fileName ) {
        await DownloadAsync( bytes , fileName , Encoding.UTF8 );
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="bytes">字节流</param>
    /// <param name="fileName">文件名,包含扩展名</param>
    /// <param name="encoding">字符编码</param>
    public static async Task DownloadAsync( byte[] bytes , string fileName , Encoding encoding ) {
        if( bytes == null || bytes.Length == 0 )
            return;
        fileName = fileName.Replace( " " , "" );
        fileName = Meow.Helper.Url.Encode( fileName , encoding );
        Response.ContentType = "application/octet-stream";
        Response.Headers.Add( "Content-Disposition" , $"attachment; filename={fileName}" );
        Response.Headers.Add( "Content-Length" , bytes.Length.ToString() );
        await Response.Body.WriteAsync( bytes , 0 , bytes.Length );
    }
    #endregion

    #region GetCookie  [获取Cookie]

    /// <summary>
    /// 获取Cookie
    /// </summary>
    /// <param name="key">cookie键名</param>
    public static string GetCookie( string key ) {
        return Request?.Cookies[ key ];
    }

    #endregion

    #region SetCookie  [设置Cookie]

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="key">cookie键名</param>
    /// <param name="value">值</param>
    public static void SetCookie( string key , string value ) {
        Response?.Cookies.Append( key , value );
    }

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="key">cookie键名</param>
    /// <param name="value">值</param>
    /// <param name="options">Cookie配置</param>
    public static void SetCookie( string key , string value , CookieOptions options ) {
        Response?.Cookies.Append( key , value , options );
    }

    #endregion

    #region RemoveCookie  [移除Cookie]

    /// <summary>
    /// 移除Cookie
    /// </summary>
    /// <param name="key">cookie键名</param>
    public static void RemoveCookie( string key ) {
        Response?.Cookies.Delete( key );
    }

    /// <summary>
    /// 移除Cookie
    /// </summary>
    /// <param name="key">cookie键名</param>
    /// <param name="options">Cookie配置</param>
    public static void RemoveCookie( string key , CookieOptions options ) {
        Response?.Cookies.Delete( key , options );
    }

    #endregion

}