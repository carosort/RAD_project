module MultiplyShift

/// <summary>
/// Implementering af *Multiply-Shift* hashing.\
/// Løser opgave 1.a.
/// </summary>
/// <param name="a">Tilfældigt 64-bit ulige heltal.</param>
/// <param name="l">Positivt heltal mindre end 64.</param>
/// <param name="x">Nøgle der skal hashes.</param>
/// <returns>Hashværdien af x.</returns>
let hashShift (a:uint64) (l:int) (x:uint64): uint64=
    (a * x) >>> (64 - l)
