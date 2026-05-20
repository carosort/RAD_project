module MultiplyShift

// a:   ulige 64-bit tal
// l:   positivt heltal mindre end 64
let hashShift (a:uint64) (l:int) (x:uint64): uint64=
    uint64 ((a * x) >>> (64 - l))
