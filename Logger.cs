using Vintagestory.API.Common;

// ReSharper disable UnusedMember.Global

namespace ZeroLossCoke;

// ReSharper disable once UnusedType.Global
public class Logger(ICoreAPI api, string modId)
{
    public void Event(string message) => api.Logger.Event($"[{modId}] {message}");
    public void Debug(string message) => api.Logger.Debug($"{modId}: {message}");
    public void Warn(string message) => api.Logger.Warning($"{modId}: {message}");
    public void Error(string message) => api.Logger.Error($"{modId}: {message}");
}