using System.Numerics;

public static class DiffieHellman
{
    private static Random random = new Random();
    public static BigInteger PrivateKey(BigInteger primeP)
    {
        return new BigInteger(random.NextDouble() * (double)primeP);
    }

    public static BigInteger PublicKey(BigInteger primeP, BigInteger primeG, BigInteger privateKey)
    {
        return BigInteger.ModPow(primeG, privateKey, primeP);
    }

    public static BigInteger Secret(BigInteger primeP, BigInteger publicKey, BigInteger privateKey)
    {
        return BigInteger.ModPow(publicKey, privateKey, primeP);
    }
}