namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;
    /// <summary>
    /// 可调节值条（用于定义生命值、法力值这些有上限值和下限值的可调节的数据）
    /// </summary>
    public interface IValueSlidable
    {
        /// <summary>
        /// 主值
        /// </summary>
        int Value { get; set; }
        /// <summary>
        /// 上限值
        /// </summary>
        int UpperLimit { get; set; }
        /// <summary>
        /// 下限值
        /// </summary>
        int LowerLimit { get; set; }
        /// <summary>
        /// 百分比
        /// </summary>
        float Percentage { get; }
        /// <summary>
        /// 恢复主值
        /// </summary>
        /// <param name="recoverAmount"></param>
        void Recover(int recoverAmount);
        /// <summary>
        /// 减少主值
        /// </summary>
        /// <param name="dropAmount"></param>
        void Drop(int dropAmount);
        /// <summary>
        /// 增加上限值
        /// </summary>
        /// <param name="enlargeAmount"></param>
        void EnlargeUpperLimit(int enlargeAmount);
        /// <summary>
        /// 降低上限值
        /// </summary>
        /// <param name="shrinkAmount"></param>
        void ShrinkUpperLimit(int shrinkAmount);
    } 
}