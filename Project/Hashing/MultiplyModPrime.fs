module MultiplyModPrime

open System.Numerics

// a:   heltal skarpt mindre end p
// b:   heltal skarpt mindre end p
// l:   positivt heltal mindre end 64
let hashModPrime (a:bigint) (b:bigint) (l:int) (x: uint64): uint64 =
    let p: bigint = (1I <<< 89) - 1I

    let z: bigint = a * (bigint x) + b
    let y: bigint = (z &&& p) + bigint (x >>> 89)
    if y >= p then
        uint64 ((y - p) &&& ((1I <<< l) - 1I))
    else
        uint64 (y &&& ((1I <<< l) - 1I))
