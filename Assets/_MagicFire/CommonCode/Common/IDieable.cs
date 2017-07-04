using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicFire.Common
{
    /// <summary>
    /// 可死亡的
    /// </summary>
    public interface IDieable
    {
        /// <summary>
        /// 重生
        /// </summary>
        void Rebirth();
        /// <summary>
        /// 死亡
        /// </summary>
        void OnDie();
    }
}
