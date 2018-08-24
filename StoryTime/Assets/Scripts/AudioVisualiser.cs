using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiser : MonoBehaviour {

	public RectTransform[] audioSpectrumObjects;
	public float heightMultiplyer;
	public int numberOfSamples = 1024;
	public FFTWindow fftWindow;
	public float lerpTime = 1;

	public List<string> devices;
	//Microphone
	public string microphone;

	private float baseScale;
	private AudioSource audioSource;

	private void Start()
	{
		baseScale = audioSpectrumObjects[0].localScale.x;
		audioSource = GetComponent<AudioSource>();
		foreach(string device in Microphone.devices)
		{
			devices.Add(device);
			if(microphone == null)
			{
				microphone = device;
			}
		}
		UpdateMicrophone();
	}
	// Update is called once per frame
	void Update ()
	{
		float[] spectrum = new float[numberOfSamples];

		GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, fftWindow);

		for(int i = 0; i < audioSpectrumObjects.Length; i++)
		{

			float intensity = baseScale + spectrum[i] * heightMultiplyer;

			float lerpY = Mathf.Lerp(audioSpectrumObjects[i].localScale.y, intensity, lerpTime);
			float lerpX = Mathf.Lerp(audioSpectrumObjects[i].localScale.x, intensity, lerpTime);
			Vector3 newScale = new Vector3(lerpX, lerpY, audioSpectrumObjects[i].localScale.z);

			audioSpectrumObjects[i].localScale = newScale;
		}

	}

	void UpdateMicrophone()
	{
		audioSource.Stop();

		audioSource.clip = Microphone.Start(microphone, true, 10, AudioSettings.outputSampleRate);
		audioSource.loop = true;

		Debug.Log(Microphone.IsRecording(microphone).ToString());

		if (Microphone.IsRecording(microphone))
		{
			while(!(Microphone.GetPosition(microphone) > 0)){
			}

			audioSource.Play();
		}
		else
		{
			Debug.Log("Mic doesnt work");
		}
	}
	
}
