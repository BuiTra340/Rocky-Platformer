using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityMap : MonoBehaviour
{
    public Transform player;
    public float currentDis;
    public float limitDis;
    public float RespawnDis;


    // Update is called once per frame
    void Update()
    {
        getDistance();
        RespawnGround();
    }
    private void RespawnGround()
    {
        if (currentDis < limitDis) return;
        Vector3 pos = transform.position;
        pos.x += RespawnDis;
        transform.position = pos;
    }
    private void getDistance()
    {
        currentDis = player.position.x - transform.position.x;
    }
}
