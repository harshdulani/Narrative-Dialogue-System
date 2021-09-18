using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
	[SerializeField] private RectTransform responseBox;
	[SerializeField] private RectTransform responseButtonTemplate;
	[SerializeField] private RectTransform responseContainer;

	private DialogueUI _dialogueUi;

	private List<GameObject> _tempResponseObjects= new List<GameObject>();
	
	private void Start()
	{
		_dialogueUi = GetComponent<DialogueUI>();
	}

	public void ShowResponses(Response[] responses)
	{
		var responseBoxHeight = 0f;

		foreach (var response in responses)
		{
			var responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
			responseButton.gameObject.SetActive(true);
			responseButton.GetComponent<Text>().text = response.ResponseText;
			responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

			_tempResponseObjects.Add(responseButton);
			
			responseBoxHeight += responseButtonTemplate.sizeDelta.y;
		}

		responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
		responseBox.gameObject.SetActive(true);
	}

	private void OnPickedResponse(Response response)
	{
		responseBox.gameObject.SetActive(true);

		foreach (var button in _tempResponseObjects)
		{
			Destroy(button);
		}
		_tempResponseObjects.Clear();

		_dialogueUi.ShowDialogue(response.DialogueObject);
	}
}
