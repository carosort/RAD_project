module Csv

open System.IO

// simpel CSV writer
// omdanner en sequence af resultater til en csv fil
// som vi så kan åbne i Python og lave plots/tabeller med
// 
// Kaldes som følger:
// writeCsv "results.csv" [ "l"; "time_ms" ] results
let writeCsv (path: string) (headers: seq<string>) (rows: seq<'a>) =

    let lines =
        seq {

            yield String.concat "," headers

            for row in rows do
                yield String.concat "," row
        }

    File.WriteAllLines(path, lines)


// Mere kompliceret CSV writer ;)))

// Type til benchmark-resultater
// (lille datastruktur der beskriver ét benchmark)
type BenchmarkResult =
    {
        L : int
        TimeMs : float
    }

// Hjælpefunktion: 
// konverterer ét resultat til strings (klar til CSV-række)
let resultToRow (r: BenchmarkResult) =
    [
        string r.L
        string r.TimeMs
    ]

// Gem en sequence af benchmark-resultater som CSV
let writeResults (path: string) (results: seq<BenchmarkResult>) =

    // Lav header
    let header =
        "l,time_ms"

    // Gør resultater klar til at blive til CSV-rækker
    let rows =
        results
        |> Seq.map resultToRow
        |> Seq.map (String.concat ",")

    // Kombinér header + rows
    let lines =
        seq {
            yield header
            yield! rows
        }

    // Opret og skriv til fil
    File.WriteAllLines(path, lines)

    printfn "Saved CSV to %s" path
