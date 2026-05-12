#nullable enable

using BaseLib.Abstracts;

namespace jhin.Powers;

public abstract class AbstractJhinPower : CustomPowerModel, IJhinPowerEventSubscriber
{
    private bool _eventsSubscribed;

    public void SubscribeEvents()
    {
        if (_eventsSubscribed)
        {
            return;
        }

        SubscribeEventHandlers();
        _eventsSubscribed = true;
    }

    public void UnsubscribeEvents()
    {
        if (!_eventsSubscribed)
        {
            return;
        }

        UnsubscribeEventHandlers();
        _eventsSubscribed = false;
    }

    protected virtual void SubscribeEventHandlers()
    {
    }

    protected virtual void UnsubscribeEventHandlers()
    {
    }
}
