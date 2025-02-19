using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotating : MonoBehaviour
{
    public float whlRotate;
    public static float whlRotationSpd = 12f;
    public const float maxAngleMod = 35f;
    public gamemanager gm;
    public GameObject LWheel;
    public GameObject RWheel;
    public static Vector3 rotateWhl;

    public bool povorachivaetL = false;
    public bool povorachivaetR = false;
    public RaceCarControl.CarCntrl car;


    void FixedUpdate()
    {
        gm.Povorachivaet = (povorachivaetL || povorachivaetR);

        rotateWhl = transform.localEulerAngles;
        float SPD = car.currSpeed;

        whlRotate = 4f/*gamemanager.WHLrotation(RaceCarControl.CarCntrl.posiSpeed, RaceCarControl.CarCntrl.antiSpeed, SPD)*/;
        if (povorachivaetL) { gm.currentEulerAngles += new Vector3(0, -whlRotate, 0) * Time.fixedDeltaTime /* whlRotationSpd*/; }
        if (povorachivaetR) { gm.currentEulerAngles += new Vector3(0, whlRotate, 0) * Time.fixedDeltaTime /* whlRotationSpd*/; }

        if (gm.currentEulerAngles.y < -maxAngleMod) { gm.currentEulerAngles.y = -maxAngleMod; }
        if (gm.currentEulerAngles.y > maxAngleMod) { gm.currentEulerAngles.y = maxAngleMod; }
        LWheel.transform.localEulerAngles = gm.currentEulerAngles;
        RWheel.transform.localEulerAngles = gm.currentEulerAngles;
    }
    public void povL()
    {
        if (!povorachivaetR)
            povorachivaetL = true;
    }
    public void povR()
    {
        if (!povorachivaetL)
            povorachivaetR = true;
    }
    public void povLstop()
    {
        povorachivaetL = false;
    }
    public void povRstop()
    {
        povorachivaetR = false;
    }
}
