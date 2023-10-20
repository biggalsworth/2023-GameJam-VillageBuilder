using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HouseBreak : MonoBehaviour
{
    public GameObject hammer;
    public Animator animator;

    public bool CanRuin;
    public GameObject CurrBuilding;
    public GameObject BuildBreak;


    [SerializeField]
    private GameObject cam;



    // Start is called before the first frame update
    void Start()
    {
        hammer.SetActive(false);
        cam = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 10))
        {
            if(hit.collider.tag == "Building")
            {
                CanRuin = true;
                CurrBuilding = hit.collider.gameObject;
            }         
            else
            {
                CurrBuilding = null;
                CanRuin= false;
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Bonking");
            DestroyBuilding();
        }

    }

    private void DestroyBuilding()
    {
        Instantiate(BuildBreak, CurrBuilding.transform.position, Quaternion.identity);
        Destroy(CurrBuilding);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
     
        Gizmos.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward));
    }


    private void OnEnable()
    {
        hammer.SetActive(true);
    }

    private void OnDisable()
    {
        hammer.SetActive(false);
    }

}
