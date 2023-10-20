using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseGhost : MonoBehaviour
{
    public string buildName;

    public bool used;

    public Transform PlacementPoint;

    private float yPos = 0;

    bool rotating = false;

    float rotationAim;

    public bool placeable;

    [Header("Materials")]
    public Material goodPlace;
    public Material badPlace;

    [Header("Building Requirements")]
    public int wood;
    public int metal;


    // Start is called before the first frame update
    void Start()
    {
        PlacementPoint = GameObject.Find("PlacementPos").transform;
        rotationAim = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            Vector3 dir = new Vector3(PlacementPoint.position.x, PlacementPoint.position.y*10, PlacementPoint.position.z);

            RaycastHit hit;

            if (Physics.Raycast(dir, Vector3.down, out hit, 200f))
            {
                Debug.DrawRay(dir, transform.TransformDirection(Vector3.down)*100, Color.yellow);

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    yPos = hit.collider.transform.position.y;

                    yPos += gameObject.transform.localScale.y / 2;
                    //yPos += gameObject.GetComponent<Renderer>().bounds.size.y / 2;
                }
            }

            transform.position = new Vector3(PlacementPoint.position.x, yPos, PlacementPoint.position.z);

            #region rotation
            if (Input.GetKeyDown(KeyCode.Q))
            {
                rotationAim = rotationAim + 15f;
                //rotating = true;

                //Vector3 newVec = transform.localRotation.eulerAngles;
                //newVec.y = Mathf.Lerp(transform.rotation.y, rotationAim, 15 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0.0f, rotationAim, 0.0f);
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                //rotating = false;

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                rotationAim = rotationAim - 15f;
                transform.rotation = Quaternion.Euler(0.0f, rotationAim, 0.0f);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                //rotating = false;

            }

            if (rotating)
            {
                //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(0.0f, rotationAim, 0.0f), 15 * Time.deltaTime);
                Vector3 newVec = transform.rotation.eulerAngles;
                newVec.y = Mathf.Lerp(transform.rotation.y, rotationAim, 15 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(newVec);
            }
            #endregion



        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Building")
        {
            placeable = false;
            GetComponent<Renderer>().material = badPlace;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Building")
        {
            placeable = false;
            GetComponent<Renderer>().material = badPlace;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Building")
        {
            placeable = true;
            GetComponent<Renderer>().material = goodPlace;
        }
    }

}
