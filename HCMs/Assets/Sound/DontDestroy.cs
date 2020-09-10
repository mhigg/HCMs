using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool DontDestroyEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (DontDestroyEnabled)
        {
            // Sceneを遷移してもサウンドが消えないようにする
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
