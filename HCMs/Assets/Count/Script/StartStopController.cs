using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RigidbodyVelocity
{
    public Vector3 velocity;//速度
    public Vector3 angularVelocity;//角速度
    public RigidbodyVelocity(Rigidbody rigidbody)
    {
        velocity = rigidbody.velocity;
        angularVelocity = rigidbody.angularVelocity;
    }
}

public class StartStopController : MonoBehaviour
{
    public bool startWait;
    bool prevWaiting;
    public GameObject[] ignoreGameObjects;
    RigidbodyVelocity[] rigidbodyVelocity;
    Rigidbody[] waitingRigidbody;
    MonoBehaviour[] waitingMonoBehaviour;

    public Text countText;
    int frameCount = 0;

    void Update()
    {
        startWait = (frameCount < 400);
        if(prevWaiting != startWait)
        {
            if (startWait)
            {
                StartCounting();
            }
            if (!startWait)
            {
                EscapingCount();
                countText.text = "";//スタート後テキストを削除
            }
            prevWaiting = startWait;
        }
        ++frameCount;

        StartText();
    }

    ///特定のオブジェクトの座標情報を保存し、停止させる
    void StartCounting()
    {
        Predicate<Rigidbody> rigidbodyPredicate =
            obj => !obj.IsSleeping()
            && Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        waitingRigidbody = Array.FindAll(transform.GetComponentsInChildren<Rigidbody>(), rigidbodyPredicate);
        rigidbodyVelocity = new RigidbodyVelocity[waitingRigidbody.Length];
        for (int i = 0; i < waitingRigidbody.Length; i++)
        {
            //速度、角度を保存
            rigidbodyVelocity[i] = new RigidbodyVelocity(waitingRigidbody[i]);
            waitingRigidbody[i].Sleep();
        }

        Predicate<MonoBehaviour> monoBehaviourPredicate =
            obj => obj.enabled && obj != this
            && Array.FindIndex(ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        waitingMonoBehaviour = Array.FindAll(
            transform.GetComponentsInChildren<MonoBehaviour>(),
            monoBehaviourPredicate);
        foreach (var monoBehaviour in waitingMonoBehaviour)
        {
            monoBehaviour.enabled = false;
        }
    }

    ///停止からの復帰
    void EscapingCount()
    {
        for(int i = 0; i < waitingRigidbody.Length; i++)
        {
            waitingRigidbody[i].WakeUp();
            waitingRigidbody[i].velocity = rigidbodyVelocity[i].velocity;
            waitingRigidbody[i].angularVelocity = rigidbodyVelocity[i].angularVelocity;
        }
        foreach (var monoBehaviour in waitingMonoBehaviour)
        {
            monoBehaviour.enabled = true;
        }
    }

    ///スタートカウント
    void StartText()
    {
        if(frameCount <= 120)
        {
            if (frameCount % 40 == 0)
            {
                countText.text += ".";
            }
        }
        else
        {
            if (frameCount == 180) countText.text = "３";
            if (frameCount == 240) countText.text = "２";
            if (frameCount == 300) countText.text = "１";
            if (frameCount == 360) countText.text = "ＳＴＡＲＴ！！！";
        }
    }
}
