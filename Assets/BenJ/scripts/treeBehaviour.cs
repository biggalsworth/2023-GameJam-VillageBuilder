using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBehaviour : MonoBehaviour
{
    public bool canCut;

    public Mesh grownTree;
    public Mesh sapling;

    public float growTime;

    public Vector3 grownSize;
    public Vector3 saplingSize;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = grownSize;
        canCut = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void gettingCut()
    {
        gameObject.GetComponent<MeshFilter>().mesh = sapling;
        gameObject.transform.localScale = saplingSize;
        StartCoroutine(ReGrow());
    }



    IEnumerator ReGrow()
    {
        canCut = false;
        yield return new WaitForSeconds(growTime);
        gameObject.GetComponent<MeshFilter>().mesh = grownTree;
        gameObject.transform.localScale = grownSize;
        canCut = true;
    }
}
