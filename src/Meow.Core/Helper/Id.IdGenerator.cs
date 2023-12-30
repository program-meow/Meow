using Yitter.IdGenerator;

namespace Meow.Helper;

/// <summary>
/// 标识生成器
/// </summary>
public static partial class Id {
    /// <summary>
    /// 使用 YitIdHelper 雪花漂移算法创建标识
    /// </summary>
    public static long CreateYitId() {
        if( string.IsNullOrEmpty( _id.Value ) == false )
            return Meow.Helper.Convert.ToLong( _id.Value );
        try {
            return YitIdHelper.NextId();
        } catch {
            IdGeneratorOptions options = new IdGeneratorOptions( 1 );
            YitIdHelper.SetIdGenerator( options );
            return YitIdHelper.NextId();
        }
    }
}