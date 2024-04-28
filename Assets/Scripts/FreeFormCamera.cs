using UnityEngine;

public class FreeFormCamera : MonoBehaviour
{
    public RescueVechicles currentPlayer;

    private Vector3 cameraPosition;
    private Vector3 cameraRotation;

    private Vector3 moveDirection;
    private Vector3 moveVector;
    private Vector3 rotation;

    private const float moveSpeed = 20.0f;
    private const float rotateSpeed = 10.0f;

    float horizontalInput = 0.0f;
    float verticalInput = 0.0f;

    float mouseX = 0.0f;
    float mouseY = 0.0f;

    bool attachedToPlayer = false;

    private void Start()
    {
        if(currentPlayer == null)
        {
            Debug.LogError("Player is not assigned, trying to assign player");

            if (GameObject.FindGameObjectWithTag("RescueVechicle").TryGetComponent<RescueVechicles>(out currentPlayer))
            {
                Debug.Log("Player assign successful");
            }
            else
            {
                Debug.Log("Player assign unsuccessful");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (attachedToPlayer)
            {
                this.transform.SetParent(null);
                this.transform.position = cameraPosition;
                this.transform.eulerAngles = cameraRotation;
            }
            else
            {
                cameraPosition = this.transform.position;
                cameraRotation = this.transform.eulerAngles;

                this.transform.SetParent(this.currentPlayer.transform);

                this.transform.localPosition = new Vector3(0.0f, 5.0f, -7.0f);
                this.transform.localEulerAngles = new Vector3(30.0f, 0.0f, 0.0f);
            }

            attachedToPlayer = !attachedToPlayer;
        }

        if (!attachedToPlayer)
        {
            // Camera Movement
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            if (!Mathf.Approximately(horizontalInput, 0.0f) || !Mathf.Approximately(verticalInput, 0.0f))
            {
                moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                moveVector = transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;
                transform.position += moveVector;
            }

            // Camera Rotation
            if (Input.GetMouseButton(1))
            {
                mouseX = Input.GetAxis("Mouse X");
                mouseY = Input.GetAxis("Mouse Y");

                if (!Mathf.Approximately(mouseX, 0.0f) || !Mathf.Approximately(mouseY, 0.0f))
                {
                    mouseX = Mathf.Clamp(mouseX, -1.0f, 1.0f); // Bugs on game window focus. Goes either more than 1.0f or less then -1.0f
                    mouseY = Mathf.Clamp(mouseY, -1.0f, 1.0f); // Bugs on game window focus. Goes either more than 1.0f or less then -1.0f 

                    rotation = new Vector3(-mouseY, mouseX, 0f) * rotateSpeed;
                    transform.eulerAngles += rotation;
                }
            }
        }
    }
}
