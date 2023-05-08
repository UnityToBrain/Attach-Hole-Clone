using UnityEngine;

public class JoyStick : MonoBehaviour
{
   public RectTransform joyStickObj;
   public RectTransform Knob;

   private void Awake()
   {
      joyStickObj = GetComponent<RectTransform>();
   }
}
