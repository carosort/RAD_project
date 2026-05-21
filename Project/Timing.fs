module Timing

open System
open System.Diagnostics

// funktion til simpel tidstagning
// Indput:
//          name:   navnet på den funktion, vi tager tid på/navnet på den test vi kører
//          f:      den funktion, vi vil tage tid på 
//
// Funktionen bliver kørt, tiden bliver målt, tiden bliver printet, resultatet af funktionen bliver returneret
//
// Kald den med en lambda funktion, i.e.:
//     measure "name" (fun () -> functionName functionInput)
let measure (name: string) f =

    let sw = Stopwatch.StartNew()

    let result = f()

    sw.Stop()

    printfn "%s took %.3f ms"
        name
        sw.Elapsed.TotalMilliseconds

    result


// funktion til benchmarks 
// (RESULTATER BLIVER SMIDT VÆK, KUN GENNEMSNITSTID BLIVER PRINTET OG RETURNERET!!)
// Indput:
//          runs:   antallet af runs, vi vil køre
//          name:   navnet på den funktion, vi tager tid på/navnet på den test vi kører
//          f:      den funktion, vi vil tage tid på 
//
// Kald den med en lambda funktion, i.e.:
//     benchmark 10 "name" (fun () -> functionName functionInput)
let benchmark (runs: int) (name: string) f =

    // Warmup
    f() |> ignore

    GC.Collect()
    GC.WaitForPendingFinalizers()
    GC.Collect()

    let times =
        [|
            for _ in 1 .. runs do

                let sw = Stopwatch.StartNew()

                f() |> ignore

                sw.Stop()

                yield sw.Elapsed.TotalMilliseconds
        |]

    let avg = Array.average times

    printfn "%s took on average (across %d runs) %.3f ms" name runs avg

    avg

