using UnityEngine;
using System.Collections;

public class ClickPointAuxiliary : MonoBehaviour {//辅助附属附加
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
