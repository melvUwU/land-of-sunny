using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int cherryNum = 0;
    public Text cherryText;
    public static ScoreManager scoreManage;
    // Start is called before the first frame update
    void Start()
    {
        cherryNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cherryText.text = ("x ") + PermanentUI.perm.cherryNum.ToString();
    }
}
