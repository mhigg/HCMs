using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    //進行方向
    [SerializeField] private float _moveForX;
    [SerializeField] private float _moveForZ;

    private Vector3 _carPosition;
    private Rigidbody _rb;

    void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _carPosition = GetComponent<Transform>().position;
    }

    void FixedUpdate()
    {
        CarMove();
    }

    //移動させる
    private void CarMove()
    {
        _rb.velocity = new Vector3(_moveForX, 0, _moveForZ);
        CarDirection();　　//向きメソッド
    }

    //進行方向を向く
    private void CarDirection()
    {
        Vector3 diff = transform.position - _carPosition;
        //動いているか調べる
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
            RotateTire();　//タイヤ回転メソッド
        }
        //ポジションを更新
        _carPosition = transform.position;
    }

    //４つのtireを回転させる。回転方向の違いがあったので、ｘとｚを入れ替える
    public GameObject[] _tire;
    private void RotateTire()
    {
        foreach (var tire in _tire)
        {
            tire.transform.Rotate(new Vector3(_moveForZ * 4, 0, _moveForX * 4));
        }
    }
}