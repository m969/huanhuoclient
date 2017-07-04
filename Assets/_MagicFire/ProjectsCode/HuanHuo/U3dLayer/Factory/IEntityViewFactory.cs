using MagicFire.Mmorpg;
using MagicFire.Mmorpg.UI;

namespace MagicFire
{
    public interface IEntityViewFactory : IBaseFactory
    {
        EntityObjectView CreateRenderObjectView(KBEngine.Entity entity, string viewType);
        EntityPanelView CreateEntityPanelView(KBEngine.Entity entity);
    }
}