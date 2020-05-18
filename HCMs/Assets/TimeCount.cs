using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public Text timeText = null;

    // Start is called before the first frame update
    void Start()
    {
        timeText = timeText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // 毎ﾌﾚｰﾑごとにﾀｲﾑ加算
        timeText.text = Time.time.ToString();
    }
}
