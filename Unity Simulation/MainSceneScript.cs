using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
    public string outputText;
    public GameObject inputField;
    public Text loadstuff;
    
    private IEnumerator WaitForSometime()
    {
        yield return new WaitForSeconds(2.5f);
        loadstuff.text = "Ok! We're good!";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }
 

    public void OnButtonClick()
    {
        outputText = inputField.GetComponent<Text>().text;
        Debug.Log(outputText);
        PlayerPrefs.SetString("path_seq", outputText);
        loadstuff.text = "Loading...";
        StartCoroutine(WaitForSometime());
        
    }
}
