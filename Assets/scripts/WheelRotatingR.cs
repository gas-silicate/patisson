using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotatingR : MonoBehaviour
{
    public float whlRotate;
    public static float whlRotationSpd = 15f;
    public const float maxAngleMod = 35f;
    public gamemanager gm;
    public static Vector3 rotateWhl;
    public WheelRotatingL rotL;

    public bool povorachivaetR = false;
    public RaceCarControl.CarCntrl car;


    void FixedUpdate()
    {
        rotateWhl = transform.localEulerAngles;
        float SPD = car.currSpeed;

        whlRotationSpd = gamemanager.WHLrotation(RaceCarControl.CarCntrl.posiSpeed, RaceCarControl.CarCntrl.antiSpeed, SPD);
        if (povorachivaetR && SPD != 0) { gm.currentEulerAngles += new Vector3(0, 1f, 0) * Time.fixedDeltaTime * whlRotationSpd; }

        if (gm.currentEulerAngles.y < -maxAngleMod) { gm.currentEulerAngles.y = -maxAngleMod; }
        if (gm.currentEulerAngles.y > maxAngleMod) { gm.currentEulerAngles.y = maxAngleMod; }
        transform.localEulerAngles = gm.currentEulerAngles;
    }
   
    public void povR()
    {
        if (!rotL.povorachivaetL)
            povorachivaetR = true;
    }
    
    public void povRstop()
    {
        povorachivaetR = false;
    }
}
