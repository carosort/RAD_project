module MultiplyModPrime

open System.Numerics

/// <summary>
/// Implementering af *Multiply-Mod-Prime* hashing.\
/// Løser opgave 1.b.
/// </summary>
/// <param name="a">Tilfældigt heltal tilhørende [p]={0,1,...,p-1}.</param>
/// <param name="b">Tilfældigt heltal tilhørende [p]={0,1,...,p-1}.</param>
/// <param name="l">Positivt heltal mindre end 64.</param>
/// <param name="x">Nøgle der skal hashes.</param>
/// <returns>Hashværdien af x.</returns>
let hashModPrime (a:bigint) (b:bigint) (l:int) (x: uint64): uint64 =
    let p: bigint = (1I <<< 89) - 1I

    let z: bigint = a * (bigint x) + b
    let y: bigint = (z &&& p) + bigint (x >>> 89)
    if y >= p then
        uint64 ((y - p) &&& ((1I <<< l) - 1I))
    else
        uint64 (y &&& ((1I <<< l) - 1I))
