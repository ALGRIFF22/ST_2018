using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurnController : MonoBehaviour {


	public List<GameObject> Pages;

	public int activePage = 0;
	public int pagesLength;

	public delegate void PageEvent(GameObject currentPage, int pageNumber);
	public static event PageEvent CurrentPageActive;

	private void OnEnable()
	{
		BookSelect.SendBookEvent += PagesInBook;
	}
	private void OnDisable()
	{
		BookSelect.SendBookEvent -= PagesInBook;
	}

	private void Start()
	{
		pagesLength = Pages.Count;
		pagesLength = pagesLength - 1;
	}

	private void PagesInBook(GameObject book)
	{
		Pages = book.transform.GetChild(0).gameObject.GetChildren();
	}

	public void NextPage()
	{
		if (activePage >= 0)
		{
			if (activePage < pagesLength)
			{
				Pages[activePage].SetActive(false);
				activePage++;
				Pages[activePage].SetActive(true);
				//send active page to sound controller
				CurrentPageActive(Pages[activePage], activePage);
			}
			else return;
		}
		else return;
	}

	public void PreviousPage()
	{
		if (activePage > 0)
		{
			if (activePage <= pagesLength)
			{
				Pages[activePage].SetActive(false);
				activePage--;
				Pages[activePage].SetActive(true);
				//send active page to sound controller
				CurrentPageActive(Pages[activePage], activePage);
			}
			else return;
		}
		else return;
	}
}
