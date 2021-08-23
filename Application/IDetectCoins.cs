using Domain;

namespace Application
{
    public interface IDetectCoins
    {
        Coin Detect(string pieceOfMetal);
    }
}