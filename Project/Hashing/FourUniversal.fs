module FourUniversal

open System.Numerics

/// <summary>
/// Implementering af *4-Universal* hashing.\
/// Løser opgave 4.
/// </summary>
/// <param name="a">Array med præcis 4 uafhængige, uniformt tilfældige heltal tilhørende [p]={0,1,...,p-1}.</param>
/// <param name="x">Nøgle der skal hashes.</param>
/// <returns>Hashværdien af x.</returns>
let hashFourUniversal (a: bigint[]) (x: uint64): bigint =
    // Error handling: a skal have præcis 4 elementer
    if a.Length <> 4 then
        invalidArg "a" "Expected exactly 4 coefficients"

    let p: bigint = (1I <<< 89) - 1I
    let xb = bigint x

    let mutable y = a[3]

    for i = 2 downto 0 do
        y <- y * xb + a[i]
        y <- (y &&& p) + (y >>> 89)
    
    if y >= p then
        y - p
    else
        y 

/// <summary>
/// Implementering af hashfunktioner til *CountSketch*.\
/// Løser opgave 5.
/// </summary>
/// <param name="t">Positivt heltal mindre end 64.</param>
/// <param name="g">Hashfunktion g: U -> [p].</param>
/// <param name="x">Nøgle der skal hashes.</param>
/// <returns>De to hashværdier h(x) og s(x).</returns>
let hashesCountSketch (t: int) (g: uint64 -> bigint) (x: uint64): uint64 * int =
    let gx = g x

    let hx = uint64 (gx &&& ((1I <<< t) - 1I))

    let bit = int ((gx >>> 88) &&& 1I)

    let sx = 1 - 2 * bit

    (hx, sx)
