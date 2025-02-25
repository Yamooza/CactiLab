using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Base Data")]
    [SerializeField] ItemData itemData;
    [SerializeField] int amount = 1;

    [Header("On Pickup")]
    [SerializeField] SoundEffect onInteractSE;
    [SerializeField] UnityEvent onInteractEvent;

    public void OnInteract()
    {
        UIManager.Instance.TryAddToInventory(itemData, amount);
        AudioManager.Instance.PlaySoundEffect(onInteractSE);
        onInteractEvent.Invoke();
        Destroy(this.gameObject);
    }
}
