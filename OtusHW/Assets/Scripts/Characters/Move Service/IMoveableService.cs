using UnityEngine;

namespace ATG.Move
{
    public interface IMoveableService
    {
        float Speed { get; }
        Vector3 CurrentPosition { get; }
        
        void MoveTo(Vector3 point);
        void Stop();
    }
}