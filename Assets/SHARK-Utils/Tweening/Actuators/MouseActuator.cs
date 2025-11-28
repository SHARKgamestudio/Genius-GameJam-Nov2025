public class MouseActuator : Actuator
{
    void OnMouseEnter() { TweenIn(); }
    void OnMouseExit() { TweenOut(); }
}