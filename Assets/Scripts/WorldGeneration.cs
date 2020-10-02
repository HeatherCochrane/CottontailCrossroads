using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField]
    List<Sprite> safePlatforms = new List<Sprite>();


    [SerializeField]
    List<Sprite> unsafePlatforms = new List<Sprite>();

    [SerializeField]
    List<Sprite> snowSafePlatforms = new List<Sprite>();

    [SerializeField]
    List<Sprite> snowUnsafePlatforms = new List<Sprite>();

    [SerializeField]
    List<Sprite> safeFallSprites = new List<Sprite>();

    [SerializeField]
    List<Sprite> safeSummerSprites = new List<Sprite>();

    List<Sprite> safeWorldSprites = new List<Sprite>();
    List<Sprite> unsafeWorldSprites = new List<Sprite>();

    [SerializeField]
    List<Sprite> finishSprites = new List<Sprite>();

    [SerializeField]
    List<Sprite> backgroundSprites = new List<Sprite>();


    [SerializeField]
    GameObject safePlatform;

    [SerializeField]
    GameObject unsafePlatform;

    GameObject platform;
    float yPos = -3.2f;

    List<GameObject> spawnedPlatforms = new List<GameObject>();

    int tracker = 0;

    int level = 0;

    [SerializeField]
    GameObject finishLine;

    GameObject finish;


    //Levels
    int[] platformOrder1 = { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0};
    int[] platformOrder2 = { 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 };
    int[] platformOrder3 = { 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0};
    int[] platformOrder4 = { 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0,0 ,1, 1, 1, 1, 0, 0, 1, 0, 1,1, 0, 0, 1, 0, 0, 1, 1, 1, 0,0, 0, 1, 0, 1,1, 1, 1, 0, 0, 1, 0,0, 0,1,1, 1, 1, 0, 0, 1, 0, 0, 1, 1,};
    int[] platformOrder5 = { 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0};
    int[] platformOrder6 = { 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0};
   
    [SerializeField]
    List<int[]> levels = new List<int[]>();
    int[] currentLevel;

    bool spawnedFinish = false;

    [SerializeField]
    UI ui;

    [SerializeField]
    GameObject blood;

    GameObject bloodSplatter;

    [SerializeField]
    GameObject bloodParent;

    List<int> unlockedLevels = new List<int>();

    bool endlessMode = false;

    [SerializeField]
    GameObject background;

    Sprite finishSprite;

    // Start is called before the first frame update
    void Start()
    {                    
        levels.Add(platformOrder1);
        levels.Add(platformOrder2);
        levels.Add(platformOrder3);
        levels.Add(platformOrder4);
        levels.Add(platformOrder5);
        levels.Add(platformOrder6);

        unlockedLevels.Add(0);
        setLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startGame()
    {
        restartLevel(false);
    }

    public void spawnNewPlatform()
    {
        if (endlessMode)
        {
            spawnEndlessPlatform();
        }
        else
        {
            if (tracker == currentLevel.Length - 1 && !spawnedFinish)
            {
                finish = Instantiate(finishLine);
                finish.transform.position = new Vector3(0, yPos, 0);
                finish.GetComponent<SpriteRenderer>().sprite = finishSprite;
                finish.transform.SetParent(this.transform);
                spawnedFinish = true;
            }

            if (tracker < currentLevel.Length - 1)
            {
                if (currentLevel[tracker] == 0)
                {
                    spawnSafePlatform();
                }
                else
                {
                    spawnUnsafePlatform();
                }
            }

            if (spawnedPlatforms.Count > 10)
            {
                removePlatform();
            }
        }
       
    }

    void spawnEndlessPlatform()
    {
        int ran = Random.Range(0, 10);

        if (ran > 5)
        {
            spawnSafePlatform();
        }
        else
        {
            spawnUnsafePlatform();
        }


        if (spawnedPlatforms.Count > 10)
        {
            removePlatform();
        }

        ui.updateScore();
    }

    void spawnSafePlatform()
    {
        int ran = Random.Range(0, safePlatforms.Count);
        platform = Instantiate(safePlatform);
        platform.GetComponent<SpriteRenderer>().sprite = safeWorldSprites[Random.Range(0, safeWorldSprites.Count)];
        platform.transform.position = new Vector3(0, yPos, 0);
        platform.transform.SetParent(this.transform);
        spawnedPlatforms.Add(platform);

        tracker += 1;
        yPos += 2.4f;

    }

    void spawnUnsafePlatform()
    {
        int ran = Random.Range(0, unsafePlatforms.Count);
        platform = Instantiate(unsafePlatform);
        platform.GetComponent<SpriteRenderer>().sprite = unsafeWorldSprites[Random.Range(0, unsafeWorldSprites.Count)];
        platform.transform.position = new Vector3(0, yPos, 0);
        platform.transform.SetParent(this.transform);
        spawnedPlatforms.Add(platform);

        tracker += 1;
        yPos += 2.4f;
    }

    public void removePlatform()
    {
        if (spawnedPlatforms.Count > 0)
        {
            GameObject p = spawnedPlatforms[0];
            spawnedPlatforms.RemoveAt(0);
            Destroy(p);
        }
    }

    public void restartLevel(bool finished)
    {
        destroyLevel();

        if (level < levels.Count && !endlessMode)
        {
            currentLevel = levels[level];

            for (int i = 0; i < 5; i++)
            {
                spawnNewPlatform();
            }
        }

        if(finished)
        {
            for(int i =0; i < bloodParent.transform.childCount; i++)
            {
                Destroy(bloodParent.transform.GetChild(i).gameObject);
            }

            int t = 0;

            for(int j =0; j < unlockedLevels.Count; j++)
            {
                if(unlockedLevels[j] == level)
                {
                    t += 1;
                }
            }

            if(t ==0)
            {
                unlockedLevels.Add(level);
            }

            ui.updateButtonUnlocks(level);
        }

        if(endlessMode)
        {
            for (int i = 0; i < bloodParent.transform.childCount; i++)
            {
                Destroy(bloodParent.transform.GetChild(i).gameObject);
            }


            level = Random.Range(0, 5);
            setLevelSprites();

            for (int i = 0; i < 5; i++)
            {
                spawnSafePlatform();
            }

        }
        
        spawnedFinish = false;
    }

    public void destroyLevel()
    {
        yPos = -3.2f;
        tracker = 0;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        Destroy(finish);

        spawnedPlatforms.Clear();
    }

    public void removeBlood()
    {
        for (int i = 0; i < bloodParent.transform.childCount; i++)
        {
            Destroy(bloodParent.transform.GetChild(i).gameObject);
        }
    }

    public void setLevel(int i)
    {
        level = i;
        currentLevel = levels[level];
        ui.setLevel(level);

        setLevelSprites();
    }

    public void spawnBlood(Vector3 pos)
    {
        int track = 0;

        for(int i =0; i < bloodParent.transform.childCount;i ++)
        {
            if(bloodParent.transform.GetChild(i).position == pos)
            {
                track += 1;
            }
           
        }

        if (track == 0)
        {
            bloodSplatter = Instantiate(blood);
            bloodSplatter.transform.position = pos;
            bloodSplatter.transform.SetParent(bloodParent.transform);

            if(bloodParent.transform.childCount > 10)
            {
                Destroy(bloodParent.transform.GetChild(0).gameObject);
            }
        }



    }

    public void nextLevel()
    {
        level += 1;

        if (level < levels.Count)
        {
            currentLevel = levels[level];
            ui.setLevel(level);

            setLevelSprites();
        }
        else
        {
            Debug.Log("Level out of range");
        }
    }
    public void setLevelSprites()
    {
        switch (level)
        {
            case 0:
                safeWorldSprites = safePlatforms;
                unsafeWorldSprites = unsafePlatforms;
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[level];
                finishSprite = finishSprites[level];
                break;
            case 1:
                safeWorldSprites = safeFallSprites;
                unsafeWorldSprites = unsafePlatforms;
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[level];
                finishSprite = finishSprites[level];
                break;
            case 2:
                safeWorldSprites = snowSafePlatforms;
                unsafeWorldSprites = snowUnsafePlatforms;
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[level];
                finishSprite = finishSprites[level];
                break;
            case 3:
                safeWorldSprites = safeSummerSprites;
                unsafeWorldSprites = unsafePlatforms;
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[level];
                finishSprite = finishSprites[level];
                break;
            case 4:
                safeWorldSprites = safePlatforms;
                unsafeWorldSprites = unsafePlatforms;
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[0];
                finishSprite = finishSprites[0];
                break;
            case 5:
                safeWorldSprites = snowSafePlatforms;
                unsafeWorldSprites = snowUnsafePlatforms;
                background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[2];
                finishSprite = finishSprites[2];
                break;
        }

      
    }

    public void setEndlessMode(bool set)
    {
        endlessMode = set;
    }
}
