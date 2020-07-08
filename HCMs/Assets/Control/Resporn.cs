using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Resporn : MonoBehaviour
{
    Transform _carTrans;
    public int _num;
    public GameObject _resporns;
    int _respornNum = 0;
    Transform _resTra;
    // Start is called before the first frame update
    void Start()
    {
        _carTrans = this.transform;
        _respornNum = _resporns.transform.childCount;
    }
    // Update is called once per frame
    void Update()
    {
        if(_carTrans.position.y <= 3.0)
        {
            _carTrans.position = _resTra.position;
            _carTrans.rotation = _resTra.rotation;
            //var num = SetRespornPoint();
            //_carTrans.position = _resporns.transform.GetChild(num).position;
            //_carTrans.rotation = _resporns.transform.GetChild(num).rotation;
        }
    }
    int SetRespornPoint()
    {       
        Vector3 carPosition = _carTrans.position;
        float range = GetHypotenuse(carPosition, _resporns.transform.GetChild(0).transform.position);
        int num = 0;
        // リスポーンポイント分回して現在地との差が小さいところでリスポーンする。
        for (int i = 1;i < _respornNum; i++)
        {
            var tmp = GetHypotenuse(carPosition, _resporns.transform.GetChild(i).transform.position);
            if (tmp < range)
            {
                range = tmp;
                num = i;
            }
        }
        return num;
    }

    float GetHypotenuse(Vector3 vec1,Vector3 vec2)
    {
        float ret = 0.0f;
        Vector2 tmp = new Vector2(0,0);
        tmp.x = vec1.x - vec2.x;
        ret = Mathf.Sqrt((tmp.x * tmp.x) + (tmp.y * tmp.y));
        return ret;
    }
    public void SetResBox(Transform tra,int i)
    {
        if (i == _num)
        {
            _resTra = tra;
        }
    }
}
