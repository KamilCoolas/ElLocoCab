using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    bool isTaxiTaken = false;
    bool isClientSpawn = false;
    int points = 0;
    public GameObject client;
    public GameObject destination;
    public Text DestinationText;
    public Text PointsText;
    Vector3[] CityPoints;
    string[] CityPointsNames;
    
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
              GameObject.Find("House3").transform.position
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
              "House"
        };
    }

    void Update()
    {
        if (!isTaxiTaken && !isClientSpawn )
        {
            RandomiseClient();
            isClientSpawn = true;
        }
        PointsText.text = "Score: "+ points.ToString();
    }
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Client"))
        {
            isTaxiTaken = true;
            isClientSpawn = false;
            RandomiseDestination();
            Destroy(target.gameObject);
        }
        else if (target.gameObject.tag.Equals("Destination"))
        {
            isTaxiTaken = false;
            DestinationText.text = "Look for new Client";
            Destroy(target.gameObject);
            points += 1;
        }
    }
    private void RandomiseDestination()
    {
        int rnd = Random.Range(0, 10);
        Instantiate(destination, CityPoints[rnd], Quaternion.identity);
        DestinationText.text = "Take me to " + CityPointsNames[rnd];
    }
    private void RandomiseClient()
    {
        int rnd = Random.Range(0, 10);
        Instantiate(client, CityPoints[rnd], Quaternion.identity);
    }
}
