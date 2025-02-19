using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTATE : MonoBehaviour
{
    public gamemanager gm;
    public RaceCarControl.CarCntrl car;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float SPDframe = car.currSpeed * Time.fixedDeltaTime;

        transform.localEulerAngles += new Vector3(KolesaDEG(0.5f, SPDframe), 0, 0);
    }
    static float KolesaDEG(float r, float spd)
    {
        float deg = 0;
        deg = (360 * spd) / (2 * r * Mathf.PI);
        return deg;
    }
}
