using System.Collections;
using UnityEngine;

public class Hardware : MonoBehaviour {

    public string GPU, GPUAPI, GPURAM, RAM, ActualResolution, MaxResolution, CPU, CPUSPEED, DeviceName, OperatingSystem;
	
	void Start () {
        
		GPU = SystemInfo.graphicsDeviceName.ToString();
		GPUAPI = SystemInfo.graphicsDeviceType.ToString();
		GPURAM = (SystemInfo.graphicsMemorySize.ToString()) + " Mb";
		RAM = (SystemInfo.systemMemorySize.ToString()) + " Mb";
		ActualResolution = (Screen.width.ToString()) + "x" + (Screen.height.ToString());
		MaxResolution = Screen.currentResolution.ToString();
		CPU = SystemInfo.processorType.ToString();
		CPUSPEED = (SystemInfo.processorFrequency.ToString()) + " MHz";
		DeviceName = SystemInfo.deviceName.ToString();
		OperatingSystem = SystemInfo.operatingSystem.ToString();

		StartCoroutine ("Send");
	
	}

	IEnumerator Send () {
		
		if (!PlayerPrefs.HasKey ("Hardware") && !PlayerPrefs.HasKey ("Editor")) {

		string UrlComplete = "http://tecwolf.tk/hardware.php" + "?GPU=" + GPU + "&GPUAPI=" + GPUAPI + "&GPURAM=" + GPURAM + "&RAM=" + RAM + "&ActualResolution=" + ActualResolution + "&MaxResolution=" + MaxResolution + "&CPU=" + CPU + "&CPUSPEED=" + CPUSPEED + "&DeviceName=" + DeviceName + "&OperatingSystem=" + OperatingSystem;

			WWW SendHardware = new WWW (UrlComplete);

			yield return SendHardware;

			if (SendHardware.error == null) {

				if (SendHardware.text == ("Sent")) {

						PlayerPrefs.SetString ("Hardware", "true");

					}
				}
			}
		}
    }
