using UnityEngine;

public class Events
{
    public class OnPlayerJump : IEvent
    {
        public float jumpImpulse;

        public void Set(params object[] data)
        {
            jumpImpulse = (float)data[0];
        }

        public void Reset()
        {
            jumpImpulse = 0f;
        }
    }

    public class OnPlayerMove : IEvent
    {
        public void Set(params object[] data)
        {

        }

        public void Reset()
        {
            
        }
    }
}
