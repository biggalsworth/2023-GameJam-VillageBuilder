using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class playerTreeCutting : MonoBehaviour
{

    public bool canBreak;

    public GameObject currTree;
    public GameObject treeBreak;
    public GameObject axe;


    private GameObject cam;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (canBreak)
        {
            canBreak = false;
        }

        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 1.5f))
        {
            if (hit.collider.tag == "Tree")
            {
                Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), Color.yellow);
                canBreak = true;
                currTree = hit.collider.gameObject;
            }
            else
            {
                currTree = null;
                canBreak = false;
            }
        }

        //detect breakage
        if (Input.GetMouseButtonDown(0))
        {
            if (canBreak && currTree.GetComponent<treeBehaviour>().canCut)
            {
                currTree.GetComponent<treeBehaviour>().gettingCut();
                Instantiate(treeBreak, currTree.transform.position, Quaternion.identity);
                InvStore.instance.wood++;

                animator.Play("Chop");
            }
        }
    }

    private void OnEnable()
    {
        axe.SetActive(true);
        cam = GameObject.FindWithTag("MainCamera");
        canBreak = true;
        currTree = null;
    }

    private void OnDisable()
    {
        axe.SetActive(false);
    }
}
