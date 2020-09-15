using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecidedMultiCarModel : MonoBehaviour
{
    [SerializeField] private CarMultiCustom _multiCustom;
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
        if (_multiCustom.GetDecidedFlag_1 && _multiCustom.GetDecidedFlag_2) _tmPro.gameObject.SetActive(true);
        else _tmPro.gameObject.SetActive(false);

        DecidedUpdate();
        BackToTheOrigin();

        _tmPro.color = new Color(red, green, blue, alfa);
        if (alfa >= 1f) fadeflag = true;
        if (alfa <= 0f) fadeflag = false;

        if (!fadeflag) alfa += speed;
        else alfa -= speed;
    }

    private void DecidedUpdate()
    {
        if (_multiCustom.GetDecidedFlag_1)
        {
            _arrows[0].gameObject.SetActive(false);
            _arrows[1].gameObject.SetActive(false);
        }

        if (_multiCustom.GetDecidedFlag_2)
        {
            _arrows[2].gameObject.SetActive(false);
            _arrows[3].gameObject.SetActive(false);
        }
       
    }

    private void BackToTheOrigin()
    {
        if (!(_multiCustom.GetDecidedFlag_1))
        {
            _arrows[0].gameObject.SetActive(true);
            _arrows[1].gameObject.SetActive(true);
        }

        if (!(_multiCustom.GetDecidedFlag_2))
        {
            _arrows[2].gameObject.SetActive(true);
            _arrows[3].gameObject.SetActive(true);
        }
    }
}
