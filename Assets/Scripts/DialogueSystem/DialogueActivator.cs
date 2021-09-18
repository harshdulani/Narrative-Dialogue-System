using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
	[SerializeField] private DialogueObject _dialogueObject;
	
	public void Interact(PlayerController player)
	{
		player.DialogueUi.ShowDialogue(_dialogueObject);
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
