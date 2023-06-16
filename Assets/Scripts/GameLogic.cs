using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    bool isTaxiTaken = false;
    bool isClientSpawn = false;
    int points = 0;
    public float timerTime = 50;
    int time = 0;
    public GameObject player;
    public GameObject client;
    public GameObject destination;
    public Text DestinationText;
    public Text PointsText;
    public Text TimeText;
    public Text Distance;
    Vector3[] CityPoints;
    string[] CityPointsNames;
    public static bool gameOver = false;
    float timeCounter;

    void Start()
    {
        CityPoints = new Vector3[]
        {
              GameObject.Find("Factory").transform.position,
              GameObject.Find("University").transform.position,
              GameObject.Find("CityHall").transform.position,
              GameObject.Find("TreeFarm").transform.position,
              GameObject.Find("School").transform.position,
              GameObject.Find("PowerPlant").transform.position,
              GameObject.Find("Mine").transform.position,
              GameObject.Find("House1").transform.position,
              GameObject.Find("House2").transform.position,
              GameObject.Find("House3").transform.position,
              GameObject.Find("FurnitureStore").transform.position,
              GameObject.Find("House4").transform.position,
              GameObject.Find("CarDealer").transform.position,
              GameObject.Find("Cinema").transform.position,
              GameObject.Find("Mall").transform.position,
              GameObject.Find("Factory").transform.position,
              GameObject.Find("House5").transform.position
        };
        CityPointsNames = new string[]
        {
              "Factory",
              "University",
              "City Hall",
              "Tree Farm",
              "School",
              "Power Plant",
              "Mine",
              "House",
              "House",
              "House",
              "Furniture Store",
              "House",
              "Car Dealer",
              "Cinema",
              "Mall",
              "Factory",
              "House"
        };
    }

    void Update()
    {
        if (!gameOver)
        {
            if (!isTaxiTaken && !isClientSpawn)
            {
                RandomiseClient();
                isClientSpawn = true;
            }
            if (isClientSpawn)
            {
                checkTargetMet(GameObject.FindWithTag("Client"));
            }
            if (isTaxiTaken)
            {
                checkTargetMet(GameObject.FindWithTag("Destination"));
            }
            timerTime -= Time.deltaTime;
            time = (int)timerTime;
            PointsText.text = "Score: " + points.ToString();
            TimeText.text = "Time: " + time.ToString();
            if (isTaxiTaken && PrometeoCarController.carSpeed >= 60)
            {
                timeCounter += Time.deltaTime;
            }
            else timeCounter = 0;
            if (timeCounter > 2)
            {
                points += 1;
                timeCounter = 0;
            }
            if (time <= 0)
            {
                DestinationText.text = "GAME OVER" + Environment.NewLine + "Click Space to Restart";
                gameOver = true;
            }
        }
        if (gameOver && (Input.GetKey(KeyCode.Space)))
        {
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    //private void OnCollisionEnter(Collision target)
    //{
    //    if (target.gameObject.tag.Equals("Client"))
    //    {
    //        isTaxiTaken = true;
    //        isClientSpawn = false;
    //        RandomiseDestination();
    //        Destroy(target.gameObject);
    //        timerTime += 30;
    //    }
    //    else if (target.gameObject.tag.Equals("Destination"))
    //    {
    //        isTaxiTaken = false;
    //        DestinationText.text = "Look for new Client";
    //        Destroy(target.gameObject);
    //        points += 1;
    //        timerTime += 30;
    //    }
    //}
    private void checkTargetMet(GameObject target)
    {
        int distance = distanceToTarget(target);
        Distance.text = "Distance: " + distance.ToString();
        if (distance < 3 && PrometeoCarController.carSpeed <= 5)
        {
            if (target.tag.Equals("Client"))
            {
                isTaxiTaken = true;
                isClientSpawn = false;
                RandomiseDestination();
                bonusTimeOnCreation targetScript = target.GetComponent<bonusTimeOnCreation>();
                int bonusTime = (int)targetScript.bonusTime;
                Destroy(target);
                timerTime += bonusTime;
            }
            else if (target.tag.Equals("Destination"))
            {
                isTaxiTaken = false;
                DestinationText.text = "Look for new Client";
                bonusTimeOnCreation targetScript = target.GetComponent<bonusTimeOnCreation>();
                int bonusTime = (int)targetScript.bonusTime;
                Destroy(target);
                points += 1;
                timerTime += bonusTime;
            }
        }
    }
    private int distanceToTarget(GameObject target)
    {
        float distance = Vector3.Distance(player.transform.position, target.transform.position);
        int idistance = (int)distance;
        return idistance;
    }
    private void RandomiseDestination()
    {
        int rnd = Random.Range(0, 16);
        Instantiate(destination, CityPoints[rnd], Quaternion.identity);
        DestinationText.text = "Take me to " + CityPointsNames[rnd];
    }
    private void RandomiseClient()
    {
        int rnd = Random.Range(0, 16);
        Instantiate(client, CityPoints[rnd], Quaternion.identity);
    }
}
