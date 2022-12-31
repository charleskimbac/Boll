using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPref : MonoBehaviour
{
    public void resetData(){
        Debug.Log("deleting: " + PlayerPrefs.GetFloat("Score"));
        PlayerPrefs.DeleteAll();
        Debug.Log("stored: " + PlayerPrefs.GetFloat("Score"));
    }
}
