using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camanim;

    public void CamShake(string triggerName) {
        camanim.SetTrigger(triggerName);
    }
}
