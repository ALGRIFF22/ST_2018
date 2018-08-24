using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookSelect : MonoBehaviour {

	public GameObject pageTurner;
	public GameObject booksParent;
	public GameObject streamingAsset;
	public GameObject audioVisualiser;

	public List<GameObject> Books;

	public delegate void SendBook(GameObject book);
	public static event SendBook SendBookEvent;
	

	private void Start()
	{
		Books = booksParent.GetChildren();
	}

	IEnumerator OpenBook(string bookName)
	{
		//deactivates book select screen
		gameObject.SetActive(false);
		//turns page turner on
		pageTurner.SetActive(true);
		//turns voice recognition on
		streamingAsset.SetActive(true);
		//turns audio visualiser on
		audioVisualiser.SetActive(true);

		//gets correct book and sets it active
		//sends the book to the pageturner controller
		for(int i = 0; i < Books.Count; i++)
		{
			if (Books[i].name == bookName)
			{
				Books[i].SetActive(true);
				SendBookEvent(Books[i]);
			}
			else yield return null;
		}
		Debug.Log("book active" + bookName);
		yield return null;
	}

	public void HanselAndGrettelClick()
	{
		string bookName = "Hansel_and_Gretel";
		StartCoroutine(OpenBook(bookName));
	}
}
 