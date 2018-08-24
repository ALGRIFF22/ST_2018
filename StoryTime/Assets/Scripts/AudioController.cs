using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {


	public List<GameObject> AmbientSounds;
	public List<GameObject> ActionSounds;

	int activePage;

	public string word;

	private void OnEnable()
	{
		BookSelect.SendBookEvent += SendBookEvent;
		PageTurnController.CurrentPageActive += PageSounds;
		ExampleStreaming.SendKeyword += PlayActionSound;
	}

	private void OnDestroy()
	{
		BookSelect.SendBookEvent -= SendBookEvent;
		PageTurnController.CurrentPageActive -= PageSounds;
		ExampleStreaming.SendKeyword -= PlayActionSound;
	}

	void SendBookEvent(GameObject book)
	{
		GameObject sound = book.transform.GetChild(1).gameObject;
		AmbientSounds = sound.GetChildren();
	}

	void PageSounds(GameObject pageSounds, int pageNumber)
	{
		
		activePage = pageNumber;
		GameObject c = pageSounds.transform.Find("Sounds").gameObject;
		StopActionSound();
		ActionSounds = c.GetChildren();
	}

	void PlayActionSound(string keyword)
	{
		word = keyword;
		Debug.Log("recieved keyword " + keyword);
		for(int i = 0; i < ActionSounds.Count; i++)
		{
			Debug.Log("checking for audio to play " + ActionSounds[i].name + "" + keyword);
			if (ActionSounds[i].name == keyword)
			{
				Debug.Log("audio and keyword match");
				AudioSource a = ActionSounds[i].GetComponent<AudioSource>();
				a.volume = 1;
				if (!a.isPlaying)
				{ 
					a.Play();
					Debug.Log("audio playing");
				}
				
			}
			
		}
	}

	void PlayAmbientSound()
	{

	}

	void StopActionSound()
	{
		for (int i = 0; i < ActionSounds.Count; i++)
		{
			AudioSource a = ActionSounds[i].GetComponent<AudioSource>();
			while (a.volume > 0)
			{
				a.volume = a.volume - .01f;
			}
		}
	}

	void StopAmbientSound()
	{

	}
}
