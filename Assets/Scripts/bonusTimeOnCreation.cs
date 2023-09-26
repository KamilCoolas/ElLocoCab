using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class bonusTimeOnCreation : MonoBehaviour
{
    public GameObject player;
    [HideInInspector]
    public float bonusTime;
    // Start is called before the first frame update
    void Start()
    {
        bonusTime = Vector3.Distance(player.transform.position, transform.position)/6;
    }
}
