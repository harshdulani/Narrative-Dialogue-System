using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
	[SerializeField] private DialogueObject dialogueObject;

	public void UpdateDialogueObject(DialogueObject newDialogueObject)
	{
		dialogueObject = newDialogueObject;
	}
	
	public void Interact(PlayerController player)
	{
		if (TryGetComponent(out DialogueResponseEvents events) && events.DialogueObject == dialogueObject)
		{
			player.DialogueUi.AddResponseEvents(events.Events);
		}
		
		player.DialogueUi.ShowDialogue(dialogueObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(!other.CompareTag("Player")) return;
		if(!other.TryGetComponent(out PlayerController controller)) return;
		
		controller.Interactable = this;
	}

	private void OnTriggerExit(Collider other)
	{
		if(!other.CompareTag("Player")) return;
		if(!other.TryGetComponent(out PlayerController controller)) return;

		if (controller.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
			controller.Interactable = null;
	}
}
