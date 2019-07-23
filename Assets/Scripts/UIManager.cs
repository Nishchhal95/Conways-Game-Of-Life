using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public SimulationManager sm;
    public InputField timeStepInputField;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnStartClick()
    {
        sm.enabled = true;
    }

    public void OnTimeStepChanged()
    {
        string value = timeStepInputField.text;
        float ts = 1;
        float.TryParse(value, out ts);
        sm.timeStep = ts;
    }

    public void OnReloadClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
