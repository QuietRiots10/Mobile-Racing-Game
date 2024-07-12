using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//InConnector must always be child 0 of the prefab
//OutConnector must always be child 0 of InConnector

public class RandomGenerationScript : MonoBehaviour
{
    //Variables
    public GameObject TrackParent;
    //How many parts have been successfully generated
    public int GenCount = 0;
    public TutorialAnimation TutorialScript;

    //Position and direction to generate in next
    Vector3 GenerateFrom;
    Vector3 GenerateDir;

    //Chances for each part (Generator goes from 100-0, parts with higher numbers have priority. A part is selected if the random output is above it's chance, but below all the chances above it)
    int Straight1Chance = 90;
    int Straight2Chance = 70;
    int Left90Chance = 35;
    int Right90Chance = 0;

    //Methods

    //Called from the triggers of level parts, and generates 1 random track piece
    public void StartGeneration()
    {
        Generate(RandomPart());
    }

    //Selects a random part based on the predefined chances
    //Returns the name of the selected part prefab as a string
    public string RandomPart()
    {
        int RandNumber = Random.Range(0, 100);

        if (Straight1Chance <= RandNumber)
        {
            return "Straight1";
        }
        else if (Straight2Chance <= RandNumber)
        {
            return "Straight2";
        }
        else if (Left90Chance <= RandNumber)
        {
            return "Left90Turn";
        }
        else if (Right90Chance <= RandNumber)
        {
            return "Right90Turn";
        }
        else
        {
            return RandomPart();
        }
    }

    //Instantiates and places the desired part
    //Fails to generate if the next part would be too close to another part (Genration resumes the next frame)
    //Inputs the name of the part to generate (returned from RandomPart())
    public void Generate(string PartName)
    {
        //Instantiate the new part
        GameObject GeneratedPart = Instantiate(Resources.Load("Prefabs/Track Part Prefabs/" + PartName)) as GameObject;
        GeneratedPart.name = GenCount.ToString();
        GeneratedPart.transform.SetParent(TrackParent.transform);
        GeneratedPart.AddComponent<UnloadTrackScript>();

        //Set position and direction of GeneratedPart's InConnector to GenerateFrom
        GeneratedPart.transform.GetChild(0).position = GenerateFrom;
        GeneratedPart.transform.GetChild(0).forward = GenerateDir;

        //Draw a raycast from the part's InConnector to the OutConnector, checking for collisions with other GameObjects
        if (!Physics.Raycast(GeneratedPart.transform.GetChild(0).GetChild(0).position, GeneratedPart.transform.GetChild(0).GetChild(0).forward * 50)) //45
        {            
            //Get the new position and direction to generate from (From the position of GeneratedPart's OutConnector)
            GenerateFrom = GeneratedPart.transform.GetChild(0).GetChild(0).position;
            GenerateDir = GeneratedPart.transform.GetChild(0).GetChild(0).forward;

            //Increase GenCount by 1
            GenCount++;
        }

        //If they intersect, step back and generate next frame
        else
        {
            Debug.Log("Generation Intersection");
            Destroy(GeneratedPart);
        }
    }

    //Awake
    private void Awake()
    {
        GenerateFrom = GameObject.Find("StartStraight").transform.GetChild(0).position;
        GenerateDir = GameObject.Find("StartStraight").transform.GetChild(0).forward;

        Debug.Log(PlayerPrefs.GetInt("DoneTutorial"));
        //Do the tutorial if the player has not done it before
        if (PlayerPrefs.GetInt("DoneTutorial") == 0 && GenCount < 15)
        {
            //Tutorial
            Debug.Log("Starting tutorial...");
            Generate("Straight1");
            Generate("Right90Turn");
            Generate("Straight1");
            Generate("Left90Turn");
            Generate("Straight1");

            //9 more
            Generate("Left90Turn");
            Generate("Right90Turn");
            Generate("Straight2");
            Generate("Right90Turn");
            Generate("Straight1");
            Generate("Left90Turn");
            Generate("Straight2");
            Generate("Straight2");
            Generate("Straight1");
        }
    }

    //Update
    private void Update()
    {
        //Generate starting track
        if (GenCount < 15)
        {
            Generate(RandomPart());
        }

        //Hints for the tutorial
        if (PlayerPrefs.GetInt("DoneTutorial") != 0)
        {
            //nothing
        }
        else if (GenCount == 16 && PlayerPrefs.GetInt("DoneTutorial") == 0)
        {
            TutorialScript.StartCoroutine("AnimationStart", "Right");
        }
        else if (GenCount == 17 && PlayerPrefs.GetInt("DoneTutorial") == 0)
        {
            TutorialScript.StartCoroutine("AnimationEnd");
        }
        else if (GenCount == 18 && PlayerPrefs.GetInt("DoneTutorial") == 0)
        {
            TutorialScript.StartCoroutine("AnimationStart", "Left");
        }
        else if (GenCount > 18 && PlayerPrefs.GetInt("DoneTutorial") == 0)
        {
            TutorialScript.StartCoroutine("AnimationEnd");
            PlayerPrefs.SetInt("DoneTutorial", 1);
            Debug.Log("Tutorial finished...");
        }
    }
}