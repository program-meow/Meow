using Meow.Authentication;
using Meow.Extension;
using Meow.Http.Extension;
using Meow.Response;

namespace Meow.Http;

/// <summary>
/// Http请求
/// </summary>
/// <typeparam name="TResult">结果类型</typeparam>
public class HttpRequest<TResult> : IHttpRequest<TResult> where TResult : class {

    #region 属性

    #region 核心

    /// <summary>
    /// Http客户端工厂
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;
    /// <summary>
    /// Http客户端
    /// </summary>
    private readonly HttpClient _httpClient;
    /// <summary>
    /// Http客户端名称
    /// </summary>
    private string _httpClientName;
    /// <summary>
    /// Http方法
    /// </summary>
    private readonly HttpMethod _httpMethod;
    /// <summary>
    /// 服务地址
    /// </summary>
    private readonly string _url;

    #endregion

    #region 配置

    /// <summary>
    /// 字符编码
    /// </summary>
    protected Encoding CharacterEncoding { get; private set; }
    /// <summary>
    /// 内容类型
    /// </summary>
    protected string HttpContentType { get; private set; }
    /// <summary>
    /// 超时时间参数
    /// </summary>
    protected TimeSpan TimeoutParam { get; private set; }
    /// <summary>
    /// 证书参数
    /// </summary>
    protected (string, string) CertificateParam { get; private set; }
    /// <summary>
    /// Json序列化配置
    /// </summary>
    private JsonSerializerOptions _jsonSerializerOptions;
    /// <summary>
    /// 是否忽略SSL证书
    /// </summary>
    private bool _ignoreSsl;
    /// <summary>
    /// 是否简单解析参数
    /// </summary>
    private bool _isEasyAnalyzingParam;

    #endregion

    #region 重试

    /// <summary>
    /// 重试校验结果方法
    /// </summary>
    private Func<TResult , bool> _retryValidateResultFunc;
    /// <summary>
    /// 重试最大次数
    /// </summary>

    private int _retryMaxTimes;
    /// <summary>
    /// 重试监听异常方法
    /// </summary>
    private Action<int , TimeSpan , SystemException> _retryListenerExceptionFunc;
    /// <summary>
    /// 重试设置延迟时间方法
    /// </summary>
    private Func<int , TimeSpan> _retryDelayFunc;

    #endregion

    #region 凭证

    /// <summary>
    /// 身份验证的凭证
    /// </summary>
    protected AuthenticationHeaderValue AuthorizationParam { get; private set; }
    /// <summary>
    /// 租户凭证
    /// </summary>
    protected (string, string) TenantParam { get; private set; }

    #endregion

    #region Cookie

    /// <summary>
    /// Cookie容器
    /// </summary>
    protected CookieContainer CookieContainer { get; private set; }

    #endregion

    #region 请求头

    /// <summary>
    /// 标准请求头参数
    /// </summary>
    protected HttpStandardHeader HeaderStandardParam { get; private set; }

    /// <summary>
    /// 自定义请求头参数集合
    /// </summary>
    protected IDictionary<string , string> HeaderCustomParams { get; private set; }

    #endregion

    #region 参数

    /// <summary>
    /// 查询字符串参数集合
    /// </summary>
    protected IDictionary<string , object> QueryParams { get; private set; }
    /// <summary>
    /// 内容参数集合
    /// </summary>
    protected IDictionary<string , object> ContentParams { get; private set; }
    /// <summary>
    /// 内容对象
    /// </summary>
    protected object ContentObject { get; private set; }
    /// <summary>
    /// 文件对象集合
    /// </summary>
    protected List<(string, byte[], string)> FileObjects { get; private set; }
    /// <summary>
    /// 文件表单参数
    /// </summary>
    protected IDictionary<string , string> FileFormFields { get; private set; }

    #endregion

    #region 方法

    /// <summary>
    /// 发送前操作
    /// </summary>
    protected Func<HttpRequestMessage , bool> SendBeforeAction { get; private set; }
    /// <summary>
    /// 发送后操作
    /// </summary>
    protected Func<HttpResponseMessage , Task<TResult>> SendAfterAction { get; private set; }
    /// <summary>
    /// 结果转换操作
    /// </summary>
    protected Func<string , TResult> ConvertAction { get; private set; }
    /// <summary>
    /// 执行成功操作
    /// </summary>
    protected Action<TResult> SuccessAction { get; private set; }
    /// <summary>
    /// 执行失败操作
    /// </summary>
    protected Action<HttpResponseMessage , object> FailAction { get; private set; }
    /// <summary>
    /// 执行完成操作
    /// </summary>
    protected Action<HttpResponseMessage , object> CompleteAction { get; private set; }

    #endregion

    #endregion

    #region 构造方法

