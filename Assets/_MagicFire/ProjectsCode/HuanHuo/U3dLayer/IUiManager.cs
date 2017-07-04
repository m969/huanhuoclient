using UnityEngine;

namespace MagicFire.Common
{

    public interface IUiManager
    {
        GameObject Canvas { get; }
        GameObject TryGetOrCreatePanel(string panelName);
    }

}