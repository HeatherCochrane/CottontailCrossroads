using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hop()
    {
        this.GetComponent<Animator>().StopPlayback();
        this.GetComponent<Animator>().SetTrigger("Hop");
    }

    public void stop()
    {
        this.GetComponent<Animator>().SetTrigger("Stop");
    }
}
