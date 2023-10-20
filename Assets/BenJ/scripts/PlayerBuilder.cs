using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBuilder : MonoBehaviour
{

    private InvStore inventory;

    public GameObject houses;
    public GameObject[] buildings;

    public GameObject building;
    private GameObject ghostBuild;
    public int buildCount;

    [Header("Placing Buildings Help")]
    public Transform placementPoint;
    [Tooltip("These are the transparent versions of the houses that are stored on the map")]
    public GameObject[] ghostBuildings;
    public Transform ghostStore;


    [Header("UI")]
    public GameObject BuilderUI;
    //public GameObject[] UiBoxes;
    public GameObject UnplacableUI; 
    public TextMeshProUGUI buildingText;

    [SerializeField]
    private TextMeshProUGUI woodAmount;
    [SerializeField]
    private TextMeshProUGUI metalAmount;
    [SerializeField]
    private TextMeshProUGUI woodNeeded;
    [SerializeField]
    private TextMeshProUGUI metalNeeded;

    // Start is called before the first frame update
    void Start()
    {
        inventory = InvStore.instance;

        #region amount UI
        woodAmount = GameObject.Find("WoodAmount").GetComponent<TextMeshProUGUI>();
        metalAmount = GameObject.Find("MetalAmount").GetComponent<TextMeshProUGUI>();
        woodNeeded = GameObject.Find("WoodNeeded").GetComponent<TextMeshProUGUI>();
        metalNeeded = GameObject.Find("MetalNeeded").GetComponent<TextMeshProUGUI>();
        #endregion

        buildCount = 0;
        building = buildings[buildCount];
        //UiBoxes[buildCount].GetComponent<Image>().color = Color.grey;
        buildingText.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().buildName).ToUpper();

        ghostBuildings[buildCount].GetComponent<HouseGhost>().used = true;
        ghostBuild = ghostBuildings[buildCount];

        UnplacableUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        #region inputs
        if (Input.GetKeyDown(KeyCode.Z) && buildCount != 0)
        {
            //move the outline of the building to the storage point of the map
            ghostBuildings[buildCount].GetComponent<HouseGhost>().used = false;
            ghostBuildings[buildCount].transform.SetParent(ghostStore);
            ghostBuildings[buildCount].transform.position = ghostStore.position;


            buildCount--;
            building = buildings[buildCount];

            buildingText.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().buildName).ToUpper();

            ghostBuildings[buildCount].GetComponent<HouseGhost>().used = true;
            ghostBuild = ghostBuildings[buildCount];

            woodNeeded.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().wood).ToString();
            metalNeeded.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().metal).ToString();

        }
        else if (Input.GetKeyDown(KeyCode.X) && buildCount != buildings.Length - 1)
        {
            ghostBuildings[buildCount].transform.SetParent(ghostStore);
            ghostBuildings[buildCount].transform.position = ghostStore.position;
            ghostBuildings[buildCount].GetComponent<HouseGhost>().used = false;


            buildCount++;
            building = buildings[buildCount];

            buildingText.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().buildName).ToUpper();

            ghostBuildings[buildCount].GetComponent<HouseGhost>().used = true;
            ghostBuild = ghostBuildings[buildCount];

            woodNeeded.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().wood).ToString();
            metalNeeded.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().metal).ToString();

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (inventory.wood >= ghostBuild.GetComponent<HouseGhost>().wood && inventory.metal >= ghostBuild.GetComponent<HouseGhost>().metal)
            {
                if (building.tag == "Building" && ghostBuildings[buildCount].GetComponent<HouseGhost>().placeable)
                {
                    Instantiate(building, ghostBuildings[buildCount].transform.position, ghostBuildings[buildCount].transform.rotation, houses.transform);

                    inventory.wood -= ghostBuildings[buildCount].GetComponent<HouseGhost>().wood;
                    inventory.metal -= ghostBuildings[buildCount].GetComponent<HouseGhost>().metal;
                    woodAmount.text = (inventory.wood).ToString();
                    metalAmount.text = (inventory.metal).ToString();
                }
                else if (ghostBuildings[buildCount].GetComponent<HouseGhost>().placeable == false)
                {
                    UnplacableUI.SetActive(true);
                }
            }
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            placementPoint.transform.Translate(Vector3.forward * Time.deltaTime * 20, Space.Self);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            placementPoint.transform.Translate(Vector3.back * Time.deltaTime * 20, Space.Self);
        }

        #endregion


        //placement permission update
        if (ghostBuildings[buildCount].GetComponent<HouseGhost>().placeable == true)
        {
            if (UnplacableUI.activeSelf == true)
            {
                UnplacableUI.SetActive(false);
            }
        }



    }

    private void OnEnable()
    {
        BuilderUI.SetActive(true);
        buildCount = 0;
        building = buildings[buildCount];
        //UiBoxes[buildCount].GetComponent<Image>().color = Color.grey;
        buildingText.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().buildName).ToUpper();
        ghostBuildings[buildCount].GetComponent<HouseGhost>().used = true;
        ghostBuild = ghostBuildings[buildCount];

        woodAmount.text = (inventory.wood).ToString();
        metalAmount.text = (inventory.metal).ToString();

        Debug.Log(inventory.wood);
        Debug.Log(inventory.metal);

        woodNeeded.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().wood).ToString();
        metalNeeded.text = (ghostBuildings[buildCount].GetComponent<HouseGhost>().metal).ToString();
    }

    private void OnDisable()
    {
        BuilderUI.SetActive(false);
        ghostBuildings[buildCount].transform.SetParent(ghostStore);
        ghostBuildings[buildCount].transform.position = ghostStore.position;
        ghostBuildings[buildCount].GetComponent<HouseGhost>().used = false;
    }
}
