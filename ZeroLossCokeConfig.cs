// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace ZeroLossCoke;

/// <summary>
/// Represents the configuration settings for the ZeroLossCoke mod.
/// This configuration controls various parameters related to the coke yield system.
/// </summary>
public class ZeroLossCokeConfig
{
    /// <summary>
    /// The multiplier applied to the base coke yield during production.
    /// Determines the factor by which the output yield is adjusted, enabling scaling
    /// of production quantities. A value of 1.0 retains the base yield, while higher
    /// or lower values increase or decrease the yield respectively.
    /// </summary>
    public float YieldMultiplier { get; set; } = 2.0f; // Standard: Lossless (double)

    /// <summary>
    /// Minimum allowable yield for coke production. Ensures that the yield does not drop below this value,
    /// even after applying the configured multipliers or other adjustments.
    /// </summary>
    public int MinYield { get; set; } = 0;

    /// <summary>
    /// Defines the maximum allowable yield for coke production.
    /// Acts as an upper limit to prevent the output quantity from exceeding
    /// a specified threshold, ensuring balanced and controlled production output.
    /// A value of 0 disables the maximum yield restriction.
    /// </summary>
    public int MaxYield { get; set; } = 0;

    /// <summary>
    /// Enables or disables debug logging for the ZeroLossCoke mod.
    /// When enabled, detailed debug messages are written to the server log,
    /// providing insights into internal operations such as yield adjustments
    /// during coke production. This can be helpful for troubleshooting or
    /// verifying mod behavior. Disable this setting in production to reduce
    /// log verbosity.
    /// </summary>
    public bool DebugLogging { get; set; } = false;
}