using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecidedCarModel : MonoBehaviour
{
    [SerializeField] private CarCustom _custom;
    [SerializeField] private List<Image> _arrows;
    [SerializeField] private TextMeshProUGUI _tmPro;

    private float speed = 0.02f;
    private float red, green, blue;
    private float alfa = 0f;
    bool fadeflag = false;


    void Start()
    {
        //red = GetComponent<TextMeshProUGUI>().color.r;
        //green = GetComponent<TextMeshProUGUI>().color.g;
        //blue = GetComponent<TextMeshProUGUI>().color.b;
    }

    void Update()
    {
        if (_custom.GetDecidedFlag)
        {
            _arrows[0].gameObject.SetActive(false);
            _arrows[1].gameObject.SetActive(false);
            _tmPro.gameObject.SetActive(true);
        }
        else
        {
            _arrows[0].gameObject.SetActive(true);
            _arrows[1].gameObject.SetActive(true);
            _tmPro.gameObject.SetActive(false);
        }

        _tmPro.color = new Color(red, green, blue, alfa);
        if (alfa >= 1f) fadeflag = true;
        if (alfa <= 0f) fadeflag = false;

        if (!fadeflag) alfa += speed;
        else alfa -= speed;

    }
}
