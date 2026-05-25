module CountSketch

open FourUniversal

let CountSketch (t: int) (a: bigint[]) (stream : seq<uint64 * int>) : int[] = 

    let C = Array.init (1 <<< t) (fun _ -> 0) // init sketch array

    let hx_sx = hashesCountSketch t (hashFourUniversal a) // "picks" hash functions h and s

    for (x,d) in stream do
        let hx, sx = hx_sx x
        C.[int hx] <- C.[int hx] + sx*d
    C