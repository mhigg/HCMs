using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public GoalFlag goalFlag = null;
    string playerID = "P1";     // プレイヤーごとに持っていて、プレイヤーから渡されるのが理想(タイムアタックは１つだからその限りではないが)

    // Start is called before the first frame update
    void Start()
    {
        goalFlag = goalFlag.GetComponent<GoalFlag>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RacingCar")
        {
            Debug.Log("チェックポイント通過");
            goalFlag.CheckPointCount(playerID);
        }
    }
}
