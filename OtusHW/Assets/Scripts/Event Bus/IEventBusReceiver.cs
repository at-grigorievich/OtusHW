using System;
using VContainer.Unity;

public interface IEvent {}

public interface IBaseEventReceiver
{
    public UniqueId Id { get; }
}

public abstract class EventReceiver<T>: IBaseEventReceiver where T: struct, IEvent
{
    protected readonly EventBus _eventBus;

    public UniqueId Id { get; } = new();
    
    public EventReceiver(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public abstract void OnEvent(T evt);

    protected virtual void Enter() => _eventBus.Register(this);
    protected virtual void Exit() => _eventBus.Unregister(this);
}

public abstract class EventLogicReceiver<T> : EventReceiver<T>, IPostInitializable, IDisposable where T: struct, IEvent
{
    protected EventLogicReceiver(EventBus eventBus) : base(eventBus) { }

    public void PostInitialize() => Enter();

    public void Dispose() => Exit();
}

public abstract class EventViewReceiver<T> : EventReceiver<T>, IInitializable, IDisposable where T : struct, IEvent
{
    protected EventViewReceiver(EventBus eventBus) : base(eventBus) { }

    public void Initialize() => Enter();

    public void Dispose() => Exit();
}
