using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour
{

    [SerializeField]
    private UnityEvent<Direction> Move;

    [SerializeField]
    private UnityEvent Select;

    [SerializeField]
    private UnityEvent Back;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Select?.Invoke();
        }
        
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Back?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move?.Invoke(Direction.DOWN);
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move?.Invoke(Direction.UP);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move?.Invoke(Direction.LEFT);
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move?.Invoke(Direction.RIGHT);
            return;
        }
    }
}
