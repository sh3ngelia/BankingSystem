namespace Investment.Domain.Exceptions;

public class AssetNotFoundException : Exception
{
    public AssetNotFoundException(Guid assetId)
        : base($"Asset with id '{assetId}' was not found.") { }

    public AssetNotFoundException(string symbol)
        : base($"Asset with symbol '{symbol}' was not found.") { }
}