using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class GearDisplay : MonoBehaviour
{
    public ImageNo imageNo = null;

    [SerializeField] private Text m_GearTex = null;
    [SerializeField] private CarController m_Car = null;
    private int m_GearNum;

    // Start is called before the first frame update
    void Start()
    {
        m_Car = m_Car.GetComponent<CarController>();
        m_GearTex = m_GearTex.GetComponent<Text>();

        imageNo = imageNo.GetComponent<ImageNo>();
    }

    // Update is called once per frame
    void Update()
    {
        m_GearNum = m_Car.CurrentGearNum;
//        m_GearTex.text = $"{m_Car.CurrentGearNum + 1}";
        imageNo.SetNo(m_Car.CurrentGearNum + 1);
    }
}
