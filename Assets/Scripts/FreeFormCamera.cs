using UnityEngine;

public class FreeFormCamera : MonoBehaviour
{
    public RescueVechicles currentPlayer;

    private Vector3 cameraPosition;
    private Vector3 cameraRotation;

    [SerializeField]
    private float moveSpeed = 20.0f; // Speed of camera movement
    [SerializeField]
    private float rotateSpeed = 10.0f; // Speed of camera rotation

    float horizontalInput;
    float verticalInput;
    float mouseX;
    float mouseY;
    bool attachedToPlayer = false;

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

        // Camera Movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (!Mathf.Approximately(horizontalInput, 0.0f) || !Mathf.Approximately(verticalInput, 0.0f))
        {
            Debug.Log(horizontalInput);
            Debug.Log(verticalInput);

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            Vector3 moveVector = transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;
            transform.position += moveVector;
        }

        // Camera Rotation
        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotateSpeed;
            transform.eulerAngles += rotation;
        }
    }
}
