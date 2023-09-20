﻿namespace Meow.Data.EntityFrameworkCore;

/// <summary>
/// 工作单元基类
/// </summary>
public abstract class UnitOfWorkBase : DbContext, IUnitOfWork, IFilterSwitch {

    #region 构造方法

    /// <summary>
    /// 初始化工作单元
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="options">配置</param>
    protected UnitOfWorkBase( IServiceProvider serviceProvider , DbContextOptions options )
        : base( options ) {
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException( nameof( serviceProvider ) );
        Environment = serviceProvider.GetService<IHostEnvironment>();
        FilterManager = ServiceProvider.GetService<IFilterManager>();
        TenantManager = ServiceProvider.GetService<ITenantManager>() ?? NullTenantManager.Instance;
        Session = serviceProvider.GetService<ISession>() ?? NullSession.Instance;
        EventBus = serviceProvider.GetService<ILocalEventBus>() ?? NullEventBus.Instance;
        ActionManager = serviceProvider.GetService<IUnitOfWorkActionManager>() ?? NullUnitOfWorkActionManager.Instance;
        SaveBeforeEvents = new List<IEvent>();
        SaveAfterEvents = new List<IEvent>();
    }

    #endregion

    #region 属性

    /// <summary>
    /// 服务提供器
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }
    /// <summary>
    /// 执行环境
    /// </summary>
    protected IHostEnvironment Environment { get; }
    /// <summary>
    /// 用户会话
    /// </summary>
    protected ISession Session { get; set; }
    /// <summary>
    /// 数据过滤器管理器
    /// </summary>
    protected IFilterManager FilterManager { get; }
    /// <summary>
    /// 租户管理器
    /// </summary>
    protected ITenantManager TenantManager { get; }
    /// <summary>
    /// 事件总线
    /// </summary>
    protected IEventBus EventBus { get; }
    /// <summary>
    /// 工作单元操作管理器
    /// </summary>
    protected IUnitOfWorkActionManager ActionManager { get; }
    /// <summary>
    /// 保存前发送的事件集合
    /// </summary>
    protected List<IEvent> SaveBeforeEvents { get; }
    /// <summary>
    /// 保存后发送的事件集合
    /// </summary>
    protected List<IEvent> SaveAfterEvents { get; }
    /// <summary>
    /// 逻辑删除过滤器是否启用
    /// </summary>
    public virtual bool IsDeleteFilterEnabled => FilterManager?.IsEnabled<IDelete>() ?? false;
    /// <summary>
    /// 租户过滤器是否启用
    /// </summary>
    public virtual bool IsTenantFilterEnabled => FilterManager?.IsEnabled<ITenant>() ?? false;
    /// <summary>
    /// 当前租户标识
    /// </summary>
    public virtual string CurrentTenantId => TenantManager.GetTenantId();
    /// <summary>
    /// 是否清除字符串两端的空白,默认为true
    /// </summary>
    protected virtual bool IsTrimString => true;

    #endregion

    #region 辅助操作

    /// <summary>
    /// 获取用户标识
    /// </summary>
    protected virtual string GetUserId() {
        return Session.UserId;
    }

    #endregion

    #region EnableFilter  [启用过滤器]

    /// <summary>
    /// 启用过滤器
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public void EnableFilter<TFilterType>() where TFilterType : class {
        FilterManager?.EnableFilter<TFilterType>();
    }

    #endregion

    #region DisableFilter  [禁用过滤器]

    /// <summary>
    /// 禁用过滤器
    /// </summary>
    /// <typeparam name="TFilterType">过滤器类型</typeparam>
    public IDisposable DisableFilter<TFilterType>() where TFilterType : class {
        if( FilterManager == null )
            return DisposeAction.Null;
        return FilterManager.DisableFilter<TFilterType>();
    }

    #endregion

    #region OnConfiguring  [配置]

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="optionsBuilder">配置生成器</param>
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
        ConfigLog( optionsBuilder );
        ConfigTenant( optionsBuilder );
    }

    #endregion

    #region ConfigLog  [配置日志]

    /// <summary>
    /// 配置日志
    /// </summary>
    /// <param name="optionsBuilder">配置生成器</param>
    protected virtual void ConfigLog( DbContextOptionsBuilder optionsBuilder ) {
        if( Environment == null )
            return;
        if( Environment.IsProduction() )
            return;
        optionsBuilder.EnableDetailedErrors().EnableSensitiveDataLogging();
    }

    #endregion

    #region ConfigTenant  [配置租户]

    /// <summary>
    /// 配置租户
    /// </summary>
    /// <param name="optionsBuilder">配置生成器</param>
    protected virtual void ConfigTenant( DbContextOptionsBuilder optionsBuilder ) {
        if( TenantManager.Enabled() == false )
            return;
        if( TenantManager.AllowMultipleDatabase() == false )
            return;
        TenantInfo tenant = TenantManager.GetTenant();
        if( tenant == null )
            return;
        string name = ConnectionStringNameAttribute.GetName( GetType() );
        string connectionString = tenant.ConnectionStrings.GetConnectionString( name );
        if( connectionString.IsEmpty() )
            return;
        ConfigTenantConnectionString( optionsBuilder , connectionString );
    }

    /// <summary>
    /// 配置租户连接字符串
    /// </summary>
    /// <param name="optionsBuilder">配置生成器</param>
    /// <param name="connectionString">连接字符串</param>
    protected virtual void ConfigTenantConnectionString( DbContextOptionsBuilder optionsBuilder , string connectionString ) {
    }

    #endregion

    #region OnModelCreating  [配置模型]

    /// <summary>
    /// 配置模型
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    protected override void OnModelCreating( ModelBuilder modelBuilder ) {
        ApplyConfigurations( modelBuilder );
        foreach( IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes() ) {
            ApplyFilters( modelBuilder , entityType );
            ApplyExtraProperties( modelBuilder , entityType );
            ApplyVersion( modelBuilder , entityType );
            ApplyIsDeleted( modelBuilder , entityType );
            ApplyTenantId( modelBuilder , entityType );
            ApplyUtc( modelBuilder , entityType );
            ApplyTrimString( modelBuilder , entityType );
        }
    }

    #endregion

    #region ApplyConfigurations  [配置实体类型]

    /// <summary>
    /// 配置实体类型
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    protected virtual void ApplyConfigurations( ModelBuilder modelBuilder ) {
        modelBuilder.ApplyConfigurationsFromAssembly( GetType().Assembly );
    }

    #endregion

    #region ApplyFilters  [配置过滤器]

    /// <summary>
    /// 配置过滤器
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected virtual void ApplyFilters( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        MethodInfo method = GetType().GetMethod( nameof( ApplyFiltersImp ) , BindingFlags.Instance | BindingFlags.NonPublic );
        method?.MakeGenericMethod( entityType.ClrType ).Invoke( this , new object[] { modelBuilder } );
    }

    /// <summary>
    /// 配置过滤器
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    protected virtual void ApplyFiltersImp<TEntity>( ModelBuilder modelBuilder ) where TEntity : class {
        if( FilterManager == null )
            return;
        if( FilterManager.IsEntityEnabled<TEntity>() == false )
            return;
        Expression<Func<TEntity , bool>> expression = GetFilterExpression<TEntity>();
        if( expression == null )
            return;
        modelBuilder.Entity<TEntity>().HasQueryFilter( expression );
    }

    /// <summary>
    /// 获取过滤器表达式
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    protected virtual Expression<Func<TEntity , bool>> GetFilterExpression<TEntity>() where TEntity : class {
        return FilterManager.GetExpression<TEntity>( this );
    }

    #endregion

    #region ApplyExtraProperties  [配置扩展属性]

    /// <summary>
    /// 配置扩展属性
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected virtual void ApplyExtraProperties( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        if( typeof( IExtraProperties ).IsAssignableFrom( entityType.ClrType ) == false )
            return;
        modelBuilder.Entity( entityType.ClrType )
            .Property( "ExtraProperties" )
            .HasColumnName( "ExtraProperties" )
            .HasComment( "扩展属性" )
            .HasConversion( new ExtraPropertiesValueConverter() )
            .Metadata.SetValueComparer( new ExtraPropertyDictionaryValueComparer() );
    }

    #endregion

    #region ApplyVersion  [配置乐观锁]

    /// <summary>
    /// 配置乐观锁
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected virtual void ApplyVersion( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        if( typeof( IVersion ).IsAssignableFrom( entityType.ClrType ) == false )
            return;
        modelBuilder.Entity( entityType.ClrType )
            .Property( "Version" )
            .HasColumnName( "Version" )
            .HasComment( "版本号" )
            .IsConcurrencyToken();
    }

    #endregion

    #region ApplyIsDeleted  [配置逻辑删除]

    /// <summary>
    /// 配置逻辑删除
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected virtual void ApplyIsDeleted( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        if( typeof( IDelete ).IsAssignableFrom( entityType.ClrType ) == false )
            return;
        modelBuilder.Entity( entityType.ClrType )
            .Property( "IsDeleted" )
            .HasColumnName( "IsDeleted" )
            .HasComment( "是否删除" );
    }

    #endregion

    #region ApplyTenantId  [配置租户标识]

    /// <summary>
    /// 配置租户标识
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected virtual void ApplyTenantId( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        if( typeof( ITenant ).IsAssignableFrom( entityType.ClrType ) == false )
            return;
        modelBuilder.Entity( entityType.ClrType )
            .Property( "TenantId" )
            .HasColumnName( "TenantId" )
            .HasComment( "租户标识" );
    }

    #endregion

    #region ApplyUtc  [配置Utc日期]

    /// <summary>
    /// 配置Utc日期
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected virtual void ApplyUtc( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        if( TimeOptions.IsUseUtc == false )
            return;
        List<PropertyInfo> properties = entityType.ClrType.GetProperties()
            .Where( property => ( property.PropertyType == typeof( DateTime ) || property.PropertyType == typeof( DateTime? ) ) && property.CanWrite &&
                                property.GetCustomAttribute<NotMappedAttribute>() == null )
            .ToList();
        properties.ForEach( property => {
            modelBuilder.Entity( entityType.ClrType )
                .Property( property.Name )
                .HasConversion( new DateTimeValueConverter() );
        } );
    }

    #endregion

    #region ApplyTrimString  [配置清除空白字符串]

    /// <summary>
    /// 配置清除空白字符串
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected virtual void ApplyTrimString( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        if( IsTrimString == false )
            return;
        List<PropertyInfo> properties = entityType.ClrType.GetProperties()
            .Where( property => property.PropertyType == typeof( string ) && property.CanWrite &&
                                property.GetCustomAttribute<NotMappedAttribute>() == null )
            .ToList();
        properties.ForEach( property => {
            modelBuilder.Entity( entityType.ClrType )
                .Property( property.Name )
                .HasConversion( new TrimStringValueConverter() );
        } );
    }

    #endregion

    #region CommitAsync  [提交]

    /// <summary>
    /// 提交,返回影响的行数
    /// </summary>
    public async Task<int> CommitAsync() {
        try {
            return await SaveChangesAsync();
        } catch( DbUpdateConcurrencyException ex ) {
            throw new ConcurrencyException( ex );
        }
    }

    #endregion

    #region SaveChangesAsync  [保存]

    /// <summary>
    /// 保存
    /// </summary>
    public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default ) {
        await SaveChangesBefore();
        int result = await base.SaveChangesAsync( cancellationToken );
        await SaveChangesAfter();
        return result;
    }

    #endregion

    #region SaveChangesBefore  [保存前操作]

    /// <summary>
    /// 保存前操作
    /// </summary>
    protected virtual async Task SaveChangesBefore() {
        foreach( EntityEntry entry in ChangeTracker.Entries() ) {
            UpdateTenantId( entry );
            AddDomainEvents( entry );
            switch( entry.State ) {
                case EntityState.Added:
                    AddBefore( entry );
                    break;
                case EntityState.Modified:
                    UpdateBefore( entry );
                    break;
                case EntityState.Deleted:
                    DeleteBefore( entry );
                    break;
            }
        }
        await PublishSaveBeforeEventsAsync();
    }

    #endregion

    #region UpdateTenantId  [更新租户标识]

    /// <summary>
    /// 更新租户标识
    /// </summary>
    protected virtual void UpdateTenantId( EntityEntry entry ) {
        if( TenantManager.Enabled() == false )
            return;
        if( entry.Entity is not ITenant tenant )
            return;
        string tenantId = TenantManager.GetTenantId();
        if( tenantId.IsEmpty() )
            return;
        tenant.TenantId = tenantId;
    }

    #endregion

    #region AddDomainEvents  [添加领域事件]

    /// <summary>
    /// 添加领域事件
    /// </summary>
    protected virtual void AddDomainEvents( EntityEntry entry ) {
        if( entry.Entity is not IDomainEventManager eventManager )
            return;
        if( eventManager.DomainEvents == null )
            return;
        foreach( IEvent domainEvent in eventManager.DomainEvents ) {
            if( domainEvent is IIntegrationEvent ) {
                SaveAfterEvents.Add( domainEvent );
                continue;
            }
            SaveBeforeEvents.Add( domainEvent );
        }
        eventManager.ClearDomainEvents();
    }

    #endregion

    #region AddBefore  [添加前操作]

    /// <summary>
    /// 添加前操作
    /// </summary>
    protected virtual void AddBefore( EntityEntry entry ) {
        SetCreationAudited( entry.Entity );
        SetModificationAudited( entry.Entity );
        SetVersion( entry.Entity );
        AddEntityCreatedEvent( entry.Entity );
    }

    #endregion

    #region UpdateBefore  [修改前操作]

    /// <summary>
    /// 修改前操作
    /// </summary>
    protected virtual void UpdateBefore( EntityEntry entry ) {
        SetModificationAudited( entry.Entity );
        SetVersion( entry.Entity );
        AddEntityUpdatedEvent( entry.Entity );
    }

    #endregion

    #region DeleteBefore  [删除前操作]

    /// <summary>
    /// 删除前操作
    /// </summary>
    protected virtual void DeleteBefore( EntityEntry entry ) {
        SetModificationAudited( entry.Entity );
        AddEntityDeletedEvent( entry.Entity );
    }

    #endregion

    #region SetCreationAudited  [设置创建审计信息]

    /// <summary>
    /// 设置创建审计信息
    /// </summary>
    protected virtual void SetCreationAudited( object entity ) {
        CreationAuditedSetter.Set( entity , GetUserId() );
    }

    #endregion

    #region SetModificationAudited  [设置修改审计信息]

    /// <summary>
    /// 设置修改审计信息
    /// </summary>
    protected virtual void SetModificationAudited( object entity ) {
        ModificationAuditedSetter.Set( entity , GetUserId() );
    }

    #endregion

    #region SetVersion  [设置版本号]

    /// <summary>
    /// 设置版本号
    /// </summary>
    protected virtual void SetVersion( object obj ) {
        if( !( obj is IVersion entity ) )
            return;
        byte[] version = GetVersion();
        if( version == null )
            return;
        entity.Version = version;
    }

    #endregion

    #region GetVersion  [获取版本号]

    /// <summary>
    /// 获取版本号
    /// </summary>
    protected virtual byte[] GetVersion() {
        return Encoding.UTF8.GetBytes( Guid.NewGuid().ToString() );
    }

    #endregion

    #region AddEntityChangedEvent  [添加实体变更事件]

    /// <summary>
    /// 添加实体变更事件
    /// </summary>
    protected virtual void AddEntityChangedEvent( object entity , EntityChangeTypeEnum changeType ) {
        SystemType eventType = typeof( EntityChangedEvent<> ).MakeGenericType( entity.GetType() );
        IEvent @event = Meow.Helper.Reflection.CreateInstance<IEvent>( eventType , entity , changeType );
        SaveAfterEvents.Add( @event );
    }

    #endregion

    #region AddEntityCreatedEvent  [添加实体创建事件]

    /// <summary>
    /// 添加实体创建事件
    /// </summary>
    protected virtual void AddEntityCreatedEvent( object entity ) {
        IEvent @event = CreateEntityEvent( typeof( EntityCreatedEvent<> ) , entity );
        SaveAfterEvents.Add( @event );
        AddEntityChangedEvent( entity , EntityChangeTypeEnum.Created );
    }

    /// <summary>
    /// 创建实体事件
    /// </summary>
    protected IEvent CreateEntityEvent( SystemType eventType , object entity ) {
        SystemType eventGenericType = eventType.MakeGenericType( entity.GetType() );
        return Meow.Helper.Reflection.CreateInstance<IEvent>( eventGenericType , entity );
    }

    #endregion

    #region AddEntityUpdatedEvent  [添加实体修改事件]

    /// <summary>
    /// 添加实体修改事件
    /// </summary>
    protected virtual void AddEntityUpdatedEvent( object entity ) {
        if( entity is IDelete { IsDeleted: true } ) {
            AddEntityDeletedEvent( entity );
            return;
        }
        IEvent @event = CreateEntityEvent( typeof( EntityUpdatedEvent<> ) , entity );
        SaveAfterEvents.Add( @event );
        AddEntityChangedEvent( entity , EntityChangeTypeEnum.Updated );
    }

    #endregion

    #region AddEntityDeletedEvent  [添加实体删除事件]

    /// <summary>
    /// 添加实体删除事件
    /// </summary>
    protected virtual void AddEntityDeletedEvent( object entity ) {
        IEvent @event = CreateEntityEvent( typeof( EntityDeletedEvent<> ) , entity );
        SaveAfterEvents.Add( @event );
        AddEntityChangedEvent( entity , EntityChangeTypeEnum.Deleted );
    }

    #endregion

    #region PublishSaveBeforeEventsAsync  [发布保存前事件]

    /// <summary>
    /// 发布保存前事件
    /// </summary>
    protected virtual async Task PublishSaveBeforeEventsAsync() {
        if( SaveBeforeEvents.Count == 0 )
            return;
        List<IEvent> events = new List<IEvent>( SaveBeforeEvents );
        SaveBeforeEvents.Clear();
        await EventBus.PublishAsync( events );
    }

    #endregion

    #region SaveChangesAfter  [保存后操作]

    /// <summary>
    /// 保存后操作
    /// </summary>
    protected virtual async Task SaveChangesAfter() {
        await PublishSaveAfterEventsAsync();
        await ExecuteActionsAsync();
    }

    #endregion

    #region PublishSaveAfterEventsAsync  [发布保存后事件]

    /// <summary>
    /// 发布保存后事件
    /// </summary>
    protected virtual async Task PublishSaveAfterEventsAsync() {
        List<IEvent> events = new List<IEvent>( SaveAfterEvents );
        SaveAfterEvents.Clear();
        await EventBus.PublishAsync( events );
    }

    #endregion

    #region ExecuteActionsAsync  [执行工作单元操作集合]

    /// <summary>
    /// 执行工作单元操作集合
    /// </summary>
    protected virtual async Task ExecuteActionsAsync() {
        await ActionManager.ExecuteAsync();
    }

    #endregion
}