using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
	[SerializeField] private GameObject dialogueBox;
	[SerializeField] private Text dialogueText;

	public bool isOpen { get; private set; }
	
	private TypeWriter _writer;
	private ResponseHandler _responseHandler;
	
	private void Start()
	{
		_writer = GetComponent<TypeWriter>();
		_responseHandler = GetComponent<ResponseHandler>();
		
		CloseDialogueBox();
	}
	
	private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
	{

		for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
		{
			string dialogue = dialogueObject.Dialogue[i];
			yield return _writer.Run(dialogue, dialogueText);
			
			if(i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
			
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
		}
		
		if(dialogueObject.HasResponses)
		{
			_responseHandler.ShowResponses(dialogueObject.Responses);
			yield break;
		}

		CloseDialogueBox();
	}

	public void ShowDialogue(DialogueObject dialogueObject)
	{
		isOpen = true;
		dialogueBox.SetActive(true);
		StartCoroutine(StepThroughDialogue(dialogueObject));
	}

	private void CloseDialogueBox()
	{
		isOpen = false;
		dialogueBox.SetActive(false);
		dialogueText.text = string.Empty;
	}
}