using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
public class JoystickManager : MonoBehaviour
{
    public static JoystickManager JoystickManagerInstance;
    [SerializeField] private Vector2 JoystickSize = new Vector2(200, 200);
    public JoyStick Joystick;
    public NavMeshAgent playerNavMeshAgent;
    private Finger MovementFinger;
    public Vector2 MovementAmount;
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        JoystickManagerInstance = this;
    }
    
    
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    
    }
    private void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, Joystick.joyStickObj.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - Joystick.joyStickObj.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.joyStickObj.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
        
    }
   
    private void HandleFingerDown(Finger touchedFinger)
    {
        if (MovementFinger == null && touchedFinger.screenPosition.x <= Screen.width)
        {
            MovementFinger = touchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.joyStickObj.sizeDelta = JoystickSize;
            Joystick.joyStickObj.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
        }
    }
    
    
    private void HandleLoseFinger(Finger lostFinger)
    {
        if (lostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }
    
    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < JoystickSize.x / 2)
        {
            startPosition.x = JoystickSize.x / 2;
        }

        if (startPosition.y < JoystickSize.y / 2)
        {
            startPosition.y = JoystickSize.y / 2;
        }
        else if (startPosition.y > Screen.height - JoystickSize.y / 2)
        {
            startPosition.y = Screen.height - JoystickSize.y / 2;
        }

        return startPosition;
    }
    void Update()
    {
        Vector3 scaledMovement = playerNavMeshAgent.speed * Time.deltaTime * new Vector3(MovementAmount.x, 0, MovementAmount.y);
        playerNavMeshAgent.Move(scaledMovement);

    }
}
