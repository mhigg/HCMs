using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resporn : MonoBehaviour
{
    Transform _carTrans;            // 現在の車体の情報
    Rigidbody _carRigid;            // 車のrigidbody  
    Transform _resTra;              // 現在のリスポーン地点の情報
    public GameObject _resporns;
    public int _playerNum = 0;
    int _respornNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        _carTrans = this.transform;
        _carRigid = this.GetComponent<Rigidbody>();
        _respornNum = _resporns.transform.childCount;
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent.name == "RacingCar1P")
        {
            _playerNum = 1;
        }
        else if (this.transform.parent.name == "RacingCar2P")
        {
            _playerNum = 2;
        }
        var back = "Back_" + _playerNum;
        if (Input.GetButtonDown(back))
        {
            _carTrans.position = _resTra.position;
            _carTrans.rotation = _resTra.rotation;
            _carRigid.velocity = new Vector3(0, 0, 0);
        }
    }
    public void SetResBox(Transform tra, int i)
    {
        // 後で消します。意味ないです
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.parent.name == "CheckPoint")
        {
            _resTra = col.transform;
        }
    }
}
