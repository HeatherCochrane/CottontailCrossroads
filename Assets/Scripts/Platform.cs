using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    List<GameObject> cars = new List<GameObject>();

    [SerializeField]
    Transform left;

    [SerializeField]
    Transform right;

    Vector3 spawn;
    GameObject car;
    bool startRight = false;

    [SerializeField]
    List<Sprite> carSprites = new List<Sprite>();


    GameObject player;

    Audio audioScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerHandle");
        audioScript = GameObject.Find("Audio").GetComponent<Audio>();

        int ran = Random.Range(0, 10);
        if (ran > 5)
        {
            spawn = right.position;
            startRight = true;
        }
        else
        {
            spawn = left.position;
            startRight = false;
        }

        spawnCar();
        checkSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnCar()
    {
      
        car = Instantiate(cars[Random.Range(0, cars.Count)]);
        car.GetComponent<SpriteRenderer>().sprite = carSprites[Random.Range(0, carSprites.Count)];
        car.transform.position = spawn;
        car.transform.SetParent(this.transform);
        car.GetComponent<Car>().setDirection(startRight);

        Invoke("spawnCar", Random.Range(1, 4));
    }

    void checkSound()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 5)
        {
            audioScript.playCarNoise(this.GetComponent<AudioSource>());
        }

        Invoke("checkSound", 2);
    }

}
