namespace Meow.Http;

/// <summary>
/// Http方法
/// </summary>
public enum HttpMethodEnum {
    /// <summary>
    /// Get
    /// </summary>
    [Description( "Get" )]
    Get = 1,
    /// <summary>
    /// Post
    /// </summary>
    [Description( "Post" )]
    Post = 2,
    /// <summary>
    /// Put
    /// </summary>
    [Description( "Put" )]
    Put = 3,
    /// <summary>
    /// Delete
    /// </summary>
    [Description( "Delete" )]
    Delete = 4,
}