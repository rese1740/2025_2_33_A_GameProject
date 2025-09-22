using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("상호 작용 설정")]
    public float interactionRange = 2.0f;
    public LayerMask interactionMask = 1;
    public KeyCode interactionKey = KeyCode.E;

    [Header("UI 설정")]
    public Text interactionText;
    public GameObject interactionUI;

    private Transform playerTransform;
    private InteractableObject currentInteractable;

    private void Start()
    {
        playerTransform = transform;
        HideInteractionUI();
    }

    private void Update()
    {
        CheckForInteractables();
        HandleInteractionInput();
    }

    void HandleInteractionInput() // 인터랙션 입력 함수
    {
        if (currentInteractable != null && Input.GetKeyDown(interactionKey)) // 인터랙션 키 값을 눌렀을 때
        {
            currentInteractable.Interact(); // 행동을 실행 한다.
        }
    }

    // 참조 0개
    void ShowInteractionUI(string text) // 인터랙션 창을 연다.
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(true);
        }

        if (interactionText != null)
        {
            interactionText.text = text;
        }
    }

    // 참조 0개
    void HideInteractionUI() // 인터랙션 창을 닫는다.
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    void CheckForInteractables()
    {
        Vector3 checkPosition = playerTransform.position + playerTransform.forward * (interactionRange * 0.5f);

        Collider[] hitColliders = Physics.OverlapSphere(checkPosition, interactionRange, interactionMask);

        InteractableObject closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in hitColliders)
        {
            InteractableObject interactable = collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(playerTransform.position, collider.transform.position);

                Vector3 directionToObject = (collider.transform.position - playerTransform.position).normalized;
                float angle = Vector3.Angle(playerTransform.forward, directionToObject);

                if (angle < 90f && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        if (closestInteractable != currentInteractable)
        {
            if(currentInteractable != null)
            {
                currentInteractable.OnPlayerExit();
            }

            currentInteractable = closestInteractable;

            if (currentInteractable != null)
            {
                currentInteractable.OnPlayerEnter();
                ShowInteractionUI(currentInteractable.GetInteracrionText());
            }
            else
            {
                HideInteractionUI();
            }
        }
    }

}
