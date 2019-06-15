 using UnityEngine;

public class RandomExperiment : MonoBehaviour
{

    static public GameObject[] ExperimentObject;
    static public int ExperimentNumber;
	   
	void Awake()
    {

        ExperimentObject = GameObject.FindGameObjectsWithTag("Experiment");

        foreach (GameObject Go in ExperimentObject)
        {

            Go.SetActive(false);

        }

		ExperimentNumber = Random.Range(0, ExperimentObject.Length);
        ExperimentObject[ExperimentNumber].SetActive(true);

    }

}