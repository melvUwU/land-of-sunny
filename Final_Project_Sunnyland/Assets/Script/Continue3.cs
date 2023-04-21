using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue3 : MonoBehaviour
{
    public static bool contThree = false;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("End");
            contThree = false;
        }
    }
}
