using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Text text;
    public Scrollbar scrollbar;
	// Use this for initialization
	void Start () 
    {
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(onValueChanged);
        }
	}
    void onValueChanged(float value)
    {
        Debug.LogError(value);
        text.text = value.ToString();
    }
	// Update is called once per frame
	void Update () 
    {
	
	}
    //void RecordPlayerInputNumber(int index)
    //{
// button.onClick.AddListener(delegate() {RecordPlayerInputNumber(1);});
    //}
}
