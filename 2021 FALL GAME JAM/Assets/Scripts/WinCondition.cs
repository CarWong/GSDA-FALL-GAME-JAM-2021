using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public bool inCar;
    // Start is called before the first frame update
    void Start()
    {
        inCar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inCar)
        {
            //you won wow!!
            SceneManager.LoadScene("EndScene");
            Debug.Log("hit");
        }
    }
}
