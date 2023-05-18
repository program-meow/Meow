﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Meow.Extension;

namespace Meow.Event.Extension;

/// <summary>
/// 事件总线扩展
/// </summary>
public static class EventBusExtension
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="eventBus">事件总线</param>
    /// <param name="events">事件集合</param>
    public static async Task PublishAsync(this IEventBus eventBus, IEnumerable<IEvent> events)
    {
        eventBus.CheckNull(nameof(eventBus));
        if (events == null)
            return;
        foreach (var @event in events)
        {
            await eventBus.PublishAsync(@event);
        }
    }
}