using UnityEngine;

public class Events
{
    public class OnShoot : IEvent
    {
        private float damage = 0;

        public void Set(params object[] data)
        {
            damage = (float)data[0];
        }

        public void Reset()
        {
            damage = 0;
        }
    }
}
