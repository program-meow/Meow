using Meow.Authentication;
using Meow.Response;
using SystemException = System.Exception;

namespace Meow.Http;

/// <summary>
/// Http请求
/// </summary>
public interface IHttpRequest {
}

/// <summary>
/// Http请求
/// </summary>
/// <typeparam name="TResult">结果类型</typeparam>
public interface IHttpRequest<TResult> : IHttpRequest where TResult : class {
    #region 设置属性

    #region 配置

    #region Encoding  [设置字符编码]

    /// <summary>
    /// 设置字符编码
    /// </summary>
    /// <param name="encoding">字符编码,范例：gb2312</param>
    IHttpRequest<TResult> Encoding( string encoding );
    /// <summary>
    /// 设置字符编码
    /// </summary>
    /// <param name="encoding">字符编码</param>
    IHttpRequest<TResult> Encoding( Encoding encoding );

    #endregion

    #region ContentType  [设置内容类型]

    /// <summary>
    /// 设置内容类型
    /// </summary>
    /// <param name="contentType">内容类型</param>
    IHttpRequest<TResult> ContentType( HttpContentTypeEnum contentType );
    /// <summary>
    /// 设置内容类型
    /// </summary>
    /// <param name="contentType">内容类型</param>
    IHttpRequest<TResult> ContentType( string contentType );

    #endregion

    #region Timeout  [设置超时时间]

    /// <summary>
    /// 设置超时时间
    /// </summary>
    /// <param name="second">秒</param>
    IHttpRequest<TResult> Timeout( int second );

    /// <summary>
    /// 设置超时时间
    /// </summary>
    /// <param name="time">超时时间</param>
    IHttpRequest<TResult> Timeout( TimeSpan time );

    #endregion

    #region Certificate  [设置证书]

    /// <summary>
    /// 设置证书
    /// </summary>
    /// <param name="path">证书路径</param>
    /// <param name="password">证书密码</param>
    IHttpRequest<TResult> Certificate( string path , string password );

    #endregion

    #region JsonSerializerOptions  [设置Json序列化配置]

    /// <summary>
    /// 设置Json序列化配置
    /// </summary>
    /// <param name="options">Json序列化配置</param>
    IHttpRequest<TResult> JsonSerializerOptions( JsonSerializerOptions options );

    #endregion

    #region UseAnalyzingParam  [使用全解析参数]

    /// <summary>
    /// 使用全解析参数
    /// </summary>
    IHttpRequest<TResult> UseAnalyzingParam();

    #endregion

    #endregion

    #region 重试

    #region RetryTimes  [设置失败重试次数]

    /// <summary>
    /// 设置失败重试次数
    /// </summary>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxTimes">最大重试次数。第一次失败后，再次尝试重新发起请求的次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    IHttpRequest<TResult> RetryTimes( Func<TResult , bool> validateResultFunc = null , int maxTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null );

    #endregion

    #endregion

    #region 凭证

    /// <summary>
    /// 设置身份验证的凭证
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="scheme">授权方案</param>
    IHttpRequest<TResult> Authorization( string token , AuthenticationSchemeEnum scheme = AuthenticationSchemeEnum.Basic );
    /// <summary>
    /// 设置身份验证的凭证
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="scheme">授权方案</param>
    IHttpRequest<TResult> Authorization( string token , string scheme = "Basic" );
    /// <summary>
    /// 设置租户凭证
    /// </summary>
    /// <param name="tenant">租户凭证</param>
    /// <param name="key">键</param>
    IHttpRequest<TResult> Tenant( string tenant , string key = "Tenant" );

    #endregion

    #region Cookie

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="value">值</param>
    /// <param name="url">地址</param>
    IHttpRequest<TResult> Cookie( string name , string value , string url = "" );
    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="cookie">cookie</param>
    /// <param name="url">地址</param>
    IHttpRequest<TResult> Cookie( Cookie cookie , string url = "" );
    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="cookies">cookie键值对</param>
    /// <param name="url">地址</param>
    IHttpRequest<TResult> Cookie( IDictionary<string , string> cookies , string url = "" );
    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="cookies">cookie容器</param>
    /// <param name="url">地址</param>
    IHttpRequest<TResult> Cookie( CookieCollection cookies , string url = "" );

    #endregion

    #region Header  [设置请求头]

    /// <summary>
    /// 设置标准请求头
    /// </summary>
    /// <param name="header">接受的内容类型</param>
    IHttpRequest<TResult> HeaderByStandard( HttpStandardHeader header );
    /// <summary>
    /// 设置自定义请求头
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    IHttpRequest<TResult> HeaderByCustom( string key , string value );
    /// <summary>
    /// 设置自定义请求头
    /// </summary>
    /// <param name="headers">请求头键值对集合</param>
    IHttpRequest<TResult> HeaderByCustom( IDictionary<string , string> headers );
    /// <summary>
    /// 设置自定义请求头
    /// </summary>
    /// <param name="header">请求头对象</param>
    IHttpRequest<TResult> HeaderByCustom( object header );

    #endregion

    #endregion

    #region 设置参数

    #region Query  [设置查询字符串]

