using UnityEngine;
/// <summary>
/// 图形显示函数委托
/// </summary>
/// <param name="u">参数一</param>
/// <param name="v">参数二</param>
/// <param name="t">时间参数</param>
/// <returns>点的显示位置</returns>
delegate Vector3 GraphFunction(float u, float v, float t);