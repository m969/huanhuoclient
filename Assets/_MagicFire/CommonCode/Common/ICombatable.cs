using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicFire.Common
{
    /// <summary>
    /// 可战斗的
    /// </summary>
    public interface ICombatable : IDieable
    {
        /// <summary>
        /// 攻击
        /// </summary>
        void Attack();
        /// <summary>
        /// 受到攻击
        /// </summary>
        void ReceiveAttack();
    }
}
