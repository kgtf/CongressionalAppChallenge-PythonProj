using UnityEngine;
using UnityEngine.EventSystems;


public class UiControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject helpBox;
    public float scaleFactor = 1.2f;       // How big it gets on hover
    public float speed = 10f;              // How fast it scales
    private Vector3 originalScale;
    private Vector3 targetScale;
    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }
    public void openHelp()
    {
        helpBox.SetActive(true);
    }

    public void closeHelp()
    {
        helpBox.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("hit");
        targetScale = originalScale * scaleFactor;
        transform.SetAsLastSibling();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("left");
        targetScale = originalScale;
    }

    void Update()
    {
        // Smooth transition
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }
}
