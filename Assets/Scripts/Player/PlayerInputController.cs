using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementController playerMovementController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerMovementController.Move(Direction.DOWN);
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerMovementController.Move(Direction.UP);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerMovementController.Move(Direction.LEFT);
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerMovementController.Move(Direction.RIGHT);
            return;
        }
    }
}
