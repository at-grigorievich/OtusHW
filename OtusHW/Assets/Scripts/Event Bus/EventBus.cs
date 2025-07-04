using System;
using System.Collections.Generic;
using System.Linq;

public class EventBus
{
    private readonly Dictionary<Type, List<WeakReference<IBaseEventReceiver>>> _receivers = new();
    private readonly Dictionary<string, WeakReference<IBaseEventReceiver>> _receiversByHash = new();

    public void Register<T>(EventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);

        if (_receivers.ContainsKey(eventType) == false)
        {
            _receivers[eventType] = new List<WeakReference<IBaseEventReceiver>>();
        }

        if (_receiversByHash.TryGetValue(receiver.Id, out WeakReference<IBaseEventReceiver> weakReceiver) == false)
        {
            weakReceiver = new WeakReference<IBaseEventReceiver>(receiver);
            _receiversByHash[receiver.Id] = weakReceiver;
        }
        
        _receivers[eventType].Add(weakReceiver);
    }

    public void Unregister<T>(EventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        if (_receivers.ContainsKey(eventType) == false || _receiversByHash.ContainsKey(receiver.Id) == false)
            return;

        WeakReference<IBaseEventReceiver> reference = _receiversByHash[receiver.Id];

        _receivers[eventType].Remove(reference);
        
        int weakRefCount = _receivers.SelectMany(x => x.Value).Count(x => x == reference);
        if (weakRefCount == 0)
            _receiversByHash.Remove(receiver.Id);
    }
    
    public void Raise<T>(T @event) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        
        if (_receivers.ContainsKey(eventType) == false)
            return;

        List<WeakReference<IBaseEventReceiver>> references = _receivers[eventType];
        
        for (int i = references.Count - 1; i >= 0; i--)
        {
            if (references[i].TryGetTarget(out IBaseEventReceiver receiver))
                ((EventReceiver<T>)receiver).OnEvent(@event);
        }
    }
}