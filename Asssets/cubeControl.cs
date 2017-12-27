using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeControl : MonoBehaviour {

    public enum cubeMode {
        init,
        close,
        noraml,
    };

    public cubeMode nowMode = cubeMode.init;
    public GameObject obj;
    private bool bNormalDis = false;
    private float normalDistime = 0;

    // Use this for initialization
    void Start () {
    }        
	
	// Update is called once per frame
	void Update () {
        if (bNormalDis && (Time.time - normalDistime) > 3) {
            bNormalDis = false;
            changeMode(cubeMode.close);
        }
	}

    public void changeMode(cubeMode newMode)
    {
        if (newMode == cubeMode.noraml)
        {
            bNormalDis = false;
            obj.transform.localScale = Vector3.one;
        }
        else if(newMode == cubeMode.close)
        {
            bNormalDis = false;
            obj.transform.localScale = Vector3.zero;
        }
        else
        {
            obj.transform.localScale = Vector3.zero;
        }
        nowMode = newMode;
    }

    public bool checkLive()
    {
        if (nowMode == cubeMode.noraml)
        {
            bNormalDis = true;
            normalDistime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
}
