using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DOOR : MonoBehaviour
{
    public GameObject Player;
    public GameObject Car;
    
    public Component TrigCollider;
    bool InCar = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("the_hand"))
        {
            InCar = true;
        }
    }

    void Update()
    {
        if(InCar)
        {
            Vector3 carPOS = Car.transform.position;
            Player.transform.localPosition = carPOS + new Vector3(2.6f,0,1.8f);
        }
    }
}
