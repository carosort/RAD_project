module ExactExperiments 

open StreamGenerator
open Timing
open Csv
open MultiplyShift
open MultiplyModPrime
open HashTable


let a = 0b1001_1001_0011_0001_0010_0101_1000_1000_0011_1001_0011_1001_0010_0101_0110_0111UL // has to be odd
let a': bigint = 47529364501441017828375504I // cannot use suffix I after 0b notation, hence decimal
let b': bigint = 538434095195490410313078429I


// OPGAVE 1.c :
let testMultiplyShift (stream : (uint64 * int)[]) l = 
    let mutable sum = 0I

    for (x, _) in stream do
        let hx = hashShift a l x
        sum <- sum + bigint hx

    sum

let testMultiplyModPrime (stream : (uint64 * int)[]) l =
    let mutable sum = 0I

    for (x, _) in stream do
        let hx = hashModPrime a' b' l x
        sum <- sum + bigint hx

    sum

let runHashBenchmarks n l =
    let stream = createStream n l |> Seq.toArray

    let sum1 = measure "MultiplyShift" (fun () -> testMultiplyShift stream l)
    printfn "    (sum=%A)" sum1

    let sum2 = measure "MultiplyModPrime" (fun () -> testMultiplyModPrime stream l)
    printfn "    (sum=%A)" sum2



// OPGAVE 3

// we return uint64 to prevent overflow and because all squares are positiv
let squareSum (stream : seq<uint64 * int>) (table : HashTable) : uint64 =
    for (x,d) in stream do
        table.increment(x,d)

    let mutable square_sum = 0UL
    for bucket in table.getTable() do
        for (_,v) in bucket do
            square_sum <- square_sum + uint64 (v*v)

    square_sum

let runSquareSumBenchmark n ls =
    for l' in ls do
        let stream = createStream n l'
        let table = HashTable(hashShift a, l')
        let sum = measure "SquareSum-MulShift" (fun () -> squareSum stream table)
        printfn " with 2^%A different keys    (sum=%A)" l' sum

    for l' in ls do
        let stream = createStream n l'
        let table = HashTable(hashModPrime a' b', l')
        let sum = measure "SquareSum-MulModPrime" (fun () -> squareSum stream table)
        printfn " with 2^%A different keys    (sum=%A)" l' sum
