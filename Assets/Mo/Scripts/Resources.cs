using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Resources : MonoBehaviour
{
  

    public void OpenLog()
    {
        //Path of file
        Debug.Log("The Statement is called");
        string path = Application.dataPath + "/Mo/scripts/ResourceLog.txt";
        Debug.Log("Looking in " + path);

        //create file if it doesn't exist
        if (!File.Exists(path)) 
        {
            Debug.Log("The Funny if statement is working");
            File.WriteAllText(path, "0 \n 0 \n 0"); //First int is for wood, second int is for metal
            /*StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("0");
            sw.WriteLine("0");*/
        }
        //content of the file
        StreamReader Reader = new StreamReader(path);

        CheckFile();
    }

    void CheckFile() 
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        OpenLog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
