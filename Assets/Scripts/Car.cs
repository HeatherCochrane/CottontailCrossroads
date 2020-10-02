using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    float x = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(x, 0, 0);
    }

    public void setDirection(bool right)
    {
        if(right)
        {
            x = -0.1f;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            x = 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
