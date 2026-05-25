open ExactExperiments
open SketchExperiments
open Csv
open Experiments

[<EntryPoint>]
let main argv =

    printfn "Starting experiments..."

    // GØR TING HER!!!!
    // DVs.:
    // definér funktioner til at køre experimenter i "ExactExperiments.fs"/"SketchExperiments.fs"
    // definér funktioner til at gemme resultater som csv i "Csv.fs"
    // kør de relevante funktioner her

    // PARAMETRE 
    let n: int = 1<<<20 // cirka 1_000_000 ; det er vigtigt at createStream at: 2^l ≤ n
    let l: int = 10 // keep bellow 32, i.e. l<32
    assert (0 <= l && l < (1<<<l) && 1<<<l <= n) // i.e. checks that : 0 ≤ l < 2^l ≤ n

    // OPGAVE 1.c: TEST HASH FUNCTIONS
    //
    // raportér kun køretider (for begge hash funktioner) og (fastholdt) n 
    // dvs. resultatet (sum) er irrelevant

    Opgave1 n l

    // OPGAVE 3: EXACT HASH TABLE EXPERIMENTS
    //
    // raportér køretider (for begge hash funktioner) og l (for fastholdt n)
    // gem køretider og l til en csv, og præsentér i en tabel
    // dvs. resultater (kvadratsum) er irrelevant

    Opgave3 n [|5..15|] 5

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

    // printfn "%A" (test n l)

    printfn "Finished."

    0
