module HashTable

// Hash table with chaining, i.e. One-Level hashing
type HashTable (h : int -> uint64 -> uint64, l : int) =
    // init the hash function h' such that it maps correctly to [2^l]
    let h' = fun x -> h l x |> int

    // The hashtable : a fixed length array (length = 2^l), where each entry is a dynamic length array (chain), where each entry is a tuple of (key, value)
    let table : ResizeArray<(uint64 * int)>[] = Array.init (1 <<< l) (fun _ -> ResizeArray())

    // a) : get(x): Skal returnere den værdi, der tilhører nøglen x. Hvis x ikke er i tabellen skal der returneres 0.
    member _.get(x : uint64) : int =
        // need to use match because Seq.tryFind returns an option<uint64 * uint64>, and likewise bellow
        match Seq.tryFind (fun (k, _) -> k = x) table.[h' x] with 
        | Some (_, v) -> v
        | None -> 0

    // b) : set(x,v): Skal sætte nøglen x til at have værdien v. Hvis x ikke allerede er i tabellen så tilføjes den til tabellen med værdien v.
    member _.set(x : uint64, v : int) : unit =
        let bucket = table.[h' x]
        match Seq.tryFindIndex (fun (k, _) -> k = x) bucket with 
        | Some i -> (bucket.[i] <- (x,v))
        | None -> bucket.Add (x,v)

    // c) : increment(x,d): Skal lægge d til værdien tilhørende x. Hvis x ikke er i tabellen, skal x tilføjes til tabellen med værdien d.
    member _.increment(x : uint64, d : int) : unit =
        // this implementation is significantly faster than using the above get and set methods, as we remove their overhead and only seach once
        let bucket = table.[h' x]
        match Seq.tryFindIndex (fun (k,_) -> k = x) bucket with
        | Some i ->
            let _,v = bucket.[i]
            bucket.[i] <- (x, v + d)
        | None ->
            bucket.Add (x,d)

    // helper for opgave 3
    member _.getTable () = table



let squareSum (stream : (uint64 * int)[]) (table : HashTable) : bigint =
    for (x,d) in stream do
        table.increment(x,d)

    let mutable sum = 0I
    for bucket in table.getTable() do
        for (_,v) in bucket do
            let s = bigint v
            sum <- sum + s*s
    sum
    