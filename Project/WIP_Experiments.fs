module WIP_Experiments 

open System.Diagnostics
open StreamGenerator
open Timing
open MultiplyShift
open MultiplyModPrime
open HashTable
open CountSketch

// OPGAVE 1.c: TEST HASH FUNCTIONS
//
// raportér kun køretider (for begge hash funktioner) og (fastholdt) n 
// dvs. resultatet (sum) er irrelevant

let Opg1_runtime n l a a' b' = 
    let stream = generateStream n l
    // benchmarking MultiplyShift hashing
    let sw = Stopwatch()
    let h = hashShift a l
    let sum = Array.sumBy (fun (x,_) -> timer sw (fun _ -> h x)) stream
    let time = sw.Elapsed.TotalMilliseconds
    // benchmarking MultiplyModPrime hashing
    let sw' = Stopwatch()
    let h' = hashModPrime a' b' l
    let sum' = Array.sumBy (fun (x,_) -> timer sw' (fun _ -> h' x)) stream
    let time' = sw'.Elapsed.TotalMilliseconds
    (time, sum),(time', sum')

let Opg1_avg_runtime n l array_a array_a' array_b' =
    let times_sums = 
        Array.zip array_a' array_b'
        |> Array.zip array_a
        |> Array.map (fun (a,(a',b')) -> Opg1_runtime n l a a' b') 
    let runtime = Array.averageBy (fun ((t,_),_) -> t) times_sums
    let sum = Array.sumBy (fun ((_,s),_) -> s) times_sums
    let runtime' = Array.averageBy (fun (_,(t',_)) -> t') times_sums
    let sum' = Array.sumBy (fun (_,(_,s')) -> s') times_sums
    runtime, sum, runtime', sum'

let Opgave_1 n l array_a array_a' array_b' =
    printfn "    \"Opgave_1\": {"
    let runtime, sum, runtime', sum' = Opg1_avg_runtime n l array_a array_a' array_b'
    printfn "        \"runtime_ms\": {"
    printfn "            \"mulshift\": %.3f" runtime
    printfn "            \"mulmodprime\": %.3f" runtime'
    printfn "        },"
    printfn "        \"debug_sums\": {"
    printfn "            \"mulshift\": %A" sum
    printfn "            \"mulmodprime\": %A" sum'
    printfn "        }"
    printfn "    },\n"

// OPGAVE 3: EXACT HASH TABLE EXPERIMENTS
//
// raportér køretider (for begge hash funktioner) og l (for fastholdt n)
// gem køretider og l til en csv, og præsentér i en tabel
// dvs. resultater (kvadratsum) er irrelevant

let Opg3_runtime_l n l a a' b' =
    let stream = generateStream n l
    let table = HashTable(hashShift a, l)
    let time, sum = runtime (fun () -> squareSum stream table)
    let table' = HashTable(hashModPrime a' b', l)
    let time', sum' = runtime (fun () -> squareSum stream table')
    (time, sum), (time', sum')

let Opg3_avg_runtime_l n l array_a array_a' array_b' = 
    let parameters = Array.zip array_a' array_b' |> Array.zip array_a
    let times_sums = Array.map (fun (a, (a', b')) -> Opg3_runtime_l n l a a' b') parameters
    let runtime = Array.averageBy (fun ((t,_),_) -> t) times_sums
    let sum = Array.sumBy (fun ((_,s),_) -> s) times_sums
    let runtime' = Array.averageBy (fun (_,(t',_)) -> t') times_sums
    let sum' = Array.sumBy (fun (_,(_,s')) -> s') times_sums
    runtime, sum, runtime', sum'

let Opgave_3 n array_l array_a array_a' array_b' =
    printfn "    \"Opgave_3\": ["
    for l in array_l do
        let runtime, sum, runtime', sum' = Opg3_avg_runtime_l n l array_a array_a' array_b'
        printfn "        {"
        printfn "            \"l\": %A" l
        printfn "            \"runtime_ms\": {"
        printfn "                \"mulshift\": %.3f" runtime
        printfn "                \"mulmodprime\": %.3f" runtime'
        printfn "            }," 
        printfn "            \"debug_sums\": {"
        printfn "                \"mulshift\": %A" sum
        printfn "                \"mulmodprime\": %A" sum'
        printfn "            }"
        printfn "        },"
    printfn "    ],\n"

// OPGAVE 7 & 8: COUNT-SKETCH EXPERIMENTS
// 7:
// brug fastholdt l og n
// returnér RESULTATET (S = kvadratsum) med ExactExperiment
// returnér RESULTATET (X_i = kvadratsumsestimat) af 100 runs af CountSketch
// sørg for at de 100 iterationer af CountSketch bruger nye tilfældige bits
// gem kvadratsumsestimater til en csv og lav diverse plots af X_i og S
// 
// 8: 
// gentag de 100 runs (samme stream, l og n) med 2-3 forskellige værdier af m
// returnér både RESULTATER og KØRETIDER (gennemsnit) for forskellige m
// gem kvadratsumsestimater til en csv og lav diverse plots af X_i og S

let Opg7_8_calc n l array_t array_a0123 =
    let stream = generateStream n l
    let a = 0b1001_1001_0011_0001_0010_0101_1000_1000_0011_1001_0011_1001_0010_0101_0110_0111UL
    let S = squareSum stream (HashTable(hashShift a, l))

    let experiments_t t array_a0123 =
        let sw = Stopwatch()
        let results = Array.map (fun a' -> timer sw (fun () -> CountSketch t a' stream) |> estimateSquareSum) array_a0123
        let avg_time = sw.Elapsed.TotalMilliseconds / float (Array.length array_a0123)
        let medians =
            results
            |> Array.chunkBySize 11
            |> Array.filter (fun chunk -> chunk.Length = 11)
            |> Array.map (fun chunk -> (Array.sort chunk)[6])
            |> Array.sort
            |> Array.zip [|1..((Array.length array_a0123)/11)|]
        let mse = Array.sumBy (fun X -> (X-S)*(X-S)) results / bigint (Array.length array_a0123)
        let estimates = 
            results
            |> Array.sort
            |> Array.zip [|1..(Array.length array_a0123)|] 

        t, avg_time, mse, S, estimates, medians

    Array.map (fun t -> experiments_t t array_a0123) array_t

let Opgave_7_8 n l array_t array_a0123 =
    printfn "    \"Opgave_7_8\": ["
    for t, avg_time, mse, S, estimates, medians in Opg7_8_calc n l array_t array_a0123 do
        printfn "        {"
        printfn "            \"t\": %A" t
        printfn "            \"m\": %A" (uint64 1<<<t)
        printfn "            \"runtime_ms\": %.3f" avg_time
        printfn "            \"square_sum\": %A" S
        printfn "            \"mean_squared_error\": %A" mse
        printfn "            \"estimates\": [" 
        for x,y in estimates do
            printfn "                {\"x\": %A, \"y\": %A}," x y
        printfn "            ]," 
        printfn "            \"median_estimates\": [" 
        for x,y in medians do
            printfn "                {\"x\": %A, \"y\": %A}," x y
        printfn "            ]" 
        printfn "        },"
        printfn "    ]"