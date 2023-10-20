using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerActions : MonoBehaviour
{
    bool detectInput;

    [Header("Action States")]
    public bool inactive;
    public bool building;
    public bool allowBuild;
    
    public bool axing;

    public bool mining;
    
    public bool unBuild;

    [Header("Action UIs")]
    public GameObject commonUI;
    public GameObject buildNotification;
    public GameObject BuildUI;
    public GameObject MineUI;


    private PlayerBuilder buildScript;
    private playerTreeCutting axeScript;
    private playerStoneMine miningScript;
    private HouseBreak unBuildScript;

    // Start is called before the first frame update
    void Start()
    {
        detectInput = true;

        buildScript = GetComponent<PlayerBuilder>();
        axeScript = GetComponent<playerTreeCutting>();
        miningScript = GetComponent<playerStoneMine>();
        unBuildScript = GetComponent<HouseBreak>();

        resetState();

        setUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectInput)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && allowBuild)
            {
                if (!building)
                {
                    resetState();
                    building = true;
                    buildScript.enabled = true;
                    commonUI.SetActive(false);
                }
                else
                {
                    resetState();
                    inactive = true;
                }
                StartCoroutine(buttonCooldown());
            }


            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!mining)
                {
                    resetState();
                    mining = true;
                    miningScript.enabled = true;
                }
                else
                {
                    resetState();
                    inactive = true;
                }
                StartCoroutine(buttonCooldown());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!axing)
                {
                    resetState();
                    axing = true;
                    axeScript.enabled = true;
                }
                else
                {
                    resetState();
                    inactive = true;
                }
                StartCoroutine(buttonCooldown());
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (!unBuild)
                {
                    resetState();
                    unBuild = true;
                    unBuildScript.enabled = true;
                }
                else
                {
                    resetState();
                    inactive = true;
                }
                StartCoroutine(buttonCooldown());
            }
        }

        //can player build
        if (allowBuild)
        {
            buildNotification.SetActive(true);
        }
        else if(!allowBuild && buildNotification.activeSelf == true)
        {
            buildScript.enabled = false;
            commonUI.SetActive(true);
            BuildUI.SetActive(false);
            buildNotification.SetActive(false);
        }

    }

    IEnumerator buttonCooldown()
    {
        detectInput = false;
        yield return new WaitForSeconds(0.5f);
        detectInput = true;
    }


    void resetState()
    {
        commonUI.SetActive(true);

        inactive = false;
        building = false;
        axing = false;
        mining = false;
        unBuild = false;

        buildScript.enabled = false;
        axeScript.enabled = false;
        miningScript.enabled = false;
        unBuildScript.enabled = false;
    }

    void setUI()
    {
        BuildUI.SetActive(false);
    }
}
