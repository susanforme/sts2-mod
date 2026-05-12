#nullable enable

namespace jhin.Powers;

public interface IJhinPowerEventSubscriber
{
    void SubscribeEvents();

    void UnsubscribeEvents();
}

public static class JhinPowerEventSubscription
{
    public static void SubscribeIfSupported(object? power)
    {
        if (power is IJhinPowerEventSubscriber subscriber)
        {
            subscriber.SubscribeEvents();
        }
    }

    public static void UnsubscribeIfSupported(object? power)
    {
        if (power is IJhinPowerEventSubscriber subscriber)
        {
            subscriber.UnsubscribeEvents();
        }
    }
}
