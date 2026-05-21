module FourUniversal

open System.Numerics

// a:       array af heltal skarpt mindre end p
let hashFourUniversal (a: bigint[]) (x: uint64): uint64 =
    let p: bigint = (1I <<< 89) - 1I

    let mutable y = a[0]

    for i = 1 to 3 do
        y <- y * (bigint x) + a[i]
        y <- (y &&& p) + (y >>> 89)
    
    if y >= p then
        uint64 (y - p)
    else
        uint64 y 