    /// <summary>
    /// 初始化Http请求
    /// </summary>
    /// <param name="httpClientFactory">Http客户端工厂</param>
    /// <param name="httpClient">Http客户端处理器</param>
    /// <param name="httpMethod">Http方法</param>
    /// <param name="url">服务地址</param>
    public HttpRequest( IHttpClientFactory httpClientFactory , HttpClient httpClient , HttpMethod httpMethod , string url ) {
        if( httpClientFactory == null && httpClient == null )
            throw new ArgumentNullException( nameof( httpClientFactory ) );
        if( url.IsEmpty() )
            throw new ArgumentNullException( nameof( url ) );

        #region 核心

        _httpClientFactory = httpClientFactory;
        _httpClient = httpClient;
        _httpMethod = httpMethod;
        _url = url;

        #endregion

        #region 配置

        CharacterEncoding = System.Text.Encoding.UTF8;
        HttpContentType = HttpContentTypeEnum.Json.GetDescription();
        TimeoutParam = new TimeSpan( 0 , 0 , 0 , 60 );
        CertificateParam = (null, null);
        _jsonSerializerOptions = null;
        _isEasyAnalyzingParam = true;

        #endregion

        #region 重试

        _retryValidateResultFunc = null;
        _retryMaxTimes = 0;
        _retryListenerExceptionFunc = null;
        _retryDelayFunc = null;

        #endregion

        #region 凭证

        AuthorizationParam = null;
        TenantParam = (null, null);

        #endregion

        #region Cookie

        CookieContainer = new CookieContainer();

        #endregion

        #region 请求头

        HeaderStandardParam = null;
        HeaderCustomParams = new Dictionary<string , string>();

        #endregion

        #region 参数

        QueryParams = new Dictionary<string , object>();
        ContentParams = new Dictionary<string , object>();
        ContentObject = null;
        FileObjects = new List<(string, byte[], string)>();
        FileFormFields = new Dictionary<string , string>();

        #endregion

        #region 方法

        SendBeforeAction = null;
        SendAfterAction = null;
        ConvertAction = null;
        SuccessAction = null;
        FailAction = null;
        CompleteAction = null;

        #endregion
    }

    #endregion

    #region 设置属性

    #region 配置

    #region HttpClientName  [设置Http客户端名称]

    /// <inheritdoc />
    public IHttpRequest<TResult> HttpClientName( string name ) {
        _httpClientName = name;
        return this;
    }

    #endregion

    #region Encoding  [设置字符编码]

    /// <summary>
    /// 设置字符编码
    /// </summary>
    /// <param name="encoding">字符编码,范例：gb2312</param>
    public IHttpRequest<TResult> Encoding( string encoding ) {
        return Encoding( System.Text.Encoding.GetEncoding( encoding ) );
    }

    /// <summary>
    /// 设置字符编码
    /// </summary>
    /// <param name="encoding">字符编码</param>
    public IHttpRequest<TResult> Encoding( Encoding encoding ) {
        CharacterEncoding = encoding;
        return this;
    }

    #endregion

    #region ContentType  [设置内容类型]

    /// <summary>
    /// 设置内容类型
    /// </summary>
    /// <param name="contentType">内容类型</param>
    public IHttpRequest<TResult> ContentType( HttpContentTypeEnum contentType ) {
        return ContentType( contentType.GetDescription() );
    }

    /// <summary>
    /// 设置内容类型
    /// </summary>
    /// <param name="contentType">内容类型</param>
    public IHttpRequest<TResult> ContentType( string contentType ) {
        HttpContentType = contentType;
        return this;
    }

    #endregion

    #region Timeout  [设置超时时间]

    /// <summary>
    /// 设置超时时间
    /// </summary>
    /// <param name="second">秒</param>
    public IHttpRequest<TResult> Timeout( int second ) {
        return Timeout( second.GetTimeSpanBySecond() );
    }

    /// <summary>
    /// 设置超时时间
    /// </summary>
    /// <param name="time">超时时间</param>
    public IHttpRequest<TResult> Timeout( TimeSpan time ) {
        TimeoutParam = time;
        return this;
    }

    #endregion

    #region Certificate  [设置证书]

    /// <summary>
    /// 设置证书
    /// </summary>
    /// <param name="path">证书路径</param>
    /// <param name="password">证书密码</param>
    public IHttpRequest<TResult> Certificate( string path , string password ) {
        CertificateParam = (path, password);
        return this;
    }

    #endregion

    #region JsonSerializerOptions  [设置Json序列化配置]

    /// <summary>
    /// 设置Json序列化配置
    /// </summary>
    /// <param name="options">Json序列化配置</param>
    public IHttpRequest<TResult> JsonSerializerOptions( JsonSerializerOptions options ) {
        _jsonSerializerOptions = options;
        return this;
    }

    #endregion

    #region IgnoreSsl  [是否忽略SSL证书]

    /// <inheritdoc />
    public IHttpRequest<TResult> IgnoreSsl() {
        _ignoreSsl = true;
        return this;
    }

    #endregion

    #region UseAnalyzingParam  [使用全解析参数]

    /// <summary>
    /// 使用全解析参数
    /// </summary>
    public IHttpRequest<TResult> UseAnalyzingParam() {
        _isEasyAnalyzingParam = false;
        return this;
    }

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
    public IHttpRequest<TResult> RetryTimes( Func<TResult , bool> validateResultFunc = null , int maxTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        if( maxTimes < 0 )
            return this;
        _retryValidateResultFunc = validateResultFunc;
        _retryMaxTimes = maxTimes;
        _retryListenerExceptionFunc = listenerExceptionFunc;
        _retryDelayFunc = delayFunc;
        return this;
    }

    #endregion

    #endregion

    #region 凭证

    /// <summary>
    /// 设置身份验证的凭证
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="scheme">授权方案</param>
    public IHttpRequest<TResult> Authorization( string token , AuthenticationSchemeEnum scheme = AuthenticationSchemeEnum.Basic ) {
        return Authorization( token , scheme.GetDescription() );
    }

