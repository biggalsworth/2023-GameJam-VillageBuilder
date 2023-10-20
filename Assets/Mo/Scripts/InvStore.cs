using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvStore : MonoBehaviour
{
    //Instances
    public static InvStore instance;

    public TextMeshProUGUI woodAmount;
    public TextMeshProUGUI metalAmount;

    public int wood = 0;
    public int metal = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = gameObject.GetComponent<InvStore>();
    }

    // Update is called once per frame
    void Update()
    {
        woodAmount.text = wood.ToString();
        metalAmount.text = metal.ToString();
    }
}
