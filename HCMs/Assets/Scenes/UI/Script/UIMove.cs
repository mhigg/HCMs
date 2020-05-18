using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    public AnimationCurve animCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public Vector3 _beforePos;  // スライド前の位置
    public Vector3 _afterPos;   // スライド後の位置
    public float _duration;     // スライドする時間
    public float _waitTime;     // スライドまでの待機時間
        
    float startTime = 0;                    // 開始時間
    bool _isfirstFlame = true;          // 開始フレームかどうか
    Vector3 startPos;                   // 開始位置
    Vector3 moveDistance;               // 移動距離および方向

    private void Start()
    {
        transform.localPosition = _beforePos;
    }
    void Update()
    {
        if (_isfirstFlame)
        {
            startPos = transform.localPosition;     // 開始位置
            _isfirstFlame = false;
            _waitTime += Time.time;
        }
        else
        {
            if (Time.time > _waitTime)
            {
                if (startTime == 0)
                {
                    startTime = Time.time;
                }
                if (transform.localPosition.x < _afterPos.x)
                {
                    moveDistance = (_afterPos - startPos);
                    transform.localPosition = startPos + moveDistance * animCurve.Evaluate((Time.time - startTime) / _duration);
                }
            }           
        }
    }
}
