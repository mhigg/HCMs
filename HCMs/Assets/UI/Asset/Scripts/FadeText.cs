using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    private float speed = 0.02f;
    private float red, green, blue;
    private float alfa = 0f;
    bool fadeflag = false;

    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        if (alfa >= 1f) fadeflag = true;
        if (alfa <= 0f) fadeflag = false;

        if (!fadeflag) alfa += speed;
        else alfa -= speed;
    }
}
