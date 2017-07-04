using UnityEngine;
using System.Collections;

public class DieEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroySelf", 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
