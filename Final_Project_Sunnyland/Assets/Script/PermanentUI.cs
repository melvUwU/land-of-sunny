using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentUI : MonoBehaviour
{
    public int cherryNum = 0;
    public Text cherryText;

    public static PermanentUI perm;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Continue1.contOne)
        {
            Destroy(gameObject);
        }
        if (Continue2.contTwo)
        {
            Destroy(gameObject);
        }
        if (Continue3.contThree)
        {
            Destroy(gameObject);
        }
    }
    public void Reset()
    {
        cherryNum = 0;
        cherryText.text = cherryNum.ToString();
    }
}
