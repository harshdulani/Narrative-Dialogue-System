using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ResponseHandler : MonoBehaviour
{
	[SerializeField] private RectTransform responseBox;
	[SerializeField] private RectTransform responseButtonTemplate;
	[SerializeField] private RectTransform responseContainer;

	private DialogueUI _dialogueUi;
	private List<Object> _tempResponseObjects = new List<Object>();

	private ResponseEvent[] _responseEvents;

	private void Start()
	{
		_dialogueUi = GetComponent<DialogueUI>();
	}

	public void AddResponseEvents(ResponseEvent[] events)
	{
		_responseEvents = events;
	}
	
	public void ShowResponses(Response[] responses)
	{
		var responseBoxHeight = 0f;

		for(int i =0; i < responses.Length; i++)
		{
			var response = responses[i];
			var responseIndex = i;
			
			var responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
			responseButton.gameObject.SetActive(true);
			responseButton.GetComponent<Text>().text = response.ResponseText;
			responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

			_tempResponseObjects.Add(responseButton);
			
			responseBoxHeight += responseButtonTemplate.sizeDelta.y;
		}

		responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
		responseBox.gameObject.SetActive(true);
	}

	private void OnPickedResponse(Response response, int responseIndex)
	{
		responseBox.gameObject.SetActive(false);

		foreach (var button in _tempResponseObjects)
		{
			Destroy(button);
		}
		_tempResponseObjects.Clear();

		if (_responseEvents != null && responseIndex <= _responseEvents.Length)
			_responseEvents[responseIndex].OnPickedResponse?.Invoke();
		
		_dialogueUi.ShowDialogue(response.DialogueObject);
	}
}
