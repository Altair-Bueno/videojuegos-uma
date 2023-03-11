using UnityEngine;

namespace rvsr.Rabbit.State
{
    public interface IRabbitState
    {
        void Update()
        {
        }

        void Destroy()
        {
        }

        void OnCollision(Collision collision)
        {
        }
    }
}