    /// <summary>
    /// 设置查询字符串,即url中?后面的参数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    IHttpRequest<TResult> Query( string key , object value );
    /// <summary>
    /// 设置查询字符串,即url中?后面的参数
    /// </summary>
    /// <param name="query">查询字符串键值对集合</param>
    IHttpRequest<TResult> Query( IDictionary<string , object> query );
    /// <summary>
    /// 设置查询字符串,即url中?后面的参数
    /// </summary>
    /// <param name="query">查询字符串对象</param>
    IHttpRequest<TResult> Query( object query );

    #endregion

    #region Content  [添加内容参数]

    /// <summary>
    /// 添加参数,作为请求内容发送
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    IHttpRequest<TResult> Content( string key , object value );
    /// <summary>
    /// 添加参数,作为请求内容发送
    /// </summary>
    /// <param name="parameters">参数字典</param>
    IHttpRequest<TResult> Content( IDictionary<string , object> parameters );
    /// <summary>
    /// 添加参数,作为请求内容发送
    /// </summary>
    /// <param name="value">值</param>
    IHttpRequest<TResult> Content( object value );

    #endregion

    #region ContentByJson  [添加Json参数]

    /// <summary>
    /// 添加内容类型为 application/json 的参数
    /// </summary>
    /// <param name="value">值</param>
    IHttpRequest<TResult> ContentByJson( object value );

    #endregion

    #region ContentByXml  [添加Xml参数]

    /// <summary>
    /// 添加内容类型为 application/xml 的参数
    /// </summary>
    /// <param name="value">值</param>
    IHttpRequest<TResult> ContentByXml( string value );

    #endregion

    #region ContentByFile  [添加文件参数]

    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="fileKey">文件键</param>
    /// <param name="formFields">表单字段</param>
    IHttpRequest<TResult> ContentByFile( string filePath , string fileKey = "file" , IDictionary<string , string> formFields = null );
    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="fileBytes">文件流</param>
    /// <param name="fileKey">文件键</param>
    /// <param name="formFields">表单字段</param>
    IHttpRequest<TResult> ContentByFile( string fileName , byte[] fileBytes , string fileKey = "file" , IDictionary<string , string> formFields = null );
    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="filePaths">文件路径集合。文件key自动赋不重复值</param>
    /// <param name="formFields">表单字段</param>
    IHttpRequest<TResult> ContentByFile( IEnumerable<string> filePaths , IDictionary<string , string> formFields = null );
    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="filePaths">文件路径集合：key为文件路径，value为文件key（key可为null,若为null则自动赋不重复值）</param>
    /// <param name="formFields">表单字段</param>
    IHttpRequest<TResult> ContentByFile( IDictionary<string , string> filePaths , IDictionary<string , string> formFields = null );
    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="files">文件集合：第一个参数为文件名；第二参数为文件流，第三个参数为文件key（key可为null,若为null则自动赋不重复值）</param>
    /// <param name="formFields">表单字段</param>
    IHttpRequest<TResult> ContentByFile( IEnumerable<(string, byte[], string)> files , IDictionary<string , string> formFields = null );

    #endregion

    #endregion

    #region 设置方法

    #region OnSendBefore & OnSendAfter  [发送前/后事件]

    /// <summary>
    /// 发送前事件
    /// </summary>
    /// <param name="action">发送前操作,返回false取消发送</param>
    IHttpRequest<TResult> OnSendBefore( Func<HttpRequestMessage , bool> action );

    /// <summary>
    /// 发送后事件
    /// </summary>
    /// <param name="action">发送后操作,自定义解析返回值</param>
    IHttpRequest<TResult> OnSendAfter( Func<HttpResponseMessage , Task<TResult>> action );


    #endregion

    #region OnConvert  [结果转换事件]

    /// <summary>
    /// 结果转换事件
    /// </summary>
    /// <param name="action">结果转换操作,参数为响应内容</param>
    IHttpRequest<TResult> OnConvert( Func<string , TResult> action );

    #endregion

    #region OnSuccess  & OnFail  [请求成功/失败事件]

    /// <summary>
    /// 请求成功事件
    /// </summary>
    /// <param name="action">执行成功操作,参数为响应结果</param>
    IHttpRequest<TResult> OnSuccess( Action<TResult> action );
    /// <summary>
    /// 请求失败事件
    /// </summary>
    /// <param name="action">执行失败操作,参数为响应消息和响应内容</param>
    IHttpRequest<TResult> OnFail( Action<HttpResponseMessage , object> action );

    #endregion

    #region OnComplete  [请求完成事件]

    /// <summary>
    /// 请求完成事件,不论成功失败都会执行
    /// </summary>
    /// <param name="action">执行完成操作,参数为响应消息和响应内容</param>
    IHttpRequest<TResult> OnComplete( Action<HttpResponseMessage , object> action );

    #endregion

    #endregion

    #region 获取结果

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    Task<Result<TResult>> GetResultAsync( Action<SystemException> listenerExceptionFunc = null );
    /// <summary>
    /// 获取流
    /// </summary>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    Task<Result<byte[]>> GetStreamAsync( Action<SystemException> listenerExceptionFunc = null );
    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    Task<Result> WriteAsync( string filePath , Action<SystemException> listenerExceptionFunc = null );

    #endregion
}