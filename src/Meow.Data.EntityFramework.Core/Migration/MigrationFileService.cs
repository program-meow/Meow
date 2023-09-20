namespace Meow.Data.EntityFrameworkCore.Migration;

/// <summary>
/// 迁移文件服务
/// </summary>
public class MigrationFileService : IMigrationFileService {
    /// <summary>
    /// 日志
    /// </summary>
    private readonly ILogger<MigrationFileService> _logger;
    /// <summary>
    /// 迁移目录绝对路径
    /// </summary>
    private string _migrationsPath;
    /// <summary>
    /// 迁移名称
    /// </summary>
    private string _migrationName;
    /// <summary>
    /// 是否移除所有外键
    /// </summary>
    private bool _isRemoveForeignKeys;

    /// <summary>
    /// 初始化迁移文件服务
    /// </summary>
    /// <param name="logger">日志</param>
    public MigrationFileService( ILogger<MigrationFileService> logger ) {
        _logger = logger ?? NullLogger<MigrationFileService>.Instance;
    }

    /// <inheritdoc />
    public IMigrationFileService MigrationsPath( string path ) {
        _migrationsPath = path;
        return this;
    }

    /// <inheritdoc />
    public IMigrationFileService MigrationName( string name ) {
        _migrationName = name;
        return this;
    }

    /// <inheritdoc />
    public IMigrationFileService RemoveForeignKeys() {
        _isRemoveForeignKeys = true;
        return this;
    }

    /// <inheritdoc />
    public string GetFilePath() {
        if( _migrationsPath.IsEmpty() )
            return null;
        if( _migrationName.IsEmpty() )
            return null;
        List<FileInfo> files = Meow.Helper.File.GetAllFiles( _migrationsPath , "*.cs" );
        FileInfo file = files.FirstOrDefault( t => t.Name.EndsWith( $"{_migrationName}.cs" ) );
        if( file == null )
            return null;
        return file.FullName;
    }

    /// <inheritdoc />
    public string GetContent() {
        string filePath = GetFilePath();
        if( filePath.IsEmpty() )
            return null;
        return Meow.Helper.File.ReadToString( filePath );
    }

    /// <inheritdoc />
    public void Save( string filePath = null ) {
        if( _isRemoveForeignKeys == false )
            return;
        if( filePath.IsEmpty() )
            filePath = GetFilePath();
        string content = GetContent();
        string pattern = @"table.ForeignKey\([\s\S]+?\);";
        string result = Meow.Helper.Regex.Replace( content , pattern , "" );
        pattern = @$"\s+{Meow.Helper.String.Line}\s+{Meow.Helper.String.Line}";
        result = Meow.Helper.Regex.Replace( result , pattern , Meow.Helper.String.Line );
        Meow.Helper.File.Write( filePath , result );
        _logger.LogTrace( $"修改迁移文件并保存成功,路径:{filePath}" );
    }
}