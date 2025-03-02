using UnityEngine;

namespace UIFramework.Rendering
{
    /// <summary>
    /// UI渲染器接口
    /// </summary>
    public interface IUIRenderer
    {
        /// <summary>
        /// 设置渲染材质
        /// </summary>
        /// <param name="material">材质</param>
        void SetMaterial(Material material);

        /// <summary>
        /// 更新渲染数据
        /// </summary>
        void UpdateRenderData();

        /// <summary>
        /// 执行渲染
        /// </summary>
        void Render();

        /// <summary>
        /// 获取当前使用的材质
        /// </summary>
        Material GetMaterial();

        /// <summary>
        /// 设置渲染顺序
        /// </summary>
        int SortingOrder { get; set; }

        /// <summary>
        /// 获取或设置是否启用渲染
        /// </summary>
        bool IsEnabled { get; set; }
    }
}