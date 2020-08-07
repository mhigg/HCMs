using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeText : MonoBehaviour
{
    private float speed = 0.02f;
    private float red, green, blue;
    private float alfa = 0f;
    bool fadeflag = false;

    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<TextMeshProUGUI>().color.r;
        green = GetComponent<TextMeshProUGUI>().color.g;
        blue = GetComponent<TextMeshProUGUI>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().color = new Color(red, green, blue, alfa);
        if (alfa >= 1f) fadeflag = true;
        if (alfa <= 0f) fadeflag = false;

        if (!fadeflag) alfa += speed;
        else alfa -= speed;
    }
}