    /// <summary>
    /// 设置身份验证的凭证
    /// </summary>
    /// <param name="token">令牌</param>
    /// <param name="scheme">授权方案</param>
    public IHttpRequest<TResult> Authorization( string token , string scheme = "Basic" ) {
        if( token.IsEmpty() )
            return this;
        AuthorizationParam = new AuthenticationHeaderValue( scheme , token );
        return this;
    }

    /// <summary>
    /// 设置租户凭证
    /// </summary>
    /// <param name="tenant">租户凭证</param>
    /// <param name="key">键</param>
    public IHttpRequest<TResult> Tenant( string tenant , string key = "Tenant" ) {
        if( tenant.IsEmpty() )
            return this;
        TenantParam = (tenant, key);
        return this;
    }

    #endregion

    #region Cookie

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="value">值</param>
    /// <param name="url">地址</param>
    public IHttpRequest<TResult> Cookie( string name , string value , string url = "" ) {
        if( name.IsEmpty() || value.IsEmpty() )
            return this;
        return Cookie( new Cookie( name , value ) , url );
    }

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="cookie">cookie</param>
    /// <param name="url">地址</param>
    public IHttpRequest<TResult> Cookie( Cookie cookie , string url = "" ) {
        if( cookie == null )
            return this;
        if( cookie.Name.IsEmpty() || cookie.Value.IsEmpty() )
            return this;
        if( url.IsEmpty() )
            CookieContainer.Add( cookie );
        else
            CookieContainer.Add( new Uri( url ) , cookie );
        return this;
    }

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="cookies">cookie键值对</param>
    /// <param name="url">地址</param>
    public IHttpRequest<TResult> Cookie( IDictionary<string , string> cookies , string url = "" ) {
        if( cookies.IsEmpty() )
            return this;
        CookieCollection cookieCollection = new CookieCollection();
        foreach( KeyValuePair<string , string> each in cookies ) {
            if( each.Key.IsEmpty() || each.Value.IsEmpty() )
                continue;
            cookieCollection.Add( new Cookie( each.Key , each.Value ) );
        }
        return Cookie( cookieCollection , url );
    }

    /// <summary>
    /// 设置Cookie
    /// </summary>
    /// <param name="cookies">cookie容器</param>
    /// <param name="url">地址</param>
    public IHttpRequest<TResult> Cookie( CookieCollection cookies , string url = "" ) {
        if( cookies.IsEmpty() )
            return this;
        if( url.IsEmpty() )
            CookieContainer.Add( cookies );
        else
            CookieContainer.Add( new Uri( url ) , cookies );
        return this;
    }

    #endregion

    #region Header  [设置请求头]

    /// <summary>
    /// 设置标准请求头
    /// </summary>
    /// <param name="header">接受的内容类型</param>
    public IHttpRequest<TResult> HeaderByStandard( HttpStandardHeader header ) {
        if( header == null )
            return this;
        HeaderStandardParam = header;
        return this;
    }

    /// <summary>
    /// 设置自定义请求头
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public IHttpRequest<TResult> HeaderByCustom( string key , string value ) {
        if( key.IsEmpty() )
            return this;
        if( value.IsEmpty() ) {
            HeaderCustomParams.Remove( key );
            return this;
        }
        if( HeaderCustomParams.ContainsKey( key ) )
            HeaderCustomParams.Remove( key );
        HeaderCustomParams.Add( key , value );
        return this;
    }

    /// <summary>
    /// 设置自定义请求头
    /// </summary>
    /// <param name="headers">请求头键值对集合</param>
    public IHttpRequest<TResult> HeaderByCustom( IDictionary<string , string> headers ) {
        headers = headers.RemoveEmpty();
        if( headers.IsEmpty() )
            return this;
        foreach( KeyValuePair<string , string> header in headers )
            HeaderByCustom( header.Key , header.Value );
        return this;
    }

    /// <summary>
    /// 设置自定义请求头
    /// </summary>
    /// <param name="header">请求头对象</param>
    public IHttpRequest<TResult> HeaderByCustom( object header ) {
        IDictionary<string , string> dictionary = header.ToDictionary<string>();
        dictionary = dictionary.RemoveEmpty();
        if( dictionary.IsEmpty() )
            return this;
        return HeaderByCustom( dictionary );
    }

    #endregion

    #endregion

    #region 设置参数

    #region Query  [设置查询字符串]

