namespace Api.Utilities.Exceptions;

public sealed class PriceAndDistanceWeightZeroException() : InvalidOperationException("PriceWeight and DistanceWeight must not both be zero or null.");