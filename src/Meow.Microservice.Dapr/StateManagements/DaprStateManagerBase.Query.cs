namespace Meow.Microservice.Dapr.StateManagements;

/// <summary>
/// Dapr状态管理 - 查询
/// </summary>
public partial class DaprStateManagerBase<TStateManager> {

    #region Limit

    /// <summary>
    /// 添加分页大小
    /// </summary>
    /// <param name="pageSize">分页大小</param>
    public TStateManager Limit( int pageSize ) {
        Page.Limit = pageSize;
        return Return();
    }

    #endregion

    #region Token

    /// <summary>
    /// 添加迭代令牌
    /// </summary>
    /// <param name="token">迭代令牌</param>
    public TStateManager Token( int token ) {
        Page.Token = token.ToString();
        return Return();
    }

    #endregion

    #region OrderBy

    /// <inheritdoc />
    public TStateManager OrderBy( string orderBy ) {
        Sort.OrderBy( orderBy );
        return Return();
    }

    #endregion

    #region Equal

    /// <inheritdoc />
    public TStateManager Equal( string property , object value ) {
        Filter.Equal( property , value );
        return Return();
    }

    #endregion

    #region EqualIf

    /// <inheritdoc />
    public TStateManager EqualIf( string property , object value , bool condition ) {
        return condition ? Equal( property , value ) : Return();
    }

    #endregion

    #region In

    /// <inheritdoc />
    public TStateManager In( string property , IEnumerable<object> values ) {
        Filter.In( property , values );
        return Return();
    }

    /// <inheritdoc />
    public TStateManager In( string property , params object[] values ) {
        Filter.In( property , values );
        return Return();
    }

    #endregion

    #region And

    /// <inheritdoc />
    public TStateManager And( params IStateCondition[] conditions ) {
        if( conditions == null )
            return Return();
        foreach( IStateCondition condition in conditions )
            Filter.And( condition );
        return Return();
    }

    #endregion

    #region Or

    /// <inheritdoc />
    public TStateManager Or( params IStateCondition[] conditions ) {
        if( conditions == null )
            return Return();
        foreach( IStateCondition condition in conditions )
            Filter.Or( condition );
        return Return();
    }

    #endregion

    #region GetKeyByIdAsync

    /// <summary>
    /// 通过标识获取存储键
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="id">标识</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<string> GetKeyByIdAsync<TValue>( string id , CancellationToken cancellationToken = default ) where TValue : IDataKey {
        if( id.IsEmpty() )
            return null;
        SetDataTypeCondition<TValue>();
        Page.Limit = 1;
        Equal( "id" , id );
        string query = CreateQuery();
        StateQueryResponse<TValue> response = await Client.QueryStateAsync<TValue>( GetStoreName() , query , Metadatas , cancellationToken );
        StateQueryItem<TValue> result = response.Results.FirstOrDefault();
        ClearQuery();
        return result?.Key;
    }

    #endregion

    #region GetStateAndETagAsync

    /// <inheritdoc />
    public virtual async Task<(TValue value, string etag)> GetStateAndETagAsync<TValue>( string key , CancellationToken cancellationToken = default ) {
        (TValue value, string etag) result = await Client.GetStateAndETagAsync<TValue>( GetStoreName() , key , ConsistencyMode , Metadatas , cancellationToken );
        ClearQuery();
        return result;
    }

    #endregion

    #region GetAsync

    /// <inheritdoc />
    public virtual async Task<TValue> GetAsync<TValue>( string key , CancellationToken cancellationToken = default ) {
        if( IsETag<TValue>() == false ) {
            TValue state = await Client.GetStateAsync<TValue>( GetStoreName() , key , ConsistencyMode , Metadatas , cancellationToken );
            ClearQuery();
            return state;
        }
        (TValue value, string etag) result = await GetStateAndETagAsync<TValue>( key , cancellationToken );
        if( result.value == null )
            return default;
        if( result.value is IETag eTag )
            eTag.ETag = result.etag;
        return result.value;
    }

    /// <inheritdoc />
    public virtual async Task<IList<TValue>> GetAsync<TValue>( IReadOnlyList<string> keys , int? parallelism = 0 , CancellationToken cancellationToken = default ) {
        List<TValue> result = new List<TValue>();
        IReadOnlyList<BulkStateItem> items = await Client.GetBulkStateAsync( GetStoreName() , keys , parallelism , Metadatas , cancellationToken );
        if( items == null || items.Count == 0 )
            return result;
        foreach( BulkStateItem item in items ) {
            TValue state = Meow.Helper.Json.ToObject<TValue>( item.Value , SerializerOptions );
            if( state is IETag eTag )
                eTag.ETag = item.ETag;
            result.Add( state );
        }
        ClearQuery();
        return result;
    }

    #endregion

    #region GetByIdAsync

    /// <inheritdoc />
    public virtual async Task<TValue> GetByIdAsync<TValue>( string id , CancellationToken cancellationToken = default ) where TValue : IDataKey {
        if( id.IsEmpty() )
            return default;
        string key = KeyGenerator.CreateKey<TValue>( id );
        TValue result = await GetAsync<TValue>( key , cancellationToken );
        if( result != null )
            return result;
        key = await GetKeyByIdAsync<TValue>( id , cancellationToken );
        return await GetAsync<TValue>( key , cancellationToken );
    }

    #endregion

    #region SingleAsync

    /// <inheritdoc />
    public virtual async Task<TValue> SingleAsync<TValue>( CancellationToken cancellationToken = default ) where TValue : IDataKey {
        SetDataTypeCondition<TValue>();
        Page.Limit = 1;
        IList<TValue> result = await QueryAsync<TValue>( cancellationToken );
        TValue value = result.FirstOrDefault();
        return await GetByIdAsync<TValue>( value?.Id , cancellationToken );
    }

    #endregion

    #region GetAllAsync

    /// <summary>
    /// 获取全部数据
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<IList<TValue>> GetAllAsync<TValue>( CancellationToken cancellationToken = default ) {
        SetDataTypeCondition<TValue>();
        return await QueryAsync<TValue>( cancellationToken );
    }

    #endregion

    #region QueryAsync

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<IList<TValue>> QueryAsync<TValue>( CancellationToken cancellationToken = default ) {
        List<TValue> result = new List<TValue>();
        string query = CreateQuery();
        StateQueryResponse<TValue> response = await Client.QueryStateAsync<TValue>( GetStoreName() , query , Metadatas , cancellationToken );
        ClearQuery();
        if( response == null )
            return result;
        foreach( StateQueryItem<TValue> item in response.Results ) {
            if( item.Data is IETag eTag )
                eTag.ETag = item.ETag;
            result.Add( item.Data );
        }
        return result;
    }

    /// <summary>
    /// 创建状态查询对象
    /// </summary>
    protected virtual string CreateQuery() {
        StateQuery query = new StateQuery {
            Filter = Filter.GetCondition() ,
            Sort = Sort ,
            Page = Page
        };
        return Meow.Helper.Json.ToJson( query , SerializerOptions );
    }

    #endregion

    #region PageQueryAsync

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="page">分页参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<PageList<TValue>> PageQueryAsync<TValue>( IPage page , CancellationToken cancellationToken = default ) {
        Sort.OrderBy( page.Order );
        Page.Limit = page.PageSize;
        Page.Token = page.GetSkipCount() > 0 ? page.GetSkipCount().ToString() : null;
        IList<TValue> data = await QueryAsync<TValue>( cancellationToken );
        return new PageList<TValue>( page , data );
    }

    #endregion

}