    /// <summary>
    /// 设置查询字符串,即url中?后面的参数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public IHttpRequest<TResult> Query( string key , object value ) {
        QueryParams.AddNotNull( key , value );
        return this;
    }

    /// <summary>
    /// 设置查询字符串,即url中?后面的参数
    /// </summary>
    /// <param name="query">查询字符串键值对集合</param>
    public IHttpRequest<TResult> Query( IDictionary<string , object> query ) {
        QueryParams.AddRangeNotNull( query );
        return this;
    }

    /// <summary>
    /// 设置查询字符串,即url中?后面的参数
    /// </summary>
    /// <param name="query">查询字符串对象</param>
    public IHttpRequest<TResult> Query( object query ) {
        IDictionary<string , object> dictionary = _isEasyAnalyzingParam ? query.ToDictionary() : query.AnalyzingToDictionary();
        QueryParams.AddRangeNotNull( dictionary );
        return this;
    }

    #endregion

    #region Content(添加内容参数)

    /// <summary>
    /// 添加参数,作为请求内容发送
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public IHttpRequest<TResult> Content( string key , object value ) {
        ContentParams.AddNotNull( key , value );
        return this;
    }

    /// <summary>
    /// 添加参数,作为请求内容发送
    /// </summary>
    /// <param name="parameters">参数字典</param>
    public IHttpRequest<TResult> Content( IDictionary<string , object> parameters ) {
        ContentParams.AddRangeNotNull( parameters );
        return this;
    }

    /// <summary>
    /// 添加参数,作为请求内容发送
    /// </summary>
    /// <param name="value">值</param>
    public IHttpRequest<TResult> Content( object value ) {
        IDictionary<string , object> dictionary = _isEasyAnalyzingParam ? value.ToDictionary() : value.AnalyzingToDictionary();
        ContentParams.AddRangeNotNull( dictionary );
        return this;
    }

    #endregion

    #region ContentByJson  [添加Json参数]

    /// <summary>
    /// 添加内容类型为 application/json 的参数
    /// </summary>
    /// <param name="value">值</param>
    public IHttpRequest<TResult> ContentByJson( object value ) {
        if( value == null )
            return this;
        ContentType( Http.HttpContentTypeEnum.Json );
        ContentObject = value;
        return this;
    }

    #endregion

    #region ContentByXml  [添加Xml参数]

    /// <summary>
    /// 添加内容类型为 application/xml 的参数
    /// </summary>
    /// <param name="value">值</param>
    public IHttpRequest<TResult> ContentByXml( string value ) {
        if( value.IsEmpty() )
            return this;
        ContentType( Http.HttpContentTypeEnum.Xml );
        ContentObject = value;
        return this;
    }

    #endregion

    #region ContentByFile  [添加文件参数]

    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="fileKey">文件键</param>
    /// <param name="formFields">表单字段</param>
    public IHttpRequest<TResult> ContentByFile( string filePath , string fileKey = "file" , IDictionary<string , string> formFields = null ) {
        if( filePath.IsEmpty() )
            return this;
        return ContentByFile( Path.GetFileName( filePath ) , File.ReadAllBytes( filePath ) , fileKey , formFields );
    }

    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="fileBytes">文件流</param>
    /// <param name="fileKey">文件键</param>
    /// <param name="formFields">表单字段</param>
    public IHttpRequest<TResult> ContentByFile( string fileName , byte[] fileBytes , string fileKey = "file" , IDictionary<string , string> formFields = null ) {
        if( fileBytes.IsEmpty() )
            return this;
        if( FileObjects.Exists( t => t.Item3.Contains( fileKey ) ) )
            return this;
        ContentType( Http.HttpContentTypeEnum.FormFile );
        FileObjects.Add( (fileName.IsEmpty() ? Guid.NewGuid().SafeString() : fileName, fileBytes, fileKey) );
        FileFormFields.AddRangeNotEmpty( formFields );
        return this;
    }

    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="filePaths">文件路径集合。文件key自动赋不重复值</param>
    /// <param name="formFields">表单字段</param>
    public IHttpRequest<TResult> ContentByFile( IEnumerable<string> filePaths , IDictionary<string , string> formFields = null ) {
        if( filePaths.IsEmpty() )
            return this;
        ContentType( Http.HttpContentTypeEnum.FormFile );
        foreach( string file in filePaths )
            FileObjects.Add( (Path.GetFileName( file ) ?? Guid.NewGuid().SafeString(), File.ReadAllBytes( file ), Guid.NewGuid().SafeString()) );
        FileFormFields.AddRangeNotEmpty( formFields );
        return this;
    }

    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="filePaths">文件路径集合：key为文件路径，value为文件key（key可为null,若为null则自动赋不重复值）</param>
    /// <param name="formFields">表单字段</param>
    public IHttpRequest<TResult> ContentByFile( IDictionary<string , string> filePaths , IDictionary<string , string> formFields = null ) {
        if( filePaths.IsEmpty() )
            return this;
        ContentType( Http.HttpContentTypeEnum.FormFile );
        foreach( KeyValuePair<string , string> file in filePaths ) {
            if( file.Key.IsEmpty() )
                continue;
            if( !file.Value.IsEmpty() )
                if( FileObjects.Exists( t => t.Item3.Contains( file.Value ) ) )
                    continue;
            FileObjects.Add( (Path.GetFileName( file.Key ) ?? Guid.NewGuid().SafeString(), File.ReadAllBytes( file.Key ), file.Value.IsEmpty() ? Guid.NewGuid().SafeString() : file.Value) );
        }
        FileFormFields.AddRangeNotEmpty( formFields );
        return this;
    }

    /// <summary>
    /// 添加内容类型为 multipart/form-data 的参数
    /// </summary>
    /// <param name="files">文件集合：第一个参数为文件名；第二参数为文件流，第三个参数为文件key（key可为null,若为null则自动赋不重复值）</param>
    /// <param name="formFields">表单字段</param>
    public IHttpRequest<TResult> ContentByFile( IEnumerable<(string, byte[], string)> files , IDictionary<string , string> formFields = null ) {
        if( files.IsEmpty() )
            return this;
        ContentType( Http.HttpContentTypeEnum.FormFile );
        foreach( (string, byte[], string) file in files ) {
            if( file.Item2.IsEmpty() )
                continue;
            if( !file.Item3.IsEmpty() )
                if( FileObjects.Exists( t => t.Item3.Contains( file.Item3 ) ) )
                    continue;
            FileObjects.Add( (file.Item1.IsEmpty() ? Guid.NewGuid().SafeString() : file.Item1, file.Item2, file.Item3.IsEmpty() ? Guid.NewGuid().SafeString() : file.Item3) );
        }
        FileFormFields.AddRangeNotEmpty( formFields );
        return this;
    }

    #endregion

    #endregion

    #region 设置方法

    #region OnSendBefore & OnSendAfter  [发送前/后事件]

    /// <summary>
    /// 发送前事件
    /// </summary>
    /// <param name="action">发送前操作,返回false取消发送</param>
    public IHttpRequest<TResult> OnSendBefore( Func<HttpRequestMessage , bool> action ) {
        SendBeforeAction = action;
        return this;
    }

    /// <summary>
    /// 发送后事件
    /// </summary>
    /// <param name="action">发送后操作,自定义解析返回值</param>
    public IHttpRequest<TResult> OnSendAfter( Func<HttpResponseMessage , Task<TResult>> action ) {
        SendAfterAction = action;
        return this;
    }

    #endregion

    #region OnConvert  [结果转换事件]

    /// <summary>
    /// 结果转换事件
    /// </summary>
    /// <param name="action">结果转换操作,参数为响应内容</param>
    public IHttpRequest<TResult> OnConvert( Func<string , TResult> action ) {
        ConvertAction = action;
        return this;
    }

    #endregion

    #region OnSuccess  & OnFail  [请求成功/失败事件]

    /// <summary>
    /// 请求成功事件
    /// </summary>
    /// <param name="action">执行成功操作,参数为响应结果</param>
    public IHttpRequest<TResult> OnSuccess( Action<TResult> action ) {
        SuccessAction = action;
        return this;
    }

    /// <summary>
    /// 请求失败事件
    /// </summary>
    /// <param name="action">执行失败操作,参数为响应消息和响应内容</param>
    public IHttpRequest<TResult> OnFail( Action<HttpResponseMessage , object> action ) {
        FailAction = action;
        return this;
    }

    #endregion

    #region OnComplete  [请求完成事件]

    /// <summary>
    /// 请求完成事件,不论成功失败都会执行
    /// </summary>
    /// <param name="action">执行完成操作,参数为响应消息和响应内容</param>
    public IHttpRequest<TResult> OnComplete( Action<HttpResponseMessage , object> action ) {
        CompleteAction = action;
        return this;
    }

    #endregion

    #endregion

    #region 获取结果

    #region GetResultAsync  [获取结果]

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    public async Task<Result<TResult>> GetResultAsync( Action<SystemException> listenerExceptionFunc = null ) {
        if( _retryMaxTimes > 0 )
            return await Meow.Helper.Retry.TryInvokeAsync( RunResultAsync , _retryValidateResultFunc , _retryMaxTimes , _retryListenerExceptionFunc , _retryDelayFunc );
        try {
            TResult result = await RunResultAsync();
            return new Result<TResult>( ResultStatusEnum.Success , ResultStatusEnum.Success.GetDescription() , result );
        } catch( SystemException ex ) {
            listenerExceptionFunc?.Invoke( ex );
            return new Result<TResult>( ResultStatusEnum.Error , ex.Message );
        }
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    protected virtual async Task<TResult> RunResultAsync() {
        HttpRequestMessage message = CreateMessage();
        if( SendBefore( message ) == false )
            return default;
        HttpResponseMessage response = await SendAsync( message );
        return await SendAfterAsync( response );
    }

    #endregion

    #region GetStreamAsync  [获取流]

    /// <summary>
    /// 获取流
    /// </summary>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    public async Task<Result<byte[]>> GetStreamAsync( Action<SystemException> listenerExceptionFunc = null ) {
        if( _retryMaxTimes > 0 )
            return await Meow.Helper.Retry.TryInvokeAsync( RunStreamAsync , ( ( result ) => result != null ) , _retryMaxTimes , _retryListenerExceptionFunc , _retryDelayFunc );
        try {
            byte[] result = await RunStreamAsync();
            return new Result<byte[]>( ResultStatusEnum.Success , ResultStatusEnum.Success.GetDescription() , result );
        } catch( SystemException ex ) {
            listenerExceptionFunc?.Invoke( ex );
            return new Result<byte[]>( ResultStatusEnum.Error , ex.Message );
        }
    }

    /// <summary>
    /// 获取流
    /// </summary>
    public virtual async Task<byte[]> RunStreamAsync() {
        HttpRequestMessage message = CreateMessage();
        if( SendBefore( message ) == false )
            return default;
        HttpResponseMessage response = await SendAsync( message );
        return await GetStream( response );
    }

    /// <summary>
    /// 发送后操作
    /// </summary>
    /// <param name="response">响应消息</param>
    protected virtual async Task<byte[]> GetStream( HttpResponseMessage response ) {
        byte[] content = null;
        try {
            content = await response.Content.ReadAsByteArrayAsync();
            if( response.IsSuccessStatusCode )
                return content;
            FailHandler( response , content );
            return null;
        } finally {
            CompleteHandler( response , content );
        }
    }

    #endregion

    #region WriteAsync  [写入文件]

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    public async Task<Result> WriteAsync( string filePath , Action<SystemException> listenerExceptionFunc = null ) {
        Result<byte[]> result = await GetStreamAsync( listenerExceptionFunc );
        if( result.IsSuccess() )
            return new Result( result.Code , result.Message , null );
        try {
            await result.Data.FileWriteAsync( filePath );
            return new Result( ResultStatusEnum.Success , ResultStatusEnum.Success.GetDescription() );
        } catch( SystemException ex ) {
            listenerExceptionFunc?.Invoke( ex );
            return new Result( ResultStatusEnum.Error , ex.Message );
        }
    }

    #endregion

    #endregion

    #region 执行相关

    #region CreateMessage  [创建请求消息]

    /// <summary>
    /// 创建请求消息
    /// </summary>
    protected virtual HttpRequestMessage CreateMessage() {
        HttpRequestMessage message = new HttpRequestMessage( _httpMethod , GetUrl( _url ) );
        AddHeaders( message );
        message.Content = CreateHttpContent();
        return message;
    }

    /// <summary>
    /// 获取服务地址
    /// </summary>
    /// <param name="url">服务地址</param>
    protected virtual string GetUrl( string url ) {
        return Meow.Helper.Url.AddQueryString( url , QueryParams );
    }

    /// <summary>
    /// 创建请求内容
    /// </summary>
    protected virtual HttpContent CreateHttpContent() {
        string contentType = HttpContentType.SafeString().ToLower();
        switch( contentType ) {
            case HttpContentTypeCode.FormData:
                return CreateFormContent();
            case HttpContentTypeCode.Json:
                return CreateJsonContent();
            case HttpContentTypeCode.Xml:
                return CreateXmlContent();
            case HttpContentTypeCode.FormFile:
                return CreateFileContent();
        }
        return null;
    }

    #region 创建表单内容

    /// <summary>
    /// 创建表单内容
    /// </summary>
    protected virtual HttpContent CreateFormContent() {
        Dictionary<string , string> contentParams = GetContentParams();
        return new FormUrlEncodedContent( contentParams );
    }

    /// <summary>
    /// 获取内容参数
    /// </summary>
    private Dictionary<string , string> GetContentParams() {
        Dictionary<string , string> content = new Dictionary<string , string>();
        foreach( KeyValuePair<string , object> param in ContentParams ) {
            string value = param.Value.SafeString();
            if( value.IsEmpty() )
                continue;
            content.Add( param.Key , value );
        }
        return content;
    }

    #endregion

    #region 创建json内容

    /// <summary>
    /// 创建json内容
    /// </summary>
    protected virtual HttpContent CreateJsonContent() {
        string content = GetJsonContentValue();
        if( content.IsEmpty() )
            return null;
        return new StringContent( content , CharacterEncoding , HttpContentTypeCode.Json );
    }

    /// <summary>
    /// 获取json内容值
    /// </summary>
    private string GetJsonContentValue() {
        JsonSerializerOptions options = GetJsonSerializerOptions();
        if( ContentObject != null && ContentParams.Count > 0 )
            return GetParameters().ToJson( options );
        if( ContentObject != null )
            return ContentObject.ToJson( options );
        if( ContentParams.Count > 0 )
            return ContentParams.ToJson( options );
        return null;
    }

    /// <summary>
    /// 获取Json序列化配置
    /// </summary>
    protected virtual JsonSerializerOptions GetJsonSerializerOptions() {
        if( _jsonSerializerOptions != null )
            return _jsonSerializerOptions;
        return new JsonSerializerOptions {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull ,
            Converters = {
                new Meow.Json.Converter.DateTimeJsonConverter(),
                new Meow.Json.Converter.NullableDateTimeJsonConverter()
            }
        };
    }

    /// <summary>
    /// 获取参数
    /// </summary>
    protected IDictionary<string , object> GetParameters() {
        Dictionary<string , object> result = new Dictionary<string , object>( ContentParams );
        IDictionary<string , object> dictionary = _isEasyAnalyzingParam ? ContentObject.ToDictionary() : ContentObject.AnalyzingToDictionary();
        if( dictionary.IsEmpty() )
            return result;
        result.AddRangeNotNull( dictionary , false );
        return result;
    }

    #endregion

    #region 创建xml内容

    /// <summary>
    /// 创建xml内容
    /// </summary>
    protected virtual HttpContent CreateXmlContent() {
        return new StringContent( ContentObject.SafeString() , CharacterEncoding , HttpContentTypeCode.Xml );
    }

    #endregion

    #region 创建文件类型

    /// <summary>
    /// Upload file File header format
    /// 0:fileKey
    /// 1:fileName
    /// </summary>
    private const string _fileHeaderFormat =
        "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
        "Content-Type: application/octet-stream\r\n\r\n";

    /// <summary>
    /// FileFormDataFormat
    /// 0:key
    /// 1:value
    /// 2:boundary
    /// </summary>
    private const string _fileFormDataFormat = "\r\n--{2}\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

    /// <summary>
    /// 创建文件内容
    /// </summary>
    protected virtual HttpContent CreateFileContent() {
        if( FileObjects.IsEmpty() )
            throw new ArgumentNullException( nameof( FileObjects ) );

        string boundary = $"----------------------------{Meow.Helper.Time.Now.Ticks:X}";

        MultipartFormDataContent content = new MultipartFormDataContent( boundary );
        Dictionary<string , string> contentParams = GetContentParams();
        foreach( KeyValuePair<string , string> param in contentParams )
            content.Add( new StringContent( param.Value ) , param.Key );

        byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes( $"\r\n--{boundary}\r\n" );
        byte[] endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes( $"\r\n--{boundary}--" );
        using( MemoryStream memStream = new MemoryStream() ) {
            if( !FileFormFields.IsEmpty() )
                foreach( KeyValuePair<string , string> pair in FileFormFields )
                    memStream.Write( CharacterEncoding.GetBytes( string.Format( _fileFormDataFormat , pair.Key , pair.Value , boundary ) ) );

            foreach( (string, byte[], string) file in FileObjects ) {
                if( file.Item1.IsEmpty() || file.Item2.IsEmpty() | file.Item3.IsEmpty() )
                    throw new ArgumentNullException( nameof( file ) );

                memStream.Write( boundaryBytes );
                memStream.Write( CharacterEncoding.GetBytes( string.Format( _fileHeaderFormat , file.Item3 , file.Item1 ) ) );
                memStream.Write( file.Item2 );
            }
            memStream.Write( endBoundaryBytes );
            content.Add( new ByteArrayContent( memStream.ToArray() ) );
        }
        return content;
    }

    #endregion

    #endregion

    #region SendBefore  [发送前操作]

    /// <summary>
    /// 发送前操作
    /// </summary>
    /// <param name="message">请求消息</param>
    protected virtual bool SendBefore( HttpRequestMessage message ) {
        if( SendBeforeAction == null )
            return true;
        return SendBeforeAction( message );
    }

    #endregion

    #region SendAsync  [发送请求]

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="message">请求消息</param>
    protected async Task<HttpResponseMessage> SendAsync( HttpRequestMessage message ) {
        HttpClient client = GetClient();
        return await client.SendAsync( message );
    }

    #endregion

    #region GetClient  [获取Http客户端]

    /// <summary>
    /// 获取Http客户端
    /// </summary>
    protected HttpClient GetClient() {
        HttpClient client = GetHttpClient();
        InitHttpClient( client );
        return client;
    }

    /// <summary>
    /// 初始化Http客户端
    /// </summary>
    /// <param name="client">Http客户端</param>
    protected virtual void InitHttpClient( HttpClient client ) {
        InitTimeout( client );
    }

    /// <summary>
    /// 初始化超时时间
    /// </summary>
    /// <param name="client">Http客户端</param>
    protected void InitTimeout( HttpClient client ) {
        client.Timeout = TimeoutParam;
    }

    /// <summary>
    /// 获取Http客户端
    /// </summary>
    protected HttpClient GetHttpClient() {
        if( _httpClient != null )
            return _httpClient;
        HttpClientHandler clientHandler = CreateHttpClientHandler();
        InitHttpClientHandler( clientHandler );
        return _httpClientName.IsEmpty() ? _httpClientFactory.CreateClient() : _httpClientFactory.CreateClient( _httpClientName );
    }

    /// <summary>
    /// 创建Http客户端处理器
    /// </summary>
    protected HttpClientHandler CreateHttpClientHandler() {
        IHttpMessageHandlerFactory handlerFactory = _httpClientFactory as IHttpMessageHandlerFactory;
        if( handlerFactory == null )
            return null;
        HttpMessageHandler handler = _httpClientName.IsEmpty() ? handlerFactory.CreateHandler() : handlerFactory.CreateHandler( _httpClientName );
        while( handler is DelegatingHandler delegatingHandler ) {
            handler = delegatingHandler.InnerHandler;
        }
        return handler as HttpClientHandler;
    }

    /// <summary>
    /// 初始化Http客户端处理器
    /// </summary>
    /// <param name="handler">Http客户端处理器</param>
    protected virtual void InitHttpClientHandler( HttpClientHandler handler ) {
        InitCertificate( handler );
        InitCookie( handler );
        IgnoreSsl( handler );
    }

    #endregion

    #region InitCertificate  [初始化证书]

    /// <summary>
    /// 初始化证书
    /// </summary>
    protected void InitCertificate( HttpClientHandler handler ) {
        if( CertificateParam.Item1.IsEmpty() )
            return;
        X509Certificate2 certificate = new X509Certificate2( CertificateParam.Item1 , CertificateParam.Item2 , X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet );
        handler.ClientCertificates.Add( certificate );
    }

    #endregion

    #region InitCookie  [初始化Cookie]

    /// <summary>
    /// 初始化Cookie
    /// </summary>
    protected void InitCookie( HttpClientHandler handler ) {
        CookieContainer ??= new CookieContainer();
        if( CookieContainer?.Count == 0 ) {
            handler.UseCookies = false;
            return;
        }
        handler.UseCookies = true;
        handler.CookieContainer = CookieContainer;
    }

    #endregion

    #region IgnoreSsl  [忽略SSL证书错误]

    /// <summary>
    /// 忽略SSL证书错误
    /// </summary>
    protected virtual void IgnoreSsl( HttpClientHandler handler ) {
        if( _ignoreSsl == false )
            return;
        handler.ServerCertificateCustomValidationCallback ??= ( _ , _ , _ , _ ) => true;
    }

    #endregion

    #region SendAfterAsync  [发送后操作]

    /// <summary>
    /// 发送后操作
    /// </summary>
    /// <param name="response">响应消息</param>
    protected virtual async Task<TResult> SendAfterAsync( HttpResponseMessage response ) {
        if( SendAfterAction != null )
            return await SendAfterAction( response );
        string content = null;
        try {
            content = await response.Content.ReadAsStringAsync();
            if( response.IsSuccessStatusCode )
                return SuccessHandler( response , content );
            FailHandler( response , content );
            return null;
        } finally {
            CompleteHandler( response , content );
        }
    }

    #endregion

    #region SuccessHandler  [成功处理操作]

    /// <summary>
    /// 成功处理操作
    /// </summary>
    protected virtual TResult SuccessHandler( HttpResponseMessage response , string content ) {
        TResult result = ConvertTo( content , response.GetContentType() );
        return result;
    }

    #endregion

    #region ConvertTo  [将内容转换为结果]

    /// <summary>
    /// 将内容转换为结果
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="contentType">内容类型</param>
    protected virtual TResult ConvertTo( string content , string contentType ) {
        if( ConvertAction != null )
            return ConvertAction( content );
        if( typeof( TResult ) == typeof( string ) )
            return ( TResult ) ( object ) content;
        if( contentType.SafeString().ToLower() == HttpContentTypeCode.Json ) {
            JsonSerializerOptions options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true ,
                Converters = {
                    new Meow.Json.Converter.DateTimeJsonConverter(),
                    new Meow.Json.Converter.NullableDateTimeJsonConverter()
                }
            };
            return content.ToJsonObject<TResult>( options );
        }
        return null;
    }

    #endregion

    #region FailHandler  [失败处理操作]

    /// <summary>
    /// 失败处理操作
    /// </summary>
    protected virtual void FailHandler( HttpResponseMessage response , object content ) {
        FailAction?.Invoke( response , content );
    }

    #endregion

    #region CompleteHandler  [执行完成操作]

    /// <summary>
    /// 执行完成操作
    /// </summary>
    protected virtual void CompleteHandler( HttpResponseMessage response , object content ) {
        CompleteAction?.Invoke( response , content );
    }

    #endregion

    #region 添加请求头

    /// <summary>
    /// 添加请求头
    /// </summary>
    /// <param name="message">请求消息</param>
    protected virtual void AddHeaders( HttpRequestMessage message ) {
        AddAuthenticationHeaders( message );
        AddStandardHeaders( message );
        AddCustomHeaders( message );
    }

    /// <summary>
    /// 添加授权请求头
    /// </summary>
    /// <param name="message"></param>
    private void AddAuthenticationHeaders( HttpRequestMessage message ) {
        message.Headers.Authorization.AssignNotNull( AuthorizationParam );
        if( !TenantParam.Item2.IsEmpty() )
            message.Headers.Add( TenantParam.Item1 , TenantParam.Item2 );
    }

    /// <summary>
    /// 添加标准请求头
    /// </summary>
    /// <param name="message">请求消息</param>
    private void AddStandardHeaders( HttpRequestMessage message ) {
        message.Headers.Date = HeaderStandardParam?.Date ?? Meow.Helper.Time.Now;

        if( HeaderStandardParam == null )
            return;

        #region 设置接受参数

        message.Headers.Accept.AddNotEmpty( HeaderStandardParam.Accept );
        message.Headers.AcceptCharset.AddNotEmpty( HeaderStandardParam.AcceptCharset );
        message.Headers.AcceptEncoding.AddNotEmpty( HeaderStandardParam.AcceptEncoding );
        message.Headers.AcceptLanguage.AddNotEmpty( HeaderStandardParam.AcceptLanguage );

        #endregion

        message.Headers.CacheControl.AssignNotNull( HeaderStandardParam.CacheControl );
        message.Headers.Connection.AddNotEmpty( HeaderStandardParam.Connection );
        message.Headers.Expect.AddNotNull( HeaderStandardParam.Expect );
        message.Headers.From = HeaderStandardParam.From;
        message.Headers.Host = HeaderStandardParam.Host;
        message.Headers.IfMatch.AddNotNull( HeaderStandardParam.IfMatch );
        message.Headers.IfModifiedSince = HeaderStandardParam.IfModifiedSince;
        message.Headers.IfNoneMatch.AddNotNull( HeaderStandardParam.IfNoneMatch );
        message.Headers.IfRange.AssignNotNull( HeaderStandardParam.IfRange );
        message.Headers.IfUnmodifiedSince = HeaderStandardParam.IfUnmodifiedSince;
        message.Headers.MaxForwards = HeaderStandardParam.MaxForwards;
        message.Headers.Pragma.AddNotNull( HeaderStandardParam.Pragma );
        message.Headers.ProxyAuthorization.AssignNotNull( HeaderStandardParam.ProxyAuthorization );
        message.Headers.Range.AssignNotNull( HeaderStandardParam.Range );
        message.Headers.Referrer.AssignNotNull( HeaderStandardParam.Referrer );
        message.Headers.TE.AddNotNull( HeaderStandardParam.TE );
        message.Headers.Upgrade.AddNotNull( HeaderStandardParam.Upgrade );
        message.Headers.UserAgent.AddNotNull( HeaderStandardParam.UserAgent );
        message.Headers.Via.AddNotNull( HeaderStandardParam.Via );
        message.Headers.Warning.AddNotNull( HeaderStandardParam.Warning );
    }

    /// <summary>
    /// 添加自定义请求头
    /// </summary>
    /// <param name="message">请求消息</param>
    private void AddCustomHeaders( HttpRequestMessage message ) {
        foreach( KeyValuePair<string , string> header in HeaderCustomParams ) {
            if( message.Headers.Contains( header.Key ) )
                continue;
            message.Headers.Add( header.Key , header.Value );
        }
    }

    #endregion

    #endregion
}