using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
	[SerializeField] private Text dialogueText;
	[SerializeField] private DialogueObject testDialogue;
	
	private TypeWriter _writer;
	private ResponseHandler _responseHandler;
	
	private void Start()
	{
		_writer = GetComponent<TypeWriter>();
		_responseHandler = GetComponent<ResponseHandler>();
		
		CloseDialogueBox();
		ShowDialogue(testDialogue);
	}

	public void ShowDialogue(DialogueObject dialogueObject)
	{
		dialogueText.gameObject.SetActive(true);
		StartCoroutine(StepThroughDialogue(dialogueObject));
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

	private void CloseDialogueBox()
	{
		dialogueText.gameObject.SetActive(false);
		dialogueText.text = string.Empty;
	}
}