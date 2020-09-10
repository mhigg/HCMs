using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualOrAllCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("CPU").Length > 0)
        {
            transform.FindChild("DualScreenCanvas").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("AllScreenCanvas").gameObject.SetActive(false);
        }
    }
}
