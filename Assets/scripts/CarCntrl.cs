using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RaceCarControl
{
    public class CarCntrl : MonoBehaviour
    {
        public bool gazuet, tormozit, dvizenie, stoit;
        public const float acc = 8f;
        float accTime;
        public gamemanager gm;

        public const float posiSpeed = 80f;
        public const float antiSpeed = -20f;
        public float currSpeed;
        public TMP_Text speedText;

        void Start()
        {
            gazuet = false;
            tormozit = false;
            dvizenie = false;
            stoit = true;
            currSpeed = 0f;
        }

        void FixedUpdate()
        {
            stoit = !(gazuet || tormozit || dvizenie);
            if (currSpeed < -8 || currSpeed > 8) { dvizenie = true; }
            speedText.text = currSpeed.ToString();

            Vector3 rotateCar = transform.eulerAngles;
            if (dvizenie && currSpeed > 0 && gm.Povorachivaet) { rotateCar.y -= Time.deltaTime * -gm.currentEulerAngles.y; }
            if (dvizenie && currSpeed > 0 && gm.Povorachivaet) { rotateCar.y += Time.deltaTime * gm.currentEulerAngles.y; }
            if (dvizenie && currSpeed < 0 && gm.Povorachivaet) { rotateCar.y -= Time.deltaTime * gm.currentEulerAngles.y; }
            if (dvizenie && currSpeed < 0 && gm.Povorachivaet) { rotateCar.y += Time.deltaTime * -gm.currentEulerAngles.y; }

            if (gazuet && accTime <= currSpeed/acc) { accTime += Time.deltaTime*10; }
            if (tormozit && accTime>=currSpeed/acc) { accTime -= Time.deltaTime*10; }

            currSpeed = acc * accTime;
            if (currSpeed > posiSpeed) { currSpeed = posiSpeed; }
            if (currSpeed < antiSpeed) { currSpeed = antiSpeed; }


            float ugolCar = Mathf.Deg2Rad * rotateCar.y;
            transform.Translate(Vector3.forward * currSpeed * Time.fixedDeltaTime);
            //transform.position += transform.forward * currSpeed * Time.fixedDeltaTime;
            //transform.position += new Vector3(currSpeed * Time.fixedDeltaTime * Mathf.Sin(ugolCar), 0, currSpeed * Time.fixedDeltaTime * Mathf.Cos(ugolCar));
            transform.rotation = Quaternion.Euler(rotateCar);
        }

        public void gaz()
        {
            if(!tormozit)
                gazuet = true;
        }
        public void tormoz()
        {
            if (!gazuet)
                tormozit = true;
        }

        public void gazStop()
        {
            gazuet = false;
        }
        public void tormozStop()
        {
            tormozit = false;
        }
    }
}