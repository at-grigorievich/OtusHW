using System;

namespace DefaultNamespace.Conveyor
{
    public interface ILoadedZoneChecker
    {
        bool IsLoadedZoneAvailable { get; }
        event Action OnLoadedZoneAmountChanged;
    }
}