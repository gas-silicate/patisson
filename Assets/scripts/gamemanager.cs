using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public float currSPD;
    public Vector3 currentEulerAngles;
    public bool Povorachivaet;
    public WheelRotatingL rotL;
    public WheelRotatingR rotR;
    public RaceCarControl.CarCntrl car;
    public Transform rul;

    private void Start()
    {
        Povorachivaet = false;
    }

    void FixedUpdate()
    {
        if (!Povorachivaet) { currentEulerAngles = new Vector3(0, 0, 0); }
        currSPD = car.currSpeed;
    }

    public void PovorotRulya()
    {
        Povorachivaet = true;
        currentEulerAngles.y = rul.eulerAngles.y;
    }

    public static float WHLrotation(float SpeedMax, float SpeedMin, float curSpeed)
    {
        float r = 0;
        if (curSpeed < (SpeedMin / 2)) { r = -(200 / curSpeed); }
        if (curSpeed > (SpeedMin / 2) && curSpeed < (SpeedMin / 4)) { r = Mathf.Abs(curSpeed); }
        if (curSpeed > (SpeedMin / 4) && curSpeed < (SpeedMax / 4)) { r = curSpeed*curSpeed; }
        if (curSpeed > (SpeedMax / 4) && curSpeed < (SpeedMax / 2)) { r = Mathf.Abs(curSpeed); }
        if (curSpeed > (SpeedMax / 2)) { r = 200 / curSpeed; }
        return r;
    }

}
