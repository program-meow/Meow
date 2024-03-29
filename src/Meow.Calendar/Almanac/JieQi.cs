﻿namespace Meow.Calendar.Almanac;

/// <summary>
/// 节气
/// </summary>
public class JieQi {

    /// <summary>
    /// 名称
    /// </summary>
    private string _name;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name {
        get => _name;
        set {
            _name = value;
            for( int i = 0, j = LunarUtil.JIE_QI.Length ; i < j ; i++ ) {
                if( !value.Equals( LunarUtil.JIE_QI[ i ] ) ) continue;
                if( i % 2 == 0 ) {
                    Qi = true;
                } else {
                    Jie = true;
                }
                return;
            }
        }
    }

    /// <summary>
    /// 阳历日期
    /// </summary>
    public Solar Solar { get; set; }

    /// <summary>
    /// 是否节令
    /// </summary>
    public bool Jie { get; set; }

    /// <summary>
    /// 是否气令
    /// </summary>
    public bool Qi { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public JieQi() {
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="solar">阳历日期</param>
    public JieQi( string name , Solar solar ) {
        Name = name;
        Solar = solar;
    }

    /// <inheritdoc />
    public override string ToString() {
        return Name;
    }
}
