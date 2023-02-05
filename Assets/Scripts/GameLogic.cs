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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTaxiTaken == false && isClientSpawn == false)
        {
            GeneratePerson();
            isClientSpawn = true;
        }
        PointsText.text = "Score: "+ points.ToString();
    }
    void GeneratePerson()
    {
        Instantiate(client, new Vector3(6, (float)0.15, -16), Quaternion.identity);
    }
    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Client") == true)
        {
            isTaxiTaken = true;
            isClientSpawn = false;
            DestinationText.text = "Take me to City Hall";
            Destroy(target.gameObject);
            Instantiate(destination, new Vector3((float)162.3034, (float)3.162743, (float)38.37389), Quaternion.identity);
        }
        else if (target.gameObject.tag.Equals("Destination") == true)
        {
            isTaxiTaken = false;
            DestinationText.text = "";
            Destroy(target.gameObject);
            points += 1;
        }
    }
}
