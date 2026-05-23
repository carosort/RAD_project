module ExactExperiments 

open StreamGenerator
open Timing
open Csv
open MultiplyShift
open MultiplyModPrime
open HashTable


let a = 0b1001_1001_0011_0001_0010_0101_1000_1000_0011_1001_0011_1001_0010_0101_0110_0111UL
let a': bigint = 47529364501441017828375504I
let b': bigint = 538434095195490410313078429I 


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
    printfn "MultiplyShift sum = %A" sum1

    let sum2 = measure "MultiplyModPrime" (fun () -> testMultiplyModPrime stream l)
    printfn "ModPrime sum = %A" sum2
