using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStoneMine : MonoBehaviour
{
    public GameObject Miner;
    public GameObject laser;
    public LineRenderer laserLine;
    public Vector3 laserTarget;

    [Header("Camera")]
    [SerializeField] private GameObject cam;



    [Header("Mining Time")]
    [SerializeField] private float HoldStartTimeMax;
    [SerializeField] private Image _holdImmageUI;
    private float _holdImmageFill = 0;
    public GameObject Ore;
    public GameObject OreBreak;
    private float HoldStartTime;
    private bool holding;

    [Header("Laser")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform shootPos;


    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(false);

        cam = GameObject.FindWithTag("MainCamera");
        _holdImmageUI.fillAmount = 0;

        laserLine.positionCount = 2;
        laser.GetComponent<LineRenderer>().SetPosition(0, shootPos.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        MineController();

        if (Input.GetMouseButtonDown(0) )
        {
            StartMining();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndMining();
            ResetValues();

        }

        if(holding && Ore.tag == "Ore")
        {
            BeginLaser();
        }

    }

    #region -- MINING --

    private void TimeCounter()
    {
        HoldStartTime += Time.deltaTime;
        Debug.Log(HoldStartTime);
        if (HoldStartTime > HoldStartTimeMax)
        {
            //Debug.Log("Start Destroying");
            Mine();
            HoldStartTime = 0;
        }


    }

    private void MineController()
    {
        if (holding)
        {
            TimeCounter();
            laser.GetComponent<LineRenderer>().SetPosition(0, shootPos.transform.position);
            laser.GetComponent<LineRenderer>().SetPosition(1, laserTarget);
        }

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 10))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward)*10, Color.yellow);
            if (hit.collider.tag == "Ore")
            {
                Ore = hit.collider.gameObject;
                laserTarget = hit.point;
            }
            else
            {
                Ore = null;
                EndMining();
                ResetValues(); 
            }
        }
        else
        {
            if (hit.collider == null)
            {
                Ore = null;
                EndMining();
                ResetValues();
            }
        }

        _holdImmageFill = Mathf.Clamp(HoldStartTime, 0, HoldStartTimeMax) / HoldStartTimeMax;

        _holdImmageUI.fillAmount = _holdImmageFill;
    }
    private void StartMining()
    {
        if(Ore != null)
        {
            holding = true;
        }
    }

    private void EndMining()
    {
        holding = false;
        HoldStartTime = 0;
        laser.SetActive(false);
    }

    private void Mine()
    {
        Instantiate(OreBreak, Ore.transform.position, Quaternion.identity);
        //Destroy(Ore);
        InvStore.instance.metal += 2;
        Debug.Log("Destroyed");
        _holdImmageUI.fillAmount = 0;
        Ore = null;
    }

    void BeginLaser()
    {
        laser.SetActive(true);
        laserLine.enabled = true;
    }

    private void ResetValues()
    {
        _holdImmageUI.fillAmount = 0;
        HoldStartTime = 0;
    }
    #endregion-- MINING --




    private void OnEnable()
    {
        Miner.SetActive(true);
        ResetValues();
        laser.SetActive(true);

    }

    private void OnDisable()
    {
        Miner.SetActive(false);
        laser.SetActive(false);
    }


}
