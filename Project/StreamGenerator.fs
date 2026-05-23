module StreamGenerator

/// <summary>
/// Generere en *n* lang strøm, med 2^l forskellige nøgler.\
/// Bemærk d_i ∈ {-1, 1} for et vilkårligt d_i.
/// </summary>
/// <param name="n">Heltal, antallet af elementer i strømmen.</param>
/// <param name="l">Heltal, beskriver antallet af forskellige nøgler i strømmen.</param>
/// <returns>En *n* lang sekvens af (nøgle, værdi) par.</returns>
let createStream (n: int) (l: int) : seq<uint64 * int> = 
    seq {
        // We generate a random uint64 number.
        let rnd = System.Random()

        let mutable a = 0UL

        let b : byte [] = Array.zeroCreate 8 
        rnd.NextBytes(b)

        for i = 0 to 7 do
            a <- (a <<< 8) + uint64(b.[i])

        // We demand that our random number has 30 zeros on the least
        // significant bits and then a one.
        a <- (a ||| ((1UL <<< 31) - 1UL)) ^^^ ((1UL <<< 30) - 1UL)
        
        let mutable x : uint64 = 0UL
        for i = 1 to (n/3) do
            x <- x + a
            yield (x &&& (((1UL <<< l) - 1UL) <<< 30), 1)

        for i = 1 to ((n + 1)/3) do
            x <- x + a
            yield (x &&& (((1UL <<< l) - 1UL) <<< 30), -1)

        for i = 1 to (n + 2)/3 do
            x <- x + a
            yield (x &&& (((1UL <<< l) - 1UL) <<< 30), 1)
    }
