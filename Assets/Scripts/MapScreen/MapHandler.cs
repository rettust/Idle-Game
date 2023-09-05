using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour, IDataPersistence
{
    public GameObject[] planets;
    [SerializeField] private PlanetData[] planetData;
    public GameObject playerShip;
    private Coroutine coroutine;
    public TMP_Text distanceText;
    public GameObject[] travelButtonGO;
    public Button[] travelButtons;
    public TMP_Text[] buttonText;
    private Vector3 destination;
    private bool isDocked;
    private bool isShipTraveling;
    private Vector3 shipPos;
    private string planetDestination;



    void Start()
    {
        for(int i = 0; i < travelButtonGO.Length; i++){
            travelButtons[i] = travelButtonGO[i].GetComponent<Button>();
            buttonText[i] = travelButtonGO[i].GetComponentInChildren<TMP_Text>();
        }
        planetData = new PlanetData[planets.Length];
        for(int i = 0; i < planets.Length; i++){
            planetData[i] = planets[i].GetComponent<PlanetData>();
        }
    }


    private void Update()
    {
        ButtonDistanceText();
        PlanetRotation();
    }

    public void PlanetRotation()
    {
        foreach(GameObject _planet in planets)
        {

            foreach(PlanetData _pData in planetData)
            {
                if (_pData.planet == _planet)
                {
                    _pData.rotationConst += _pData._finalRotationSpeed;
                    float x;
                    float y;
                    float sanitycheckx;
                    x = _pData.vertex1 + (_pData.covertex1 * Mathf.Cos(_pData.rotationConst *.005f));
                    y = _pData.vertex2 + (_pData.covertex2 * Mathf.Sin(_pData.rotationConst *.005f));
                    sanitycheckx = Mathf.Cos(_pData.rotationConst *.005f);
                    _planet.gameObject.transform.position = new Vector3(x,y,0);

                    if (sanitycheckx == 1)
                    {
                        _pData.rotationConst = 1.0f;
                        Debug.Log("Reset");
                    }
                    if(Vector3.Distance(playerShip.transform.position, _planet.transform.position) > 100f)
                    {
                        playerShip.transform.RotateAround(_planet.transform.position, new Vector3(0,0,0), DataHandler.playerShipSpeed * Time.deltaTime);
                    }

                }
            }
        }

        if(isDocked == true)
            {
                playerShip.transform.position = new Vector2(playerShip.transform.parent.position.x + Mathf.Cos(Time.time*DataHandler.playerShipSpeed)*0.5f,
                                                            playerShip.transform.parent.position.y + Mathf.Sin(Time.time*DataHandler.playerShipSpeed)*0.5f);
            }
    }

    // controls the buttons to move ship between planets - begins coroutine(planet, credits earned) and moves playerShip GO to new parent
    public void GoToPlanet(string planet)
    {
        planetDestination = planet;
        switch(planet)
        {
            case "1":
                isDocked = false;
                Move(planets[0], 69);
                playerShip.transform.SetParent(planets[0].transform);
            break;
            case "2":
                isDocked = false;
                Move(planets[1], 6969);
                playerShip.transform.SetParent(planets[1].transform);
            break;
            case "3":
                isDocked = false;
                playerShip.transform.SetParent(planets[2].transform);
                Move(planets[2], 100000);
            break;
        }


    }

    // calculates the amount of time it will take to move between planets based on distance between ship & planets and displays it on a button text
    void ButtonDistanceText()
    {
        var distance0 = Vector3.Distance(playerShip.transform.position, planets[0].transform.position);
        var distance1 = Vector3.Distance(playerShip.transform.position, planets[1].transform.position);
        var distance2 = Vector3.Distance(playerShip.transform.position, planets[2].transform.position);
        var distanceTime0 = (distance0 / DataHandler.playerShipSpeed);
        var distanceTime1 = (distance1 / DataHandler.playerShipSpeed);
        var distanceTime2 = (distance2 / DataHandler.playerShipSpeed);
        buttonText[0].text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(distanceTime0 / 60), Mathf.FloorToInt(distanceTime0 % 60));
        buttonText[1].text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(distanceTime1 / 60), Mathf.FloorToInt(distanceTime1 % 60));
        buttonText[2].text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(distanceTime2 / 60), Mathf.FloorToInt(distanceTime2 % 60));
    }

    // all of this logic is controlling the lerp'ed movement of the playership object between planets and is called in GoToPlanet above
    public void Move(GameObject target, float moneys)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(MoveCoroutine(target, moneys));
    }

    private IEnumerator MoveCoroutine(GameObject target, float moneyToEarn)
    {
        destination = target.transform.position;

        while (Vector3.Distance(playerShip.transform.position, destination) > 0.1f)
        {

            isShipTraveling = true;
            destination = target.transform.position;
            playerShip.transform.position = Vector2.MoveTowards(playerShip.transform.position, destination, DataHandler.playerShipSpeed * Time.deltaTime);
            yield return null;
        }

        isDocked = true;
        DataHandler.creditAmount += moneyToEarn;
        isShipTraveling = false;
        coroutine = null;

    }

    // normal save/load scripts present in a lot of the monobehavior managers
    public void LoadData(GameData data)
    {
        for(int i = 0; i < planetData.Length; i++){
            planetData[i].rotationConst = data.planetPos;
        }
        if(data.isShipTraveling){
            playerShip.transform.position = data.shipPos;
            GoToPlanet(data.planetDestination);
        } else if (!data.isShipTraveling) {
            playerShip.transform.position = data.shipPos;
        }

    }

    public void SaveData(GameData data)
    {
        data.planetPos = planetData[0].rotationConst;
        data.shipPos = playerShip.transform.position;
        if(isShipTraveling){
            data.planetDestination = planetDestination;
            data.isShipTraveling = true;
        }
    }

}
