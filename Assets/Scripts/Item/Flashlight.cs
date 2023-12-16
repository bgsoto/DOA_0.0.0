using Unity.Netcode;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Flashlight : NetworkBehaviour, IInteractable
{
    [Header("Relationships")]
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject spot;
    [SerializeField] private AudioClip onSound;
    [SerializeField] private AudioClip offSound;
    private AudioSource source;

    [Header("Settings")]
    [SerializeField] private ItemData flashlightData;
    [SerializeField] private Material lens;
    [SerializeField] private bool isOn = false;
    [SerializeField] private bool pickable;
    [SerializeField] private string actionText;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        lens.DisableKeyword("_EMISSION");
        transform.parent.gameObject.GetComponent<NetworkObject>().Despawn(true);
        Destroy(spot);
        Destroy(transform.parent.gameObject);
    }

    public void Use()
    {
        if (!IsOwner) return;
        isOn = !isOn;
        if (isOn)
        {
            point.SetActive(true);
            spot.SetActive(true);
            lens.EnableKeyword("_EMISSION");
            source.PlayOneShot(onSound);
        }
        else
        {
            point.SetActive(false);
            spot.SetActive(false);
            lens.DisableKeyword("_EMISSION");
            source.PlayOneShot(offSound);
        }
    }

    public ItemData ItemData { get { return flashlightData; } set { flashlightData = value; } }
    public bool Pickable { get { return pickable; } set { pickable = value; } }
    public string ActionText { get { return actionText; } set { actionText = value; } }
    public bool itemOutline { get { return itemOutline; } set { itemOutline = value; } }
}
