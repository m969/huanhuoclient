using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (PlayerInputController.instance == null)
        {
            return;
        }
        transform.DOMove(new Vector3(PlayerInputController.instance.transform.position.x, 10, PlayerInputController.instance.transform.position.z), 1);
    }
}
